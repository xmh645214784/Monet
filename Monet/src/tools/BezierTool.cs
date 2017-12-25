using Monet.src.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    class BezierTool : Tool
    {
        List<Point> controlPointsArray = new List<Point>();
        Image doubleBuffer;
        bool isDrawing = false;
        bool isFirstClick = true;

        public BezierTool(PictureBox mainView) : base(mainView)
        {
        }
        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                Bezier bezier = new Bezier(controlPointsArray);
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, bezier.pointArray, bezier.pen);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Draw(Graphics g, List<Point> controlPointsArray,Pen pen)
        {
            for (int i = 0; i <= 1000; i++)
            {
                double t = i / 1000.0;
                double x = 0, y = 0, Ber;
                int k;
                for (k = 0; k < controlPointsArray.Count; k++)
                {
                    Ber = C(controlPointsArray.Count - 1, k) * Math.Pow(t, k) * Math.Pow(1 - t, controlPointsArray.Count - 1 - k);
                    x += controlPointsArray[k].X * Ber;
                    y += controlPointsArray[k].Y * Ber;
                }
                Common.DrawPix(g, new Point((int)x, (int)y),pen);
            }
        }

        private int factorial(int n)
        {
            if (n == 1 || n == 0)
            {
                return 1;
            }
            else
            {
                int sum = 1;
                for (int i = 2; i <= n; i++)
                    sum *= i;
                return sum;
            }
        }
        private double C(int n, int i)
        {
            return ((double)factorial(n)) / ((factorial(i) * factorial(n - i)));
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.MouseClick += MainView_MouseClick;
            mainView.MouseMove += MainView_MouseMove;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                mainView.Image.Dispose();
                mainView.Image = doubleBuffer.Clone() as Image;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, controlPointsArray, Setting.GetInstance().Pen);
                }
            }
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFirstClick)
            {
                doubleBuffer = mainView.Image.Clone() as Image;
                isFirstClick = false;
            }
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                controlPointsArray.Add(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;

                Bezier bezier = new Bezier(controlPointsArray);
                bezier.pen = Setting.GetInstance().Pen;
                History.GetInstance().PushBackAction(
                    new history.MAction(this,bezier)
                    );
            }
        }

        public override void UnRegisterTool()
        {
            mainView.MouseClick -= MainView_MouseClick;
            mainView.MouseMove -= MainView_MouseMove;
            base.UnRegisterTool();
        }
    }
}
