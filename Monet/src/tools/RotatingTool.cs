///-------------------------------------------------------------------------------------------------
/// \file src\tools\RotatingTool.cs.
///
/// \brief Implements the rotating tool class
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
    /// \class RotatingTool
    ///
    /// \brief A rotating tool.
    ///-------------------------------------------------------------------------------------------------

    class RotatingTool : Tool
    {
        /// \brief The rotating button
        RotatingButton rotatingButton_mid;
        TrackBar trackBar;

        Rotatable whichToBeRotated;

        Image doubleBuffer;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public RotatingTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public RotatingTool(PictureBox mainView) : base(mainView)
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
            if(rotatingButton_mid==null)
            rotatingButton_mid = new RotatingButton(mainView,new Point(100,100),Cursors.SizeAll);
            
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
            whichToBeRotated = null;
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

            Shape shape = null;
            for (int i = 0; i <= History.GetInstance().Index; i++)
            {
                if (array[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    shape = (Shape)actionParameters;
                    Rotatable dummy = (Rotatable)shape;
                    if (shape.IsSelectMe(e.Location))
                    {
                        History.GetInstance().AddBackUpClone(shape.RetMAction());
                        shape.ShowAsSelected();
                        whichToBeRotated = (Rotatable)shape;
                        ShowRotatingWin();
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


        void ShowRotatingWin()
        {
            RotatingForm form = new RotatingForm();
            form.FormClosed += Form_FormClosed;
            trackBar = form.trackBar1;
            form.trackBar1.Scroll += TrackBar1_Scroll;
            form.ShowDialog();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Shape)whichToBeRotated).ShowAsNotSelected();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            whichToBeRotated.Rotate(rotatingButton_mid.Location, trackBar.Value);
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
            try
            {
                mainView.MouseClick -= MainView_MouseClick;
                whichToBeRotated = null;
                rotatingButton_mid.Visible = false;
                rotatingButton_mid.Dispose();
                rotatingButton_mid = null;
            }
            catch (NullReferenceException)
            {
                ;
            }
            
        }
    }
}
