#define DDA
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace Monet
{

    public enum LineImplementMethod
    {
        LINE_SYSTEM, LINE_DDA, LINE_BRESENHAM, LINE_MIDPOINT
    }

    interface DrawLinerAdapter
    {
        void DrawLine(Graphics g, Pen pen, Point p1, Point p2);
    }

    sealed class LineTool : DrawShapeTool
    {
        public DrawLinerAdapter lineAgent;
        Point startPoint;
        Point nowPoint;
        public LineTool(PictureBox mainView, Button button) : base(mainView, button)
        {
            lineAgent = new Dda();
            isEnabled = false;
        }

        public void ChangeImplementMethod(LineImplementMethod newmtd)
        {
            switch (newmtd)
            {
                case LineImplementMethod.LINE_SYSTEM:
                    lineAgent = new SystemDraw();
                    break;
                case LineImplementMethod.LINE_DDA:
                    lineAgent = new Dda();
                    break;
                case LineImplementMethod.LINE_BRESENHAM:
                    lineAgent = new Bresenham();
                    break;
                case LineImplementMethod.LINE_MIDPOINT:
                    lineAgent = new Midpoint();
                    break;
                default:
                    throw new Exception("UnKnown lineImplement method");
            }
        }


        public void Draw(Graphics g, Pen pen, Point p1, Point p2)
        {
            lineAgent.DrawLine(g, pen, p1, p2);
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
                Image lastValidClone= (Image)History.GetInstance().TopAction().Clone();
                History.GetInstance().PushBackAction(lastValidClone); 
                mainView.Image = lastValidClone;

                //draw
                using (Graphics g = Graphics.FromImage(mainView.Image))
                { 
                    nowPoint = e.Location;
                    lineAgent.DrawLine(g, Setting.GetInstance().Pen, startPoint, nowPoint);
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
                    lineAgent.DrawLine(g, Setting.GetInstance().Pen, startPoint, nowPoint);
                }
            }
        }
    }


    sealed class Dda : DrawLinerAdapter
    {
        private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            //System.Diagnostics.Debug.Assert(pen.Width == 1, "Draw a line whose width not equal 1");
            double dx, dy, e, x, y;
            int x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            dx = x2 - x1;
            dy = y2 - y1;
            e = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx /= e; dy /= e;
            x = x1;
            y = y1;
            for (int i = 1; i <= e; i++)
            {
                Common.DrawPix(g, new Point((int)(x + 0.5), (int)(y + 0.5)), pen);
                x += dx;
                y += dy;
            }
        }

        void DrawLinerAdapter.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }

    sealed class Midpoint : DrawLinerAdapter
    {
        private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            System.Diagnostics.Debug.Assert(pen.Width == 1, "Draw a line whose width not equal 1");
            int a, b, delta1, delta2, d, x, y;
            int x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            a = y1 - y2;
            b = x2 - x1;
            d = 2 * a + b;
            delta1 = 2 * a;
            delta2 = 2 * (a + b);
            x = x1;
            y = y1;
            Common.DrawPix(g,new Point(x,y),pen);
            while (x < x1)
            {
                if (d < 0)
                {
                    x++;
                    y++;
                    d += delta2;
                }
                else
                {
                    x++;
                    d += delta1;
                }
                Common.DrawPix(g, new Point(x, y), pen);
            } /* while */
        
        }

        void DrawLinerAdapter.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }


    sealed class Bresenham : DrawLinerAdapter
    {
        private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            System.Diagnostics.Debug.Assert(pen.Width == 1, "Draw a line whose width not equal 1");
            int x, y, dx, dy, p;
            int x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            x = x1;
            y = y1;
            dx = x2 - x1;
            dy = y2 - y1;
            p = 2 * dy - dx;
            for (; x <= x2; x++)
            {
                Common.DrawPix(g, new Point(x, y), pen);
                if (p >= 0)
                {
                    y++;
                    p += 2 * (dy - dx);
                }
                else
                {
                    p += 2 * dy;
                }
            }

        }

        public void DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }

    sealed class SystemDraw : DrawLinerAdapter
    {
        public void DrawLine(Graphics g,  Pen pen, Point p1, Point p2)
        {
            g.DrawLine(pen, p1, p2);
        }
    }



}
