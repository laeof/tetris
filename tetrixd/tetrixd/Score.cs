namespace tetrixd
{
    public class ScoreM
    {
        /// <summary>
        /// очки
        /// </summary>
        private int _score = 30;
        /// <summary>
        /// скорость
        /// </summary>
        private int _speed;
        /// <summary>
        /// очки за линию
        /// </summary>
        private int _score_row = 100;
        //readonly
        //свойства
        public int Score_Row => _score_row;
        public int Speed => _speed;
        public int Score => _score;
        /// <summary>
        /// конструктор
        /// </summary>
        public ScoreM()
        {
            _speed = 1;
        }
        /// <summary>
        /// увеличение сложности
        /// </summary>
        public void Score_Calc()
        {
            _speed++;
            _score_row *= 2;
            _score *= 2;
        }
    }
}
