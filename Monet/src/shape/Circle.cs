///-------------------------------------------------------------------------------------------------
/// \file src\shape\Circle.cs.
///
/// \brief Implements the circle class
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
    /// \class Circle
    ///
    /// \brief A circle.
    ///-------------------------------------------------------------------------------------------------

    public class Circle : Shape,Resizeable
    {
        /// \brief The start point
        public Point startPoint;
        /// \brief The end point
        public Point endPoint;

        /// \brief The resize button
        public MoveableButtonWithDoubleBuffering resizeButton;
        /// \brief The move button
        public MoveableButtonWithDoubleBuffering moveButton;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override object Clone()
        ///
        /// \brief Makes a deep copy of this object
        ///
        /// \return A copy of this object.
        ///-------------------------------------------------------------------------------------------------

        public override object Clone()
        {
            Circle copy = new Circle();
            copy.startPoint = startPoint;
            copy.endPoint = endPoint;
            copy.pen = (Pen)pen.Clone();
            copy.backColor = backColor;
            return copy;
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
            return Math.Abs(Math.Sqrt(Math.Pow(point.X - startPoint.X, 2) + Math.Pow(point.Y - startPoint.Y, 2))
                - Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2)))<10 ? true:false ;
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
                resizeButton.Visible = moveButton.Visible= false;
                resizeButton.Dispose();
                moveButton.Dispose();
            }
            catch (NullReferenceException)
            {
                ;
            }
            finally
            {
                resizeButton = null;
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
            if (resizeButton == null)
            {
                resizeButton = new AdjustButton(MainWin.GetInstance().MainView(), this, new Point(endPoint.X - 3, endPoint.Y - 3), Cursors.SizeNS);
                resizeButton.SetBindingPoints(
                    new Ref<Point>(() => endPoint, z => { endPoint = z; })
                    );
                resizeButton.MouseDown += ResizeButton_MouseDown;
                resizeButton.MouseUp += ResizeButton_MouseUp;
                resizeButton.MouseMove += ResizeButton_MouseMove;

                moveButton = new MoveableButtonWithDoubleBuffering(MainWin.GetInstance().MainView(), this, new Point(startPoint.X - 3, startPoint.Y - 3), Cursors.SizeAll);
                moveButton.SetBindingPoints(
                    new Ref<Point>(() => startPoint, z => { startPoint = z; }),
                    new Ref<Point>(() => endPoint, z => { endPoint = z; }),
                    new Ref<Point>(() => resizeButton.Location, z => { resizeButton.Location = z; })
                    );
                moveButton.MouseDown += MoveButton_MouseDown;
                moveButton.MouseUp += MoveButton_MouseUp;
                moveButton.MouseMove += MoveButton_MouseMove;
            }
            Log.LogText("Select Circle");
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
        /// \fn private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MoveButton for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if(isMoving)
            {
                isMoving = false;
                Log.LogText(string.Format("Move Circle ({0},{1}),r={2}", startPoint.X, startPoint.Y,
                        Math.Sqrt(      Math.Pow((startPoint.X - endPoint.X), 2)
                                        + Math.Pow((startPoint.Y - endPoint.Y), 2))
                                        )
                                        );
                ShowAsNotSelected();
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
                RetMAction().Action();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by ResizeButton for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                RetMAction().Action();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by ResizeButton for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isAdjusting)
            {
                isAdjusting = false;
                Log.LogText(string.Format("Resize Circle R={0}", Math.Sqrt(
                                        Math.Pow((startPoint.X - endPoint.X), 2)
                                        + Math.Pow((startPoint.Y - endPoint.Y), 2)
                    )
                    ));
                ShowAsNotSelected();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void ResizeButton_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by ResizeButton for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void ResizeButton_MouseDown(object sender, MouseEventArgs e)
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
