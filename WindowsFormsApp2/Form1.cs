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
using System.IO;
using System.Text;
using System.Data;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public string steamid = "";
        public int Gamecount;
        public string Raw;
        public string Processed1;
        public string KEY = ""; //This is your private STEAMAPI KEY only compile with it, Dont give it away https://steamcommunity.com/dev/apikey 

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
                AddtoLBox(gameid, Name);
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
                steamid = SteamUser.GetSteamID().ToString();
                string name = SteamFriends.GetPersonaName();
                string steamlevel = SteamUser.GetPlayerSteamLevel().ToString();
                int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
                SteamFriendCountBox.Text = friendCount.ToString();
                SteamNameBox.Text = name;
                SteamLevelBox.Text = steamlevel;
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
            if (CustomSteamIDBox.Text == "")
            {
                DialogResult Ok = MessageBox.Show("You need to type in a SteamID in the box before trying to fetch owned steam games list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (FilterFreeGamesCHB.Checked)
                {
                    OwnedSteamGamesFilteredNoFreeGames(OwnedGamesCustomSteamIDLB, CustomSteamIDBox.Text);
                    CountOwnedGamesCID.Text = OwnedGamesCustomSteamIDLB.Items.Count.ToString();
                    
                }
                else
                {
                    GetOwnedSteamGames(CustomSteamIDBox.Text, OwnedGamesCustomSteamIDLB);
                    CountOwnedGamesCID.Text = CountOfOwnedGames(CustomSteamIDBox.Text).ToString();
                }
            }
            
        }

        

        public void AddtoLBox(IEnumerable items, ListBox ListOwnedGamesName)
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

        public static List<int> GetOwnedSteamGames(string KEY, string SteamID)
        {
            
            string Raw;
            int Gamecount;
            List<int> gameid = new List<int>();
            using (WebClient client = new WebClient())
            {
                
                try
                {
                    Raw = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + KEY + "&steamid=" + SteamID + "&format=json");
                    Gamecount = Regex.Matches(Regex.Escape(Raw), "appid").Count;
                    for (int i = 0; i < Gamecount; i++)
                    {
                        Regex regex = new Regex("d\": (.*?),");
                        var v = regex.Match(Raw);
                        string s = v.Groups[1].ToString();
                        Raw = Regex.Replace(Raw, "d\": " + s + ",", "");

                        if (s != "")
                        {
                            gameid.Add(int.Parse(s));
                        }
                    }
                    gameid.Sort();
                }
                catch (Exception)
                {
                    DialogResult Ok = MessageBox.Show("An Error Occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return gameid;
        }
        public void OwnedSteamGamesFilteredNoFreeGames(ListBox name, string STEAMID)
        {
            string RawSteamdbinfodatahtmlcode;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://steamdb.info/tags/?tagid=113");
            request.UserAgent = "wowoww";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                name.Items.Clear();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                RawSteamdbinfodatahtmlcode = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                int count = Regex.Matches(Regex.Escape(RawSteamdbinfodatahtmlcode), "/app/").Count;

                //string path = "RawSteamdbInfoDataHtmlCode.html";
                //TextWriter tw = new StreamWriter(path, true);                 FOR DEBUG PURPOSES
                //tw.WriteLine(RawSteamdbinfodatahtmlcode);

                List<int> gameid = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    Regex regex = new Regex("data-appid=\"(.*?)\">");
                    var v = regex.Match(RawSteamdbinfodatahtmlcode);
                    string s = v.Groups[1].ToString();
                    RawSteamdbinfodatahtmlcode = Regex.Replace(RawSteamdbinfodatahtmlcode, "data-appid=\"" + s + "\">", "");
                    if (s != "")
                    {
                        gameid.Add(int.Parse(s));
                    }
                }
                gameid.Sort();
                List<int> CopyOwnedSGames = new List<int>(GetOwnedSteamGames(KEY, STEAMID));
                for (int i = 0; i < gameid.Count; i++)
                {
                    for (int g = 0; g < CopyOwnedSGames.Count; g++)
                    {
                        if (CopyOwnedSGames[g] == gameid[i])
                        {
                            CopyOwnedSGames[g] = 0;
                        }
                    }
                }
                for (int i = 0; i < CopyOwnedSGames.Count; i++)
                {
                    if (CopyOwnedSGames[i] != 0)
                    {
                        name.Items.Add(CopyOwnedSGames[i]);
                    }
                }
            }
        }
        private void FilterFreeGamesCHB_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFreeGamesCHB.Checked)
            {
                if (OwnedGamesCustomSteamIDLB.Items.Count != 0)
                {
                    OwnedSteamGamesFilteredNoFreeGames(OwnedGamesCustomSteamIDLB, CustomSteamIDBox.Text);
                    CountOwnedGamesCID.Text = OwnedGamesCustomSteamIDLB.Items.Count.ToString();
                }
                FilterFreeGamesCHB.Enabled = false;
                OwnedSteamGamesFilteredNoFreeGames(OwnedGamesLBox, steamid);
                OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString(); // For updating the GameCount in owned games
                FilterFreeGamesCHB.Enabled = true;
            }
            else
            {
                if (OwnedGamesCustomSteamIDLB.Items.Count != 0)
                {
                    OwnedGamesCustomSteamIDLB.Items.Clear();
                    GetOwnedSteamGames(CustomSteamIDBox.Text, OwnedGamesCustomSteamIDLB);
                    CountOwnedGamesCID.Text = OwnedGamesCustomSteamIDLB.Items.Count.ToString();
                }
                FilterFreeGamesCHB.Enabled = false;
                OwnedGamesLBox.Items.Clear();
                GetOwnedSteamGames(steamid, OwnedGamesLBox);
                OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString(); // For updating the GameCount in owned games
                FilterFreeGamesCHB.Enabled = true;
            }
        }

        private void OwnedGamesCustomSteamIDLB_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(OwnedGamesCustomSteamIDLB.SelectedItem.ToString());
            MessageBox.Show("Copied " + OwnedGamesCustomSteamIDLB.SelectedItem.ToString() + " to clipboard!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void OwnedGamesLBox_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(OwnedGamesLBox.SelectedItem.ToString());
            MessageBox.Show("Copied " + OwnedGamesLBox.SelectedItem.ToString() + " to clipboard!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
