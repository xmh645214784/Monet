///-------------------------------------------------------------------------------------------------
/// \file src\shape\Polygon.cs.
///
/// \brief Implements the polygon class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
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
    /// \class Polygon
    ///
    /// \brief A polygon.
    ///-------------------------------------------------------------------------------------------------

    class Polygon: Shape,Resizeable,Rotatable,Clipable
    {
        /// \brief Array of points
        public List<Point> pointArray = new List<Point>();
        
        /// \brief The adjust buttons
        ArrayList adjustButtons;
        /// \brief The move button
        MoveButton moveButton;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public Polygon()
        ///
        /// \brief Default constructor
        ///-------------------------------------------------------------------------------------------------

        public Polygon()
        {
            adjustButtons = new ArrayList();
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
            Polygon copy = new Polygon();
            copy.pointArray = new List<Point>(this.pointArray.ToArray());
            copy.pen = (Pen)this.pen.Clone();
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
            int length = pointArray.Count;
            for(int i=0;i<length;i++)
            {
                if(Line.DistanceOfPoint2Line((Point)pointArray[i%length],(Point)pointArray[(i+1)%length],point)<10)
                {
                    return true;
                }
            }
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
            int sumx = 0,sumy=0;
            for (int i=0;i<pointArray.Count;i++)
            {
                Point pointtemp = (Point)pointArray[i];
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

            Log.LogText("Select Polygon");
        }


        /// \brief True if this object is moving
        bool isMoving = false;
        /// \brief The move buttonstartpoint
        Point moveButtonstartpoint;

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
            if(isMoving==false)
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
            if(isMoving)
            {
                int offsetX = e.Location.X - moveButtonstartpoint.X;
                int offsetY = e.Location.Y - moveButtonstartpoint.Y;
                for (int i=0;i<pointArray.Count;i++)
                {
                    Point temp = (Point)pointArray[i];
                    pointArray[i] = new Point(temp.X + offsetX, temp.Y + offsetY);
                }

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
            isMoving = false;
            Log.LogText(string.Format("Moving Polygon"));
            ShowAsNotSelected();
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
            if(isResizing)
            {
                AdjustButton ins = (AdjustButton)sender;
                int index = adjustButtons.IndexOf(sender);
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
            Log.LogText(string.Format("Adjust Polygon"));
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

        ///-------------------------------------------------------------------------------------------------
        /// \fn public Rectangle ExternalRectangle()
        ///
        /// \brief External rectangle
        ///
        /// \return A Rectangle.
        ///-------------------------------------------------------------------------------------------------

        public Rectangle ExternalRectangle()
        {
            int minX  = 0xffffff, minY=0xffffff;
            int maxX = 0, maxY = 0;

            for (int i = 0; i < pointArray.Count; i++)
            {
                Point pointtemp = (Point)pointArray[i];
                minX=Math.Min(minX,pointtemp.X);
                maxX = Math.Max(maxX, pointtemp.X);
                minY = Math.Min(minY, pointtemp.Y);
                maxY = Math.Max(maxY, pointtemp.Y);
            }
            return Common.Rectangle(
                new Point(minX, minY),
                new Point(maxX, maxY));
        }


        /// \brief The resize rectangle
        ResizeRect resizeRect;
        /// \brief True to isresizing
        private bool isresizing=false;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void ShowAsResizing()
        ///
        /// \brief Shows as resizing
        ///-------------------------------------------------------------------------------------------------

        public void ShowAsResizing()
        {
            resizeRect=new ResizeRect(MainWin.GetInstance().MainView(), ExternalRectangle(), this);
            resizeRect.NEButton.MouseDown += NEButton_MouseDown;
            resizeRect.NEButton.MouseMove += NEButton_MouseMove;
            resizeRect.NEButton.MouseUp += NEButton_MouseUp;
        }

        /// \brief List of back up points
        private List<Point> backUpPointList = new List<Point>();

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void NEButton_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by NEButton for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void NEButton_MouseUp(object sender, MouseEventArgs e)
        {
            isresizing = false;
            backUpPointList.Clear();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void NEButton_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by NEButton for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void NEButton_MouseMove(object sender, MouseEventArgs e)
        {
            if(isresizing)
            {
                double xRate=resizeRect.rect.Width/(double)resizeRect.originalRect.Width;
                double yRate = resizeRect.rect.Height / (double)resizeRect.originalRect.Height;
                Point swPoint = new Point(resizeRect.rect.Left, resizeRect.rect.Bottom);
                for(int i=0;i<pointArray.Count;i++)
                {
                    pointArray[i] = resizeOnePoint(backUpPointList[i], xRate, yRate, swPoint);
                }
                this.RetMAction().Action();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn static Point resizeOnePoint(Point point,double xRate,double yRate,Point swPoint)
        ///
        /// \brief resize one point due to xRate and yRate & SW point
        ///
        /// \param point   The point.
        /// \param xRate   The rate.
        /// \param yRate   The rate.
        /// \param swPoint The software point.
        ///
        /// \return A Point.
        ///-------------------------------------------------------------------------------------------------

        static Point resizeOnePoint(Point point,double xRate,double yRate,Point swPoint)
        {
            int swx = swPoint.X;
            int swy = swPoint.Y;
            return new Point(
                (int)((point.X - swx) * xRate) + swx,
                (int)((point.Y - swy) * yRate) + swy);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void NEButton_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by NEButton for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void NEButton_MouseDown(object sender, MouseEventArgs e)
        {
            isresizing = true;
            backUpPointList = new List<Point>(pointArray.ToArray());
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void ShowAsNotResizing()
        ///
        /// \brief Shows as not resizing
        ///-------------------------------------------------------------------------------------------------

        public void ShowAsNotResizing()
        {
            try
            {
                resizeRect.Unshow();
            }
            catch (NullReferenceException)
            {
                ;
            }
            
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
            for (int i=0;i<pointArray.Count;i++)
            {
                pointArray[i] = Common.RotatingPoint((Point)pointArray[i], midPoint, angle);
                ((AdjustButton)adjustButtons[i]).Location
                    = Common.RotatingPoint(((AdjustButton)adjustButtons[i]).Location, midPoint, angle);
            }
            moveButton.Location = Common.RotatingPoint(moveButton.Location, midPoint, angle);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Clip(Rectangle rect)
        ///
        /// \brief Clips the given rectangle
        ///
        /// \param rect The rectangle.
        ///-------------------------------------------------------------------------------------------------

        public void Clip(Rectangle rect)
        {
            LineClip(new Point(rect.Left, rect.Bottom),new Point(rect.Left, rect.Top));
            LineClip(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top));
            LineClip(new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom));
            LineClip(new Point(rect.Right, rect.Bottom), new Point(rect.Left, rect.Bottom));
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void LineClip(Point a,Point b)
        ///
        /// \brief Line clip
        ///
        /// \param a A Point to process.
        /// \param b A Point to process.
        ///-------------------------------------------------------------------------------------------------

        private void LineClip(Point a,Point b)
        {
            List<Point> tempPointList = new List<Point>();
            for (int i = 0; i < pointArray.Count; i++)
            {
                Point p1 = pointArray[i % pointArray.Count];
                Point p2 = pointArray[(i+1) % pointArray.Count];
                if (Line.PointInUpEdge(p1, a.X, a.Y, b.X, b.Y) &&
                    Line.PointInUpEdge(p2, a.X, a.Y, b.X, b.Y))
                {
                    tempPointList.Add(p1);
                }
                else if (Line.PointInUpEdge(p1, a.X, a.Y, b.X, b.Y) &&
                    !Line.PointInUpEdge(p2, a.X, a.Y, b.X, b.Y))
                {
                    tempPointList.Add(p1);
                    tempPointList.Add(
                        Line.Line2LineIntersectionPoint(p1, p2,
                            new Point(a.X, a.Y), new Point(b.X, b.Y)
                        )
                    );
                }
                else if (!Line.PointInUpEdge(p1, a.X, a.Y, b.X, b.Y) &&
                    Line.PointInUpEdge(p2, a.X, a.Y, b.X, b.Y))
                {
                    tempPointList.Add(
                         Line.Line2LineIntersectionPoint(p1, p2,
                            new Point(a.X, a.Y), new Point(b.X, b.Y)
                        )
                        );
                }
            }
            pointArray = tempPointList;
        }
    }
}
