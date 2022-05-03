using System;
using System.Windows.Forms;

namespace tetrixd
{
    public partial class Form1 : Form
    {
        //экземпляр фигуры
        Shapes curshape;
        Shapes nextShape;
        //экземпляр карты
        Map mapp;
        Files file;
        //коллизия
        bool col;
        bool col_r_l;

        public Form1()
        {
            Program.f = this;
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            //результаты

            file = new Files("results.xml");
            file.ImportFromFile();
            label2.Text = "1. " + file.Tier1.ToString();
            label3.Text = "2. " + file.Tier2.ToString();
            label4.Text = "3. " + file.Tier3.ToString();

            //размер клеточки
            int size = 25;
            //экземпляр карты
            mapp = new Map(10, 20, size);

            timer1.Start();

            //экземпляр фигуры
            curshape = new Shapes(3, -1);
            nextShape = new Shapes(3, -1);

            //результат
            mapp._score = 0;
            label1.Text = "Score: " + mapp._score;

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
            mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
            
            //вниз
            curshape.Move();

            //инициализация
            mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);

            col = mapp.Collisions(curshape.x, curshape.y, curshape.shapelength,
                curshape.shapeheight, curshape.typeshape_x, curshape.typeshape_y, curshape);

            //fixme
            col_r_l = mapp.Collisions_Right_Left();

            //коллизия
            if (col)
            {
                label1.Text = "Score: " + mapp._score;
                curshape = nextShape;
                nextShape = new Shapes(3, -1);
                timer1.Interval = 500;
            }
            //отрисовка
            Invalidate();
            //дебаг
            Text = Convert.ToString("x: " + curshape.x + "y: " + curshape.y);
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            //отрисовка
            mapp.DrawMap(e.Graphics);
            mapp.DrawGame(e.Graphics);
            mapp.DrawNextMap(e.Graphics);
            mapp.DrawNextShape(e.Graphics, nextShape);
        }
        bool isesc = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //два первых условия добавить условие справа или слева находится фигура то-есть 1
            if (e.KeyCode == Keys.A && curshape.x > 0 && curshape.y >= 0)
            {
                mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                curshape.MoveLeft();
                mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                Invalidate();
            }
            //pressed d && x < limit_x && (y >= 0 cause error happens)
            else if (e.KeyCode == Keys.D && curshape.x < curshape.typeshape_x && curshape.y >= 0)
            {
                mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                curshape.MoveRight();
                mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                Invalidate();
            }
            else if (e.KeyCode == Keys.R)
            {
                mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                curshape.Rotate();
                mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                Invalidate();
            }
            else if (e.KeyCode == Keys.S)
            {
                if (!col)
                    timer1.Interval = 10;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (!isesc)
                {
                    timer1.Stop();
                    isesc = true;
                }
                else
                {
                    timer1.Start();
                    isesc = false;
                }
            }
        }
        //рестарт
        private void button1_Click(object sender, EventArgs e)
        {
            mapp.Restart(file, mapp._score);
            label2.Text = "1. " + file.Tier1.ToString();
            label3.Text = "2. " + file.Tier2.ToString();
            label4.Text = "3. " + file.Tier3.ToString();
            timer1.Enabled = true;
            button1.Visible = false;
            label1.Text = "Score: " + mapp._score;
            this.Focus();
        }

    }
}