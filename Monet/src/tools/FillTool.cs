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
    sealed class FillTool : Tool
    {
        static Queue<Point> queue = new Queue<Point>(capacity: 1000000);
        public FillTool(PictureBox mainView, Button button) : base(mainView, button)
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
            if(e.Button==MouseButtons.Left)
            {
                newColor = Setting.GetInstance().FrontColor;
            }
            else
            {
                newColor = Setting.GetInstance().BackgroundColor;
            }
            queue.Clear();
            Bitmap bitmap = new Bitmap(mainView.Image);
            Color posColor = bitmap.GetPixel(e.Location.X,e.Location.Y);
            queue.Enqueue(e.Location);
            GraphicsUnit units = GraphicsUnit.Pixel;
            while (queue.Count != 0)
            {
                Point p = queue.Dequeue();
                //if out of bounds
                if (!bitmap.GetBounds(ref units).Contains(p))
                    continue;
                if (bitmap.GetPixel(p.X, p.Y) == posColor
                    && bitmap.GetPixel(p.X, p.Y) != newColor)
                {
                    bitmap.SetPixel(p.X, p.Y, newColor);
                    queue.Enqueue(new Point(p.X-1, p.Y));
                    queue.Enqueue(new Point(p.X, p.Y-1));
                    queue.Enqueue(new Point(p.X+1, p.Y));
                    queue.Enqueue(new Point(p.X, p.Y+1));
                }
            }
            mainView.Image = bitmap;
            History.GetInstance().PushBackAction(mainView.Image);
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseClick -= MainView_MouseClick;
        }
    }
}
