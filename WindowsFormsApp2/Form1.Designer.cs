namespace WindowsFormsApp2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OwnedGamesBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SteamFriendCountBox = new System.Windows.Forms.TextBox();
            this.SteamFriendCount = new System.Windows.Forms.Label();
            this.SteamIDBox = new System.Windows.Forms.TextBox();
            this.SteamLevelBox = new System.Windows.Forms.TextBox();
            this.SteamNameBox = new System.Windows.Forms.TextBox();
            this.CalculateCustomSteamIDButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OwnedGamesLBox = new System.Windows.Forms.ListBox();
            this.DifferentSteamIDRB = new System.Windows.Forms.RadioButton();
            this.CustomSteamIDBox = new System.Windows.Forms.TextBox();
            this.OwnSteamIDRB = new System.Windows.Forms.RadioButton();
            this.OwnedGamesCustomSteamIDLB = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CountOwnedGamesCID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Change AppID";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(204, 138);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(4, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "SteamID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(4, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Steam level:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(4, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Steam name:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.81853F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.18147F));
            this.tableLayoutPanel1.Controls.Add(this.OwnedGamesBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.SteamFriendCountBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.SteamFriendCount, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.SteamIDBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SteamLevelBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.SteamNameBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(89, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 107);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // OwnedGamesBox
            // 
            this.OwnedGamesBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OwnedGamesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OwnedGamesBox.Location = new System.Drawing.Point(89, 88);
            this.OwnedGamesBox.Name = "OwnedGamesBox";
            this.OwnedGamesBox.ReadOnly = true;
            this.OwnedGamesBox.Size = new System.Drawing.Size(167, 13);
            this.OwnedGamesBox.TabIndex = 18;
            this.OwnedGamesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Games owned:";
            // 
            // SteamFriendCountBox
            // 
            this.SteamFriendCountBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SteamFriendCountBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SteamFriendCountBox.Location = new System.Drawing.Point(89, 67);
            this.SteamFriendCountBox.Name = "SteamFriendCountBox";
            this.SteamFriendCountBox.ReadOnly = true;
            this.SteamFriendCountBox.Size = new System.Drawing.Size(167, 13);
            this.SteamFriendCountBox.TabIndex = 13;
            this.SteamFriendCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SteamFriendCount
            // 
            this.SteamFriendCount.AutoSize = true;
            this.SteamFriendCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.SteamFriendCount.Location = new System.Drawing.Point(4, 64);
            this.SteamFriendCount.Name = "SteamFriendCount";
            this.SteamFriendCount.Size = new System.Drawing.Size(77, 20);
            this.SteamFriendCount.TabIndex = 10;
            this.SteamFriendCount.Text = "Steam Friends:";
            // 
            // SteamIDBox
            // 
            this.SteamIDBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SteamIDBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SteamIDBox.Location = new System.Drawing.Point(89, 46);
            this.SteamIDBox.Name = "SteamIDBox";
            this.SteamIDBox.ReadOnly = true;
            this.SteamIDBox.Size = new System.Drawing.Size(167, 13);
            this.SteamIDBox.TabIndex = 12;
            this.SteamIDBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SteamLevelBox
            // 
            this.SteamLevelBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SteamLevelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SteamLevelBox.Location = new System.Drawing.Point(89, 25);
            this.SteamLevelBox.Name = "SteamLevelBox";
            this.SteamLevelBox.ReadOnly = true;
            this.SteamLevelBox.Size = new System.Drawing.Size(167, 13);
            this.SteamLevelBox.TabIndex = 11;
            this.SteamLevelBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SteamNameBox
            // 
            this.SteamNameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SteamNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SteamNameBox.Location = new System.Drawing.Point(89, 4);
            this.SteamNameBox.Name = "SteamNameBox";
            this.SteamNameBox.ReadOnly = true;
            this.SteamNameBox.Size = new System.Drawing.Size(167, 13);
            this.SteamNameBox.TabIndex = 10;
            this.SteamNameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CalculateCustomSteamIDButton
            // 
            this.CalculateCustomSteamIDButton.Location = new System.Drawing.Point(91, 187);
            this.CalculateCustomSteamIDButton.Name = "CalculateCustomSteamIDButton";
            this.CalculateCustomSteamIDButton.Size = new System.Drawing.Size(107, 23);
            this.CalculateCustomSteamIDButton.TabIndex = 11;
            this.CalculateCustomSteamIDButton.Text = "Calculate";
            this.CalculateCustomSteamIDButton.UseVisualStyleBackColor = true;
            this.CalculateCustomSteamIDButton.Visible = false;
            this.CalculateCustomSteamIDButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Enter desired AppID:";
            // 
            // OwnedGamesLBox
            // 
            this.OwnedGamesLBox.FormattingEnabled = true;
            this.OwnedGamesLBox.Location = new System.Drawing.Point(4, 25);
            this.OwnedGamesLBox.Name = "OwnedGamesLBox";
            this.OwnedGamesLBox.Size = new System.Drawing.Size(79, 173);
            this.OwnedGamesLBox.TabIndex = 13;
            // 
            // DifferentSteamIDRB
            // 
            this.DifferentSteamIDRB.AutoSize = true;
            this.DifferentSteamIDRB.Location = new System.Drawing.Point(220, 187);
            this.DifferentSteamIDRB.Name = "DifferentSteamIDRB";
            this.DifferentSteamIDRB.Size = new System.Drawing.Size(129, 17);
            this.DifferentSteamIDRB.TabIndex = 15;
            this.DifferentSteamIDRB.TabStop = true;
            this.DifferentSteamIDRB.Text = "Use different SteamID";
            this.DifferentSteamIDRB.UseVisualStyleBackColor = true;
            this.DifferentSteamIDRB.CheckedChanged += new System.EventHandler(this.DifferentSteamIDRB_CheckedChanged);
            // 
            // CustomSteamIDBox
            // 
            this.CustomSteamIDBox.Location = new System.Drawing.Point(366, 41);
            this.CustomSteamIDBox.Name = "CustomSteamIDBox";
            this.CustomSteamIDBox.Size = new System.Drawing.Size(129, 20);
            this.CustomSteamIDBox.TabIndex = 16;
            this.CustomSteamIDBox.Visible = false;
            this.CustomSteamIDBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CustomSteamIDBox_Keypress);
            // 
            // OwnSteamIDRB
            // 
            this.OwnSteamIDRB.AutoSize = true;
            this.OwnSteamIDRB.Checked = true;
            this.OwnSteamIDRB.Location = new System.Drawing.Point(220, 164);
            this.OwnSteamIDRB.Name = "OwnSteamIDRB";
            this.OwnSteamIDRB.Size = new System.Drawing.Size(111, 17);
            this.OwnSteamIDRB.TabIndex = 14;
            this.OwnSteamIDRB.TabStop = true;
            this.OwnSteamIDRB.Text = "Use own SteamID";
            this.OwnSteamIDRB.UseVisualStyleBackColor = true;
            this.OwnSteamIDRB.CheckedChanged += new System.EventHandler(this.OwnSteamIDRB_CheckedChanged);
            // 
            // OwnedGamesCustomSteamIDLB
            // 
            this.OwnedGamesCustomSteamIDLB.FormattingEnabled = true;
            this.OwnedGamesCustomSteamIDLB.Location = new System.Drawing.Point(366, 67);
            this.OwnedGamesCustomSteamIDLB.Name = "OwnedGamesCustomSteamIDLB";
            this.OwnedGamesCustomSteamIDLB.Size = new System.Drawing.Size(129, 134);
            this.OwnedGamesCustomSteamIDLB.TabIndex = 17;
            this.OwnedGamesCustomSteamIDLB.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Enter SteamID";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Owned Games:";
            this.label4.Visible = false;
            // 
            // CountOwnedGamesCID
            // 
            this.CountOwnedGamesCID.AutoSize = true;
            this.CountOwnedGamesCID.Location = new System.Drawing.Point(449, 9);
            this.CountOwnedGamesCID.Name = "CountOwnedGamesCID";
            this.CountOwnedGamesCID.Size = new System.Drawing.Size(0, 13);
            this.CountOwnedGamesCID.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(93, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Created by krcenov on 12.07.2017";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "You own:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 211);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CountOwnedGamesCID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OwnedGamesCustomSteamIDLB);
            this.Controls.Add(this.CustomSteamIDBox);
            this.Controls.Add(this.DifferentSteamIDRB);
            this.Controls.Add(this.OwnSteamIDRB);
            this.Controls.Add(this.OwnedGamesLBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CalculateCustomSteamIDButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "krcenov\'s Steam info box";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox SteamIDBox;
        private System.Windows.Forms.TextBox SteamLevelBox;
        private System.Windows.Forms.TextBox SteamNameBox;
        private System.Windows.Forms.TextBox SteamFriendCountBox;
        private System.Windows.Forms.Label SteamFriendCount;
        private System.Windows.Forms.Button CalculateCustomSteamIDButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox OwnedGamesLBox;
        private System.Windows.Forms.TextBox OwnedGamesBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton DifferentSteamIDRB;
        private System.Windows.Forms.TextBox CustomSteamIDBox;
        private System.Windows.Forms.RadioButton OwnSteamIDRB;
        private System.Windows.Forms.ListBox OwnedGamesCustomSteamIDLB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CountOwnedGamesCID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

