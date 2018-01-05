///-------------------------------------------------------------------------------------------------
/// \file src\tools\ClipTool.cs.
///
/// \brief Implements the clip tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class ClipTool
    ///
    /// \brief A clip tool.
    ///-------------------------------------------------------------------------------------------------

    internal class ClipTool : Tool
    {
        /// \brief The start point
        Point startPoint;
        /// \brief The end point
        Point endPoint;
        /// \brief The clip rectangle
        Rectangle clipRect;

        /// \brief True if this object is cliping
        bool isCliping = false;

        /// \brief The solid pen
        Pen solidPen;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public ClipTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public ClipTool(PictureBox mainView) : base(mainView)
        {
            solidPen = new Pen(Color.Gray, 2);
            solidPen.DashStyle = DashStyle.Custom;
            solidPen.DashPattern = new float[] { 1f, 1f };
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
                doubleBuffer = mainView.Image.Clone() as Image;
                isCliping = true;

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

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void MakeAction(ActionParameters_t toolParameters)
        ///
        /// \brief Makes an action
        ///
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            ;
        }
    }
}