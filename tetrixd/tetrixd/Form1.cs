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

            //вызываем event update
            timer1.Tick += new EventHandler(update);

            //фигура посередине
            curshape = new Shapes(3, 0);

            timer1.Start();

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
            curshape.Move();
            if (curshape.y == 17)
                curshape = new Shapes(3, 0);
            Merge();
            //отрисовка
            Invalidate();
           
        }
        /// <summary>
        /// инициализация матрицы
        /// </summary>
        public void Merge()
        {
            for (int i = curshape.y; i <curshape.y + 3; i++)
            {
                for (int j = curshape.x; j < curshape.x + 3; j++)
                {
                    if (curshape.matrix[i - curshape.y, j - curshape.x] != 0)
                        map[i, j] = curshape.matrix[i - curshape.y, j - curshape.x];
                }
            }
        }
        public void Clear()
        {
            for (int i = curshape.y; i < curshape.y + 3; i++)
            {
                for (int j = curshape.x; j < curshape.x + 3; j++)
                {
                    if(i >=0 && j>=0 && i < 20 && j < 10)
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
            if (e.KeyCode == Keys.A)
            {
                Clear();
                curshape.MoveLeft();
                Merge();
                Invalidate();
            }
            else if (e.KeyCode == Keys.D)
            {
                Clear();
                curshape.MoveRight();
                Merge();
                Invalidate();
            }
        }
    }
}
