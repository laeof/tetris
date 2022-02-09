using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrixd
{
    class Shapes
    {
        public Brush color;
        /// <summary>
        /// 
        /// </summary>
        public int x, y; 
        /// <summary>
        /// 
        /// </summary>
        public int[,] matrix;
        /// <summary>
        /// 
        /// </summary>
        public Shapes(int x, int y)
        {
            Random rnd = new Random();
            int value = rnd.Next(7);
            this.x = x;
            this.y = y;

            switch (value)
            {
                case 0:
                    //J
                    color = Brushes.Blue;
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 1},
                        { 0, 0, 1},
                        { 0, 1, 1},
                        { 0, 0, 0}
                    };
                    break;
                case 1:
                    //I
                    color = Brushes.Aqua;
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 1},
                        { 0, 0, 1},
                        { 0, 0, 1},
                        { 0, 0, 1}
                    };
                    break;
                case 2:
                    color = Brushes.Yellow;
                    //O
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 0},
                        { 0, 1, 1},
                        { 0, 1, 1},
                        { 0, 0, 0}
                    };
                    break;
                case 3:
                    //L
                    color = Brushes.Orange;
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 1},
                        { 0, 0, 1},
                        { 0, 1, 1},
                        { 0, 0, 0}
                    };
                    break;
                case 4:
                    //z
                    color = Brushes.Red;
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 0},
                        { 1, 1, 0},
                        { 0, 1, 1},
                        { 0, 0, 0}
                    };
                    break;
                case 5:
                    //t
                    color = Brushes.Purple;
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 0},
                        { 0, 1, 0},
                        { 1, 1, 1},
                        { 0, 0, 0}
                    };
                    break;
                case 6:
                    color = Brushes.Green;
                    //s
                    matrix = new int[4, 3]
                    {
                        { 0, 0, 0},
                        { 0, 1, 1},
                        { 1, 1, 0},
                        { 0, 0, 0}
                    };
                    break;
            }
        }
        public void Move()
        {
            if (y < 17)
            y++;

        }
        public void MoveRight()
        {
            if(x < 7)
            x++;
        }
        public void MoveLeft()
        {
            if (x > 0)
            x--;
        }
    }
}
