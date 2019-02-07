using System;
using System.Windows.Forms;
using System.IO;

namespace RadioStream
{
    public partial class Form1 : Form
    {
        public struct RadioPost
        {
            public string PostName, PostCountry, PostGenre, PostLanguage, PostSources;
            // Constructor:
            public RadioPost(string PostName, string PostCountry, string PostGenre, string PostLanguage, string PostSources)
            {
                this.PostName = PostName;
                this.PostCountry = PostCountry;
                this.PostGenre = PostGenre;
                this.PostLanguage = PostLanguage;
                this.PostSources = PostSources;
            }
            // Override the ToString method:
            public override string ToString()
            {
                return "";
            }
        }

            public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

        public void search_RadioPost(string key_words)
        {
            int c = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (var myString in File.ReadAllLines(@"RadioStationDB.rsd"))
            {
                if(myString.Contains(key_words))
                {
                    string full_details = myString;
                    int index;
                    index = full_details.IndexOf('\t');
                    string post_name = full_details.Substring(0, index);
                    full_details = full_details.Substring(1);
                    full_details = full_details.Substring(index);
                    index = full_details.IndexOf('\t');
                    string post_details = full_details.Substring(0, index);
                    full_details = full_details.Substring(1);
                    full_details = full_details.Substring(index);
                    index = full_details.IndexOf('\t');
                    string post_genre = full_details.Substring(0, index);
                    full_details = full_details.Substring(1);
                    full_details = full_details.Substring(index);
                    index = full_details.IndexOf('\t');
                    string post_country = full_details.Substring(0, index);
                    full_details = full_details.Substring(1);
                    full_details = full_details.Substring(index);
                    index = full_details.IndexOf('\t');
                    string post_language = full_details.Substring(0, index);
                    full_details = full_details.Substring(1);
                    full_details = full_details.Substring(index);
                    index = full_details.IndexOf('\t');
                    string post_source = full_details.Substring(0, index);
                    RadioPost RP = new RadioPost(post_name, post_country, post_genre, post_language, post_source);
                    c++;
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = RP.PostName;
                    row.Cells[1].Value = RP.PostGenre;
                    row.Cells[2].Value = RP.PostCountry;
                    row.Cells[3].Value = RP.PostLanguage;
                    row.Cells[4].Value = RP.PostSources;
                    dataGridView1.Rows.Add(row);
                }
            }
            label1.Text = c.ToString() + " radio posts found for \"" + key_words.ToString() + "\".";
        }

        public static void PlayMusicFromURL(string url)
        {
            player.URL = url;
            player.settings.volume = 100;
            player.controls.play();
        }

        public static void PlayerStop()
        {
            player.controls.stop();
        }

        public static void SetPlayerVolume(int volume)
        {
            player.settings.volume = volume;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayMusicFromURL("http://radio.bigupradio.com:8080/dancehall.asx");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayerStop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 4)
            {
                search_RadioPost(textBox1.Text);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            string URL = row.Cells[4].Value.ToString();
            PlayMusicFromURL(URL);
        }
    }
}
