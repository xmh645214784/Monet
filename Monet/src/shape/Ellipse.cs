///-------------------------------------------------------------------------------------------------
/// \file src\shape\Ellipse.cs.
///
/// \brief Implements the ellipse class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
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
    /// \class Ellipse
    ///
    /// \brief An ellipse. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    public sealed class Ellipse : Shape,Rotatable,Resizeable
    {

        /// \brief The adjust button
        public AdjustButton adjustButton;
        /// \brief The move button
        public MoveButton moveButton;

        /// \brief The rectangle
        public Rectangle rect;

        /// \brief The angle
        public double angle = 0.0F;
        /// \brief The middle point
        public Point midPoint;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override object Clone()
        ///
        /// \brief Makes a deep copy of this object
        ///
        /// \return A copy of this object.
        ///-------------------------------------------------------------------------------------------------

        public override object Clone()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.rect = this.rect;
            ellipse.pen = this.pen.Clone() as Pen;
            ellipse.midPoint = this.midPoint;
            ellipse.angle = this.angle;
            ellipse.backColor = this.backColor;
            return ellipse;
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
            point = Common.RotatingPoint(point, midPoint, -angle);

            bool isVertical = rect.Width < rect.Height;
            double ellipseA = Math.Max(rect.Width / 2.0, rect.Height / 2.0);
            double ellipseB = Math.Min(rect.Width / 2.0, rect.Height / 2.0);
            double ellipseC = Math.Sqrt(ellipseA * ellipseA - ellipseB * ellipseB);
            Point focusA, focusB;//焦点位置
            if (isVertical)
            {
                focusA = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 + (int)ellipseC);
                focusB = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 - (int)ellipseC);
            }
            else
            {
                focusA = new Point(rect.X + rect.Width / 2 + (int)ellipseC, rect.Y + rect.Height / 2 );
                focusB = new Point(rect.X + rect.Width / 2 - (int)ellipseC, rect.Y + rect.Height / 2 );
            }
            return Math.Abs(DisOfPoint2Point(point, focusA) + DisOfPoint2Point(point, focusB)-2*ellipseA)<5;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn static double DisOfPoint2Point(Point a,Point b)
        ///
        /// \brief Dis of point 2 point
        ///
        /// \param a A Point to process.
        /// \param b A Point to process.
        ///
        /// \return A double.
        ///-------------------------------------------------------------------------------------------------

        static double DisOfPoint2Point(Point a,Point b)
        {
            return Math.Sqrt(
                Math.Pow(a.X-b.X,2)+ Math.Pow(a.Y - b.Y, 2)
                );
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
                adjustButton.Visible = moveButton.Visible = false;
                adjustButton.Dispose();
                moveButton.Dispose();
            }
            catch (NullReferenceException)
            {
                ;
            }
            finally
            {
                adjustButton = null;
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
            if (adjustButton == null)
            {
                adjustButton = new AdjustButton(MainWin.GetInstance().MainView(), this, 
                    Common.RotatingPoint(new Point(rect.Right,rect.Top),midPoint,angle), 
                    Cursors.SizeNS);
                adjustButton.MouseDown += AdjustButton_MouseDown;
                adjustButton.MouseUp += AdjustButton_MouseUp;
                adjustButton.MouseMove += AdjustButton_MouseMove;
            }
            if (moveButton == null)
            {
                moveButton = new MoveButton(MainWin.GetInstance().MainView(),
                    this, 
                    Common.RotatingPoint(new Point(rect.Left +rect.Width / 2, rect.Top+rect.Height/2),midPoint,angle),
                    Cursors.SizeAll);
                moveButton.MouseDown += MoveButton_MouseDown;
                moveButton.MouseMove += MoveButton_MouseMove;
                moveButton.MouseUp += MoveButton_MouseUp;
                moveButton.SetBindingPoints(
                    new Ref<Point>(()=>rect.Location,z=> {rect.Location= Common.RotatingPoint(z,midPoint,-angle); }     ),
                    new Ref<Point>(() => adjustButton.Location, z => { adjustButton.Location = z; })
                    );
            }
            Log.LogText(string.Format("Select Ellipse ({0},{1}),({2},{3})", rect.Left, rect.Top, rect.Right, rect.Bottom));
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
                Log.LogText(string.Format("Move Ellipse ({0},{1}),({2},{3})", rect.Left, rect.Top, rect.Right, rect.Bottom));
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
                moveButton.Location = Common.RotatingPoint(
                    new Point(rect.X+rect.Width/2,rect.Y+rect.Height/2),midPoint,angle
                    );
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
                adjustButton.Visible = false;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }

        /// \brief True if this object is adjusting
        bool isAdjusting=false;

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void AdjustButton_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by AdjustButton for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void AdjustButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isAdjusting = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void AdjustButton_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by AdjustButton for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void AdjustButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                isAdjusting = false;
                RetMAction().Action();
                Log.LogText(string.Format("Adjust Ellipse ({0},{1}),({2},{3})",rect.Left,rect.Top,rect.Right,rect.Bottom));
                ShowAsNotSelected();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void AdjustButton_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by AdjustButton for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void AdjustButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                rect = Common.Rectangle(new Point(rect.Left, rect.Bottom), Common.RotatingPoint(adjustButton.Location,midPoint,-angle));
                moveButton.Location = Common.RotatingPoint(
                    new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2),
                    midPoint,angle
                    );
                RetMAction().Action();      
            }
        }

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
            this.angle = angle;
            this.midPoint = midPoint;
            this.ShowAsNotSelected();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void ShowAsResizing()
        ///
        /// \brief Shows as resizing
        ///-------------------------------------------------------------------------------------------------

        public void ShowAsResizing()
        {
            ShowAsSelected();            
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void ShowAsNotResizing()
        ///
        /// \brief Shows as not resizing
        ///-------------------------------------------------------------------------------------------------

        public void ShowAsNotResizing()
        {
            ShowAsNotSelected();            
        }
    }
}
