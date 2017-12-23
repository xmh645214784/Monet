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
    class Polygon: Shape,Resizeable,Rotatable
    {
        public ArrayList pointArray;
        ArrayList resizeButtons;
        MoveButton moveButton;

        public Polygon()
        {
            resizeButtons = new ArrayList();
        }

        public override object Clone()
        {
            Polygon copy = new Polygon();
            copy.pointArray = (ArrayList)pointArray.Clone();
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
                foreach (AdjustButton item in resizeButtons)
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
                resizeButtons.Clear();
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
                resizeButtons.Add(temp);

                temp.MouseDown += ResizeButton_MouseDown;
                temp.MouseUp += ResizeButton_MouseUp;
                temp.MouseMove += ResizeButton_MouseMove;

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
                foreach (AdjustButton item in resizeButtons)
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
        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if(isResizing)
            {
                AdjustButton ins = (AdjustButton)sender;
                int index = resizeButtons.IndexOf(sender);
                pointArray[index] = ins.Location;
                RetMAction().Action();
            }
            
        }

        private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            RetMAction().Action();
            Log.LogText(string.Format("Adjust Polygon"));
            ShowAsNotSelected();
        }

        private void ResizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResizing = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                this.ShowAsNotSelected();
                his.AddBackUpClone(mAction);
                this.ShowAsSelected();
                
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

        public void ShowAsResizing()
        {
            resizeRect=new ResizeRect(MainWin.GetInstance().MainView(), ExternalRectangle(), this);
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
        double preangle;
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
                ((AdjustButton)resizeButtons[i]).Location
                    = Common.RotatingPoint(((AdjustButton)resizeButtons[i]).Location, midPoint, angle);
            }
            moveButton.Location = Common.RotatingPoint(moveButton.Location, midPoint, angle);
        }
    }
}
