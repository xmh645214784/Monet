using Monet.src.history;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    class ResizeButton : Button,Actionable
    {
        PictureBox mainView;
        Point tempPoint;
        bool isEnabled;

        public ResizeButton(PictureBox mainView)
        {
            this.mainView = mainView;
            this.Location = new Point(mainView.Image.Width, mainView.Image.Height);
            this.Cursor = Cursors.SizeNWSE;
            this.isEnabled = false;
            mainView.Controls.Add(this);
            this.MouseDown += ResizeButton_MouseDown;
            this.MouseMove += ResizeButton_MouseMove;
            this.MouseUp += ResizeButton_MouseUp;

        }

        private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            isEnabled = false;

            ActionParameters actionParameters = new ActionParameters();
            actionParameters.backgroundColor = Setting.GetInstance().BackgroundColor;
            actionParameters.coords[0] = e.Location;
            History.GetInstance().PushBackAction(new MAction(this, actionParameters));

        }

        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if(isEnabled)
            {
                this.Location = new Point(this.Left + (e.X - tempPoint.X),
                    this.Top + (e.Y - tempPoint.Y));
                Resize(new Size(this.Location),Setting.GetInstance().BackgroundColor);
            }
        }

        private void ResizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            tempPoint = e.Location;
            isEnabled = true;
        }

        void Resize(Size size,Color backgroundColor)
        {
            Bitmap newBitmap =new Bitmap(size.Width,size.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.Clear(backgroundColor);
                g.DrawImage(mainView.Image, 0, 0);
            }
            mainView.Image.Dispose();
            mainView.Image = newBitmap;
        }

        public void MakeAction(ActionParameters toolParameters)
        {
            Resize(new Size(toolParameters.coords[0]), toolParameters.backgroundColor);
        }
    }
}
