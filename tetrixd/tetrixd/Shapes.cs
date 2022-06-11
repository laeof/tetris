using System;

/// <summary>
/// перечисление для вращения
/// </summary>
enum rot
{
    top,
    right,
    down,
    left
}


namespace tetrixd
{
    public class Shapes
    {
        /// <summary>
        /// длина матрицы
        /// </summary>
        public int matlen;
        /// <summary>
        /// расположение матрицы
        /// </summary>
        public int x, y;
        /// <summary>
        /// матрица
        /// </summary>
        public int[,] matrix;
        /// <summary>
        /// массив для отслеживания коллизий
        /// </summary>
        public int[] shapemass;
        /// <summary>
        /// вспомогательные переменные
        /// </summary>
        public int typeshape_x;
        public int typeshape_y;
        public char typeshape_c;

        /// <summary>
        /// длина и ширина фигуры
        /// </summary>
        public int shapelength;
        public int shapeheight;
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shapes(int x, int y, Random rnd, int lvl)
        {
            //какая следующая фигура
            int value = lvl > 3 ? rnd.Next(9) : rnd.Next(7);

            //debug value
            //int value = 8;

            j = 0;

            this.x = x;
            this.y = y;

            switch (value)
            {
                case 0:
                    //J
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
                    matrix = new int[4, 4]
                    {
                           { 2, 0, 0, 0},
                           { 2, 0, 0, 0},
                           { 2, 0, 0, 0},
                           { 2, 0, 0, 0}
                    };
                    shapelength = 1;
                    shapeheight = 4;
                    typeshape_c = 'i';
                    break;
                case 2:
                    //O
                    matrix = new int[2, 2]
                    {
                        { 3, 3 },
                        { 3, 3}
                    };
                    typeshape_c = 'o';

                    shapelength = 2;
                    shapeheight = 2;

                    break;
                case 3:
                    //L
                    matrix = new int[3, 3]
                    {
                        { 4, 0, 0 },
                        { 4, 0, 0 },
                        { 4, 4, 0 }
                    };
                    typeshape_c = 'l';

                    shapelength = 2;
                    shapeheight = 3;
                    break;
                case 4:
                    //z
                    matrix = new int[3, 3]
                    {
                        { 5, 5, 0 },
                        { 0, 5, 5 },
                        { 0, 0, 0 }
                    };
                    typeshape_c = 'z';

                    shapelength = 3;
                    shapeheight = 2;
                    break;
                case 5:
                    //t
                    matrix = new int[3, 3]
                    {
                        { 0, 6, 0 },
                        { 6, 6, 6 },
                        { 0, 0, 0 }
                    };
                    typeshape_c = 't';

                    shapelength = 3;
                    shapeheight = 2;
                    break;
                case 6:
                    //s
                    matrix = new int[3, 3]
                    {
                        { 0, 7, 7 },
                        { 7, 7, 0 },
                        { 0, 0, 0 }
                    };
                    typeshape_c = 's';
                    shapelength = 3;
                    shapeheight = 2;
                    break;
                case 7:
                    //c
                    matrix = new int[3, 3]
                    {
                        { 1, 3, 0 },
                        { 7, 0, 0 },
                        { 6, 5, 0 }
                    };
                    shapelength = 2;
                    shapeheight = 3;
                    typeshape_c = 'c';
                    break;
                case 8:
                    //H
                    matrix = new int[3, 3]
                    {
                        { 1, 0, 3 },
                        { 7, 2, 5 },
                        { 6, 0, 4}
                    };
                    shapelength = 3;
                    shapeheight = 3;
                    typeshape_c = 'h';
                    break;
            }
            
            s();
        }
        /// <summary>
        /// движение вниз
        /// </summary>
        public void Move()
        {
            y++;
        }
        /// <summary>
        /// движение вправо
        /// </summary>
        public void MoveRight()
        {
            x++;
        }
        /// <summary>
        /// движение влево
        /// </summary>
        public void MoveLeft()
        {
            x--;
        }
        /// <summary>
        /// экземпляр перечисления
        /// </summary>
        rot j;
        /// <summary>
        /// вращение фигуры
        /// </summary>
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

                            if (y > 16) break;
                            shapelength = 1;
                            shapeheight = 4;
                            matrix = new int[4, 4]
                            {
                                   { 2, 0, 0, 0},
                                   { 2, 0, 0, 0},
                                   { 2, 0, 0, 0},
                                   { 2, 0, 0, 0}
                            };
                            break;
                        case rot.right:
                            shapelength = 4;
                            shapeheight = 1;
                            if (x == 9) x -= 3;
                            else if (x == 8) x -= 2;
                            else if (x == 7) x -= 1;
                            matrix = new int[4, 4]
                            {
                                   { 2, 2, 2, 2},
                                   { 0, 0, 0, 0},
                                   { 0, 0, 0, 0},
                                   { 0, 0, 0, 0}
                            };
                            break;
                        case rot.down:

