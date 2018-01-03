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
    class BSpline : Shape
    {
        public List<Point> pointArray = new List<Point>();

        List<AdjustButton> adjustButtons=new List<AdjustButton>();

        MoveButton moveButton;

        public  BSpline(List<Point> pointArray)
        {
            this.pointArray = pointArray;
        }

        protected BSpline()
        {
        }

        public override object Clone()
        {
            BSpline bSpline = new BSpline();
            bSpline.pointArray = new List<Point>(pointArray.ToArray());
            bSpline.pen = (Pen)pen.Clone();
            bSpline.backColor = backColor;
            return bSpline;
        }

        public override bool IsSelectMe(Point point)
        {
            if (Common.DistanceOf2Point(point, pointArray[0]) <= 5 || Common.DistanceOf2Point(point, pointArray[pointArray
                .Count - 1]) <= 5)
                return true;
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

            Log.LogText("Select BSpline");
        }
        Point moveButtonstartpoint;
        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
            Log.LogText(string.Format("Moving Bezier"));
            ShowAsNotSelected();
        }

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
        bool isResizing = false;
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

        private void AdjustButton_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            RetMAction().Action();
            Log.LogText(string.Format("Adjust Bezier"));
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
    }
}
