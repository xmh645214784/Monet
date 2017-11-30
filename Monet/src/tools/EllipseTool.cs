using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    class EllipseTool: DrawShapeTool
    {
        /// \brief The start point
        Point startPoint;
        /// \brief The now point
        Point nowPoint;

        public EllipseTool(PictureBox mainView) : base(mainView)
        {
        }


        public override void MakeAction(ActionParameters toolParameters)
        {
            try
            {
                Ellipse ellipse = (Ellipse)toolParameters;
                DrawEllipse(ellipse.pen, ellipse.a, ellipse.b);
            }
            catch (InvalidCastException)
            {

                throw;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void UnRegisterTool()
        ///
        /// \brief Un register tool
        ///-------------------------------------------------------------------------------------------------

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse move events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEnabled)
            {
                mainView.Image.Dispose();
                mainView.Image = (Image)doubleBuffer.Clone();

                //draw
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    nowPoint = e.Location;
                    DrawEllipse(Setting.GetInstance().Pen,startPoint,e.Location);
                }
            }
        }

        private void DrawEllipse(Pen pen ,Point a,Point b)
        {
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                g.DrawEllipse(pen, Common.Rectangle(a,b));
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse down events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isEnabled = true;
                doubleBuffer = (Image)mainView.Image.Clone();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseUp(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse up events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = false;

                mainView.Image = (Image)doubleBuffer.Clone();

                //draw
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    nowPoint = e.Location;
                    DrawEllipse(Setting.GetInstance().Pen, startPoint, e.Location);
                }

                Ellipse p = new Ellipse();
                p.a = startPoint;
                p.b = nowPoint;
                p.pen = Setting.GetInstance().Pen.Clone() as Pen;

                History.GetInstance().PushBackAction(
                    new src.history.MAction(this, p));
            }
        }
        private sealed class Ellipse:ActionParameters
        {
            public Point a;
            public Point b;
            public Pen pen;
        }
    }
}
