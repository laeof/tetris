using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrixd
{
    public class ScoreM
    {
        //очки
        private int _score = 30;
        //скорость
        private int _speed;
        private int _score_row = 100;
        //readonly
        public int Score_Row => _score_row;
        public int Speed => _speed;
        public int Score => _score;
        public ScoreM()
        {
            _speed = 1;
        }
        public void Score_Calc()
        {
            _speed++;
            _score_row *= 2;
            _score *= 2;
        }
    }
}
