using Monet.src.history;
using Monet.src.shape;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Monet
{
    internal class ClipTool : Tool
    {
        Point startPoint;
        Point endPoint;
        Rectangle clipRect;

        bool isCliping = false;

        Pen solidPen;

        public ClipTool(PictureBox mainView) : base(mainView)
        {
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
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCliping)
            {
                mainView.Image.Dispose();
                mainView.Image = doubleBuffer.Clone() as Image;

                //draw a new one
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    endPoint = e.Location;
                    clipRect = Common.Rectangle(startPoint, endPoint);
                    g.DrawRectangle(solidPen,clipRect);
                }
            }
        }
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                doubleBuffer = mainView.Image.Clone() as Image;
                isCliping = true;

            }
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isCliping = false;
                History his= History.GetInstance();
                for(int i=0;i<=his.Index;i++)
                {
                    try
                    {
                        if (his.historyArray[i] is BackUpMAction)
                            continue;
                        MAction mAction = (MAction)his.historyArray[i];
                        Shape shape=(Shape)mAction.ActionParameters;
                        Clipable clipable = (Clipable)shape;
                        his.AddBackUpClone(mAction);
                        clipable.Clip(clipRect);
                    }
                    catch (System.InvalidCastException)
                    {
                        ;
                    }
                }
                his.Update();
            }
        }
        public override void MakeAction(ActionParameters_t toolParameters)
        {
            ;
        }
    }
}