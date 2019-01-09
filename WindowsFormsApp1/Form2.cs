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
    public partial class Menu : Form
    {
        //直前までプレイしていたゲームを保持する変数
        Game endedGame;

        //Menuフォームのコンストラクタ
        public Menu()
        {
            InitializeComponent();
        }

        //Exitボタンが押されたら呼ばれるメソッド
        private void ExitButtonClicked(object sender, EventArgs e)
        {
            //アプリケーションを閉じる
            Application.Exit();
        }

        //Startボタンが押されたら呼ばれるメソッド
        private void StartButtonClicked(object sender, EventArgs e)
        {
            //直前までにプレイしていたゲームがあれば
            if (endedGame != null)
            {
                //そのフォームをクローズする
                this.endedGame.MyClose();
            }
            //新たにgameフォームのインスタンスを作成
            Game game = new Game(this);
            //新たなgameフォームを表示する
            game.Show();
            //このフォームを非表示にする
            this.Hide();
        }

        //直前までプレイしていたGameフォームを受け取り、
        //このフォームのendedGame変数に代入するメソッド
        public void SetEndedGame(Game endedGame)
        {
            this.endedGame = endedGame;
        }
    }
}
