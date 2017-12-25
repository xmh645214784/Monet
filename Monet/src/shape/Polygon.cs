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
    class Polygon: Shape,Resizeable,Rotatable,Clipable
    {
        public List<Point> pointArray = new List<Point>();
        
        ArrayList adjustButtons;
        MoveButton moveButton;

        public Polygon()
        {
            adjustButtons = new ArrayList();
        }

        public override object Clone()
        {
            Polygon copy = new Polygon();
            copy.pointArray = new List<Point>(this.pointArray.ToArray());
            copy.pen = (Pen)this.pen.Clone();
            copy.backColor = backColor;
            return copy;
        }

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


        bool isMoving = false;
        Point moveButtonstartpoint;
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

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
            Log.LogText(string.Format("Moving Polygon"));
            ShowAsNotSelected();
        }

        bool isResizing = false;
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

        private void AdjustButton_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            RetMAction().Action();
            Log.LogText(string.Format("Adjust Polygon"));
            ShowAsNotSelected();
        }

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


        ResizeRect resizeRect;
        private bool isresizing=false;
        public void ShowAsResizing()
        {
            resizeRect=new ResizeRect(MainWin.GetInstance().MainView(), ExternalRectangle(), this);
            resizeRect.NEButton.MouseDown += NEButton_MouseDown;
            resizeRect.NEButton.MouseMove += NEButton_MouseMove;
            resizeRect.NEButton.MouseUp += NEButton_MouseUp;
        }

        private List<Point> backUpPointList = new List<Point>();
    
        private void NEButton_MouseUp(object sender, MouseEventArgs e)
        {
            isresizing = false;
            backUpPointList.Clear();
        }

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



        //resize one point due to xRate and yRate & SW point
        static Point resizeOnePoint(Point point,double xRate,double yRate,Point swPoint)
        {
            int swx = swPoint.X;
            int swy = swPoint.Y;
            return new Point(
                (int)((point.X - swx) * xRate) + swx,
                (int)((point.Y - swy) * yRate) + swy);
        }

        private void NEButton_MouseDown(object sender, MouseEventArgs e)
        {
            isresizing = true;
            backUpPointList = new List<Point>(pointArray.ToArray());
        }

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
            for (int i=0;i<pointArray.Count;i++)
            {
                pointArray[i] = Common.RotatingPoint((Point)pointArray[i], midPoint, angle);
                ((AdjustButton)adjustButtons[i]).Location
                    = Common.RotatingPoint(((AdjustButton)adjustButtons[i]).Location, midPoint, angle);
            }
            moveButton.Location = Common.RotatingPoint(moveButton.Location, midPoint, angle);
        }

        public void Clip(Rectangle rect)
        {
            LineClip(new Point(rect.Left, rect.Bottom),new Point(rect.Left, rect.Top));
            LineClip(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top));
            LineClip(new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom));
            LineClip(new Point(rect.Right, rect.Bottom), new Point(rect.Left, rect.Bottom));
        }

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
