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

        

        public Line()
        {
            isResizing = false;
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

        public override void ShowAsNotSelected()
        {
            try
            {
                resizeButtonA.Visible = resizeButtonB.Visible=false;
                resizeButtonA.Dispose();
                resizeButtonB.Dispose();
            }
            catch(NullReferenceException)
            {
                ;
            }
            finally
            {
                resizeButtonA = resizeButtonB = null;
            }
        }

        public override void ShowAsSelected()
        {
            if(resizeButtonA==null)
            {
                resizeButtonA = new ResizeButton(MainWin.GetInstance().MainView(), this,new Point(a.X - 3, a.Y - 3), Cursors.Cross, new Ref<Point>(() => a, z => { a = z; }));
                resizeButtonA.MouseDown += ResizeButtonA_MouseDown;
                resizeButtonA.MouseUp += ResizeButtonA_MouseUp;
                resizeButtonA.MouseMove += ResizeButtonA_MouseMove;
            }
            if (resizeButtonB == null)
            {
                resizeButtonB = new ResizeButton(MainWin.GetInstance().MainView(), this,new Point(b.X - 3, b.Y - 3), Cursors.Cross, new Ref<Point>(() => a, z => { b = z; }));
                resizeButtonB.MouseDown += ResizeButtonA_MouseDown;
                resizeButtonB.MouseUp += ResizeButtonA_MouseUp;
                resizeButtonB.MouseMove += ResizeButtonA_MouseMove;
            }
        }


        private void ResizeButtonA_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                isResizing = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }

        }

        private void ResizeButtonA_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                ToolKit.GetInstance().lineTool.MakeAction(RetMAction().ActionParameters);
            } 
        }

        private void ResizeButtonA_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            
        }

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

        public override object Clone()
        {
            Line copy = new Line();
            copy.a = a;
            copy.b = b;
            copy.pen = (Pen)pen.Clone();
            return copy;
        }
    }
 }
