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
    class EllipseTool: DrawShapeTool
    {
        /// \brief The start point
        Point startPoint;
        /// \brief The now point
        Point nowPoint;

        public EllipseTool(PictureBox mainView) : base(mainView)
        {
        }


        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                Ellipse ellipse =(Ellipse)toolParameters;
                Draw(ellipse.pen, ellipse.rect,ellipse.midPoint,ellipse.angle,ellipse.backColor);
                //add into shape array
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
                    Draw(Setting.GetInstance().Pen,startPoint,e.Location,new Point(),0,Setting.GetInstance().BackgroundColor);
                }
            }
        }
        private void Draw(Pen pen, Rectangle rect,Point midPoint,double angle,Color backColor)
        {
            MidPoint_Ellipse(pen, rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2
                , rect.Width / 2, rect.Height / 2, midPoint, angle,backColor);
        }
        private void Draw(Pen pen ,Point a,Point b, Point midPoint, double angle, Color backColor)
        {
            Rectangle rect = Common.Rectangle(a, b);
            MidPoint_Ellipse(pen, rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2
                , rect.Width / 2, rect.Height / 2, midPoint, angle,backColor);
        }

        void MidPoint_Ellipse(Pen pen,int x0, int y0, int a, int b,Point midPoint,double angle,Color backColor)
        {
            double sqa = a * a;
            double sqb = b * b;

            double d = sqb + sqa * (0.25 - b);
            int x = 0;
            int y = b;
            Ellipsepot(pen,x0, y0, x, y, midPoint,angle);
            // 1
            while (sqb * (x + 1) < sqa * (y - 0.5))
            {
                if (d < 0)
                {
                    d += sqb * (2 * x + 3);
                }
                else
                {
                    d += (sqb * (2 * x + 3) + sqa * ((-2) * y + 2));
                    --y;
                }
                ++x;
                Ellipsepot(pen,x0, y0, x, y, midPoint, angle);
            }
            d = (b * (x + 0.5)) * 2 + (a * (y - 1)) * 2 - (a * b) * 2;
            // 2
            while (y > 0)
            {
                if (d < 0)
                {
                    d += sqb * (2 * x + 2) + sqa * ((-2) * y + 3);
                    ++x;
                }
                else
                {
                    d += sqa * ((-2) * y + 3);
                }
                --y;
                Ellipsepot(pen,x0, y0, x, y, midPoint, angle);
            }
            if(backColor!=Color.White)
            {
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    g.FillEllipse(new SolidBrush(backColor), x0 - a, y0 - b, 2 * a, 2 * b);
                }
            }
            
        }

        private void Ellipsepot(Pen pen,int x0, int y0, int x, int y,Point midPoint,double angle)
        {
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                Common.DrawPix(g, Common.RotatingPoint(new Point(x0 + x, y0 + y),midPoint,angle), 
                    pen);
                Common.DrawPix(g, Common.RotatingPoint(new Point(x0 + x, y0 - y), midPoint, angle),
                    pen);
                Common.DrawPix(g, Common.RotatingPoint(new Point(x0 - x, y0 + y), midPoint, angle),
                    pen);
                Common.DrawPix(g, Common.RotatingPoint(new Point(x0 - x, y0 - y), midPoint, angle),
                    pen);
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
                nowPoint = e.Location;
                Ellipse ellipse = new Ellipse();
                ellipse.rect = Common.Rectangle(startPoint, nowPoint);
                ellipse.pen = Setting.GetInstance().Pen.Clone() as Pen;

                History.GetInstance().PushBackAction(
                    new src.history.MAction(this, ellipse));
                MakeAction(ellipse);
            }
        }
    }

}
