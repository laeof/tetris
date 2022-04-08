using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tetrixd
{
    public partial class Form1 : Form
    {
        Shapes curshape;
        int size;
        int[,] map = new int[20, 10];
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            //размер клеточки
            size = 25;

            timer1.Start();

            //фигура посередине
            curshape = new Shapes(3, -1);
            
            //отрисовка
            Invalidate();
        }
        
        /// <summary>
        /// функция помощник для таймера
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public void update(object Sender, EventArgs e)
        {
            Clear();
            
            //downmove
            curshape.Move();
            
            //init matrix
            Merge();
            
            //another shape or end
            OnCollision();

            //
            Invalidate();

            this.Text = Convert.ToString("x: " + curshape.x + "y: " + curshape.y);
        }
        public bool col = false;
        public bool ss = false;

        public void OnCollision()
        {
            for (int i = curshape.x; i < curshape.x + curshape.shapelength; i++)
            {
                if (curshape.y < curshape.typeshape_y)
                    if (map[curshape.y + curshape.shapeheight, i] == 1 && map[curshape.y + curshape.shapeheight - 1, i] == 1)//y, x
                    {
                        ss = true;
                    }
            }

            if (curshape.y == curshape.typeshape_y)
            {
                col = true;
            }
            else col = false;

            //shape falls
            if (col || ss)
            {
                ss = false;
                curshape = new Shapes(3, -1);
                timer1.Interval = 500;
            }
        }
        /// <summary>
        /// инициализация матрицы
        /// </summary>
        public void Merge()
        {
            for (int i = curshape.y; i <curshape.y + Math.Sqrt(curshape.matlen); i++)
            {
                for (int j = curshape.x; j < curshape.x + Math.Sqrt(curshape.matlen); j++)
                {
                    if (curshape.matrix[i - curshape.y, j - curshape.x] != 0)
                        map[i, j] = curshape.matrix[i - curshape.y, j - curshape.x];
                }
            }
        }
        //проблема тут
        //понять какую область чистит
        //переписать алгоритм
        //добавить удаление нижней линии
        public void Clear()
        {
            for (int i = curshape.y; i < curshape.y + Math.Sqrt(curshape.matlen); i++)
            {
                for (int j = curshape.x; j < curshape.x + Math.Sqrt(curshape.matlen); j++)
                {
                    //clear all '1' around shape
                    if (i >= 0 && j >= 0 && i < 20 && j < 10)
                        if (map[i, j] == 1) 
                                map[i, j] = 0;
                    
                }
            }
        }
        public void DrawGame(Graphics e)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 1)
                    {
                        e.FillRectangle(curshape.color, new Rectangle(10 + j * size + 1, 10 + i * size+1, size-1, size-1));
                    }
                }
            }
        }
        public void DrawMap(Graphics g)
        {
            int numcol = 10;//количество клеточек по горизонтали
            int numrow = 20;//количество клеточек по вертикали

            //отрисовываем горизонталь
            for (int i = 0; i <= numrow; i++)
            {
                //левая и правая точка
                Point pointleft = new Point(10, 10 + i * size);
                Point pointright = new Point(10 + numcol * size, 10 + i * size);
                
                //рисуем с помощью Graphics
                g.DrawLine(Pens.Black, pointleft, pointright);
            }
            //отрисовываем вертикаль
            for (int i = 0; i <= numcol; i++)
            {
                //левая и правая точка
                Point pointleft = new Point(10 + i * size, 10);
                Point pointright = new Point(10 + i * size, 10 + numrow * size);

                //рисуем с помощью Graphics
                g.DrawLine(Pens.Black, pointleft, pointright);
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawMap(e.Graphics);
            DrawGame(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //два первых условия добавить условие справа или слева находится фигура то-есть 1
            if (e.KeyCode == Keys.A && curshape.x > 0 && curshape.y >= 0)
            {
                Clear();
                curshape.MoveLeft();
                Merge();
                Invalidate();
            }
            //pressed d && x < limit_x && (y >= 0 cause error happens)
            else if (e.KeyCode == Keys.D && curshape.x < curshape.typeshape_x && curshape.y >= 0)
            {
                Clear();
                curshape.MoveRight();
                Merge();
                Invalidate();
            }
            else if (e.KeyCode == Keys.R)
            {
                Clear();
                curshape.Rotate();
                Merge();
                Invalidate();
            }
            else if (e.KeyCode == Keys.S)
            {
                if (!col)
                    timer1.Interval = 10;
            }
        }
    }
}
