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

        public MoveableButton resizeButton;
        public MoveableButton moveButton;


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
                resizeButton.Visible = moveButton.Visible= false;
                resizeButton.Dispose();
                moveButton.Dispose();
            }
            catch (NullReferenceException)
            {
                ;
            }
            finally
            {
                resizeButton = null;
                moveButton = null;
            }
        }

        public override void ShowAsSelected()
        {
            base.ShowAsSelected();
            if (resizeButton == null)
            {
                resizeButton = new ResizeButton(MainWin.GetInstance().MainView(), this, new Point(endPoint.X - 3, endPoint.Y - 3), Cursors.SizeNS);
                resizeButton.SetBindingPoints(
                    new Ref<Point>(() => endPoint, z => { endPoint = z; })
                    );
                resizeButton.MouseDown += ResizeButton_MouseDown;
                resizeButton.MouseUp += ResizeButton_MouseUp;
                resizeButton.MouseMove += ResizeButton_MouseMove;

                moveButton = new MoveableButton(MainWin.GetInstance().MainView(), this, new Point(startPoint.X - 3, startPoint.Y - 3), Cursors.SizeAll);
                moveButton.SetBindingPoints(
                    new Ref<Point>(() => startPoint, z => { startPoint = z; }),
                    new Ref<Point>(() => endPoint, z => { endPoint = z; }),
                    new Ref<Point>(() => resizeButton.Location, z => { resizeButton.Location = z; })
                    );
                moveButton.MouseDown += MoveButton_MouseDown;
                moveButton.MouseUp += MoveButton_MouseUp;
                moveButton.MouseMove += MoveButton_MouseMove;
            }
            Log.LogText("Select Circle");
        }

        private void MoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                RetMAction().Action();
            }
        }

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if(isMoving)
            {
                isMoving = false;
                Log.LogText(string.Format("Move Circle ({0},{1}),r={2}", startPoint.X, startPoint.Y,
                        Math.Sqrt(      Math.Pow((startPoint.X - endPoint.X), 2)
                                        + Math.Pow((startPoint.Y - endPoint.Y), 2))
                                        )
                                        );
                ShowAsNotSelected();
            }
        }

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoving = true;
                MAction mAction;
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out mAction);
                his.AddBackUpClone(mAction);
                if (isMoving)
                {
                    RetMAction().Action();
                }
            }
        }

        private void ResizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                RetMAction().Action();
            }
        }

        private void ResizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                isResizing = false;
                Log.LogText(string.Format("Resize Circle R={0}", Math.Sqrt(
                                        Math.Pow((startPoint.X - endPoint.X), 2)
                                        + Math.Pow((startPoint.Y - endPoint.Y), 2)
                    )
                    ));
                ShowAsNotSelected();
            }
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
