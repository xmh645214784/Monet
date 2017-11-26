///-------------------------------------------------------------------------------------------------
/// \file CircleTool.cs.
///
/// \brief Implements the circle tool class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class DrawCircleAdapter
    ///
    /// \brief A draw circle adapter.
    ///-------------------------------------------------------------------------------------------------

    abstract class DrawCircleAdapter
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn abstract public void DrawCircle(Graphics g, Pen pen, Point center, Point border);
        ///
        /// \brief Draw circle
        ///
        /// \param g      The Graphics to process.
        /// \param pen    The pen.
        /// \param center The cirle center point.
        /// \param border The border point.
        ///-------------------------------------------------------------------------------------------------

        abstract public void DrawCircle(Graphics g, Pen pen, Point center, Point border);

        ///-------------------------------------------------------------------------------------------------
        /// \fn static public void drawEightPix(Graphics g, Pen pen, Point center, Point p)
        ///
        /// \brief Draw eight pix
        ///
        /// \param g      The Graphics to process.
        /// \param pen    The pen.
        /// \param center The offset point (center point).
        /// \param p      A Point to process based on the origin of coordinates.
        ///-------------------------------------------------------------------------------------------------

        static public void drawEightPix(Graphics g, Pen pen, Point center, Point p)
        {
            int x = p.X;
            int y = p.Y;
            Common.DrawPix(g, new Point(x + center.X, y + center.Y), pen);
            Common.DrawPix(g, new Point(-x + center.X, y + center.Y), pen);
            Common.DrawPix(g, new Point(x + center.X, -y + center.Y), pen);
            Common.DrawPix(g, new Point(-x + center.X, -y + center.Y), pen);

            Common.DrawPix(g, new Point(y + center.X, x + center.Y), pen);
            Common.DrawPix(g, new Point(-y + center.X, x + center.Y), pen);
            Common.DrawPix(g, new Point(y + center.X, -x + center.Y), pen);
            Common.DrawPix(g, new Point(-y + center.X, -x + center.Y), pen);
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class CircleTool
    ///
    /// \brief A circle tool. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class CircleTool : DrawShapeTool { 
        /// \brief The circle agent
        public DrawCircleAdapter circleAgent;
        /// \brief The start point
        Point startPoint;
        /// \brief The now point
        Point nowPoint;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public CircleTool(PictureBox mainView, Button button) : base(mainView, button)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        /// \param button   The button control.
        ///-------------------------------------------------------------------------------------------------

        public CircleTool(PictureBox mainView, Button button) : base(mainView, button)
        {
            circleAgent = new BresenhamCircle();
            isEnabled = false;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void ChangeImplementMethod(LineImplementMethod newmtd)
        ///
        /// \brief Change implement method
        ///
        /// \exception Exception Thrown when an exception error condition occurs.
        ///
        /// \param newmtd The newmtd.
        ///-------------------------------------------------------------------------------------------------

        public void ChangeImplementMethod(LineImplementMethod newmtd)
        {
            switch (newmtd)
            {
                default:
                    throw new Exception("UnKnown lineImplement method");
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Draw(Graphics g, Pen pen, Point centerPoint, Point borderPoint)
        ///
        /// \brief Draws
        ///
        /// \param g           The Graphics to process.
        /// \param pen         The pen.
        /// \param centerPoint The circle center Point.
        /// \param borderPoint The circle border Point.
        ///-------------------------------------------------------------------------------------------------

        public void Draw(Graphics g, Pen pen, Point centerPoint, Point borderPoint)
        {
            circleAgent.DrawCircle(g, pen, centerPoint, borderPoint);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void UnRegisterTool()
        ///
        /// \brief Un register tool
        ///-------------------------------------------------------------------------------------------------

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

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

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

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

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = false;
            }
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class MidPointCircle
    ///
    /// \brief A middle point circle. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class MidPointCircle : DrawCircleAdapter
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn override public sealed void DrawCircle(Graphics g, Pen pen, Point center, Point boader)
        ///
        /// \brief Draw circle.
        ///
        /// \param g      The Graphics to process.
        /// \param pen    The pen.
        /// \param center The circle center Point.
        /// \param boader The border Point.
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

    ///-------------------------------------------------------------------------------------------------
    /// \class BresenhamCircle
    ///
    /// \brief A bresenham circle. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class BresenhamCircle : DrawCircleAdapter
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn override public sealed void DrawCircle(Graphics g, Pen pen, Point center, Point border)
        ///
        /// \brief Draw circle
        ///
        /// \param g      The Graphics to process.
        /// \param pen    The pen.
        /// \param center The center.
        /// \param border The border.
        ///-------------------------------------------------------------------------------------------------

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
