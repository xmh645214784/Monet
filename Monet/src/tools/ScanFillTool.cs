///-------------------------------------------------------------------------------------------------
/// \file src\tools\ScanFillTool.cs.
///
/// \brief Implements the scan fill tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
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
    /// \class ScanFillTool
    ///
    /// \brief A scan fill tool. This class cannot be inherited..
    ///-------------------------------------------------------------------------------------------------

    sealed class ScanFillTool : Tool
    {
        /// \brief The queue
        static Queue<Point> queue = new Queue<Point>(capacity: 1000000);

        ///-------------------------------------------------------------------------------------------------
        /// \fn public ScanFillTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public ScanFillTool(PictureBox mainView) : base(mainView)
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
            ArrayList array = History.GetInstance().historyArray;
            for (int i = 0; i <= History.GetInstance().Index; i++)
            {
                if (array[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    Shape shape_temp = (Shape)actionParameters;
                    if (shape_temp.IsSelectMe(e.Location))
                    {
                        History.GetInstance().AddBackUpClone((MAction)array[i]);
                        shape_temp.backColor = newColor;
                        break;
                    }
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }
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
            mainView.Cursor = Cursors.Default;
            mainView.MouseClick -= MainView_MouseClick;
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
            ;//No action
        }
    }


}
