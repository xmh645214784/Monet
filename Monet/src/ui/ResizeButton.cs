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
        PictureBox mainView;
        Point tempPoint;
        bool isEnabled=false;
        Point resultPoint;

        public ResizeButton(PictureBox mainView,
                            Point point,
                            Cursor cursor)
        {
            this.mainView = mainView;
            this.Location = point;
            this.Size = new Size(8, 8);
            mainView.Controls.Add(this);
            this.Show();
        }

        public void Disappear()
        {
            this.Enabled = false;
            mainView.Controls.Remove(this);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if(e.Button==MouseButtons.Left)
            {
                tempPoint = e.Location;
                isEnabled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (isEnabled)
            {
                this.Location = new Point(this.Left + (mevent.X - tempPoint.X),
                        this.Top + (mevent.Y - tempPoint.Y));
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                resultPoint = mevent.Location;
                isEnabled = false;
            }
            else
            {
                resultPoint = tempPoint;
                isEnabled = false;
            }
        }
    }
}
