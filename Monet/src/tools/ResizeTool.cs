///-------------------------------------------------------------------------------------------------
/// \file src\tools\ResizeTool.cs.
///
/// \brief Implements the resize tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class ResizeTool
    ///
    /// \brief A resize tool. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class ResizeTool : Tool
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn public ResizeTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public ResizeTool(PictureBox mainView) : base(mainView)
        {
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

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {
            mainView.Cursor = Cursors.Hand;
            base.RegisterTool();
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
            ArrayList array = History.GetInstance().historyArray;
            for (int i = 0; i <= History.GetInstance().Index; i++)
            {
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    Resizeable resizeable = (Resizeable)actionParameters;
                    resizeable.ShowAsNotResizing();
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }

            History.GetInstance().Update();

            for (int i = 0; i <= History.GetInstance().Index; i++)
            {
                if (array[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    Shape shape = (Shape)actionParameters;
                    Resizeable resizeable = (Resizeable)shape;
                    if (shape.IsSelectMe(e.Location))
                    { 
                        resizeable.ShowAsResizing();
                        break;
                    }
                    else
                    {
                        resizeable.ShowAsNotResizing();
                    }
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }

        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void UnRegisterTool()
        ///
        /// \brief Un register tool
        ///-------------------------------------------------------------------------------------------------

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.MouseClick -= MainView_MouseClick;
        }
    }
}
