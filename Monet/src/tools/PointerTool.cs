///-------------------------------------------------------------------------------------------------
/// \file src\tools\PointerTool.cs.
///
/// \brief Implements the pointer tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class PointerTool
    ///
    /// \brief A pointer tool.
    ///-------------------------------------------------------------------------------------------------

    class PointerTool:Tool
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn public PointerTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public PointerTool(PictureBox mainView) : base(mainView)
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
            mainView.Cursor = Cursors.Default;       
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
                if (array[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    Shape shape_temp = (Shape)actionParameters;
                    shape_temp.ShowAsNotSelected();
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }

            Shape shape=null;
            for (int i=0;i<=History.GetInstance().Index; i++)
            {
                if (array[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    shape = (Shape)actionParameters;
                    if (shape.IsSelectMe(e.Location))
                    {
                        shape.ShowAsSelected();
                        break;
                    }
                    else
                    {
                        shape.ShowAsNotSelected();
                    }
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }

            // VERY IMPORTANT
            History.GetInstance().Update();
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
