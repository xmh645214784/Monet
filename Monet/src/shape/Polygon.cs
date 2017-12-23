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
    class Polygon: Shape
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
                foreach (ResizeButton item in resizeButtons)
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
                ResizeButton temp = new ResizeButton(
                    pictureBox, this, new Point(pointtemp.X - 3, pointtemp.Y - 3), Cursors.SizeNS);
                resizeButtons.Add(temp);

                temp.MouseDown += ResizeButton_MouseDown;
                temp.MouseUp += ResizeButton_MouseUp;
                temp.MouseMove += ResizeButton_MouseMove;

                sumx += pointtemp.X;
                sumy += pointtemp.Y;
            }

            ////set moveButton attributes
            //moveButton = new MoveButton(pictureBox, this,
            //    new Point(sumx / pointArray.Count, sumy / pointArray.Count),
            //    Cursors.SizeAll
            //    );
            //Ref<Point>[] bindingPointsRefArray
            //    = new Ref<Point>[pointArray.Count];
            //for (int i = 0; i < pointArray.Count; i++)
            //{
            //    bindingPointsRefArray[i]=new Ref<Point>(() => (Point)pointArray[i], z => { pointArray[i] = z; }); 
            //}
            //moveButton.SetBindingPoints(bindingPointsRefArray);

            Log.LogText("Select Polygon");
        }

        bool isResizing = false;
        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if(isResizing)
            {
                ResizeButton ins = (ResizeButton)sender;
                int index = resizeButtons.IndexOf(sender);
                pointArray[index] = ins.Location;
                RetMAction().Action();
            }
            
        }

        private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            RetMAction().Action();
            Log.LogText(string.Format("Reszie Polygon"));
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
                his.AddBackUpClone(mAction);
            }
        }

    }
}
