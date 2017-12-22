using Monet.src.shape;
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
    class PolygonTool: DrawShapeTool
    {
        ArrayList arrayList;

        public PolygonTool(PictureBox mainView) : base(mainView)
        {
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseClick += MainView_MouseClick;
            mainView.MouseDoubleClick += MainView_MouseDoubleClick;
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            if (arrayList.Count == 0)
            {
                arrayList.Add(e.Location);
                return;
            }
            arrayList.Add(e.Location);
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                LineTool lineTool=(LineTool)ToolKit.GetInstance().lineTool;
                lineTool.Draw(g, Setting.GetInstance().Pen, (Point)arrayList[arrayList.Count-2], e.Location);
            }
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseDoubleClick -= MainView_MouseDoubleClick;
        }
        private void MainView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                LineTool lineTool = (LineTool)ToolKit.GetInstance().lineTool;
                lineTool.Draw(g, Setting.GetInstance().Pen, (Point)arrayList[arrayList.Count - 1], (Point)arrayList[0]);
            }
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
        }
        
       

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                Polygon polygon = (Polygon)toolParameters;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    LineTool lineTool = (LineTool)ToolKit.GetInstance().lineTool;
                    int length = polygon.pointArray.Count;
                    for (int i = 0; i < length; i++)
                    {
                        lineTool.Draw(g, polygon.pen, (Point)arrayList[i%length], (Point)arrayList[(i+1)%length]);
                    }
                }
            }
            catch (InvalidCastException)
            {
                throw;
            }
        }
    }
}
