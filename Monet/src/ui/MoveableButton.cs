using Monet.src.history;
using Monet.src.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.ui
{
    public class MoveableButton:Button
    {
        // 这个点非常重要，我们改变的是下面的数组，每个点都会跟着偏移一些距离
        Ref<Point>[] bindingPoints;
        Point[] backUpPoints;

        PictureBox mainView;
        Shape shape;

        Point tempPoint;
        bool isEnabled = false;

        Point startLocation;

        protected Image doubleBuffer;


        public MoveableButton(PictureBox mainView,
                            Shape shape,
                            Point location,
                            Cursor cursor
                            )
        {
            this.mainView = mainView;
            this.Location = startLocation= location;
            this.shape = shape;
            this.Cursor = cursor;
            this.Size = new Size(10, 10);
            mainView.Controls.Add(this);
            this.Show(); 
        }

        public void SetBindingPoints(params Ref<Point>[] bindingPoints)
        {
            this.bindingPoints = bindingPoints;
            backUpPoints = new Point[bindingPoints.Length];
            for (int i = 0; i < backUpPoints.Length; i++)
            {
                backUpPoints[i] = bindingPoints[i].Value;
            }
        }

        public void Disappear()
        {
            this.Enabled = false;
            this.Visible = false;
            mainView.Controls.Remove(this);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tempPoint = e.Location;
                isEnabled = true;
                History history = History.GetInstance();
                MAction mAction;
                history.FindShapeInHistory(shape, out mAction);
                mAction.visible = false;
                history.Update();
                doubleBuffer = (Image)mainView.Image.Clone();
                doubleBuffer.Save("1.png");
                mAction.visible = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (isEnabled)
            {
                this.Location = new Point(this.Left + (mevent.X - tempPoint.X),
                        this.Top + (mevent.Y - tempPoint.Y));
                try
                {
                    for (int i = 0; i < bindingPoints.Length; i++)
                    {
                        bindingPoints[i].Value = new Point
                            (backUpPoints[i].X + Location.X - startLocation.X,
                            backUpPoints[i].Y + this.Location.Y - startLocation.Y);
                    }
                }
                catch (NullReferenceException)
                {
                    ;
                }
               
                mainView.Image.Dispose();
                mainView.Image = (Image)doubleBuffer.Clone();
            }
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                isEnabled = false;
            }
            base.OnMouseUp(mevent);
            History.GetInstance().Update();
        }
    }
}
