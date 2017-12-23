using Monet.src.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.ui
{
    class ResizeRect
    {
        PictureBox mainView;
        Rectangle rect;
        Shape shape;
        Pen solidPen;
        MoveableButton NEButton;

        public ResizeRect(PictureBox pictureBox,
                          Rectangle rect,
                          Shape shape)
        {
            this.mainView = pictureBox;
            this.rect = rect;
            this.shape = shape;
            this.solidPen = new Pen(Color.Gray, 2);
            this.solidPen.DashStyle = DashStyle.Custom;
            solidPen.DashPattern = new float[] { 1f, 1f };
            NEButton = new MoveableButton(mainView, shape,new Point(rect.Right,rect.Top), Cursors.SizeNESW);
            NEButton.Show();
            //draw a new one
            mainView.Image = (Image)mainView.Image.Clone();
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                g.DrawRectangle(solidPen, rect);
            }
            NEButton.MouseDown += NEButton_MouseDown;
            NEButton.MouseMove += NEButton_MouseMove;
            NEButton.MouseUp += NEButton_MouseUp;
        }

        bool isResizing;

        private void NEButton_MouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NEButton_MouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NEButton_MouseDown(object sender, MouseEventArgs e)
        {
            isResizing=true;
        }

        public void Unshow()
        {
            try
            {
                this.NEButton.Visible = false;
            }
            catch (NullReferenceException)
            {
                ;
            }
            
        }
    }
}
