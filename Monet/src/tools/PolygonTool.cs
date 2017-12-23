using Monet.src.history;
using Monet.src.shape;
using Monet.src.ui;
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
        Image doubleBuffer;

        public PolygonTool(PictureBox mainView) : base(mainView)
        {
            arrayList = new ArrayList();
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseClick += MainView_MouseClick;
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (arrayList.Count == 0)
                {
                    arrayList.Add(e.Location);
                    return;
                }
                arrayList.Add(e.Location);

                mainView.Image = (Image)mainView.Image.Clone();
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    // g.DrawLine(Setting.GetInstance().Pen, (Point)arrayList[arrayList.Count - 2], e.Location);
                    LineTool lineTool = (LineTool)ToolKit.GetInstance().lineTool;
                    lineTool.Draw(g, Setting.GetInstance().Pen, (Point)arrayList[arrayList.Count - 2], e.Location);
                }
            }
            else
            {
                mainView.Image = (Image)mainView.Image.Clone();
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    LineTool lineTool = (LineTool)ToolKit.GetInstance().lineTool;
                    lineTool.Draw(g, Setting.GetInstance().Pen, (Point)arrayList[arrayList.Count - 1], (Point)arrayList[0]);
                }


                //save into actions array.
                Polygon polygon = new Polygon();
                polygon.pen= Setting.GetInstance().Pen.Clone() as Pen;
                polygon.pointArray = new ArrayList(arrayList);
                Log.LogText(String.Format("Create Polygon"));
                History.GetInstance().PushBackAction(
                   new MAction(this, polygon));
            }
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
