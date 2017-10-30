///-------------------------------------------------------------------------------------------------
/// \file PencilTool.cs.
///
/// \brief Implements the pencil tool class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class PencilTool
    ///
    /// \brief A pencil tool.
    ///-------------------------------------------------------------------------------------------------

    sealed class PencilTool : DrawShapeTool
    {
        /// \brief The old location
        Point oldLocation;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public PencilTool(PictureBox mainView, Button button) : base(mainView, button)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view pictureBox.
        /// \param button   The button control.
        ///-------------------------------------------------------------------------------------------------

        public PencilTool(PictureBox mainView, Button button) : base(mainView, button)
        {
        }


        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool.
        ///        It changes the current Cursor, and adds some mouseEvent Handle functions.
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {    
            base.RegisterTool();
            mainView.Cursor = new Cursor(GetType(), @"pencilCursor.cur");
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void UnRegisterTool()
        ///
        /// \brief Un register tool.
        ///        It changes the current Cursor, and removes some mouseEvent Handle functions.
        ///-------------------------------------------------------------------------------------------------

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
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
            isDrawing = false;
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
            if(isDrawing)
            {
                Graphics g = Graphics.FromImage(mainView.Image);
                LineTool lt = (LineTool)(ToolKit.GetInstance().LineTool);
                lt.lineAgent.DrawLine(g, Setting.GetInstance().Pen, oldLocation, e.Location);
                oldLocation = e.Location;
                mainView.Invalidate();
                g.Dispose();
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
            if(e.Button==MouseButtons.Left)
            {
                isDrawing = true;
                oldLocation = e.Location;
            }
            
        }
    }
}
