///-------------------------------------------------------------------------------------------------
/// \file src\shape\Line.cs.
///
/// \brief Implements the line class
///-------------------------------------------------------------------------------------------------

using Monet.src.tools;
using Monet.src.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.shape
{
    ///-------------------------------------------------------------------------------------------------
    /// \class Line
    ///
    /// \brief A line.
    ///-------------------------------------------------------------------------------------------------

    public class Line : Shape
    {
        /// \brief A Point to process
        public Point a;
        /// \brief A Point to process
        public Point b;
        /// \brief The pen
        public Pen pen;

        public ResizeButton resizeButtonA;
        public ResizeButton resizeButtonB;

        private bool isResizing;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override bool IsSelectMe(Point point)
        ///
        /// \brief Query if 'point' is select me
        ///
        /// \param point The point.
        ///
        /// \return True if select me, false if not.
        ///-------------------------------------------------------------------------------------------------

        public override bool IsSelectMe(Point point)
        {
            double dis = DistanceOfPoint2Line(point);
            Log.LogText(String.Format("{0}", dis));
            return dis < 30 ? true : false;
        }

        public override void ShowAsNotSelected()
        {
            resizeButtonA.Dispose();
            resizeButtonB.Dispose();
        }

        public override void ShowAsSelected()
        {
            resizeButtonA = new ResizeButton(MainWin.GetInstance().MainView(), new Point(a.X-3,a.Y-3), Cursors.Cross);
            resizeButtonA.MouseDown += ResizeButtonA_MouseDown;
            resizeButtonA.MouseUp += ResizeButtonA_MouseUp;
            resizeButtonA.MouseMove += ResizeButtonA_MouseMove;
            resizeButtonB = new ResizeButton(MainWin.GetInstance().MainView(), new Point(b.X - 3, b.Y - 3), Cursors.Cross);
        }

        private void ResizeButtonA_MouseMove(object sender, MouseEventArgs e)
        {
            ;
        }

        private void ResizeButtonA_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void ResizeButtonA_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn double DistanceOfPoint2Line(Point p)
        ///
        /// \brief Distance of point 2 line
        ///
        /// \param p A Point to process.
        ///
        /// \return A double.
        ///-------------------------------------------------------------------------------------------------

        double DistanceOfPoint2Line(Point p)
        {
            double dis = 0;


            if (a.X == b.X)
            {
                dis = Math.Abs(p.X - a.X);
                return dis;
            }
            double lineK = (b.Y - a.Y) / (b.X - a.X);
            double lineB = a.Y-(a.Y-b.Y)/(a.X-b.X)*a.X;
            dis = Math.Abs(lineK * p.X - p.Y + lineB) / (Math.Sqrt(lineK * lineK + 1));
            return dis;
        }
    }
 }
