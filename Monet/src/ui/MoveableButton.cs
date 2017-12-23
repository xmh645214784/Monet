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
    public class MoveableButton : Button
    {

        PictureBox mainView;
        Shape shape;

        Point tempPoint;
        bool isEnabled = false;

        Point startLocation;

        public MoveableButton()
        {
        }

        public MoveableButton(PictureBox mainView,
                            Shape shape,
                            Point location,
                            Cursor cursor
                            ) : base()
        {

            this.mainView = mainView;
            this.Location = startLocation = location;
            this.shape = shape;
            this.Cursor = cursor;
            this.Size = new Size(10, 10);
            mainView.Controls.Add(this);
            this.Show();
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
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (isEnabled)
            {
                this.Location = new Point(this.Left + (mevent.X - tempPoint.X),
                        this.Top + (mevent.Y - tempPoint.Y));
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
