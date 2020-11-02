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
    public partial class Game1 : Form
    {
        public Game1()
        {
            InitializeComponent();
        }

        private Timer Game1_Bard_Timer = new Timer();
        private void Game1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            WMP1.settings.volume = 100;
            WMP2.settings.volume = 100;
            WMP1.URL = "Trolling Settings\\Bard_Woods.mp4";

            Game1_Bard_Timer.Interval = 1000;
            Game1_Bard_Timer.Enabled = true;
            Game1_Bard_Timer.Tick += new EventHandler(Game1_Bard_Exit);
        }

        private static int counter = 0;
        private void Game1_Bard_Exit(object sender, EventArgs e)
        {
            //---> Counter 21 olunca ekrana mesaj yazdiracaktir
            if (counter == 21)
            {
                Game1_Bard_Timer.Enabled = false;
                WMP2.URL = "Trolling Settings\\Bard_Troll_Voice.mp3";
                if (MessageBox.Show("Bard iyice kontrolden cikti !\nArtik birisinin onu durdurmasi lazim !\nGoruyorum ki sen onun karsisina cikabilecek kadar cesursun :)\nSimdiden kolay gelsin ve kendine dikkat et !", "Hurkan'dan Bir Istek Mesaji Var !", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {                 
                    Game2 Game_Starts = new Game2();
                    this.Hide();
                    Game_Starts.Show();
                }               
            }

            counter++;
        }
    }
}
