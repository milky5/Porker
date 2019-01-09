using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static List<string> playerFileName = new List<string>();
        static List<string> cpuFileName = new List<string>();
        static List<PictureBox> playerCards = new List<PictureBox>();
        static List<int> playerSelected = new List<int>();

        public Form1()
        {
            InitializeComponent();

            StartGame();

        }

        private void StartGame()
        {
            playerCards.Add(playerCard0);
            playerCards.Add(playerCard1);
            playerCards.Add(playerCard2);
            playerCards.Add(playerCard3);
            playerCards.Add(playerCard4);

            (playerFileName, cpuFileName) = GameMaster.DealCards();
            DisplayCards();
        }

        private void playerCard0_Click(object sender, EventArgs e)
        {
            if (playerCard0.Top >= 275)
            {
                playerCard0.Top = 148;
            }
            else if (playerCard0.Top <= 148)
            {
                playerCard0.Top = 275;
            }
        }

        private void playerCard1_Click(object sender, EventArgs e)
        {
            if (playerCard1.Top >= 275)
            {
                playerCard1.Top = 148;
            }
            else if (playerCard1.Top <= 148)
            {
                playerCard1.Top = 275;
            }
        }

        private void playerCard2_Click(object sender, EventArgs e)
        {
            if (playerCard2.Top >= 275)
            {
                playerCard2.Top = 148;
            }
            else if (playerCard2.Top <= 148)
            {
                playerCard2.Top = 275;
            }
        }

        private void playerCard3_Click(object sender, EventArgs e)
        {
            if (playerCard3.Top >= 275)
            {
                playerCard3.Top = 148;
            }
            else if (playerCard3.Top <= 148)
            {
                playerCard3.Top = 275;
            }
        }

        private void playerCard4_Click(object sender, EventArgs e)
        {
            if (playerCard4.Top >= 275)
            {
                playerCard4.Top = 148;
            }
            else if (playerCard4.Top <= 148)
            {
                playerCard4.Top = 275;
            }
        }

        private void exchangeButton_Click(object sender, EventArgs e)
        {
            if (playerCard0.Top <= 148)
            {
                playerCard0.Top = 23;
                playerCard0.Left = 54;
                playerSelected.Add(0);
            }
            if (playerCard1.Top <= 148)
            {
                playerCard1.Top = 23;
                playerCard1.Left = 54;
                playerSelected.Add(1);
            }
            if (playerCard2.Top <= 148)
            {
                playerCard2.Top = 23;
                playerCard2.Left = 54;
                playerSelected.Add(2);
            }
            if (playerCard3.Top <= 148)
            {
                playerCard3.Top = 23;
                playerCard3.Left = 54;
                playerSelected.Add(3);
            }
            if (playerCard4.Top <= 148)
            {
                playerCard4.Top = 23;
                playerCard4.Left = 54;
                playerSelected.Add(4);
            }

            timer1.Start();
        }

        //1000mscに一度呼ばれるメソッド
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int i = 0;
            i++;

            if (i >= 1)
            {
                timer1.Stop();

                ReDealCard();
            }
           
        }

        private void ReDealCard()
        {
            playerFileName.Clear();
            playerFileName = GameMaster.ReDealCards(playerSelected);

            if (playerCard0.Top <= 23)
            {
                playerCard0.Top = 275;
                playerCard0.Left = 30;
            }
            if (playerCard1.Top <= 23)
            {
                playerCard1.Top = 275;
                playerCard1.Left = 110;
            }
            if (playerCard2.Top <= 23)
            {
                playerCard2.Top = 275;
                playerCard2.Left = 190;
            }
            if (playerCard3.Top <= 23)
            {
                playerCard3.Top = 275;
                playerCard3.Left = 270;
            }
            if (playerCard4.Top <= 23)
            {
                playerCard4.Top = 275;
                playerCard4.Left = 350;
            }

            DisplayCards();
            string result = GameMaster.Judge();
            playerRankText.Text = result;
        }

        private void DisplayCards()
        {
            for (int i = 0; i < playerFileName.Count; i++)
            {
                string temp = playerFileName[i];
                playerCards[i].ImageLocation = $@"..\..\Resources\CardPicture\{temp}.png";
            }
        }

    }
}
