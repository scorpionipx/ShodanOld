namespace Shodan3._0
{
    partial class Shodan
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shodan));
            this.historyPannel = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CommandsPannel = new System.Windows.Forms.RichTextBox();
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.InputButton = new System.Windows.Forms.Button();
            this.AbordButton = new System.Windows.Forms.Button();
            this.ShowCPbtn = new System.Windows.Forms.Button();
            this.HideCPbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.RemoteBtn = new System.Windows.Forms.Button();
            this.StopReadingBtn = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.ListenBtn = new System.Windows.Forms.Button();
            this.ReplyBtn = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostGenre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostSources = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.RadioHide = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // historyPannel
            // 
            this.historyPannel.BackColor = System.Drawing.SystemColors.WindowText;
            this.historyPannel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.historyPannel.ForeColor = System.Drawing.Color.YellowGreen;
            this.historyPannel.Location = new System.Drawing.Point(0, 272);
            this.historyPannel.Name = "historyPannel";
            this.historyPannel.ReadOnly = true;
            this.historyPannel.Size = new System.Drawing.Size(485, 39);
            this.historyPannel.TabIndex = 1;
            this.historyPannel.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CommandsPannel
            // 
            this.CommandsPannel.BackColor = System.Drawing.SystemColors.MenuText;
            this.CommandsPannel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandsPannel.ForeColor = System.Drawing.Color.Honeydew;
            this.CommandsPannel.Location = new System.Drawing.Point(338, 0);
            this.CommandsPannel.Name = "CommandsPannel";
            this.CommandsPannel.Size = new System.Drawing.Size(147, 272);
            this.CommandsPannel.TabIndex = 5;
            this.CommandsPannel.Text = "Commands pannel";
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox.Font = new System.Drawing.Font("Georgia", 10F);
            this.TextBox.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TextBox.Location = new System.Drawing.Point(0, 182);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(275, 90);
            this.TextBox.TabIndex = 6;
            this.TextBox.Text = "<Write anything here then use any of the available commands>";
            this.TextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(10, 182);
            this.textBox1.MaxLength = 20;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 25);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "<write username here>";
            this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // InputButton
            // 
            this.InputButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.InputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InputButton.Location = new System.Drawing.Point(10, 207);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(142, 23);
            this.InputButton.TabIndex = 9;
            this.InputButton.Text = "Submit";
            this.InputButton.UseVisualStyleBackColor = false;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // AbordButton
            // 
            this.AbordButton.BackColor = System.Drawing.Color.LightCoral;
            this.AbordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AbordButton.Location = new System.Drawing.Point(10, 230);
            this.AbordButton.Name = "AbordButton";
            this.AbordButton.Size = new System.Drawing.Size(142, 23);
            this.AbordButton.TabIndex = 10;
            this.AbordButton.Text = "Abord";
            this.AbordButton.UseVisualStyleBackColor = false;
            this.AbordButton.Click += new System.EventHandler(this.AbordButton_Click);
            // 
            // ShowCPbtn
            // 
            this.ShowCPbtn.Location = new System.Drawing.Point(91, 8);
            this.ShowCPbtn.Name = "ShowCPbtn";
            this.ShowCPbtn.Size = new System.Drawing.Size(60, 23);
            this.ShowCPbtn.TabIndex = 11;
            this.ShowCPbtn.Text = "Show CP";
            this.ShowCPbtn.UseVisualStyleBackColor = true;
            this.ShowCPbtn.Click += new System.EventHandler(this.ShowCPbtn_Click);
            this.ShowCPbtn.MouseLeave += new System.EventHandler(this.ShowCPbtn_MouseLeave);
            this.ShowCPbtn.MouseHover += new System.EventHandler(this.ShowCPbtn_MouseHover);
            // 
            // HideCPbtn
            // 
            this.HideCPbtn.Location = new System.Drawing.Point(157, 8);
            this.HideCPbtn.Name = "HideCPbtn";
            this.HideCPbtn.Size = new System.Drawing.Size(60, 23);
            this.HideCPbtn.TabIndex = 12;
            this.HideCPbtn.Text = "Hide CP";
            this.HideCPbtn.UseVisualStyleBackColor = true;
            this.HideCPbtn.Click += new System.EventHandler(this.HideCPbtn_Click);
            this.HideCPbtn.MouseLeave += new System.EventHandler(this.HideCPbtn_MouseLeave);
            this.HideCPbtn.MouseHover += new System.EventHandler(this.HideCPbtn_MouseHover);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(91, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            this.button2.MouseHover += new System.EventHandler(this.button2_MouseHover);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(172, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            this.button3.MouseHover += new System.EventHandler(this.button3_MouseHover);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(10, 66);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            this.button4.MouseHover += new System.EventHandler(this.button4_MouseHover);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(91, 66);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.button5.MouseLeave += new System.EventHandler(this.button5_MouseLeave);
            this.button5.MouseHover += new System.EventHandler(this.button5_MouseHover);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(172, 66);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 19;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.MouseLeave += new System.EventHandler(this.button6_MouseLeave);
            this.button6.MouseHover += new System.EventHandler(this.button6_MouseHover);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(10, 95);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 18;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            this.button7.MouseLeave += new System.EventHandler(this.button7_MouseLeave);
            this.button7.MouseHover += new System.EventHandler(this.button7_MouseHover);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(91, 95);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 17;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.MouseLeave += new System.EventHandler(this.button8_MouseLeave);
            this.button8.MouseHover += new System.EventHandler(this.button8_MouseHover);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(172, 95);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 22;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            this.button9.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
            this.button9.MouseHover += new System.EventHandler(this.button9_MouseHover);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(10, 124);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 21;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            this.button10.MouseLeave += new System.EventHandler(this.button10_MouseLeave);
            this.button10.MouseHover += new System.EventHandler(this.button10_MouseHover);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(91, 124);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 27;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(172, 124);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 26;
            this.button12.Text = "button12";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(10, 153);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 25;
            this.button13.Text = "button13";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(91, 153);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 24;
            this.button14.Text = "button14";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(172, 153);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 23;
            this.button15.Text = "button15";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // RemoteBtn
            // 
            this.RemoteBtn.BackColor = System.Drawing.Color.Maroon;
            this.RemoteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoteBtn.Location = new System.Drawing.Point(12, 8);
            this.RemoteBtn.Name = "RemoteBtn";
            this.RemoteBtn.Size = new System.Drawing.Size(23, 23);
            this.RemoteBtn.TabIndex = 28;
            this.RemoteBtn.Text = "R";
            this.RemoteBtn.UseVisualStyleBackColor = false;
            this.RemoteBtn.Click += new System.EventHandler(this.RemoteBtn_Click);
            this.RemoteBtn.MouseLeave += new System.EventHandler(this.RemoteBtn_MouseLeave);
            this.RemoteBtn.MouseHover += new System.EventHandler(this.RemoteBtn_MouseHover);
            // 
            // StopReadingBtn
            // 
            this.StopReadingBtn.BackColor = System.Drawing.Color.Coral;
            this.StopReadingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopReadingBtn.Location = new System.Drawing.Point(10, 128);
            this.StopReadingBtn.Name = "StopReadingBtn";
            this.StopReadingBtn.Size = new System.Drawing.Size(58, 48);
            this.StopReadingBtn.TabIndex = 29;
            this.StopReadingBtn.Text = "Stop \r\nReading";
            this.StopReadingBtn.UseVisualStyleBackColor = false;
            this.StopReadingBtn.Click += new System.EventHandler(this.StopReadingBtn_Click);
            this.StopReadingBtn.MouseLeave += new System.EventHandler(this.StopReadingBtn_MouseLeave);
            this.StopReadingBtn.MouseHover += new System.EventHandler(this.StopReadingBtn_MouseHover);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Click to restore.";
            this.notifyIcon.BalloonTipTitle = "Shodan";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Shodan - Click to restore.";
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ForeColor = System.Drawing.Color.SpringGreen;
            this.label1.Location = new System.Drawing.Point(338, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 41);
            this.label1.TabIndex = 30;
            this.label1.Text = "label1\r\ntext to show\r\ninfo2";
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // ListenBtn
            // 
            this.ListenBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ListenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListenBtn.Location = new System.Drawing.Point(37, 8);
            this.ListenBtn.Name = "ListenBtn";
            this.ListenBtn.Size = new System.Drawing.Size(23, 23);
            this.ListenBtn.TabIndex = 34;
            this.ListenBtn.Text = "L";
            this.ListenBtn.UseVisualStyleBackColor = false;
            this.ListenBtn.Click += new System.EventHandler(this.ListenBtn_Click);
            this.ListenBtn.MouseLeave += new System.EventHandler(this.ListenBtn_MouseLeave);
            this.ListenBtn.MouseHover += new System.EventHandler(this.ListenBtn_MouseHover);
            // 
            // ReplyBtn
            // 
            this.ReplyBtn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ReplyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplyBtn.Location = new System.Drawing.Point(62, 8);
            this.ReplyBtn.Name = "ReplyBtn";
            this.ReplyBtn.Size = new System.Drawing.Size(23, 23);
            this.ReplyBtn.TabIndex = 37;
            this.ReplyBtn.Text = "V";
            this.ReplyBtn.UseVisualStyleBackColor = false;
            this.ReplyBtn.Click += new System.EventHandler(this.ReplyBtn_Click);
            this.ReplyBtn.MouseLeave += new System.EventHandler(this.ReplyBtn_MouseLeave);
            this.ReplyBtn.MouseHover += new System.EventHandler(this.ReplyBtn_MouseHover);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Shodan3._0.Properties.Resources.rec_cropped;
            this.pictureBox3.Location = new System.Drawing.Point(444, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(41, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 36;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseLeave += new System.EventHandler(this.pictureBox3_MouseLeave);
            this.pictureBox3.MouseHover += new System.EventHandler(this.pictureBox3_MouseHover);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Shodan3._0.Properties.Resources.Wifi_Gratis_Koneksi_Internet_Free_Hotpost_Zone_Animated;
            this.pictureBox2.Location = new System.Drawing.Point(419, 272);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(66, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            this.pictureBox2.MouseHover += new System.EventHandler(this.pictureBox2_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Shodan3._0.Properties.Resources.g__1_;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(485, 272);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.Transparent;
            this.ExitBtn.BackgroundImage = global::Shodan3._0.Properties.Resources.exitbtn;
            this.ExitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitBtn.Location = new System.Drawing.Point(223, 8);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ExitBtn.Size = new System.Drawing.Size(23, 23);
            this.ExitBtn.TabIndex = 38;
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            this.ExitBtn.MouseLeave += new System.EventHandler(this.ExitBtn_MouseLeave);
            this.ExitBtn.MouseHover += new System.EventHandler(this.ExitBtn_MouseHover);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PostName,
            this.PostGenre,
            this.PostCountry,
            this.PostLanguage,
            this.PostSources});
            this.dataGridView1.Location = new System.Drawing.Point(10, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(322, 200);
            this.dataGridView1.TabIndex = 39;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // PostName
            // 
            this.PostName.HeaderText = "PostName";
            this.PostName.Name = "PostName";
            this.PostName.ReadOnly = true;
            // 
            // PostGenre
            // 
            this.PostGenre.HeaderText = "PostGenre";
            this.PostGenre.Name = "PostGenre";
            this.PostGenre.ReadOnly = true;
            // 
            // PostCountry
            // 
            this.PostCountry.HeaderText = "PostCountry";
            this.PostCountry.Name = "PostCountry";
            this.PostCountry.ReadOnly = true;
            // 
            // PostLanguage
            // 
            this.PostLanguage.HeaderText = "PostLanguage";
            this.PostLanguage.Name = "PostLanguage";
            this.PostLanguage.ReadOnly = true;
            // 
            // PostSources
            // 
            this.PostSources.HeaderText = "PostSources";
            this.PostSources.Name = "PostSources";
            this.PostSources.ReadOnly = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(257, 46);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 20);
            this.textBox2.TabIndex = 40;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // RadioHide
            // 
            this.RadioHide.BackColor = System.Drawing.Color.Maroon;
            this.RadioHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RadioHide.Location = new System.Drawing.Point(0, 66);
            this.RadioHide.Name = "RadioHide";
            this.RadioHide.Size = new System.Drawing.Size(10, 200);
            this.RadioHide.TabIndex = 41;
            this.RadioHide.UseVisualStyleBackColor = false;
            this.RadioHide.Click += new System.EventHandler(this.RadioHide_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(339, 220);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(75, 45);
            this.trackBar1.TabIndex = 44;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Shodan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadioHide);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ReplyBtn);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ListenBtn);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.StopReadingBtn);
            this.Controls.Add(this.RemoteBtn);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HideCPbtn);
            this.Controls.Add(this.ShowCPbtn);
            this.Controls.Add(this.AbordButton);
            this.Controls.Add(this.InputButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.CommandsPannel);
            this.Controls.Add(this.historyPannel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Shodan";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shodan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Shodan_FormClosing);
            this.Load += new System.EventHandler(this.Shodan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox historyPannel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox CommandsPannel;
        private System.Windows.Forms.RichTextBox TextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.Button AbordButton;
        private System.Windows.Forms.Button ShowCPbtn;
        private System.Windows.Forms.Button HideCPbtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button RemoteBtn;
        private System.Windows.Forms.Button StopReadingBtn;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button ListenBtn;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button ReplyBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostGenre;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostCountry;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostSources;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button RadioHide;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

