using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class GameMaster
    {
        #region クラス内で使用する変数の定義
        //マーク4種が入る配列を宣言
        public string[] marks = new string[] { "Spade", "Diamond", "Heart", "Clover" };
        //Card型インスタンスが入るListを宣言
        public List<Card> cards = new List<Card>(52);
        //シャッフル後のCardを保持するリストを宣言
        public List<Card> shuffledCards = new List<Card>(52);
        //シャッフル後のCardをどこまで配ったかを保持する変数を宣言
        public int nowIndex = 0;
        //プレイヤーに配られたCardを保持するリストを宣言
        public List<Card> playerCards = new List<Card>();
        //コンピュータに配られたCardを保持するリストを宣言
        public List<Card> cpuCards = new List<Card>();
        #endregion

        //シャッフル済カードを作るメソッド
        public void ShuffleCards()
        {
            for (int i = 0; i < marks.Length; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    //4種のうち1種のマーク、1～13のうち1つの数字を順番に渡し、インスタンスを作成
                    //作成したインスタンスを、List<Card>型リストに追加する
                    cards.Add(new Card(marks[i], j));
                }
            }
            //cardsをシャッフルし、shuffleCardsに追加する
            shuffledCards = cards.OrderBy(i => Guid.NewGuid()).ToList();
        }

        //シャッフル済カードをプレイヤーとコンピュータに配るメソッド
        public (List<string>, List<string>) DealCards()
        {
            ShuffleCards();

            //プレイヤーとコンピュータにそれぞれ5枚、計10枚配る
            for (int i = 0; i < 10; i++)
            {
                //もしiが奇数なら、プレイヤーに配る
                if (i % 2 == 1)
                {
                    playerCards.Add(shuffledCards[nowIndex]);
                }
                //もしiが0か偶数なら、コンピュータに配る
                else
                {
                    cpuCards.Add(shuffledCards[nowIndex]);
                }
                //現在の行を一つ更新する
                nowIndex++;
            }
            var playerFileName = new List<string>();
            var cpuCardsDate = new List<string>();
            //プレイヤーの持ち札をstring型にする
            foreach (var p in playerCards)
            {
                playerFileName.Add(p.mark + p.number.ToString());
            }
            //コンピュータの持ち札をstring型にする
            foreach (var c in cpuCards)
            {
                cpuCardsDate.Add(c.mark + c.number.ToString());
            }
            //それぞれに配られたカードを、Mainメソッドに戻り値として返す
            return (playerFileName, cpuCardsDate);
        }

        //選択されたカードの枚数だけ再配布するメソッド
        public List<string> ReDealCards(List<int> selected)
        {
            //逆順にソートする
            //(Listの後ろの要素から消さないと狂うため)
            selected.Sort();
            selected.Reverse();
            //交換するカードのインデックスを指定して削除
            for (int i = 0; i < selected.Count; i++)
            {
                playerCards.RemoveAt(selected[i]);
            }
            //選ばれた枚数だけプレイヤーに配布
            for (int i = 0; i < selected.Count; i++)
            {
                //シャッフル済カードの現在のインデックスの要素をプレイヤーの持ち札に追加
                playerCards.Add(shuffledCards[nowIndex]);
                //現在のインデックスを更新
                nowIndex++;
            }
            //再配布された後の持ち札をstring型にする
            var playerCardsDate = new List<string>();
            foreach (var p in playerCards)
            {
                playerCardsDate.Add(p.mark + p.number.ToString());
            }
            return playerCardsDate;
        }

        //役をジャッジするメソッド
        public string Judge()
        {
            #region 詳細ルール
            //同じカードでも数字の高い方が基本的に強いカードです
            //マークはスペード＞ダイヤ＞ハート＞クラブと強さちがいます。
            //⑨ロイヤル・ストレート・フラッシュ １０・Ｊ・Ｑ・Ｋ・Ａの全て同じマーク
            //⑧ストレート・フラッシュ 例：４，５，６，７，８などの並んでいる数字でマークが全て同じ
            //⑦フォーカード 例：７、７，７，７、１０などのマークばらばら
            //⑥フルハウス ５枚のカードの内３枚が同じ数字２枚が同じ数字 例：４，４，４、８，８、などのマークばらばら
            //⑤フラッシュ ５枚全てのマークが同じ
            //④ストレート 例：６，７，８，９、１０などのカードばらばら ＫからＡにもつなげることが可能
            //③スリー・カード 例：Ａ、Ａ、Ａ、５、７のマークばらばら
            //②ツウ・ペア 例：６，６、９，９、３のマークばらばら
            //①ワン・ペア 例；４、４、６、８、２のマークばらばら
            #endregion
            int cnResult = 5;
            bool smResult = false;
            string smTrueMark = null;

            //⑨ロイヤルストレートフラッシュを判定する
            cnResult = JudgeContinueNumber();
            if (cnResult == 2)
            {
                (smResult, smTrueMark) = JudgeSameMark();
                if (smResult)
                {
                    return "ロイヤルストレートフラッシュです";
                }
            }

            //⑧ストレートフラッシュを判定する
            //変数を初期化
            cnResult = 5;
            smResult = false;
            smTrueMark = null;

            cnResult = JudgeContinueNumber();
            if (cnResult == 1)
            {
                (smResult, smTrueMark) = JudgeSameMark();
                if (smResult)
                {
                    return "ストレートフラッシュです";
                }
            }

            //⑦フォーカードを判定する
            //手札の数字だけを抜き出し、配列に入れる
            var temp = new List<int>();
            for (int i = 0; i < playerCards.Count; i++)
            {
                temp.Add(playerCards[i].number);
            }
            for (int i = 0; i < 13; i++)
            {
                if (temp.Where(n => n == i).Count() == 4)
                {
                    return "フォーカードです";
                }
            }

            //⑥フルハウスを判定する
            for (int i = 1; i <= 13; i++)
            {
                if (temp.Where(n => n == i).Count() == 3)
                {
                    for (int j = 1; j <= 13; j++)
                    {
                        if (temp.Where(n => n == j).Count() == 2)
                        {
                            return "フルハウスです";
                        }
                    }
                }
            }

            //⑤フラッシュを判定する
            //変数を初期化
            smResult = false;
            smTrueMark = null;

            (smResult, smTrueMark) = JudgeSameMark();
            if (smResult)
            {
                return "フラッシュです";
            }

            //④ストレートを判定する
            //変数を初期化
            cnResult = 5;
            cnResult = JudgeContinueNumber();
            if (cnResult == 1 || cnResult == 2)
            {
                return "ストレートです";
            }

            //③スリーカードを判定する
            for (int i = 0; i < 13; i++)
            {
                if (temp.Where(n => n == i).Count() == 3)
                {
                    return "スリーカードです";
                }
            }

            //②ツーペアを判定する
            var temp1 = new List<int>();
            for (int i = 0; i < playerCards.Count; i++)
            {
                temp1.Add(playerCards[i].number);
            }
            temp1.Sort();
            for (int i = 1; i <= 13; i++)
            {
                if (temp1.Where(n => n == i).Count() == 2)
                {
                    for (i++; i <= 13; i++)
                    {
                        if (temp1.Where(n => n == i).Count() == 2)
                        {
                            return "ツーペアです";
                        }
                    }
                }
            }

            //①ワンペアを判定する
            for (int i = 1; i <= 13; i++)
            {
                if (temp.Where(n => n == i).Count() == 2)
                {
                    return "ワンペアです";
                }
            }

            //役がない場合
            return "役はありません";
        }

        //5枚すべてが同じマークかどうか判定するメソッド
        public (bool, string) JudgeSameMark()
        {
            for (int i = 0; i < marks.Length; i++)
            {
                int count = 0;
                foreach (var p in playerCards)
                {
                    //マークiを含むならばカウントを1増やす
                    if (p.mark.Contains($"{marks[i]}"))
                    {
                        count++;
                    }
                }
                //マークiを5枚判定した後で、5枚すべてがマークiを含むのならば全て同じマーク
                if (count == 5)
                {
                    //すべて同じマークであることと、どのマークなのかを返す
                    return (true, marks[i]);
                }
            }

            //すべて同じマークではないことと、nullを返す
            return (false, null);
        }

        //5枚の数字が全て連続しているか判定するメソッド
        public int JudgeContinueNumber()
        {
            //手札の数字だけを抜き出し、配列に入れる
            var temp = new List<int>();
            for (int i = 0; i < playerCards.Count; i++)
            {
                temp.Add(playerCards[i].number);
            }

            //連番判定のために配列をソートする
            temp.Sort();

            //次のインデックスの中身、インデックスの中身に1を足したものを比較し、
            //同じならcountに1を足す
            int count = 0;
            for (int i = 0; i < temp.Count - 1; i++)
            {
                if (temp[i + 1] == temp[i] + 1)
                {
                    count++;
                }
            }
            //全ての要素を判定した後、比較したすべてが一致していれば連番
            if (count == 4)
            {
                //数字が連続しているだけをパターン1とみなし1を返す
                return 1;
            }
            // 1,10,?,?,?でcountが3なら1,10,11,12,13ということになる
            else if (count == 3 && temp[0] == 1 && temp[1] == 10)
            {
                //ロイヤルストレートフラッシュの可能性があるものをパターン2とみなし2を返す
                return 2;
            }
            //(1,2,3,4,13)(1,2,3,12,13)(1,2,11,12,13)の場合もストレート
            else if (count == 3 && temp[0] == 1 && temp[4] == 13)
            {
                //数字が連続しているだけをパターン1とみなし1を返す
                return 1;
            }
            else
            {
                //0をfalseと同義とみなし、0を返す
                return 0;
            }
        }
    }
}
