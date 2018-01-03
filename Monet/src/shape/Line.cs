///-------------------------------------------------------------------------------------------------
/// \file src\shape\Line.cs.
///
/// \brief Implements the line class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.tools;
using Monet.src.ui;
using System;
using System.Collections;
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

    public class Line : Shape, Rotatable,Clipable
    {
        /// \brief A Point to process
        public Point a;
        /// \brief A Point to process
        public Point b;
        

        /// \brief The adjust button a
        public AdjustButton adjustButtonA;
        /// \brief The adjust button b
        public AdjustButton adjustButtonB;
        /// \brief The move button
        public MoveButton moveButton;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public Line()
        ///
        /// \brief Default constructor
        ///-------------------------------------------------------------------------------------------------

        public Line()
        {
            isAdjusting = false;
        }

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
            double dis = DistanceOfPoint2Line(a, b, point);
            return dis < 10 ? true : false;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void ShowAsNotSelected()
        ///
        /// \brief Shows as not selected
        ///-------------------------------------------------------------------------------------------------

        public override void ShowAsNotSelected()
        {
            base.ShowAsNotSelected();
            try
            {
                adjustButtonA.Visible = adjustButtonB.Visible=moveButton.Visible=false;

                adjustButtonA.Dispose();
                moveButton.Dispose();
                adjustButtonB.Dispose();
            }
            catch(NullReferenceException)
            {
                ;
            }
            finally
            {
                adjustButtonA = adjustButtonB = null;
                moveButton = null;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void ShowAsSelected()
        ///
        /// \brief Shows as selected
        ///-------------------------------------------------------------------------------------------------

        public override void ShowAsSelected()
        {
            base.ShowAsSelected();
            if(adjustButtonA==null)
            {
                adjustButtonA = new AdjustButton(MainWin.GetInstance().MainView(), this, new Point(a.X - 3, a.Y - 3), Cursors.SizeNS);
                adjustButtonA.MouseDown += ResizeButtonA_MouseDown;
                adjustButtonA.MouseUp += ResizeButtonA_MouseUp;
                adjustButtonA.MouseMove += ResizeButtonA_MouseMove;
            }
            if (adjustButtonB == null)
            {
                adjustButtonB = new AdjustButton(MainWin.GetInstance().MainView(), this,new Point(b.X - 3, b.Y - 3), Cursors.SizeNS);
                adjustButtonB.MouseDown += ResizeButtonA_MouseDown;
                adjustButtonB.MouseUp += ResizeButtonA_MouseUp;
                adjustButtonB.MouseMove += ResizeButtonA_MouseMove;
            }
            if(moveButton==null)
            {
                moveButton = new MoveButton(MainWin.GetInstance().MainView(),
                    this, new Point(a.X / 2 + b.X / 2, a.Y / 2 + b.Y / 2),
                    Cursors.SizeAll);
                moveButton.MouseDown += MoveButton_MouseDown;
                moveButton.MouseMove += MoveButton_MouseMove;
                moveButton.MouseUp += MoveButton_MouseUp;
            }

            adjustButtonA.SetBindingPoints(
                    new Ref<Point>(() => a, z => { a = z; })
                    );
            adjustButtonB.SetBindingPoints(
                    new Ref<Point>(() => b, z => { b = z; })
                );
            moveButton.SetBindingPoints(
                    new Ref<Point>(() => a, z => { a = z; }),
                    new Ref<Point>(() => b, z => { b = z; }),
                    // only lines'point moving is not enough. We need move its buttons.
                    new Ref<Point>(() => adjustButtonA.Location, z => { adjustButtonA.Location = z; }),
                    new Ref<Point>(() => adjustButtonB.Location, z => { adjustButtonB.Location = z; })
                );

            Log.LogText("Select Line");
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MoveButton for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                isMoving = false;
                RetMAction().Action();
                Log.LogText(string.Format("Move Line ({0},{1}),({2},{3})", a.X, a.Y, b.X, b.Y));
                ShowAsNotSelected();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MoveButton_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MoveButton for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                RetMAction().Action();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MoveButton for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoving = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void ResizeButtonA_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by ResizeButtonA for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void ResizeButtonA_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                isAdjusting = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }

        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void ResizeButtonA_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by ResizeButtonA for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void ResizeButtonA_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                RetMAction().Action();
                moveButton.Location = new Point(a.X / 2 + b.X / 2, a.Y / 2 + b.Y / 2);
            } 
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void ResizeButtonA_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by ResizeButtonA for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void ResizeButtonA_MouseUp(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                isAdjusting = false;
                RetMAction().Action();
                Log.LogText(string.Format("Resize Line ({0},{1}),({2},{3})",a.X,a.Y,b.X,b.Y));
                ShowAsNotSelected();
            }
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public static double DistanceOfPoint2Line(PointF pt1, PointF pt2, PointF pt3)
        ///
        /// \brief Distance of point 2 line
        ///
        /// \param pt1 The first point.
        /// \param pt2 The second point.
        /// \param pt3 The third point.
        ///
        /// \return A double.
        ///-------------------------------------------------------------------------------------------------

        public static double DistanceOfPoint2Line(PointF pt1, PointF pt2, PointF pt3)

        {
            double dis = 0;
            if (pt1.X == pt2.X)
            {
                dis = Math.Abs(pt3.X - pt1.X);

                return dis;
            }
            double lineK = (pt2.Y - pt1.Y) / (pt2.X - pt1.X);

            double lineC = (pt2.X * pt1.Y - pt1.X * pt2.Y) / (pt2.X - pt1.X);

            dis = Math.Abs(lineK * pt3.X - pt3.Y + lineC) / (Math.Sqrt(lineK * lineK + 1));

            return dis;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override object Clone()
        ///
        /// \brief Makes a deep copy of this object
        ///
        /// \return A copy of this object.
        ///-------------------------------------------------------------------------------------------------

        public override object Clone()
        {
            Line copy = new Line();
            copy.a = a;
            copy.b = b;
            copy.pen = (Pen)pen.Clone();
            return copy;
        }

        /// \brief The preangle
        double preangle=0;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Rotate(Point midPoint, double angle)
        ///
        /// \brief Rotates
        ///
        /// \param midPoint The middle point.
        /// \param angle    The angle.
        ///-------------------------------------------------------------------------------------------------

        public void Rotate(Point midPoint, double angle)
        {
            if (angle <= 0)
                ;
            else
            {
                Rotate(midPoint, -preangle);
                preangle = angle;
            }
            a = Common.RotatingPoint(a, midPoint, angle);
            b= Common.RotatingPoint(b, midPoint, angle);
            adjustButtonA.Location=Common.RotatingPoint(adjustButtonA.Location, midPoint, angle);
            adjustButtonB.Location= Common.RotatingPoint(adjustButtonB.Location, midPoint, angle); 
            moveButton.Location = Common.RotatingPoint(moveButton.Location, midPoint, angle);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public Point Line2LineIntersectionPoint(int X0, int Y0, int X1, int Y1)
        ///
        /// \brief Line 2 line intersection point
        ///
        /// \param X0 The x coordinate 0.
        /// \param Y0 The y coordinate 0.
        /// \param X1 The first x value.
        /// \param Y1 The first y value.
        ///
        /// \return A Point.
        ///-------------------------------------------------------------------------------------------------

        public Point Line2LineIntersectionPoint(int X0, int Y0, int X1, int Y1)
        {
            int x = (X0 * Y1 * a.X - X1 * Y0 * a.X - X0 * Y1 * b.X + X1 * Y0 * b.X - X0 * a.X * b.Y + X0 * b.X * a.Y + X1 * a.X * b.Y - X1 * b.X * a.Y) / (X0 * a.Y - Y0 * a.X - X0 * b.Y - X1 * a.Y + Y0 * b.X + Y1 * a.X + X1 * b.Y - Y1 * b.X);
            int y = (X0 * Y1 * a.Y - X1 * Y0 * a.Y - X0 * Y1 * b.Y + X1 * Y0 * b.Y - Y0 * a.X * b.Y + Y0 * b.X * a.Y + Y1 * a.X * b.Y - Y1 * b.X * a.Y) / (X0 * a.Y - Y0 * a.X - X0 * b.Y - X1 * a.Y + Y0 * b.X + Y1 * a.X + X1 * b.Y - Y1 * b.X);
            return new Point(x, y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public static Point Line2LineIntersectionPoint(Point a,Point b, Point c, Point d)
        ///
        /// \brief Line 2 line intersection point
        ///
        /// \param a A Point to process.
        /// \param b A Point to process.
        /// \param c A Point to process.
        /// \param d A Point to process.
        ///
        /// \return A Point.
        ///-------------------------------------------------------------------------------------------------

        public static Point Line2LineIntersectionPoint(Point a,Point b, Point c, Point d)
        {
            int X0 = c.X;
            int Y0 = c.Y;
            int X1 = d.X;
            int Y1 = d.Y;
            int x = (X0 * Y1 * a.X - X1 * Y0 * a.X - X0 * Y1 * b.X + X1 * Y0 * b.X - X0 * a.X * b.Y + X0 * b.X * a.Y + X1 * a.X * b.Y - X1 * b.X * a.Y) / (X0 * a.Y - Y0 * a.X - X0 * b.Y - X1 * a.Y + Y0 * b.X + Y1 * a.X + X1 * b.Y - Y1 * b.X);
            int y = (X0 * Y1 * a.Y - X1 * Y0 * a.Y - X0 * Y1 * b.Y + X1 * Y0 * b.Y - Y0 * a.X * b.Y + Y0 * b.X * a.Y + Y1 * a.X * b.Y - Y1 * b.X * a.Y) / (X0 * a.Y - Y0 * a.X - X0 * b.Y - X1 * a.Y + Y0 * b.X + Y1 * a.X + X1 * b.Y - Y1 * b.X);
            return new Point(x, y);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void LineClip(int X0, int Y0, int X1, int Y1)
        ///
        /// \brief use a line to clip old line
        ///
        /// \param X0 The x coordinate 0.
        /// \param Y0 The y coordinate 0.
        /// \param X1 The first x value.
        /// \param Y1 The first y value.
        ///-------------------------------------------------------------------------------------------------

        public void LineClip(int X0, int Y0, int X1, int Y1)
        {
            Point t;
            try
            {
                t=Line2LineIntersectionPoint(X0, Y0, X1, Y1);
            }
            catch (DivideByZeroException)
            {
                return ;
            }
            
            if (PointInUpEdge(a, X0, Y0, X1, Y1) && !PointInUpEdge(b, X0, Y0, X1, Y1))
            {
                b = t;
            }
            else if (!PointInUpEdge(a, X0, Y0, X1, Y1) && PointInUpEdge(b, X0, Y0, X1, Y1))
            {
                a = t;
            }
            else if (!PointInUpEdge(a, X0, Y0, X1, Y1) && !PointInUpEdge(b, X0, Y0, X1, Y1))
            {
                //remove this line
                a = b = new Point(1, 1);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn static public bool PointInUpEdge(Point point, int x0, int y0, int x1, int y1)
        ///
        /// \brief Point in up edge
        ///
        /// \param point The point.
        /// \param x0    The x coordinate 0.
        /// \param y0    The y coordinate 0.
        /// \param x1    The first x value.
        /// \param y1    The first y value.
        ///
        /// \return True if it succeeds, false if it fails.
        ///-------------------------------------------------------------------------------------------------

        static public bool PointInUpEdge(Point point, int x0, int y0, int x1, int y1)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            return -(point.X - x0) * dy + (point.Y - y0) * dx > 0;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Clip(Rectangle rect)
        ///
        /// \brief 矩形框裁剪
        ///
        /// \param rect The rectangle.
        ///-------------------------------------------------------------------------------------------------

        public void Clip(Rectangle rect)
        {
            LineClip(rect.Left, rect.Bottom, rect.Left, rect.Top);
            LineClip(rect.Left, rect.Top, rect.Right, rect.Top);
            LineClip(rect.Right, rect.Top, rect.Right, rect.Bottom);
            LineClip(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
        }

    }
}
