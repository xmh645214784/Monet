﻿using Monet.src.history;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    public class ResizeCanvasButton : Button,Actionable
    {
        PictureBox mainView;
        Point tempPoint;
        bool isEnabled;

        public ResizeCanvasButton(PictureBox mainView)
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

            ResizeParam actionParameters = new ResizeParam();
            actionParameters.backgroundColor = Setting.GetInstance().BackgroundColor;
            actionParameters.size = new Size(this.Left + (e.X - tempPoint.X),
                    this.Top + (e.Y - tempPoint.Y));
            History.GetInstance().PushBackAction(new MAction(this, actionParameters));

        }

        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if(isEnabled)
            {
                this.Location = new Point(this.Left + (e.X - tempPoint.X),
                    this.Top + (e.Y - tempPoint.Y));
                ResizeAction(new Size(this.Location),Setting.GetInstance().BackgroundColor);
            }
        }

        private void ResizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            tempPoint = e.Location;
            isEnabled = true;
        }

        void ResizeAction(Size size,Color backgroundColor)
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

        public void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                ResizeParam resizeParam = (ResizeParam)toolParameters;
                ResizeAction(resizeParam.size, resizeParam.backgroundColor);
                this.Location = new Point(resizeParam.size);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public sealed class ResizeParam:ActionParameters_t
        {
            public Size size;
            public Color backgroundColor;

            public object Clone()
            {
                ResizeParam copy = new ResizeParam();
                copy.size = size;
                copy.backgroundColor = backgroundColor;
                return copy;
            }
        }
    }
}
