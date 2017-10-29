using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    interface IDrawLiner
    {
        void DrawLine(Graphics g, Pen pen,Point p1, Point p2);
    }

    sealed class Dda : IDrawLiner
    {
        private void DrawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            System.Diagnostics.Debug.Assert(pen.Width == 1, "Draw a line whose width not equal 1");
            int YDis = (p2.Y - p1.Y);
            int XDis = (p2.X - p1.X);
            int MaxStep = Math.Max(Math.Abs(XDis), Math.Abs(YDis));
            float fXUnitLen = 1.0f;  // X方向的单位步进  
            float fYUnitLen = 1.0f;  // Y方向的单位步进  
            fYUnitLen = YDis / (float)MaxStep;
            fXUnitLen = XDis / (float)MaxStep;
            // 设置起点像素颜色  
            Common.DrawPix(g, p1, pen);
            float x = p1.X;
            float y = p1.Y;
            // 循环步进  
            for (long i = 1; i <= MaxStep; i++)
            {
                x = x + fXUnitLen;
                y = y + fYUnitLen;
                Common.DrawPix(g, new Point((int)x, (int)y), pen);
            }
        }

        public void DrawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            
        }
    }
    sealed class SystemDraw : IDrawLiner
    {
        public void DrawLine(Graphics g,  Pen pen, Point p1, Point p2)
        {
            g.DrawLine(pen, p1, p2);
        }
    }


    sealed class LineTool : DrawShapeTool
    {
        public IDrawLiner lineAgent;
        Point startPoint;
        Point nowPoint;

        public LineTool(PictureBox mainView, Button button) : base(mainView,button)
        {
            lineAgent = new SystemDraw();
            isDrawing = false;
        }

        public override void Draw(ToolParameters toolParameters)
        {
            System.Diagnostics.Debug.Assert(toolParameters.coords.Length == 2);
            lineAgent.DrawLine(g, toolParameters.pen, toolParameters.coords[0], toolParameters.coords[1]);
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseDown  += MainView_MouseDown;
            mainView.MouseMove  += MainView_MouseMove;
            mainView.MouseUp    += MainView_MouseUp;
        }


        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown  -= MainView_MouseDown;
            mainView.MouseMove  -= MainView_MouseMove;
            mainView.MouseUp    -= MainView_MouseUp;
        }

        
        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            { 

                nowPoint = e.Location;
                lineAgent.DrawLine(g, SettingPanel.GetInstance().Pen, startPoint, nowPoint);
            }
        }
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                startPoint = e.Location;
                isDrawing = true;
            }
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                isDrawing = false;
            }
        }
    }
}
