using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Steamworks;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public int Gamecount;
        public string Raw;
        public string Processed1;
        public string KEY = "A2CEC00C2471A0F4E3796F3C42BC0396"; //This is your private STEAMAPI KEY only compile with it, Dont give it away https://steamcommunity.com/dev/apikey 

        public MainForm()
        {
            InitializeComponent();
        }
        static void ExitApp()
        {
            Application.Exit();
        }

        public void GetOwnedSteamGames(string SteamID, ListBox Name)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    Raw = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key="+ KEY +"&steamid=" + SteamID + "&format=json");
                }
                catch (Exception)
                {
                    DialogResult Ok = MessageBox.Show("You have typed an incorrect SteamID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto EndofGetOwnedSteamGames;
                }
                try
                {
                    Gamecount = Regex.Matches(Regex.Escape(Raw), "appid").Count;
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not process SteamID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto EndofGetOwnedSteamGames;
                    throw;
                }
                List<int> gameid = new List<int>();
                for (int i = 0; i < Gamecount; i++)
                {
                    Regex regex = new Regex("d\": (.*?),");
                    var v = regex.Match(Raw);
                    string s = v.Groups[1].ToString();
                    Raw = Regex.Replace(Raw, "d\": " + s + ",", "");
                    //s.Substring(4, s.Length - 4);
                    if (s != "")
                    {
                        gameid.Add(int.Parse(s));
                    }
                }
                gameid.Sort();
                AddtoLBoxOG(gameid, Name);
            EndofGetOwnedSteamGames:;
            }
        }              //Getting Steam Owned Games
        public int CountOfOwnedGames(string SteamID)
        {
            using (WebClient client = new WebClient())
            {

                Raw = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + KEY + "&steamid=" + SteamID + "&format=json");
            }
            int Count = Regex.Matches(Regex.Escape(Raw), "appid").Count;

            return Count;
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int parsed;
            int TextboxContents;
            string Savedappid = System.IO.File.ReadAllText("steam_appid.txt"); //save steam_appid.txt contents
            Int32.TryParse(textBox1.Text, out TextboxContents);
            string lines = TextboxContents.ToString();
            System.IO.StreamWriter file = new System.IO.StreamWriter("steam_appid.txt"); //Write the contents of the Textbox to steam_appid.txt NOTE: If textbox == empty, 0 will be written to the txt file.
            file.WriteLine(lines);
            file.Close();
            string Checktxtdoc = System.IO.File.ReadAllText("steam_appid.txt"); 
            int.TryParse(Checktxtdoc,out parsed);
            if (parsed == 0)
            {
                MessageBox.Show("You need to have an AppID typed in, in order to change the current AppID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.IO.StreamWriter SavedAppID = new System.IO.StreamWriter("steam_appid.txt");
                SavedAppID.WriteLine(Savedappid);
                SavedAppID.Close();
            }
            else
            {
                DialogResult YesNoExit = MessageBox.Show("You need to restart the application in order to be playing a different game in Steam. Restart now?" + "\n" + "Note: You have to start the application manually.", "Restart prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (YesNoExit == DialogResult.Yes)
                {
                    file.Close();
                    ExitApp();
                }
            }
        }                    

        private void Form1_Load(object sender, EventArgs e)
        {
            try 
            {
                //Start of Initialization shit
                SteamAPI.Init();  //Must initialise SteamApi before using steamwork functions
                string name = SteamFriends.GetPersonaName();
                string steamlevel = SteamUser.GetPlayerSteamLevel().ToString();
                int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
                SteamFriendCountBox.Text = friendCount.ToString();
                SteamNameBox.Text = name;
                SteamLevelBox.Text = steamlevel;
                string steamid = SteamUser.GetSteamID().ToString();
                SteamIDBox.Text = steamid;
                GetOwnedSteamGames(steamid, OwnedGamesLBox);
                OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString();
                //End of initialization shit
            }
            catch (Exception) //look for an error
            {
                MessageBox.Show("Steam not running!" + "\n" + "Terminating program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);           //Error messagebox
                ExitApp();                                                                                                                           //Used to Exit program
            }
        }                      //Initializing everything


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)           //Make shure that textbox1 has only numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        } //Make shure that only numbers go inside APPID Textbox

        private void button2_Click(object sender, EventArgs e)               //Get owned games custom id 
        {
            if (OwnedGamesCustomSteamIDLB.Items.Count != 0)
            {
                OwnedGamesCustomSteamIDLB.Items.Clear();
            }
            else
            {

            }
            if (CustomSteamIDBox.Text == "")
            {
                DialogResult Ok = MessageBox.Show("You need to type in a SteamID in the box before trying to fetch owned steam games list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GetOwnedSteamGames(CustomSteamIDBox.Text, OwnedGamesCustomSteamIDLB);
                CountOwnedGamesCID.Text = CountOfOwnedGames(CustomSteamIDBox.Text).ToString();
            }
            
        }

        

        public void AddtoLBoxOG(IEnumerable items, ListBox ListOwnedGamesName)
        {
            foreach (object o in items)
            {
                ListOwnedGamesName.Items.Add(o);
            }
        }      //Adding items to listbox
        private void CustomSteamIDBox_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }  //making shure that CustomSteamIDBox has only numbers

        private void DifferentSteamIDRB_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = true;
            CustomSteamIDBox.Visible = true;
            CalculateCustomSteamIDButton.Visible = true;
            label4.Visible = true;
            CountOwnedGamesCID.Visible = true;
            OwnedGamesCustomSteamIDLB.Visible = true;
            this.Size = new Size(517, 250);
        }     //RB For Different SteamID Check

        private void OwnSteamIDRB_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
            CustomSteamIDBox.Visible = false;
            CalculateCustomSteamIDButton.Visible = false;
            label4.Visible = false;
            CountOwnedGamesCID.Visible = false;
            OwnedGamesCustomSteamIDLB.Visible = false;
            this.Size = new Size(370, 250);
        }           //RB For Own SteamID Check
    }
}