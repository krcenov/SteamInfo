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
using System.Web;
using System.Text;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        bool firststart = false;
        List<string> FreeGamesList = new List<string>();
        public string steamid = "";
        public int Gamecount;
        public string Raw;
        public string Processed1;
        public string KEY = ""; //This is your private STEAMAPI KEY only compile with it, Dont give it away https://steamcommunity.com/dev/apikey 

        public MainForm()
        {
            InitializeComponent();
        }

        public void GetOwnedSteamGames(string SteamID, ListBox Name)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    Raw = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + KEY + "&steamid=" + SteamID + "&format=json");
                }
                catch (Exception)
                {
                    DialogResult Ok = MessageBox.Show("You have typed an incorrect SteamID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto EndofGetOwnedSteamGames;
                }
                try
                {
                    Gamecount = Int32.Parse(Regex.Match(Raw, "game_count\":(.+?),").Groups[1].Value);
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
                    Regex regex = new Regex("d\":(.*?),");
                    var v = regex.Match(Raw);
                    string s = v.Groups[1].ToString();
                    Raw = Regex.Replace(Raw, "d\":" + s + ",", "");
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
        }                  //Getting Steam Owned Games

        public int CountOfOwnedGames(string SteamID)
        {
            using (WebClient client = new WebClient())
            {

                Raw = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + KEY + "&steamid=" + SteamID + "&format=json");
            }
            int Count = Regex.Matches(Regex.Escape(Raw), "appid").Count;

            return Count;

        }                                  //Counting Owned Games

        private void button1_Click(object sender, EventArgs e)
        {
            int parsed;
            int TextboxContents;
            string Savedappid = System.IO.File.ReadAllText("steam_appid.txt"); //save steam_appid.txt contents
            Int32.TryParse(textBox1.Text, out TextboxContents);
            string lines = TextboxContents.ToString();
            System.IO.StreamWriter file = new System.IO.StreamWriter("steam_appid.txt"); //Write the contents of the Textbox to steam_appid.txtF
            file.WriteLine(lines);
            file.Close();
            string Checktxtdoc = System.IO.File.ReadAllText("steam_appid.txt");
            int.TryParse(Checktxtdoc, out parsed);
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
                    Application.Exit();
                }
            }
        }                  

        public void FreeGamesListFnc()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://steamspy.com/genre/Free+to+Play");
            request.UserAgent = "wowoww";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
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

                string RawSteamdbinfodatahtmlcode = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                int count = Regex.Matches(Regex.Escape(RawSteamdbinfodatahtmlcode), "href=/app/").Count;
                List<int> gameid = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    Regex regex = new Regex("href=/app/(.*?)><img src=");
                    var v = regex.Match(RawSteamdbinfodatahtmlcode);
                    string s = v.Groups[1].ToString();
                    RawSteamdbinfodatahtmlcode = Regex.Replace(RawSteamdbinfodatahtmlcode, "href=/app/" + s + "><img src=", "");
                    if (s != "")
                    {
                        gameid.Add(int.Parse(s));
                    }
                }
                gameid.Sort();
                for (int i = 0; i < gameid.Count; i++)
                {
                    FreeGamesList.Add(gameid[i].ToString());
                }
            }
        }                                               //Gathering Free Games list


        
        public void Steamnamesuptodatecheck()
        {
            int AllGamesCount;
            using (WebClient client = new WebClient())
            {
                string Steamnamesfilename = "Steamnames.txt";
                client.Encoding = Encoding.UTF8;
                try
                {
                    Raw = client.DownloadString("http://api.steampowered.com/ISteamApps/GetAppList/v2");
                }
                catch
                {
                    MessageBox.Show("Could not connect to update server!" + "\n" + "Can not check for updates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);           //No connection to steam web api error
                }
                AllGamesCount = Regex.Matches(Raw, "\"appid\":").Count;
                int lineCount = File.ReadLines(Steamnamesfilename).Count();
                if (lineCount/2 == AllGamesCount)
                {

                }
                else
                {
                    if (firststart = true)
                    {
                        string FilteredRAW = "";
                        FilteredRAW = Raw;
                        FilteredRAW = FilteredRAW.Replace("{\"applist\":{\"apps\":[", "");
                        FilteredRAW = FilteredRAW.Replace("{\"appid\":", "\n");
                        FilteredRAW = FilteredRAW.Replace(",\"name\":\"", "\n");
                        FilteredRAW = FilteredRAW.Replace("\"},", "");
                        if (File.Exists(Directory.GetCurrentDirectory() + "/" + Steamnamesfilename) == true)
                        {
                            File.Delete(Steamnamesfilename);
                            File.Create(Steamnamesfilename).Close();
                        }
                        else
                        {
                            File.Create(Steamnamesfilename).Close();
                        }
                        File.AppendAllText(Directory.GetCurrentDirectory() + "/" + Steamnamesfilename, FilteredRAW);    //Pushing all the text to a txt file
                        var lines = File.ReadAllLines(Steamnamesfilename);                                              //Reading all the lines of the text file
                        File.WriteAllLines(Steamnamesfilename, lines.Skip(1).ToArray());                                //Writing the same lines without the first one
                    }
                    else
                    {
                        var Answer = MessageBox.Show("You need to update!" + "\n" + "Update now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (Answer == DialogResult.Yes)
                        {

                            string FilteredRAW = "";
                            FilteredRAW = Raw;
                            FilteredRAW = FilteredRAW.Replace("{\"applist\":{\"apps\":[", "");
                            FilteredRAW = FilteredRAW.Replace("{\"appid\":", "\n");
                            FilteredRAW = FilteredRAW.Replace(",\"name\":\"", "\n");
                            FilteredRAW = FilteredRAW.Replace("\"},", "");
                            if (File.Exists(Directory.GetCurrentDirectory() + "/" + Steamnamesfilename) == true)
                            {
                                File.Delete(Steamnamesfilename);
                                File.Create(Steamnamesfilename).Close();
                            }
                            else
                            {
                                File.Create(Steamnamesfilename).Close();
                            }
                            File.AppendAllText(Directory.GetCurrentDirectory() + "/" + Steamnamesfilename, FilteredRAW);    //Pushing all the text to a txt file
                            var lines = File.ReadAllLines(Steamnamesfilename);                                              //Reading all the lines of the text file
                            File.WriteAllLines(Steamnamesfilename, lines.Skip(1).ToArray());                                //Writing the same lines without the first one
                        }
                    }
                }
            }
        }                                        //Checking the last time that the games list with names has been updated and pushing an update if needed
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //Start of Initialization shit
                if (!File.Exists(Directory.GetCurrentDirectory() + "/Steamnames.txt"))
                {

                    File.Create(Directory.GetCurrentDirectory() + "/Steamnames.txt").Close();

                }
                if (!File.Exists(Directory.GetCurrentDirectory() + "/steam_appid.txt"))
                {

                    File.Create(Directory.GetCurrentDirectory() + "/steam_appid.txt").Close();
                    File.AppendAllText(Directory.GetCurrentDirectory() + "/" + "/steam_appid.txt", "480");

                }
                SteamAPI.Init();   //Must initialise SteamApi before using steamwork functions
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            FreeGamesListFnc();
            Steamnamesuptodatecheck();
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
            catch (Exception) 
            {
                MessageBox.Show("Failed to initialize!" + "\n" + "Terminating program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);           //Error messagebox
                Application.Exit();                                                                                                                  //Used to Exit program
            }
        }                          //Initializing everything

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)              //Make shure that textbox1 has only numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        } //Make shure that only numbers go inside APPID Textbox

        private void button2_Click(object sender, EventArgs e)                          //Get owned games custom id 
        {
            if (OwnedGamesCustomSteamIDLB.Items.Count != 0)
            {
                OwnedGamesCustomSteamIDLB.Items.Clear();
            }
            if (CustomSteamIDBox.Text == "")
            {
                MessageBox.Show("You need to type in a SteamID in the box before trying to fetch owned steam games list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (FilterFreeGamesCHB.Checked)
                {
                    OwnedSteamGamesFilteredNoFreeGames(OwnedGamesCustomSteamIDLB, CustomSteamIDBox.Text);
                    OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString();
                    CountOwnedGamesCID.Text = OwnedGamesCustomSteamIDLB.Items.Count.ToString();

                }
                else
                {
                    GetOwnedSteamGames(CustomSteamIDBox.Text, OwnedGamesCustomSteamIDLB);
                    OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString();
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
        }         //Adding items to listbox

        private void CustomSteamIDBox_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }   //making shure that CustomSteamIDBox has only numbers

        private void DifferentSteamIDRB_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = true;
            CustomSteamIDBox.Visible = true;
            CalculateCustomSteamIDButton.Visible = true;
            label4.Visible = true;
            CountOwnedGamesCID.Visible = true;
            OwnedGamesCustomSteamIDLB.Visible = true;
            this.Size = new Size(517, 300);
        }   //RB For Different SteamID Check

        private void OwnSteamIDRB_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
            CustomSteamIDBox.Visible = false;
            CalculateCustomSteamIDButton.Visible = false;
            label4.Visible = false;
            CountOwnedGamesCID.Visible = false;
            OwnedGamesCustomSteamIDLB.Visible = false;
            this.Size = new Size(370, 300);
        }         //RB For Own SteamID Check

        public static List<string> GetOwnedSteamGames(string KEY, string SteamID)
        {

            string Raw;
            int Gamecount;
            List<string> gameid = new List<string>();
            using (WebClient client = new WebClient())
            {

                try
                {
                    Raw = client.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + KEY + "&steamid=" + SteamID + "&format=json");
                    Gamecount = Int32.Parse(Regex.Match(Raw, "game_count\":(.+?),").Groups[1].Value);
                    for (int i = 0; i < Gamecount; i++)
                    {
                        Regex regex = new Regex("d\":(.*?),");
                        var v = regex.Match(Raw);
                        string s = v.Groups[1].ToString();
                        Raw = Regex.Replace(Raw, "d\":" + s + ",", "");

                        if (s != "")
                        {
                            gameid.Add(s);
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
                List<int> FAKE = new List<int>();
                List<string> CopyFreeGamesList = new List<string>(FreeGamesList);

                List<string> CopyOwnedSGames = new List<string>(GetOwnedSteamGames(KEY, STEAMID));
                for (int i = 0; i < CopyFreeGamesList.Count; i++)
                {
                    for (int g = 0; g < CopyOwnedSGames.Count; g++)
                    {
                        if (CopyOwnedSGames[g] == FreeGamesList[i])
                        {
                            CopyOwnedSGames[g] = "0";
                        }
                    }
                }
                name.Items.Clear();
                for (int i = 0; i < CopyOwnedSGames.Count; i++)
                {
                    if (CopyOwnedSGames[i] != "0")
                    {
                        FAKE.Add(int.Parse(CopyOwnedSGames[i]));
                    }
                }
                name.Items.Clear();
                FAKE.Sort();
                for (int i = 0; i < FAKE.Count; i++)
                {
                    if (FAKE[i] != 0)
                    {
                        name.Items.Add(FAKE[i]);
                    }
                }
            
        }

        private void FilterFreeGamesCHB_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterFreeGamesCHB.Checked)
            {
                if (OwnedGamesCustomSteamIDLB.Items.Count != 0)
                {
                        FilterFreeGamesCHB.Enabled = false;
                        OwnedSteamGamesFilteredNoFreeGames(OwnedGamesCustomSteamIDLB, CustomSteamIDBox.Text);
                        OwnedSteamGamesFilteredNoFreeGames(OwnedGamesLBox, steamid);
                        CountOwnedGamesCID.Text = OwnedGamesCustomSteamIDLB.Items.Count.ToString();
                        FilterFreeGamesCHB.Enabled = true;
                }
                else
                {
                        FilterFreeGamesCHB.Enabled = false;
                        OwnedSteamGamesFilteredNoFreeGames(OwnedGamesLBox, steamid);
                        OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString(); // For updating the GameCount in owned games
                        FilterFreeGamesCHB.Enabled = true;
                }
                OwnedGamesBox.Text = OwnedGamesLBox.Items.Count.ToString();
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

        public void Exporter(ListBox ListBox,string TxtName)                              //Exporting Function
        {
            bool matchmatches = false;
            string ProblemGamesid = "";
            int Problemgames = 0;
            bool Problem = false;
            int i;
            string[] SteamGames = System.IO.File.ReadAllLines("Steamnames.txt");
            List<string> lines = new List<string>();
            for (i = 0; i < ListBox.Items.Count; i++)
            {
                string match = null;
                //var match = SteamGames.FirstOrDefault(stringToCheck => stringToCheck.Contains(ListBox.Items[i].ToString()));
                bool contains = SteamGames.Contains(ListBox.Items[i].ToString());
                if (contains == true)
                {
                    match = ListBox.Items[i].ToString();
                }
                if (match == ListBox.Items[i].ToString())
                {
                    if (match != null)
                    {
                        var index = Array.FindIndex(SteamGames, row => row == match);
                        lines.Add(match + " " + File.ReadLines("Steamnames.txt").ElementAtOrDefault(index + 1));
                    }
                }
                else
                {
                    if (match != null)
                    {
                        var index = Array.FindIndex(SteamGames, row => row.Contains(match));
                        lines.Add(match + " " + File.ReadLines("Steamnames.txt").ElementAtOrDefault(index + 1));
                    }
                    Problem = true;
                    Problemgames++;
                    ProblemGamesid = match + " | " + ProblemGamesid;
                }
            }
            System.IO.File.WriteAllLines(TxtName, lines);
            if (Problem == true)
            {
                MessageBox.Show(Problemgames + " games have not been exported or have been incorrectly exported! GAMEID=" + ProblemGamesid + "SEND THIS TO THE DEVELOPER AT krcenov@abv.bg", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exportMyGamesAppidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("My Steam Games.txt"))
            {
                var Answer = MessageBox.Show("You have exported your games recently, would you like to override?", "Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Answer == DialogResult.No)
                {
                    MessageBox.Show("No changes were made to \"My Steam Games.txt\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Exporter(OwnedGamesLBox, "My Steam Games.txt");
                    MessageBox.Show("Your Owned Games AppID's and names have been saved to the current directory in a file called My Steam Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Exporter(OwnedGamesLBox, "My Steam Games.txt");
                MessageBox.Show("Your Owned Games AppID's and names have been saved to the current directory in a file called My Steam Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exportDifferentSteamIDGamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("Custom SteamID Games.txt"))
            {
                var Answer = MessageBox.Show("You have exported the games of a user recently, would you like to override?", "Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Answer == DialogResult.No)
                {
                    MessageBox.Show("No changes were made to \"Custom SteamID Games.txt\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (OwnedGamesCustomSteamIDLB.Items.Count <= 0)
                    {
                        var answer = MessageBox.Show("You haven't inputted a custom SteamID. \n Would you like to do that now?", "Confict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            label3.Visible = true;
                            CustomSteamIDBox.Visible = true;
                            CalculateCustomSteamIDButton.Visible = true;
                            label4.Visible = true;
                            CountOwnedGamesCID.Visible = true;
                            OwnedGamesCustomSteamIDLB.Visible = true;
                            this.Size = new Size(517, 300);
                        }
                    }
                    else
                    {
                        Exporter(OwnedGamesCustomSteamIDLB, "Custom SteamID Games.txt");
                        MessageBox.Show("CustomID Owned Games AppID's and names have been saved to the current directory in a file called Custom SteamID Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
              }
            else
            {
                if (OwnedGamesCustomSteamIDLB.Items.Count <= 0)
                {
                    var answer = MessageBox.Show("You haven't inputted a custom SteamID. \n Would you like to do that now?", "Confict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        label3.Visible = true;
                        CustomSteamIDBox.Visible = true;
                        CalculateCustomSteamIDButton.Visible = true;
                        label4.Visible = true;
                        CountOwnedGamesCID.Visible = true;
                        OwnedGamesCustomSteamIDLB.Visible = true;
                        this.Size = new Size(517, 300);
                    }
                }
                else
                {
                    Exporter(OwnedGamesCustomSteamIDLB, "Custom SteamID Games.txt");
                    MessageBox.Show("CustomID Owned Games AppID's and names have been saved to the current directory in a file called Custom SteamID Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void exportBothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("My Steam Games.txt"))
            {
                var Answer = MessageBox.Show("You have exported your games recently, would you like to override?", "Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Answer == DialogResult.No)
                {
                    MessageBox.Show("No changes were made to \"My Steam Games.txt\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Exporter(OwnedGamesLBox, "My Steam Games.txt");
                    MessageBox.Show("Your Owned Games AppID's and names have been saved to the current directory in a file called My Steam Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Exporter(OwnedGamesLBox, "My Steam Games.txt");
                MessageBox.Show("Your Owned Games AppID's and names have been saved to the current directory in a file called My Steam Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (File.Exists("Custom SteamID Games.txt"))
            {
                var Answer = MessageBox.Show("You have exported the games of a user recently, would you like to override?", "Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Answer == DialogResult.No)
                {
                    MessageBox.Show("No changes were made to \"Custom SteamID Games.txt\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (OwnedGamesCustomSteamIDLB.Items.Count <= 0)
                    {
                        var answer = MessageBox.Show("You haven't inputted a custom SteamID. \n Would you like to do that now?", "Confict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            label3.Visible = true;
                            CustomSteamIDBox.Visible = true;
                            CalculateCustomSteamIDButton.Visible = true;
                            label4.Visible = true;
                            CountOwnedGamesCID.Visible = true;
                            OwnedGamesCustomSteamIDLB.Visible = true;
                            this.Size = new Size(517, 300);
                        }
                    }
                    else
                    {
                        Exporter(OwnedGamesCustomSteamIDLB, "Custom SteamID Games.txt");
                        MessageBox.Show("CustomID Owned Games AppID's and names have been saved to the current directory in a file called Custom SteamID Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                if (OwnedGamesCustomSteamIDLB.Items.Count <= 0)
                {
                    var answer = MessageBox.Show("You haven't inputted a custom SteamID. \n Would you like to do that now?", "Confict", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        label3.Visible = true;
                        CustomSteamIDBox.Visible = true;
                        CalculateCustomSteamIDButton.Visible = true;
                        label4.Visible = true;
                        CountOwnedGamesCID.Visible = true;
                        OwnedGamesCustomSteamIDLB.Visible = true;
                        this.Size = new Size(517, 300);
                    }
                }
                else
                {
                    Exporter(OwnedGamesCustomSteamIDLB, "Custom SteamID Games.txt");
                    MessageBox.Show("CustomID Owned Games AppID's and names have been saved to the current directory in a file called Custom SteamID Games.txt", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }
    }
}
