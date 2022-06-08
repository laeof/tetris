using System;
using System.Drawing;
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
        //экземпляр рандома
        Random rnd = new Random();
        public int speed = 0;
        public Form1()
        {
            Program.f = this;
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// инициализация формы
        /// </summary>
        public void Init()
        {
            Program.f.Focus();
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

            //экземпляр фигуры
            curshape = new Shapes(3, -1, rnd, speed);
            nextShape = new Shapes(3, -1, rnd, speed);
            timer1.Start();
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

            col = mapp.Collisions(curshape.x, curshape.y, curshape.shapelength,
                curshape.shapeheight, curshape.typeshape_x, curshape.typeshape_y, curshape);

            //вниз
            if (!col)
                curshape.Move();

            if (WindowState == FormWindowState.Minimized)
            {
                panel5.Visible = true;
                isesc = true;
                timer1.Stop();
            }

            try
            {
                //инициализация
                mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                
            }
            catch { mapp.EndGame(); }

            mapp.ScoreCalc();

            //коллизия
            if (col)
            {
                mapp.ClearOneRow();
                label1.Text = "Score: " + mapp._score;
                curshape = nextShape;
                nextShape = new Shapes(3, -1, rnd, speed);
                mapp.ChangeInterval();
            }
            //отрисовка
            Invalidate();
        }
        /// <summary>
        /// рисуем все
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            //отрисовка
            mapp.DrawMap(e.Graphics);
            mapp.DrawGame(e.Graphics);
            mapp.DrawNextMap(e.Graphics);
            mapp.DrawNextShape(e.Graphics, nextShape);
        }
        //рестарт
        private void button1_Click(object sender, EventArgs e)
        {
            speed = 0;
            isesc = false;
            button3.Enabled = true;
            panel5.Visible = false;
            mapp.Restart(file, mapp._score);
            label2.Text = "1. " + file.Tier1.ToString();
            label3.Text = "2. " + file.Tier2.ToString();
            label4.Text = "3. " + file.Tier3.ToString();
            timer1.Enabled = true;
            label1.Text = "Score: " + mapp._score;
            mapp = new Map(10, 20, 25);
            curshape = new Shapes(3, -1, rnd, speed);
            nextShape = new Shapes(3, -1, rnd, speed);
            panel4.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        Point lastPoint;
        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            panel4.Focus();
        }

        /// <summary>
        /// обработчик клавиш
        /// </summary>
        public bool isesc = false;
        private void panel4_PreviewKeyDown(object    sender, PreviewKeyDownEventArgs e)
        {
            if (!isesc)
            {
                //два первых условия добавить условие справа или слева находится фигура то-есть 1
                if (e.KeyCode == Keys.A && curshape.x > 0 && curshape.y >= 0 && !mapp.Collisions_Left(curshape))
                {
                    mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    curshape.MoveLeft();
                    mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    Invalidate();
                }
                //pressed d && x < limit_x && (y >= 0 cause error happens)
                else if (e.KeyCode == Keys.D && curshape.x < curshape.typeshape_x && curshape.y >= 0 && !mapp.Collisions_Right(curshape))
                {
                    mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    curshape.MoveRight();
                    mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.R && !col)
                {
                    mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    curshape.Rotate();
                    mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Up && !col)
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
                        panel5.Visible = true;
                        isesc = true;
                    }
                    else
                    {
                        timer1.Start();
                        isesc = false;
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (!col)
                        timer1.Interval = 10;
                }
                else if (e.KeyCode == Keys.Left && curshape.x > 0 && curshape.y >= 0 && !mapp.Collisions_Left(curshape))
                {
                    mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    curshape.MoveLeft();
                    mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Right && curshape.x < curshape.typeshape_x && curshape.y >= 0 && !mapp.Collisions_Right(curshape))
                {
                    mapp.Clear(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    curshape.MoveRight();
                    mapp.Merge(curshape.x, curshape.y, curshape.shapelength, curshape.shapeheight, curshape);
                    Invalidate();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (!isesc)
                    {
                        timer1.Stop();
                        panel5.Visible = true;
                        isesc = true;
                    }
                    else
                    {
                        timer1.Start();
                        isesc = false;
                        panel5.Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// вернуться к игре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            isesc = false;
            panel5.Visible = false;
            panel4.Focus();
        }
        /// <summary>
        /// сворачивание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            panel5.Visible = true;
            isesc = true;
            timer1.Stop();
        }
    }
}

