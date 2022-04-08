using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum rot
{
    top,
    right,
    down,
    left
}

namespace tetrixd
{
    class Shapes
    {
        /// <summary>
        /// 
        /// </summary>
        public int matlen;
        /// <summary>
        /// 
        /// </summary>
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
        public int typeshape_x;
        public int typeshape_y;
        public char typeshape_c;

        public int shapelength;
        public int shapeheight;

        public Shapes(int x, int y)
        {
            Random rnd = new Random();

            //какая следующая фигура
            int value = rnd.Next(7);
            //int value = 5;
            j = 0;
            this.x = x;
            this.y = y;

            switch (value)
            {
                case 0:
                    //J
                    color = Brushes.Blue;
                    matrix = new int[3, 3]
                    {
                        { 0, 1, 0},
                        { 0, 1, 0},
                        { 1, 1, 0}
                    };
                    shapelength = 2;
                    shapeheight = 3;
                    typeshape_c = 'j';
                    break;
                case 1:
                    //I
                    color = Brushes.Aqua;
                    matrix = new int[4, 4]
                    {
                           { 1, 0, 0, 0},
                           { 1, 0, 0, 0},
                           { 1, 0, 0, 0},
                           { 1, 0, 0, 0}
                    };
                    shapelength = 1;
                    shapeheight = 4;
                    typeshape_c = 'i';
                    break;
                case 2:
                    color = Brushes.Yellow;
                    //O
                    matrix = new int[2, 2]
                    {
                        { 1, 1},
                        { 1, 1}
                    };
                    typeshape_c = 'o';

                    shapelength = 2;
                    shapeheight = 2;

                    break;
                case 3:
                    //L
                    color = Brushes.Orange;
                    matrix = new int[3, 3]
                    {
                        { 1, 0, 0 },
                        { 1, 0, 0 },
                        { 1, 1, 0 }
                    };
                    typeshape_c = 'l';

                    shapelength = 2;
                    shapeheight = 3;
                    break;
                case 4:
                    //z
                    color = Brushes.Red;
                    matrix = new int[3, 3]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 0 }
                    };
                    typeshape_c = 'z';

                    shapelength = 3;
                    shapeheight = 2;
                    break;
                case 5:
                    //t
                    color = Brushes.Purple;
                    matrix = new int[3, 3]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    };
                    typeshape_c = 't';

                    shapelength = 3;
                    shapeheight = 2;
                    break;
                case 6:
                    color = Brushes.Green;
                    //s
                    matrix = new int[3, 3]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 },
                        { 0, 0, 0 }
                    };
                    typeshape_c = 's';

                    shapelength = 3;
                    shapeheight = 2;
                    break;
            }
            matlen = matrix.Length;
            typeshape_x = 10 - shapelength;
            typeshape_y = 20 - shapeheight;
        }
        public void Move()
        {
            y++;
        }
        public void MoveRight()
        {
            x++;
        }
        public void MoveLeft()
        {
            x--;
        }
        rot j;
        public void Rotate()
        {
            j = (j < rot.left) ? j + 1 : 0;//цикличное перечисление

            switch (typeshape_c)
            {
                case 'j':
                    switch (j)
                    {
                        case rot.top:
                            shapelength = 2;
                            shapeheight = 3;
                            typeshape_y = 17;
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 0},
                                { 0, 1, 0},
                                { 1, 1, 0}
                            };
                            break;
                        case rot.right:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            typeshape_y = 18;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 0},
                                { 1, 1, 1},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.down:
                            shapelength = 2;
                            shapeheight = 3;
                            typeshape_y = 17;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 0},
                                { 1, 0, 0},
                                { 1, 0, 0}
                            };
                            break;
                        case rot.left:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            typeshape_y = 18;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 1},
                                { 0, 0, 1},
                                { 0, 0, 0}
                            };
                            break;
                    }
                    break;
                case 'i':
                    switch (j)
                    {
                        case rot.top:
                            shapelength = 1;
                            shapeheight = 4;
                            matrix = new int[4, 4]
                            {
                                   { 1, 0, 0, 0},
                                   { 1, 0, 0, 0},
                                   { 1, 0, 0, 0},
                                   { 1, 0, 0, 0}
                            };
                            break;
                        case rot.right:
                            shapelength = 4;
                            shapeheight = 1;
                            if (x == 9) x -= 3;
                            matrix = new int[4, 4]
                            {
                                   { 1, 1, 1, 1},
                                   { 0, 0, 0, 0},
                                   { 0, 0, 0, 0},
                                   { 0, 0, 0, 0}
                            };
                            break;
                        case rot.down:
                            shapelength = 1;
                            shapeheight = 4;
                            matrix = new int[4, 4]
                            {
                                   { 1, 0, 0, 0},
                                   { 1, 0, 0, 0},
                                   { 1, 0, 0, 0},
                                   { 1, 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 4;
                            shapeheight = 1;
                            matrix = new int[4, 4]
                            {
                                   { 1, 1, 1, 1},
                                   { 0, 0, 0, 0},
                                   { 0, 0, 0, 0},
                                   { 0, 0, 0, 0}
                            };
                            break;
                    }
                    break;
                case 'o':
                    //не требуется
                    break;
                case 'l':
                    switch (j)
                    {
                        case rot.top:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 0 },
                                { 1, 0, 0 },
                                { 1, 1, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 1},
                                { 1, 0, 0},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.down:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 0},
                                { 0, 1, 0},
                                { 0, 1, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 0, 0, 1},
                                { 1, 1, 1},
                                { 0, 0, 0}
                            };
                            break;
                    }
                    break;
                case 'z':
                    switch (j)
                    {
                        case rot.top:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 2;
                            shapeheight = 3;
                            
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 1, 0, 0 }
                            };
                            break;
                        case rot.down:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 0},
                                { 0, 1, 1},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 0},
                                { 1, 1, 0},
                                { 1, 0, 0}
                            };
                            break;
                    }
                    break;
                case 't':
                    switch (j)
                    {
                        case rot.top:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 2;
                            shapeheight = 3;
                            //if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 0 },
                                { 1, 0, 0 }
                            };
                            break;
                        case rot.down:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 1, 1, 1},
                                { 0, 1, 0},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 0},
                                { 1, 1, 0},
                                { 0, 1, 0}
                            };
                            break;
                    }
                    break;
                case 's':
                    switch (j)
                    {
                        case rot.top:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 2;
                            shapeheight = 3;
                            //if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 }
                            };
                            break;
                        case rot.down:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 0, 1, 1},
                                { 1, 1, 0},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 0},
                                { 1, 1, 0},
                                { 0, 1, 0}
                            };
                            break;
                    }
                    break;
            }
            
            typeshape_x = 10 - shapelength;
            typeshape_y = 20 - shapeheight;
        }
    }
}