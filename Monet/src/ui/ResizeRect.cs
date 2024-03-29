﻿using Monet.src.shape;
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
    public class ResizeRect
    {
        protected PictureBox mainView;
        public Rectangle rect;
        public Rectangle originalRect;
        protected Point rectSWPoint;
        protected Shape shape;
        protected Pen solidPen;
        public MoveableButtonWithDoubleBuffering NEButton;

        public ResizeRect(PictureBox pictureBox,
                          Rectangle rect,
                          Shape shape)
        {
            this.mainView = pictureBox;
            this.rect = rect;
            this.originalRect = rect;
            this.shape = shape;
            this.solidPen = new Pen(Color.Gray, 2);
            this.solidPen.DashStyle = DashStyle.Custom;
            solidPen.DashPattern = new float[] { 1f, 1f };
            NEButton = new MoveableButtonWithDoubleBuffering(mainView, shape,new Point(rect.Right,rect.Top), Cursors.SizeNESW);
            NEButton.Show();
            //draw a new one
            mainView.Image = (Image)mainView.Image.Clone();
            rectSWPoint = new Point(rect.Left, rect.Bottom);
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                g.DrawRectangle(solidPen, rect);
            }
            NEButton.MouseDown += NEButton_MouseDown;
            NEButton.MouseMove += NEButton_MouseMove;
            NEButton.MouseUp += NEButton_MouseUp;
        }

        protected bool isResizing =false;

        public virtual void  NEButton_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            Unshow();
        }

        public virtual void NEButton_MouseMove(object sender, MouseEventArgs e)
        {
            if(isResizing)
            {
                rect = Common.Rectangle(rectSWPoint, NEButton.Location);
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    g.DrawRectangle(solidPen, rect);
                }
            }
        }

        public virtual void NEButton_MouseDown(object sender, MouseEventArgs e)
        {
            isResizing=true;
            this.originalRect = rect;
            History.GetInstance().AddBackUpClone(shape.RetMAction());
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
