using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    abstract class DrawCircleAdapter
    {
        abstract public void DrawCircle(Graphics g, Pen pen, Point center, Point border);
        static public void drawEightPix(Graphics g, Pen pen, Point offset, Point p)
        {
            int x = p.X;
            int y = p.Y;
            Common.DrawPix(g, new Point(x + offset.X, y + offset.Y), pen);
            Common.DrawPix(g, new Point(-x + offset.X, y + offset.Y), pen);
            Common.DrawPix(g, new Point(x + offset.X, -y + offset.Y), pen);
            Common.DrawPix(g, new Point(-x + offset.X, -y + offset.Y), pen);

            Common.DrawPix(g, new Point(y + offset.X, x + offset.Y), pen);
            Common.DrawPix(g, new Point(-y + offset.X, x + offset.Y), pen);
            Common.DrawPix(g, new Point(y + offset.X, -x + offset.Y), pen);
            Common.DrawPix(g, new Point(-y + offset.X, -x + offset.Y), pen);
        }
    }

    sealed class CircleTool : DrawShapeTool { 
        public DrawCircleAdapter circleAgent;
        Point startPoint;
        Point nowPoint;
        public CircleTool(PictureBox mainView, Button button) : base(mainView, button)
        {
            circleAgent = new MidPointCircle();
            isEnabled = false;
        }

        public void ChangeImplementMethod(LineImplementMethod newmtd)
        {
            switch (newmtd)
            {
                default:
                    throw new Exception("UnKnown lineImplement method");
            }
        }


        public void Draw(Graphics g, Pen pen, Point p1, Point p2)
        {
            circleAgent.DrawCircle(g, pen, p1, p2);
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
        }


        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
        }


        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEnabled)
            {
                //take out the last but two valid image, take clone of it ,push it into stack.
                History.GetInstance().UndoAction();
                Image lastValidClone = (Image)History.GetInstance().TopAction().Clone();
                History.GetInstance().PushBackAction(lastValidClone);
                mainView.Image = lastValidClone;

                //draw
                using (Graphics g = Graphics.FromImage(mainView.Image))
                { 
                    nowPoint = e.Location;
                    circleAgent.DrawCircle(g, Setting.GetInstance().Pen, startPoint, nowPoint);
                    mainView.Invalidate();
                }
            }
        }
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isEnabled = true;
                Image newClone = (Image)mainView.Image.Clone();
                History.GetInstance().PushBackAction(newClone);
                mainView.Image = newClone;
            }
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics g = mainView.CreateGraphics())
                { 
                    isEnabled = false;
                    circleAgent.DrawCircle(g, Setting.GetInstance().Pen, startPoint, nowPoint);
                    g.Dispose();
                }
            }
        }
    }

    sealed class MidPointCircle : DrawCircleAdapter
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void DrawCircleAdapter.DrawCircle(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw circle.
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The circle center Point.
        /// \param p2  The border Point.
        ///-------------------------------------------------------------------------------------------------

        override public sealed 
            void DrawCircle(Graphics g, Pen pen, Point center, Point boader)
        {
            int R = (int)Math.Sqrt(Math.Pow(boader.X - center.X,2) + Math.Pow(boader.Y - center.Y,2));
            int x, y, deltax, deltay, d;
            x = 0; y = R; d = 1 - R;
            deltax = 3;
            deltay = 5 - R - R;
            drawEightPix(g, pen, center, new Point(x, y));

            while (x < y)
            {
                if (d < 0)
                {
                    d += deltax;
                    deltax += 2;
                    deltay += 2;
                    x++;
                }
                else
                {
                    d += deltay;
                    deltax += 2;
                    deltay += 4;
                    x++;
                    y--;
                }
                drawEightPix(g, pen, center, new Point(x, y));
            }
        }

    }
    sealed class BresenhamCircle : DrawCircleAdapter
    {
        override public sealed
            void DrawCircle(Graphics g, Pen pen, Point center, Point border)
        {
            int R = (int)Math.Sqrt(Math.Pow(border.X - center.X, 2) + Math.Pow(border.Y - center.Y, 2));
            int x, y, p;
            x = 0; y = R;
            p = 3 - 2 * R;
            for (; x <= y; x++)
            {
                drawEightPix(g,pen,center,new Point(x, y));
                if (p >= 0)
                {
                    p += 4 * (x - y) + 10;
                    y--;
                }
                else
                {
                    p += 4 * x + 6;
                }
            }
        }
    }
}
