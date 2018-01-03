///-------------------------------------------------------------------------------------------------
/// \file src\shape\bezier.cs.
///
/// \brief Implements the bezier class
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
    /// \class Bezier
    ///
    /// \brief A bezier.
    ///-------------------------------------------------------------------------------------------------

    class Bezier : Shape
    {
        /// \brief Array of points
        public List<Point> pointArray = new List<Point>();

        /// \brief The adjust buttons
        List<AdjustButton> adjustButtons = new List<AdjustButton>();

        /// \brief The move button
        MoveButton moveButton;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public Bezier(List<Point> pointArray)
        ///
        /// \brief Constructor
        ///
        /// \param pointArray Array of points.
        ///-------------------------------------------------------------------------------------------------

        public Bezier(List<Point> pointArray)
        {
            this.pointArray = pointArray;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn protected Bezier()
        ///
        /// \brief Specialised default constructor for use only by derived class
        ///-------------------------------------------------------------------------------------------------

        protected Bezier()
        {
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
            Bezier bezier = new Bezier();
            bezier.pointArray = new List<Point>(pointArray.ToArray());
            bezier.pen = (Pen)pen.Clone();
            bezier.backColor = backColor;
            return bezier;
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
            if (Common.DistanceOf2Point(point, pointArray[0]) <= 5 || Common.DistanceOf2Point(point, pointArray[pointArray
                .Count-1]) <= 5)
                return true;
            return false;
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
                foreach (AdjustButton item in adjustButtons)
                {
                    item.Visible = false;
                    item.Dispose();
                }
                moveButton.Visible = false;
                moveButton.Dispose();
            }
            catch (NullReferenceException)
            {
                ;
            }
            finally
            {
                adjustButtons.Clear();
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
            PictureBox pictureBox = MainWin.GetInstance().MainView();
            int sumx = 0, sumy = 0;
            for (int i = 0; i < pointArray.Count; i++)
            {
                Point pointtemp = pointArray[i];
                AdjustButton temp = new AdjustButton(
                    pictureBox, this, new Point(pointtemp.X - 3, pointtemp.Y - 3), Cursors.SizeNS);
                adjustButtons.Add(temp);

                temp.MouseDown += AdjustButton_MouseDown;
                temp.MouseUp += AdjustButton_MouseUp;
                temp.MouseMove += AdjustButton_MouseMove;

                sumx += pointtemp.X;
                sumy += pointtemp.Y;
            }

            //set moveButton attributes
            moveButton = new MoveButton(pictureBox, this,
                new Point(sumx / pointArray.Count, sumy / pointArray.Count),
                Cursors.SizeAll
                );
            moveButton.MouseDown += MoveButton_MouseDown;
            moveButton.MouseMove += MoveButton_MouseMove;
            moveButton.MouseUp += MoveButton_MouseUp;

            Log.LogText("Select Bezier");
        }

        /// \brief True if this object is moving
        bool isMoving = false;
        /// \brief The move buttonstartpoint
        Point moveButtonstartpoint;

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
            isMoving = false;
            Log.LogText(string.Format("Moving Bezier"));
            ShowAsNotSelected();
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
                int offsetX = e.Location.X - moveButtonstartpoint.X;
                int offsetY = e.Location.Y - moveButtonstartpoint.Y;
                for (int i = 0; i < pointArray.Count; i++)
                {
                    Point temp = (Point)pointArray[i];
                    pointArray[i] = new Point(temp.X + offsetX, temp.Y + offsetY);
                }

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
            if (isMoving == false)
            {
                isMoving = true;
                moveButtonstartpoint = e.Location;
                foreach (AdjustButton item in adjustButtons)
                {
                    item.Visible = false;
                    item.Dispose();
                }
            }
        }
        /// \brief True if this object is resizing
        bool isResizing = false;

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
            if (isResizing)
            {
                AdjustButton ins = (AdjustButton)sender;
                int index = adjustButtons.IndexOf((AdjustButton)sender);
                pointArray[index] = ins.Location;
                RetMAction().Action();
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
            isResizing = false;
            RetMAction().Action();
            Log.LogText(string.Format("Adjust Bezier"));
            ShowAsNotSelected();
        }

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
                isResizing = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }
    }
}
