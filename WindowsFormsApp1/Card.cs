using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    //クラスは参照型、構造体は値型で、
    //構造体のほうがメモリへの負荷がかかるため、初学者はクラスで十分
    class Card
    {
        //カードのマークを代入する変数
        public string mark;
        //カードの数字を代入する変数
        public int number;

        //コンストラクタ
        public Card(string mark, int number)
        {
            this.mark = mark;
            this.number = number;
        }

    }
}
