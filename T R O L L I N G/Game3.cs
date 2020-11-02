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
    public partial class Game3 : Form
    {
        public Game3()
        {
            InitializeComponent();
        }
       
        private Timer Game3_Bard_Timer = new Timer();
        private void Game3_Load(object sender, EventArgs e)
        {
            WMP1.settings.volume = 100;
            WMP2.settings.volume = 100;
            this.Icon = Icon.ExtractAssociatedIcon("Trolling Settings\\Bard_Icon.ico");
            this.ShowIcon = true;
            Game3_Bard_Timer.Enabled = false; //---> Form1 ile ayni anda yuklendiginde direkt timerin aktiflesmesini engelledim
            this.Show();

            WMP2.URL = "Trolling Settings\\Bard_Death.mp3";       
            if (MessageBox.Show("Olamaz !\nBard ormandan dusundugumuzden daha erken dondu !\nHepimiz Bard'in ofkesini hissedecegiz !", "Hurkan'dan Uyari Mesaji Var !", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                WMP1.URL = "Trolling Settings\\Bard_Journey.mp4";
                Game3_Bard_Timer.Interval = 1000;
                Game3_Bard_Timer.Enabled = true;
            }
                             
            Game3_Bard_Timer.Tick += new EventHandler(Game3_Bard_Exit);        
        }

        //---> Counter 20 olunca uygulama kapanacaktir
        private static int counter = 0;
        private void Game3_Bard_Exit(object sender, EventArgs e)
        {          
            if (counter == 20)
            {
                Game3_Bard_Timer.Enabled = false;
                WMP2.URL = "Trolling Settings\\Bard_Death.mp3";
                if(MessageBox.Show("Bard tarafindan olduruldunuz !","Hurkan'dan Uzucu Bir Mesaj Var !",MessageBoxButtons.OK,MessageBoxIcon.Information)==DialogResult.OK)
                {
                    Application.Exit();
                }  
            }

            counter++;
        }
    }
}
