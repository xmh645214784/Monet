///-------------------------------------------------------------------------------------------------
/// \file src\tools\FloodFillTool.cs.
///
/// \brief Implements the flood fill tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
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
    ///-------------------------------------------------------------------------------------------------
    /// \class FloodFillTool
    ///
    /// \brief A flood fill tool. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class FloodFillTool : Tool
    {
        /// \brief The queue
        static Queue<Point> queue = new Queue<Point>(capacity: 1000000);

        ///-------------------------------------------------------------------------------------------------
        /// \fn public FloodFillTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public FloodFillTool(PictureBox mainView) : base(mainView)
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Hand;
            mainView.MouseClick += MainView_MouseClick;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void MainView_MouseClick(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by MainView for mouse click events
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            Color newColor;
            if (e.Button == MouseButtons.Left)
            {
                newColor = Setting.GetInstance().FrontColor;
            }
            else
            {
                newColor = Setting.GetInstance().BackgroundColor;
            }
            FloodFillParam fillParam = new FloodFillParam(e.Location, newColor);
            MakeAction(fillParam);
            History.GetInstance().PushBackAction(
                new MAction(this, fillParam));
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void FillAction(Point p,Color newColor)
        ///
        /// \brief Fill action
        ///
        /// \param p        A Point to process.
        /// \param newColor The new color.
        ///-------------------------------------------------------------------------------------------------

        private void FillAction(Point p,Color newColor)
        {
            queue.Clear();
            Bitmap bitmap = new Bitmap(mainView.Image);
            Color posColor = bitmap.GetPixel(p.X, p.Y);
            queue.Enqueue(p);
            GraphicsUnit units = GraphicsUnit.Pixel;
            while (queue.Count != 0)
            {
                Point q = queue.Dequeue();
                //if out of bounds
                if (!bitmap.GetBounds(ref units).Contains(q))
                    continue;
                if (bitmap.GetPixel(q.X, q.Y) == posColor
                    && bitmap.GetPixel(q.X, q.Y) != newColor)
                {
                    bitmap.SetPixel(q.X, q.Y, newColor);
                    queue.Enqueue(new Point(q.X - 1, q.Y));
                    queue.Enqueue(new Point(q.X, q.Y - 1));
                    queue.Enqueue(new Point(q.X + 1, q.Y));
                    queue.Enqueue(new Point(q.X, q.Y + 1));
                }
            }
            mainView.Image = bitmap;
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
            mainView.MouseClick -= MainView_MouseClick;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void MakeAction(ActionParameters_t toolParameters)
        ///
        /// \brief Makes an action
        ///
        /// \exception InvalidCastException Thrown when an object cannot be cast to a required type.
        ///
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                FloodFillParam fillParam = (FloodFillParam)toolParameters;
                FillAction(fillParam.point, fillParam.color);
            }
            catch (InvalidCastException)
            {

                throw;
            }
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// \class FloodFillParam
        ///
        /// \brief A flood fill parameter. This class cannot be inherited..
        ///-------------------------------------------------------------------------------------------------

        private sealed class FloodFillParam : ActionParameters_t
        {
            /// \brief The point
            public readonly Point point;
            /// \brief The color
            public readonly Color color;

            ///-------------------------------------------------------------------------------------------------
            /// \fn public FloodFillParam(Point point,Color color)
            ///
            /// \brief Constructor
            ///
            /// \param point The point.
            /// \param color The color.
            ///-------------------------------------------------------------------------------------------------

            public FloodFillParam(Point point,Color color)
            {
                this.point = point;
                this.color = color;
            }

            ///-------------------------------------------------------------------------------------------------
            /// \fn public object Clone()
            ///
            /// \brief Makes a deep copy of this object
            ///
            /// \return A copy of this object.
            ///-------------------------------------------------------------------------------------------------

            public object Clone()
            {
                return new FloodFillParam(point, color);
            }
        }
    }


}
