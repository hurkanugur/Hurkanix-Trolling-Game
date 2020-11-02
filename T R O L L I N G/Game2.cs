using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_R_O_L_L_I_N_G
{
    
        public partial class Game2 : Form
    {
        public Game2()
        {
            InitializeComponent();
        }

        //Form yuklenmesiyle bu elemanlar olusturulacak
        private Button User = new Button();
        private PictureBox Bard = new PictureBox();
        private Timer Game_Time = new Timer();
        private Timer Screen_Time = new Timer();
        private Timer Game_Clock = new Timer();
        private PictureBox Bard_Exit = new PictureBox();
        private int Saniye = 0;
        private int Dakika = 0;
        string Clock = "00 : 00";

        //Oyun islemleri icin gerekli
        private static int Score = 0;
        private static int Level = 1;            
        private void Game2_Load(object sender, EventArgs e)
        {
            //Ekran ayarlari
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(984, 562);     
            this.FormBorderStyle = FormBorderStyle.None;     
            this.Text = "T R O L L I N G";
            this.Icon = Icon.ExtractAssociatedIcon("Trolling Settings\\Bard_Icon.ico");
            this.Cursor = Cursors.Cross;

            //Kullanici ayarlari
            User.BackColor = Color.Black;
            User.Size = new Size(70, 50);
            User.Location = new Point(442, 241);
            User.Enabled = false;
            User.Text = Convert.ToString(Score);  
            User.Font = new Font("Cooper", 14, FontStyle.Bold);
            User.ForeColor = Color.Black;
            User.BackColor = Color.Aqua;

            //Bard ayarlari
            Bard.ImageLocation = "Trolling Settings\\Bard_Walking.gif";           
            Bard.SizeMode = PictureBoxSizeMode.StretchImage;
            Bard.Size = new Size(100, 80);
            Bard.Enabled = true;

            //Exit Butonu
            Bard_Exit.ImageLocation = "Trolling Settings\\Bard_Exit.png";
            Bard_Exit.SizeMode = PictureBoxSizeMode.StretchImage;
            Bard_Exit.Size = new Size(90, 80);
            Bard_Exit.Location = new Point(894, 0);
            Bard_Exit.Enabled = true;

            //Zaman Akisi Aktif
            Game_Time.Enabled = true;
            Screen_Time.Enabled = true;
            Game_Clock.Enabled = true;
            Game_Clock.Interval = 1000;
            Screen_Time.Interval = 100;

            //WMP gorunmezligi
            WMP1.Visible = false;
            WMP2.Visible = false;
            WMP1.settings.volume = 100;
            WMP2.settings.volume = 100;

            //Form elemanlari ekrana yerlestirildi
            this.Controls.Add(Bard);
            this.Controls.Add(User);
            this.Controls.Add(Bard_Exit);
          
            //Olaylar
            this.MouseDown += new MouseEventHandler(Color_OF_User);
            this.MouseMove += new MouseEventHandler(Teleport_User);
            this.Paint += new PaintEventHandler(Show_Level_And_Time);
            Game_Time.Tick += new EventHandler(Time_Loop_For_Bard);
            Game_Clock.Tick += new EventHandler(Game_Clock_Ticks);
            Screen_Time.Tick += new EventHandler(Time_Loop_For_Screen);
            Bard.MouseDown += new MouseEventHandler(Score_Click);
            Bard_Exit.MouseClick += new MouseEventHandler(Trolling_Exit);
        }


        //Kullanici Mouse Click ile renk degistiriyor
        private Random User_Color_Random = new Random();
        private void Color_OF_User(object sender, MouseEventArgs e)
        {         
            User.BackColor = Color.FromArgb(User_Color_Random.Next(0, 256), User_Color_Random.Next(0, 256), User_Color_Random.Next(0, 256));
        }

        //Kullanici ekrandaki bir yere tiklayinca oraya isinlaniyor
        private void Teleport_User(object sender, MouseEventArgs e)
        {
            User.Location = new Point(e.X, e.Y);
        }

        //Kullanicinin Bard'a tiklamasiyla puan kazanmasi
        private void Score_Click(object sender, MouseEventArgs e)
        {        
            // Kullanici renk degistirsin
            User.BackColor = Color.FromArgb(User_Color_Random.Next(0, 256), User_Color_Random.Next(0, 256), User_Color_Random.Next(0, 256));

            WMP1.URL = "Trolling Settings\\Bard_Laugh.mp3";
            Score++;
            User.Text = Convert.ToString(Score);
        }

        private void Game_Clock_Ticks(object sender, EventArgs e)
        {
            if (Saniye < 60)
                Saniye++;
            if (Saniye == 60)
            {
                Saniye = 0;
                Dakika++;
            }
        }

        //Bard'i yakalama suresinin hesaplanma yerinin baslangici
        private Random Text_Color_Random = new Random();
        private void Show_Level_And_Time(object sender, PaintEventArgs e)
        {
            //Level hesaplandi ve ekrana yazdirildi  ve  zaman ekrana yazdirildi                  
            Graphics Screen = this.CreateGraphics();
            Brush Boya = new SolidBrush(Color.Aqua);
            Font Bard_Sayi_Font = new Font("Cooper Black", 19, FontStyle.Bold);
            Font Level_Text_Font = new Font("Cooper Black", 37, FontStyle.Bold);
            Font Time_Text_Font = new Font("Cooper Black", 21, FontStyle.Bold);
            Font Exit_Text_Font = new Font("Cooper Black", 17, FontStyle.Bold);      
            Screen.DrawString("L E V E L " + Level, Level_Text_Font, Boya, 352, 5);
            Screen.DrawString("E X I T", Exit_Text_Font, Boya, 893, 78);
            Screen.DrawString("Yakalamaniz Gereken "+(25-Score)+" Tane Bard Kaldi !", Bard_Sayi_Font, Boya, 210, 78);

            //Oyun suresi hesaplandi ve ekrana yazildi
            if (Dakika < 10 && Saniye < 10)
                Clock = "T I M E R\n  0" + (Dakika) + " : 0" + (Saniye);
            if (Dakika < 10 && Saniye >= 10)
                Clock = "T I M E R\n  0" + (Dakika) + " : " + (Saniye);
            if (Dakika >= 10 && Saniye < 10)
                Clock = "T I M E R\n  " + (Dakika) + " : 0" + (Saniye);
            if (Dakika >= 10 && Saniye >= 10)
                Clock = "T I M E R\n  " + (Dakika) + " : " + (Saniye);
            Screen.DrawString(Clock, Time_Text_Font, Boya, 5, 3);
        
        }

        //Cikis Butonu Olusturuldu
        private void Trolling_Exit(object sender, MouseEventArgs e)
        {
            WMP2.URL = "Trolling Settings\\Bard_Death.mp3";
            if(MessageBox.Show("Oyunumdan cikmak istediginize emin misiniz ?","Hurkan'in Size Bir Sorusu Var !",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {               
                Application.Exit();
            }
            else
            {
                WMP2.URL = "Trolling Settings\\Bard_Troll_Voice.mp3";
            }
        }

        //Ekran Rengi Degisimi Ve Sag Tik Renk Degisimi
        private Random Screen_Color_Random = new Random();
        private void Time_Loop_For_Screen(object sender, EventArgs e)
        {
            //Ekran arkaplan rengi ayarlari
            this.BackColor = Color.FromArgb(Screen_Color_Random.Next(0, 256), Screen_Color_Random.Next(0, 256), Screen_Color_Random.Next(0, 256));
            
            //Sag Tik Renk Degisimi
            Sag_Tik.BackColor = this.BackColor;
            Sag_Tik.ForeColor = this.BackColor;

            Menu_Level_Hack.ForeColor = this.BackColor;
            Level_Hack_1.ForeColor = this.BackColor;
            Level_Hack_2.ForeColor = this.BackColor;
            Level_Hack_3.ForeColor = this.BackColor;
            Level_Hack_4.ForeColor = this.BackColor;
            Level_Hack_5.ForeColor = this.BackColor;
            
            Menu_Speed_Hack.ForeColor = this.BackColor;
            Speed_Hack_Crazy.ForeColor = this.BackColor;
            Speed_Hack_Normal.ForeColor = this.BackColor;
            Speed_Hack_Sleepy.ForeColor = this.BackColor;         
        }

        //Bard'i zaman akisina birakitigimiz yer       
        private Random Bard_Random = new Random();
        private static bool Change_Speed_Level_1 = true;
        private static bool Change_Speed_Level_2 = true;
        private static bool Change_Speed_Level_3 = true;
        private static bool Change_Speed_Level_4 = true;
        private static bool Change_Speed_Level_5 = true;
        private void Time_Loop_For_Bard(object sender, EventArgs e)
        {               
            //Bard haritaya konumlandirildi
            Bard.Location = new Point(Bard_Random.Next(0, 885), Bard_Random.Next(131, 483));         
            if(Level == 1)
            {
                if(Change_Speed_Level_1 == true)
                {
                    Game_Time.Interval = 500;
                    Change_Speed_Level_1 = false;
                }
                
                if(Score >= 5)
                {
                    Level++;
                    WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
                }
            }
            else if (Level == 2)
            {
                if (Change_Speed_Level_2 == true)
                {
                    Game_Time.Interval = 400;
                    Change_Speed_Level_2 = false;
                }
                if (Score >= 10)
                {
                    Level++;
                    WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
                }
            }
            else if (Level == 3)
            {
                if (Change_Speed_Level_3 == true)
                {
                    Game_Time.Interval = 300;
                    Change_Speed_Level_3 = false;
                }
                if (Score >= 15)
                {
                    Level++;
                    WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
                }
            }
            else if (Level == 4)
            {
                if (Change_Speed_Level_4 == true)
                {
                    Game_Time.Interval = 200;
                    Change_Speed_Level_4 = false;
                }
                if (Score >= 20)
                {
                    Level++;
                    WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
                }
            }
            else if (Level == 5)
            {
                if (Change_Speed_Level_5 == true)
                {
                    Game_Time.Interval = 100;
                    Change_Speed_Level_5 = false;
                }
                if (Score >= 25)
                {
                    Score = 25;
                    User.Text = Convert.ToString(Score);
                    Game_Time.Enabled = false;
                    Screen_Time.Enabled = false;
                    Game3 Video_Page = new Game3();
                    WMP1.URL = "Trolling Settings\\Bard_Troll_Voice.mp3";
                    if(MessageBox.Show("Bard'i atlatmak icin "+ Dakika +" dakika "+ Saniye +" saniye kadar zaman harcadiniz.\nBu sizi baya yormus olmali.\nAncak bir Bard'i yakalamak hic kolay olmadigi icin sizi tebrik ederim :)","Hurkan'dan Size Bir Tebrik Mesaji Var !",MessageBoxButtons.OK,MessageBoxIcon.Information)==DialogResult.OK)
                    {
                        WMP1.URL = "Trolling Settings\\Bard_Death.mp3";
                        if(MessageBox.Show("Dikkat et !\nBard ormanda minicin toplamaya gitti !\nBard geri dondugunde cok dikkatli oynamalisiniz !\nCunku bir sonraki karsilasmada ofkeli bir Bard ile karsilasacaksiniz !","Hurkan'dan Size Bir Tavsiye Mesaji Var !",MessageBoxButtons.OK,MessageBoxIcon.Information)==DialogResult.OK)
                        {
                            this.Hide();          
                            Video_Page.Show();  
                        }
                    }                              
                }
            }                
        }
        private void Level_Hack_1_Click(object sender, EventArgs e)
        {
            Level = 1;
            Score = 0;
            User.Text = Convert.ToString(Score);
            Change_Speed_Level_1 = true;
            Change_Speed_Level_2 = true;
            Change_Speed_Level_3 = true;
            Change_Speed_Level_4 = true;
            Change_Speed_Level_5 = true;
            WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
        }
        private void Level_Hack_2_Click(object sender, EventArgs e)
        {
            Level = 2;
            Score = 5;
            User.Text = Convert.ToString(Score);
            Change_Speed_Level_2 = true;
            Change_Speed_Level_3 = true;
            Change_Speed_Level_4 = true;
            Change_Speed_Level_5 = true;
            WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
        }
        private void Level_Hack_3_Click(object sender, EventArgs e)
        {
            Level = 3;
            Score = 10;
            User.Text = Convert.ToString(Score);
            Change_Speed_Level_3 = true;
            Change_Speed_Level_4 = true;
            Change_Speed_Level_5 = true;
            WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
        }
        private void Level_Hack_4_Click(object sender, EventArgs e)
        {
            Level = 4;
            Score = 15;
            User.Text = Convert.ToString(Score);
            Change_Speed_Level_4 = true;
            Change_Speed_Level_5 = true;
            WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
        }
        private void Level_Hack_5_Click(object sender, EventArgs e)
        {
            Level = 5;
            Score = 20;
            User.Text = Convert.ToString(Score);
            Change_Speed_Level_5 = true;
            WMP2.URL = "Trolling Settings\\Teemo_Laugh.mp3";
        }
        private void Speed_Hack_Crazy_Click(object sender, EventArgs e)
        {
            WMP2.URL = "Trolling Settings\\Bard_Troll_Voice.mp3";
            Game_Time.Interval = 1;
        }
        private void Speed_Hack_Sleepy_Click(object sender, EventArgs e)
        {
            WMP2.URL = "Trolling Settings\\Bard_Troll_Voice.mp3";
            Game_Time.Interval = 1000;
        }
        private void Speed_Hack_Normal_Click(object sender, EventArgs e)
        {
            WMP2.URL = "Trolling Settings\\Bard_Troll_Voice.mp3";
            if (Level == 1)
            {
                Change_Speed_Level_1 = true;
            }              
            else if (Level == 2)
            {
                Change_Speed_Level_2 = true;
            }                
            else if (Level == 3)
            {
                Change_Speed_Level_3 = true;
            }               
            else if (Level == 4)
            {
                Change_Speed_Level_4 = true;
            }               
            else if (Level == 5)
            {
                Change_Speed_Level_5 = true;
            }
                
        }
    }
}
