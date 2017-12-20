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
    

    public class ResizeButton : Button
    {

        // 这个点非常重要，我们改变的是
        Ref<Point> bindingPoint;

        PictureBox mainView;
        Shape shape;

        Point tempPoint;
        bool isEnabled=false;

        Point startLocation,resultLocation;

        protected Image doubleBuffer;

        public Point ResultLocation { get => resultLocation; }

        public ResizeButton(PictureBox mainView,
                            Shape shape,
                            Point location,
                            Cursor cursor,
                            Ref<Point> bindingPoint)
        {
            this.mainView = mainView;
            this.Location = location;
            this.shape = shape;
            this.Size = new Size(8, 8);
            mainView.Controls.Add(this);
            this.Show();
            this.bindingPoint = bindingPoint;
        }

        public void Disappear()
        {
            this.Enabled = false;
            this.Visible = false;
            mainView.Controls.Remove(this);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                resultLocation = this.Location;
                tempPoint = e.Location;
                isEnabled = true;
                History history = History.GetInstance();
                MAction mAction;
                history.FindShapeInHistory(shape, out mAction);
                mAction.visible = false;
                history.Update();
                doubleBuffer = (Image)mainView.Image.Clone();
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
                resultLocation = this.Location;
                bindingPoint.Value=this.Location;
                mainView.Image.Dispose();
                mainView.Image = (Image)doubleBuffer.Clone();
            }
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                resultLocation = this.Location;
                bindingPoint.Value = this.Location;
                isEnabled = false;
            }
            else
            {
                resultLocation = startLocation;
                bindingPoint.Value = startLocation;
                isEnabled = false;
            }
            base.OnMouseUp(mevent);
        }
    }
}
