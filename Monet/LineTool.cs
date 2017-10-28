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
        void drawLine(Graphics g, Pen pen,Point p1, Point p2);
    }

    sealed class Dda : IDrawLiner
    {
        private void drawWidEqOneLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            System.Diagnostics.Debug.Assert(pen.Width == 1, "Draw a line whose width not equal 1");
            int YDis = (p2.Y - p1.Y);
            int XDis = (p2.X - p1.X);
            int MaxStep = Math.Max(Math.Abs(XDis), Math.Abs(YDis));
            float fXUnitLen = 1.0f;  // X方向的单位步进  
            float fYUnitLen = 1.0f;  // Y方向的单位步进  
            fYUnitLen = static_cast<float>(YDis) / static_cast<float>(MaxStep);
            fXUnitLen = static_cast<float>(XDis) / static_cast<float>(MaxStep);
            // 设置起点像素颜色  
            Common.DrawPix(g, p1, pen);
            float x = p1.X;
            float y = p1.Y;
            // 循环步进  
            for (long i = 1; i <= MaxStep; i++)
            {
                x = x + fXUnitLen;
                y = y + fYUnitLen;
                Common.DrawPix(g, new Point(x, y), pen);
            }
        }

        public void drawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            
        }
    }
    sealed class SystemDraw : IDrawLiner
    {
        public void drawLine(Graphics g,  Pen pen, Point p1, Point p2)
        {
            g.DrawLine(pen, p1, p2);
        }
    }


    sealed class LineTool : DrawShapeTool
    {
        IDrawLiner lineagent;
        public override void Draw()
        {
          throw new NotImplementedException();
        }

        public override void RegisterTool()
        {
            throw new NotImplementedException();
        }

        public override void UnRegisterTool()
        {
            panel.Cursor = Cursors.Default;
        }
    }
}
