///-------------------------------------------------------------------------------------------------
/// \file CircleTool.cs.
///
/// \brief Implements the circle tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
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
    /// \class DrawCircleAgent
    ///
    /// \brief A draw circle adapter.
    ///-------------------------------------------------------------------------------------------------

    abstract class DrawCircleAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn abstract public void DrawCircle(Graphics g, Pen pen, Point center, Point border,Color backColor);
        ///
        /// \brief Draw circle
        ///
        /// \param g         The Graphics to process.
        /// \param pen       The pen.
        /// \param center    The cirle center point.
        /// \param border    The border point.
        /// \param backColor The back color.
        ///-------------------------------------------------------------------------------------------------

        abstract public void DrawCircle(Graphics g, Pen pen, Point center, Point border,Color backColor);

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
        public DrawCircleAgent circleAgent;
        /// \brief The start point
        Point startPoint;
        /// \brief The now point
        Point nowPoint;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public CircleTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///
        /// ### param button The button control.
        ///-------------------------------------------------------------------------------------------------

        public CircleTool(PictureBox mainView) : base(mainView)
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
        /// \fn public void Draw(Graphics g, Pen pen, Point centerPoint, Point borderPoint,Color backColor)
        ///
        /// \brief Draws
        ///
        /// \param g           The Graphics to process.
        /// \param pen         The pen.
        /// \param centerPoint The circle center Point.
        /// \param borderPoint The circle border Point.
        /// \param backColor   The back color.
        ///-------------------------------------------------------------------------------------------------

        public void Draw(Graphics g, Pen pen, Point centerPoint, Point borderPoint,Color backColor)
        {
            circleAgent.DrawCircle(g, pen, centerPoint, borderPoint, backColor);
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
                mainView.Image.Dispose();
                mainView.Image = (Image)doubleBuffer.Clone();

                //draw
                using (Graphics g = Graphics.FromImage(mainView.Image))
                { 
                    nowPoint = e.Location;
                    circleAgent.DrawCircle(g, Setting.GetInstance().Pen, startPoint, nowPoint, Setting.GetInstance().BackgroundColor);
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
                doubleBuffer = (Image)mainView.Image.Clone();
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

                mainView.Image = (Image)doubleBuffer.Clone();
                nowPoint = e.Location;
                Circle circle = new Circle();
                circle.startPoint = startPoint;
                circle.endPoint = nowPoint;
                circle.pen = Setting.GetInstance().Pen.Clone() as Pen;
                History.GetInstance().PushBackAction(
                    new src.history.MAction((Tool)this, circle));
                MakeAction(circle);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void MakeAction(ActionParameters_t toolParameters)
        ///
        /// \brief Makes an action
        ///
        /// \exception InvalidCastException Thrown when an object cannot be cast to a required type.
        ///
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                Circle circle = (Circle)toolParameters;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    circleAgent.DrawCircle(g, circle.pen, circle.startPoint, circle.endPoint, circle.backColor);
                }
                //add into shape array

            }
            catch (InvalidCastException)
            {
                throw;
            }
            
        }

    }

    ///-------------------------------------------------------------------------------------------------
    /// \class MidPointCircle
    ///
    /// \brief A middle point circle. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class MidPointCircle : DrawCircleAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn override public sealed void DrawCircle(Graphics g, Pen pen, Point center, Point boader,Color backColor)
        ///
        /// \brief Draw circle.
        ///
        /// \param g         The Graphics to process.
        /// \param pen       The pen.
        /// \param center    The circle center Point.
        /// \param boader    The border Point.
        /// \param backColor The back color.
        ///-------------------------------------------------------------------------------------------------

        override public sealed 
            void DrawCircle(Graphics g, Pen pen, Point center, Point boader,Color backColor)
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
            if(backColor!=Color.White)
                g.FillEllipse(new SolidBrush(backColor), new Rectangle(center.X - R, center.Y - R, 2 * R, 2 * R));
        }

    }

    ///-------------------------------------------------------------------------------------------------
    /// \class BresenhamCircle
    ///
    /// \brief A bresenham circle. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class BresenhamCircle : DrawCircleAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn override public sealed void DrawCircle(Graphics g, Pen pen, Point center, Point border,Color backColor)
        ///
        /// \brief Draw circle
        ///
        /// \param g         The Graphics to process.
        /// \param pen       The pen.
        /// \param center    The center.
        /// \param border    The border.
        /// \param backColor The back color.
        ///-------------------------------------------------------------------------------------------------

        override public sealed
            void DrawCircle(Graphics g, Pen pen, Point center, Point border,Color backColor)
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
            if (backColor != Color.White)
                g.FillEllipse(new SolidBrush(backColor), new Rectangle(center.X - R, center.Y - R, 2 * R, 2 * R));
        }
    }


}
