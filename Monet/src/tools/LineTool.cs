#define DDA
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Monet.src.history;
using Monet.src.shape;
using Monet.src.ui;

namespace Monet
{

    public enum LineImplementMethod
    {
        LINE_SYSTEM, LINE_DDA, LINE_BRESENHAM, LINE_MIDPOINT
    }

    interface DrawLinerAgent
    {
        void DrawLine(Graphics g, Pen pen, Point p1, Point p2);
    }

    sealed class LineTool : DrawShapeTool
    {
        public DrawLinerAgent lineAgent;
        Point startPoint;
        Point nowPoint;
        public LineTool(PictureBox mainView) : base(mainView)
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
                mainView.Image.Dispose();
                mainView.Image = (Image)doubleBuffer.Clone();

                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, Setting.GetInstance().Pen, startPoint, e.Location);
                }
            }
        }
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                startPoint = e.Location;
                isEnabled = true;
                doubleBuffer = (Image)mainView.Image.Clone();
            }
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = false;

                mainView.Image = (Image)doubleBuffer.Clone();

                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, Setting.GetInstance().Pen, startPoint, e.Location);
                }

                Line line = new Line();
                line.a = startPoint;
                line.b=e.Location ;
                line.pen = Setting.GetInstance().Pen.Clone() as Pen;

                History.GetInstance().PushBackAction(
                    new MAction(this, line));
                Log.LogText(String.Format("Line :({0},{1}) to ({2},{3})", startPoint.X, startPoint.Y, e.Location.X, e.Location.Y));

                doubleBuffer.Dispose();
            }
        }

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                Line line = (Line)toolParameters;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, line.pen, line.a, line.b);
                }
            }
            catch (InvalidCastException)
            {
                throw;
            }
        }
        
    }


    sealed class Dda : DrawLinerAgent
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

        void DrawLinerAgent.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }

    sealed class Midpoint : DrawLinerAgent
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

        void DrawLinerAgent.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }


    sealed class Bresenham : DrawLinerAgent
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

    sealed class SystemDraw : DrawLinerAgent
    {
        public void DrawLine(Graphics g,  Pen pen, Point p1, Point p2)
        {
            g.DrawLine(pen, p1, p2);
        }
    }

    

}