                            if (y > 16) break;
                            shapelength = 1;
                            shapeheight = 4;
                            matrix = new int[4, 4]
                            {
                                   { 2, 0, 0, 0},
                                   { 2, 0, 0, 0},
                                   { 2, 0, 0, 0},
                                   { 2, 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 4;
                            shapeheight = 1;
                            if (x == 9) x -= 3;
                            else if (x == 8) x -= 2;
                            else if (x == 7) x -= 1;
                            matrix = new int[4, 4]
                            {
                                   { 2, 2, 2, 2},
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
                                { 4, 0, 0 },
                                { 4, 0, 0 },
                                { 4, 4, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 4, 4, 4},
                                { 4, 0, 0},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.down:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 4, 4, 0},
                                { 0, 4, 0},
                                { 0, 4, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 0, 0, 4},
                                { 4, 4, 4},
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
                                { 5, 5, 0 },
                                { 0, 5, 5 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 2;
                            shapeheight = 3;

                            matrix = new int[3, 3]
                            {
                                { 0, 5, 0 },
                                { 5, 5, 0 },
                                { 5, 0, 0 }
                            };
                            break;
                        case rot.down:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 5, 5, 0},
                                { 0, 5, 5},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 0, 5, 0},
                                { 5, 5, 0},
                                { 5, 0, 0}
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
                                { 0, 6, 0 },
                                { 6, 6, 6 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 2;
                            shapeheight = 3;
                            //if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 6, 0, 0 },
                                { 6, 6, 0 },
                                { 6, 0, 0 }
                            };
                            break;
                        case rot.down:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 6, 6, 6},
                                { 0, 6, 0},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 0, 6, 0},
                                { 6, 6, 0},
                                { 0, 6, 0}
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
                                { 0, 7, 7 },
                                { 7, 7, 0 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 2;
                            shapeheight = 3;
                            //if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 7, 0, 0 },
                                { 7, 7, 0 },
                                { 0, 7, 0 }
                            };
                            break;
                        case rot.down:
                            if (x == 8) x--;
                            shapelength = 3;
                            shapeheight = 2;
                            matrix = new int[3, 3]
                            {
                                { 0, 7, 7},
                                { 7, 7, 0},
                                { 0, 0, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 7, 0, 0},
                                { 7, 7, 0},
                                { 0, 7, 0}
                            };
                            break;
                    }
                    break;
                case 'c':
                     
                    switch (j)
                    {
                        case rot.top:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 1, 3, 0 },
                                { 7, 0, 0 },
                                { 6, 5, 0 }
                            };
                            break;
                        case rot.right:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 6, 7, 1 },
                                { 5, 0, 3 },
                                { 0, 0, 0 }
                            };
                            break;
                        case rot.down:
                            shapelength = 2;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 5, 6, 0},
                                { 0, 7, 0},
                                { 3, 1, 0}
                            };
                            break;
                        case rot.left:
                            shapelength = 3;
                            shapeheight = 2;
                            if (x == 8) x--;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 5},
                                { 3, 7, 6},
                                { 0, 0, 0}
                            };
                            break;
                    }
                    break;
                case 'h':
                    //H
                    /*
                    matrix = new int[3, 3]
                    {
                        { 1, 0, 3 },
                        { 7, 2, 5 },
                        { 6, 0, 4}
                    };
                    break;*/
                    switch (j)
                    {
                        case rot.top:
                            shapelength = 3;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 1, 0, 3 },
                                { 7, 2, 5 },
                                { 6, 0, 4 }
                            };
                            break;
                        case rot.right:
                            shapelength = 3;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 6, 7, 1 },
                                { 0, 2, 0 },
                                { 4, 5, 3 }
                            };
                            break;
                        case rot.down:
                            shapelength = 3;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 4, 0, 6},
                                { 5, 2, 7},
                                { 3, 0, 1}
                            };
                            break;
                        case rot.left:
                            shapelength = 3;
                            shapeheight = 3;
                            matrix = new int[3, 3]
                            {
                                { 3, 5, 4},
                                { 0, 2, 0},
                                { 1, 7, 6}
                            };
                            break;
                    }
                    break;
            }
            s();
        }
        /// <summary>
        /// вспомогательная функция
        /// </summary>
        private void s()
        {
            matlen = matrix.Length;
            typeshape_x = 10 - shapelength;
            typeshape_y = 20 - shapeheight;
            shapemass = new int[shapelength];
            for (int i = 0; i < shapelength; i++)
            {
                for (int j = 1; j <= shapeheight; j++)
                    if (matrix[j - 1, i] != 0)
                    {
                        shapemass[i] = j;
                    }
            }
        }
    }
}