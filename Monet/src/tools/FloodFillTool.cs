using Monet.src.history;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    sealed class FloodFillTool : Tool
    {
        static Queue<Point> queue = new Queue<Point>(capacity: 1000000);
        public FloodFillTool(PictureBox mainView) : base(mainView)
        {
        }
        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Hand;
            mainView.MouseClick += MainView_MouseClick;
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            Color newColor;
            if (e.Button == MouseButtons.Left)
            {
                newColor = Setting.GetInstance().FrontColor;
            }
            else
            {
                newColor = Setting.GetInstance().BackgroundColor;
            }
            FillAction(e.Location, newColor);
        }

        private void FillAction(Point p,Color newColor)
        {
            queue.Clear();
            Bitmap bitmap = new Bitmap(mainView.Image);
            Color posColor = bitmap.GetPixel(p.X, p.Y);
            queue.Enqueue(p);
            GraphicsUnit units = GraphicsUnit.Pixel;
            while (queue.Count != 0)
            {
                Point q = queue.Dequeue();
                //if out of bounds
                if (!bitmap.GetBounds(ref units).Contains(q))
                    continue;
                if (bitmap.GetPixel(q.X, q.Y) == posColor
                    && bitmap.GetPixel(q.X, q.Y) != newColor)
                {
                    bitmap.SetPixel(q.X, q.Y, newColor);
                    queue.Enqueue(new Point(q.X - 1, q.Y));
                    queue.Enqueue(new Point(q.X, q.Y - 1));
                    queue.Enqueue(new Point(q.X + 1, q.Y));
                    queue.Enqueue(new Point(q.X, q.Y + 1));
                }
            }
            mainView.Image = bitmap;
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseClick -= MainView_MouseClick;
        }



        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                FloodFillParam fillParam = (FloodFillParam)toolParameters;
                FillAction(fillParam.point, fillParam.color);
            }
            catch (InvalidCastException)
            {

                throw;
            }
            
        }
        private sealed class FloodFillParam : ActionParameters_t
        {
            public readonly Point point;
            public readonly Color color;
            public FloodFillParam(Point point,Color color)
            {
                this.point = point;
                this.color = color;
            }

            public object Clone()
            {
                return new FloodFillParam(point, color);
            }
        }
    }


}
