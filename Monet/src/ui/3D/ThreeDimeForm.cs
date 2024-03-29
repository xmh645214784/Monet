﻿using Monet.src.ui._3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.ui
{
    public partial class ThreeDimeForm : Form
    {
        const int CanvasWidth = 600; //须添加const 或static
        const int CanvasHeight = 400;
        Rectangle myRec = new Rectangle(0, 0, CanvasWidth, CanvasHeight);
        ColorCube2 myCube = new ColorCube2(CanvasWidth, CanvasHeight, 0.8f);//参数：画布宽，高，大小比例
        int prevX, prevY;
        Point point;//按下鼠标时,光标的坐标

        private void threeDime_MouseDown(object sender, MouseEventArgs e)
        {
            prevX = e.X;
            prevY = e.Y;
        }

        private void threeDime_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;
                //鼠标左右移动(x轴坐标改变),绕Y轴转动. 鼠标移动的距离等于显示区域宽度时,转动180度
                double thetaY = -(x - prevX) * 180.0f / myRec.Width;
                //鼠标上下移动(Y轴坐标改变),绕X轴转动 .鼠标移动的距离等于显示区域高度时,转动180度
                double thetaX = (y - prevY) * 180.0f / myRec.Height;
                myCube.CubeRotate(thetaX, thetaY, 0);
                myCube.CalcScrPts((double)CanvasWidth / 2, (double)CanvasHeight / 2, 0);
                prevX = x; //此两句很关键,每计算完一次,都须将当前点作为起始点
                prevY = y;
                ReDraw();
            }
        }

        public ThreeDimeForm()
        {
            InitializeComponent();
            this.Width = CanvasWidth;
            this.Height = CanvasHeight;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ThreeDimeForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ThreeDimeForm";
            this.Paint += new PaintEventHandler(this.ThreeDimeForm_Paint);
            this.MouseDown += new MouseEventHandler(threeDime_MouseDown);
            this.MouseMove += new MouseEventHandler(threeDime_MouseMove);
            this.ResumeLayout(false);
            this.Width = CanvasWidth;
            this.Height = CanvasHeight;

        }

        private void ThreeDimeForm_Paint(object sender, PaintEventArgs e)
        {
            ReDraw();
        }

        protected void ReDraw()
        {
            Image myImage = new Bitmap(myRec.Width, myRec.Height);
            Graphics my_g = Graphics.FromImage(myImage);//创建一个在myImage中绘图的Graphics
            Graphics displayGraphics = this.CreateGraphics();//创建一个屏幕中绘图的Graphics
            my_g.SmoothingMode = SmoothingMode.AntiAlias;//消除锯齿须在前面加：using System.Drawing.Drawing2D;
            my_g.FillRectangle(Brushes.White, myRec); //每次绘画前，应先将原内存中的Image画面清空
            myCube.MyDraw(my_g, myRec.Width, myRec.Height);//在内存中的Image绘制当前的正方体
            displayGraphics.DrawImage(myImage, myRec); // 将内存中的Image绘制在屏幕上
            myImage.Dispose();
            displayGraphics.Dispose();
        }
    }
}
