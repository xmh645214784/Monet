using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    class BSplineTool : Tool
    {
        List<Point> controlPointsArray=new List<Point>();
        Image doubleBuffer;
        public BSplineTool(PictureBox mainView) : base(mainView)
        {
        }

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            throw new NotImplementedException();
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.MouseClick += MainView_MouseClick;
            mainView.MouseMove += MainView_MouseMove;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
           if(isDrawing)
            {
                mainView.Image.Dispose();
                mainView.Image = doubleBuffer.Clone() as Image;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, controlPointsArray);
                }
            }
        }
        bool isDrawing = false;
        bool isFirstClick = true;
        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            if(isFirstClick)
            {
                doubleBuffer = mainView.Image.Clone() as Image;
                isFirstClick = false;
            }
            if(e.Button==MouseButtons.Left)
            {
                isDrawing = true;
                controlPointsArray.Add(e.Location); 
            }
            else if(e.Button==MouseButtons.Right)
            {
                isDrawing = false;
            }
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
        }

        private  void Draw(Graphics g,List<Point> controlPointsArray)
        {
            int n = 1000;
            double deltaT = 1.0 / n;
            for (int num = 0; num < controlPointsArray.Count - 3; num++)
            {
                for (int i = 0; i <= n; i++)
                {
                    double T = i * deltaT;
                    double f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                    double f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                    double f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                    double f4 = (T * T * T) / 6.0;
                   Common.DrawPix(g,
                       (int)(f1 * controlPointsArray[num].X + f2 * controlPointsArray[num + 1].X + f3 * controlPointsArray[num + 2].X + f4 * controlPointsArray[num + 3].X),
                       (int)(f1 * controlPointsArray[num].Y + f2 * controlPointsArray[num + 1].Y + f3 * controlPointsArray[num + 2].Y + f4 * controlPointsArray[num + 3].Y), 
                               Setting.GetInstance().Pen);
                }
            }
        }
    }
}
