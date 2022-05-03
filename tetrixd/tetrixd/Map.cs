using System;
using System.Drawing;

namespace tetrixd
{
    public class Map
    {
        public int[,] _map;
        public int _score = 0;
        private int _size;
        private int _rows;
        private int _cols;
        private int _score_fall = 30;
        private int _multiply = 1;
        ScoreM score = new ScoreM();
        //250 500 700 1000 1300
        public Map(int cols, int rows, int size)
        {
            _map = new int[rows, cols];
            _size = size;
            _rows = rows;
            _cols = cols;
        }
        public void Restart(Files file, int sc)
        {
            file.ExportToFile(sc);
            file.ImportFromFile();
            _score = 0;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    _map[i, j] = 0;
                }
            }
        }
        public bool Collisions(int x, int y, int shapelenght, int shapeheight, int type_x, int type_y, Shapes shape)
        {
            for (int i = x, j = 0; i < x + shapelenght; i++, j++)
            {

                if (y < type_y)
                    if (_map[y + shape.shapemass[j], i] != 0)//y, x
                    {
                        if (y == 0)
                        {
                            Program.f.timer1.Enabled = false;
                            Program.f.button1.Visible = true;
                        }
                        _score += score.Score;
                        return true;
                    }
            }

            if (y == type_y)
            {
                _score += score.Score;
                return true;
            }

            return false;
        }

        public bool Collisions_Right_Left(Shapes shape)
        {
            if (shape.x > 0 && shape.y < shape.typeshape_y && shape.x < shape.typeshape_x)
                for (int i = 0; i < 3; i++)
                {
                    if (_map[shape.y + i, shape.x - 1] != 0 && _map[shape.y + i, shape.x] != 0)
                    {
                        return true;
                    }
                    if (_map[shape.y + i, shape.x + shape.shapelength] != 0 && _map[shape.y + i, shape.x + shape.shapelength - 1] != 0)
                    {
                        return true;
                    }
                }
            return false;
        }
        public bool Collisions_Left(Shapes shape)
        {
            if (shape.x > 0 && shape.y < shape.typeshape_y && shape.x < shape.typeshape_x)
                for (int i = 0; i < shape.shapeheight; i++)
                {
                    if (_map[shape.y + i, shape.x - 1] != 0 && _map[shape.y + i, shape.x] != 0)
                    {
                        return true;
                    }
                }
            return false;
        }
        public bool Collisions_Right(Shapes shape)
        {
            if (shape.x > 0 && shape.y < shape.typeshape_y && shape.x < shape.typeshape_x)
                for (int i = 0; i < shape.shapeheight; i++)
                {
                    if (_map[shape.y + i, shape.x + shape.shapelength] != 0 && _map[shape.y + i, shape.x + shape.shapelength - 1] != 0)
                    {
                        return true;
                    }
                }
            return false;
        }

        //fixme
        private bool IsRowFilled()
        {
            return false;
        }
        //fixme
        public void ClearOneRow()
        {
            
        }
        bool i1 = true, i2 = true, i3 = true, i4 = true, i5 = true;
        public void ScoreCalc()
        {
            if (i1 && _score >= 300)
            {
                i1 = false;
                score.Score_Calc();
            }
            else if(_score > 600)
            {
                if (i2)
                {
                    i2 = false;
                    score.Score_Calc();
                }
                else if(_score > 900)
                {
                    if (i3)
                    {
                        i3 = false;
                        score.Score_Calc();
                    }
                    else if(_score > 1200)
                    {
                        if (i4)
                        {
                            i4 = false;
                            score.Score_Calc();
                        }
                        else if (_score > 1500)
                        {
                            if (i5)
                            {
                                i5 = false;
                                score.Score_Calc();
                            }
                        }
                    }
                }
            }
        }

        public void Merge(int x, int y, int shapelenght, int shapeheight, Shapes shape)
        {
            for (int i = shape.y; i < shape.y + Math.Sqrt(shape.matlen); i++)
            {
                for (int j = shape.x; j < shape.x + Math.Sqrt(shape.matlen); j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                        _map[i, j] = shape.matrix[i - shape.y, j - shape.x];
                }
            }
        }
        public void Clear(int x, int y, int shapelenght, int shapeheight, Shapes shape)
        {
            for (int i = y; i < y + shapeheight; i++)
            {
                for (int j = x; j < x + shapelenght; j++)
                {
                    if (i >= 0 && j >= 0 && i < 20 && j < 10)
                    {
                        if (shape.matrix[i - shape.y, j - shape.x] != 0)
                            _map[i, j] = 0;
                    }
                }
            }
        }
        public void DrawGame(Graphics e)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (_map[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (_map[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Aqua, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (_map[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (_map[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Orange, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (_map[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (_map[i, j] == 6)
                    {
                        e.FillRectangle(Brushes.Purple, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (_map[i, j] == 7)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(10 + j * _size + 1, 10 + i * _size + 1, _size - 1, _size - 1));
                    }

                }
            }
        }
        public void DrawNextShape(Graphics g, Shapes shape)
        {
            int[,] mat = shape.matrix;

            for (int i = 0; i < Math.Sqrt(shape.matlen); i++)
            {
                for (int j = 0; j < Math.Sqrt(shape.matlen); j++)
                {
                    if (mat[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.Blue, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (mat[i, j] == 2)
                    {
                        g.FillRectangle(Brushes.Aqua, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (mat[i, j] == 3)
                    {
                        g.FillRectangle(Brushes.Yellow, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (mat[i, j] == 4)
                    {
                        g.FillRectangle(Brushes.Orange, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (mat[i, j] == 5)
                    {
                        g.FillRectangle(Brushes.Red, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (mat[i, j] == 6)
                    {
                        g.FillRectangle(Brushes.Purple, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }
                    else if (mat[i, j] == 7)
                    {
                        g.FillRectangle(Brushes.Green, new Rectangle(40 + 10 * _size + j * _size + 1, 40 + i * _size + 1, _size - 1, _size - 1));
                    }

                }
            }
        }
        public void DrawNextMap(Graphics g)
        {
            int numcol = 4;//количество клеточек по горизонтали
            int numrow = 4;//количество клеточек по вертикали

            //отрисовываем горизонталь
            for (int i = 0; i <= numrow; i++)
            {
                //левая и правая точка
                Point pointleft = new Point(40 + 10 * _size, 40 + i * _size);
                Point pointright = new Point(40 + 10 * _size + numcol * _size , 40 + i * _size);

                //рисуем с помощью Graphics
                g.DrawLine(Pens.Black, pointleft, pointright);
            }
            //отрисовываем вертикаль
            for (int i = 0; i <= numcol; i++)
            {
                //левая и правая точка
                Point pointleft = new Point(190 + numcol * _size + i * _size, 40);
                Point pointright = new Point(190 + numcol * _size + i * _size, 40 + numcol * _size);

                //рисуем с помощью Graphics
                g.DrawLine(Pens.Black, pointleft, pointright);
            }

        }
        public void DrawMap(Graphics g)
        {
            int numcol = _cols;//количество клеточек по горизонтали
            int numrow = _rows;//количество клеточек по вертикали

            //отрисовываем горизонталь
            for (int i = 0; i <= numrow; i++)
            {
                //левая и правая точка
                Point pointleft = new Point(10, 10 + i * _size);
                Point pointright = new Point(10 + numcol * _size, 10 + i * _size);

                //рисуем с помощью Graphics
                g.DrawLine(Pens.Black, pointleft, pointright);
            }
            //отрисовываем вертикаль
            for (int i = 0; i <= numcol; i++)
            {
                //левая и правая точка
                Point pointleft = new Point(10 + i * _size, 10);
                Point pointright = new Point(10 + i * _size, 10 + numrow * _size);

                //рисуем с помощью Graphics
                g.DrawLine(Pens.Black, pointleft, pointright);
            }
        }
    }
}
