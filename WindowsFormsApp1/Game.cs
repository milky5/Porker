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
    public partial class Game : Form
    {
        List<string> playerFileName = new List<string>();
        List<string> cpuFileName = new List<string>();
        List<PictureBox> playerCards = new List<PictureBox>();
        List<int> playerSelected = new List<int>();
        Menu menu;
        bool wantClose = false;
        GameMaster gm;

        //コンストラクタ
        public Game()
        {
            InitializeComponent();

            StartGame();
        }

        //引数付きコンストラクタ
        public Game(Menu menu)
        {
            InitializeComponent();

            //渡されたmenuをこのフォームのmenuに代入
            this.menu = menu;

            //ゲームスタートのメソッドを呼び出す
            StartGame();
        }

        //ゲームスタート時に呼び出されるメソッド
        private void StartGame()
        {

            //List<PictureBox>にPictureBox(playerCard0～4)の要素を代入
            playerCards.Add(playerCard0);
            playerCards.Add(playerCard1);
            playerCards.Add(playerCard2);
            playerCards.Add(playerCard3);
            playerCards.Add(playerCard4);

            //GameMasterクラスのインスタンスを作成する
            gm = new GameMaster();
            //gmのDealCardメソッドを呼び出し、表示する画像のファイル名をもらう
            (playerFileName, cpuFileName) = gm.DealCards();
            //プレイヤーの持ち札を表示させるメソッドを呼び出す
            DisplayCards();
        }

        //PictureBoxがクリックされたら呼び出されるメソッド
        private void playerCard0_Click(object sender, EventArgs e)
        {
            //手札ゾーンにあれば
            if (playerCard0.Top >= 275)
            {
                //交換ゾーンに移動
                playerCard0.Top = 148;
            }
            //交換ゾーンにあれば
            else if (playerCard0.Top <= 148)
            {
                //手札ゾーンに移動
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

        //交換ボタンが押されたときに呼び出されるメソッド
        private void exchangeButton_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < playerCards.Count; i++)
            {
                //もし交換ゾーンにあれば
                if (playerCards[i].Top <= 148)
                {
                    //山札の位置に移動させる
                    playerCards[i].Top = 23;
                    playerCards[i].Left = 54;
                    //交換する手札のインデックスを、List<int>に代入する
                    playerSelected.Add(i);
                }
            }
            //カードを再配布する
            ReDealCard();
        }

        private void ReDealCard()
        {
            //プレイヤーが選んだカードをメソッドに渡し、
            //帰ってきたものを持ち札ファイル名のためのリストに代入
            playerFileName = gm.ReDealCards(playerSelected);

            //手札を表示するメソッドを呼び出す
            DisplayCards();

            //PictureBoxが山札にあれば、
            if (playerCard0.Top <= 23)
            {
                //所定の位置に移動
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

            //役をジャッジするメソッドを呼び出し、役名をresultに代入
            string result = gm.Judge();
            //役名をテキストボックスに表示させる
            playerRankText.Text = result;
            //プレイ後となったこのフォームを、Menuフォームに渡す
            menu.SetEndedGame(this);

            endGameButton.Visible = true;
        }

        //ファイル名を基に、プレイヤーの持ち札を表示するメソッド
        private void DisplayCards()
        {
            for (int i = 0; i < playerFileName.Count; i++)
            {
                //ファイル名をstring型tempに代入する
                string temp = playerFileName[i];
                //pictureBoxに表示させる
                playerCards[i].ImageLocation = $@"..\..\Resources\CardPicture\{temp}.png";
            }
        }

        //直前までプレイしていたGameフォームを閉じたいときに呼ばれるメソッド
        public void MyClose(bool wantClose)
        {
            this.wantClose = wantClose;
            this.Close();
        }

        //フォームが閉じられる時に呼び出されるメソッド
        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && wantClose)
            {
                //クローズ
                e.Cancel = false;
            }
            //プレイヤーがフォームを閉じようとしている
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                //クローズをキャンセル
                e.Cancel = true;
                //アプリケーションを終了
                Application.Exit();
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            menu.Show();
        }
    }
}
