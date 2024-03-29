﻿///-------------------------------------------------------------------------------------------------
/// \file src\tools\LineTool.cs.
///
/// \brief Implements the line tool class
///-------------------------------------------------------------------------------------------------

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
    ///-------------------------------------------------------------------------------------------------
    /// \enum LineImplementMethod
    ///
    /// \brief Values that represent line implement methods
    ///-------------------------------------------------------------------------------------------------

    public enum LineImplementMethod
    {
        ///< An enum constant representing the line system option
        LINE_SYSTEM, LINE_DDA, LINE_BRESENHAM, LINE_MIDPOINT
    }

    ///-------------------------------------------------------------------------------------------------
    /// \interface DrawLinerAgent
    ///
    /// \brief Interface for draw liner agent.
    ///-------------------------------------------------------------------------------------------------

    interface DrawLinerAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void DrawLine(Graphics g, Pen pen, Point p1, Point p2);
        ///
        /// \brief Draw line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

        void DrawLine(Graphics g, Pen pen, Point p1, Point p2);
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class LineTool
    ///
    /// \brief A line tool. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class LineTool : DrawShapeTool
    {
        /// \brief The line agent
        public DrawLinerAgent lineAgent;
        /// \brief The start point
        Point startPoint;
        /// \brief The now point
        Point nowPoint;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public LineTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public LineTool(PictureBox mainView) : base(mainView)
        {
            lineAgent = new Dda();
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

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Draw(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draws
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

        public void Draw(Graphics g, Pen pen, Point p1, Point p2)
        {
            lineAgent.DrawLine(g, pen, p1, p2);
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

                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, Setting.GetInstance().Pen, startPoint, e.Location);
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

    ///-------------------------------------------------------------------------------------------------
    /// \class Dda
    ///
    /// \brief A dda. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class Dda : DrawLinerAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw wid eq one line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

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

        ///-------------------------------------------------------------------------------------------------
        /// \fn void DrawLinerAgent.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

        void DrawLinerAgent.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class Midpoint
    ///
    /// \brief A midpoint. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class Midpoint : DrawLinerAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw wid eq one line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

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

        ///-------------------------------------------------------------------------------------------------
        /// \fn void DrawLinerAgent.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

        void DrawLinerAgent.DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class Bresenham
    ///
    /// \brief A bresenham. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class Bresenham : DrawLinerAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw wid eq one line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

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

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

        public void DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            DrawWidEqOneLine(g, pen, p1, p2);
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class SystemDraw
    ///
    /// \brief A system draw. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class SystemDraw : DrawLinerAgent
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn public void DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        ///
        /// \brief Draw line
        ///
        /// \param g   The Graphics to process.
        /// \param pen The pen.
        /// \param p1  The first Point.
        /// \param p2  The second Point.
        ///-------------------------------------------------------------------------------------------------

        public void DrawLine(Graphics g,  Pen pen, Point p1, Point p2)
        {
            g.DrawLine(pen, p1, p2);
        }
    }

    

}
