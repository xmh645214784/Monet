using Monet.src.history;
using Monet.src.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.shape
{
    public class Circle : Shape
    {
        public Point startPoint;
        public Point endPoint;
        bool isResizing = false;

        public ResizeButton resizeButton;
        public override object Clone()
        {
            Circle copy = new Circle();
            copy.startPoint = startPoint;
            copy.endPoint = endPoint;
            copy.pen = (Pen)pen.Clone();
            return copy;
        }

        public override bool IsSelectMe(Point point)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(point.X - startPoint.X, 2) + Math.Pow(point.Y - startPoint.Y, 2))
                - Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2)))<10 ? true:false ;
        }

        public override void ShowAsNotSelected()
        {
            base.ShowAsNotSelected();
            try
            {
                resizeButton.Visible = false;
                resizeButton.Dispose();
            }
            catch (NullReferenceException)
            {
                ;
            }
            finally
            {
                resizeButton = null;

            }
        }

        public override void ShowAsSelected()
        {
            base.ShowAsSelected();
            if (resizeButton == null)
            {
                resizeButton = new ResizeButton(MainWin.GetInstance().MainView(), this, new Point(endPoint.X - 3, endPoint.Y - 3), Cursors.SizeAll, new Ref<Point>(() => endPoint, z => { endPoint = z; }));
                resizeButton.MouseDown += ResizeButton_MouseDown;
                resizeButton.MouseUp += ResizeButton_MouseUp;
                resizeButton.MouseMove += ResizeButton_MouseMove;
            }
            Log.LogText("Select Circle");
        }

        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                ToolKit.GetInstance().circleTool.MakeAction(RetMAction().ActionParameters);
            }
        }

        private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResizing)
                isResizing = false;
            Log.LogText(string.Format("Resize Circle R={0}",  Math.Sqrt(
                                    Math.Pow((startPoint.X-endPoint.X),2)
                                    + Math.Pow((startPoint.Y - endPoint.Y), 2)
                )
                ));
        }

        private void ResizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResizing = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
            }
        }
    }
}
