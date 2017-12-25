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

    public class Line : Shape, Rotatable
    {
        /// \brief A Point to process
        public Point a;
        /// \brief A Point to process
        public Point b;
        

        public AdjustButton adjustButtonA;
        public AdjustButton adjustButtonB;
        public MoveButton moveButton;

        
        

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

        private void MoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                RetMAction().Action();
            }
        }

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

        private void ResizeButtonA_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                RetMAction().Action();
                moveButton.Location = new Point(a.X / 2 + b.X / 2, a.Y / 2 + b.Y / 2);
            } 
        }

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

        double preangle=0;
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
    }
 }
