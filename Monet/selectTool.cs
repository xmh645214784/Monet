using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    sealed class SelectTool : Tool
    {
        Point startPoint;
        Point nowPoint;
        bool isEnabled;
        Pen solidPen;
        bool isExistOneRect;

        public SelectTool(PictureBox mainView, Button button) : base(mainView, button)
        {
            isEnabled = false;
            isExistOneRect = false;
            solidPen = new Pen(Color.Gray, 2);
            solidPen.DashStyle = DashStyle.Custom;
            solidPen.DashPattern = new float[] { 1f, 1f };
        }
        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
            mainView.PreviewKeyDown += MainView_PreviewKeyDown;
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
            mainView.PreviewKeyDown -= MainView_PreviewKeyDown;
        }

        private void MainView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEnabled)
            {
                //take out the last but two valid image, take clone of it ,push it into stack.
                History.GetInstance().UndoAction();
                Image lastValidClone = (Image)History.GetInstance().TopAction().Clone();
                History.GetInstance().PushBackAction(lastValidClone);
                mainView.Image = lastValidClone;

                //draw
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    nowPoint = e.Location;
                    g.DrawRectangle(solidPen, startPoint.X, startPoint.Y,
                        Math.Abs(nowPoint.X - startPoint.X), Math.Abs(nowPoint.Y - startPoint.Y));
                    mainView.Invalidate();
                }
            }
        }
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isEnabled = true;
                if (isExistOneRect==true)
                {
                    //take out the last but two valid image, take clone of it ,push it into stack.
                    History.GetInstance().UndoAction();
                    Image lastValidClone = (Image)History.GetInstance().TopAction().Clone();
                    History.GetInstance().PushBackAction(lastValidClone);
                    mainView.Image = lastValidClone;
                }
                else
                { 
                    Image newClone = (Image)mainView.Image.Clone();
                    History.GetInstance().PushBackAction(newClone);
                    mainView.Image = newClone;
                }
            }
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = false;
                if (startPoint == e.Location)
                    isExistOneRect = false;
                else
                    isExistOneRect = true;
            }
        }
    }
}
