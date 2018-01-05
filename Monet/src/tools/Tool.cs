///-------------------------------------------------------------------------------------------------
/// \file src\tools\Tool.cs.
///
/// \brief Implements the tool class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Monet.src.history;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class Tool
    ///
    /// \brief A tool.
    ///-------------------------------------------------------------------------------------------------

    abstract class Tool: Actionable
    {
        /// \brief The main view control
        protected PictureBox mainView;
        /// \brief Buffer for double data
        protected Image doubleBuffer;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public Tool(PictureBox mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public Tool(PictureBox mainView)
        {
            this.mainView = mainView;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn abstract public void MakeAction(ActionParameters_t toolParameters);
        ///
        /// \brief Makes an action
        ///
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        abstract public void MakeAction(ActionParameters_t toolParameters);

        ///-------------------------------------------------------------------------------------------------
        /// \fn virtual public void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        virtual public void RegisterTool()
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn virtual public void UnRegisterTool()
        ///
        /// \brief Un register tool
        ///-------------------------------------------------------------------------------------------------

        virtual public void UnRegisterTool()
        {

        }
        
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class DrawShapeTool
    ///
    /// \brief A draw shape tool.
    ///-------------------------------------------------------------------------------------------------

    abstract class DrawShapeTool:Tool
    {
        /// \brief True if this object is enabled
        protected bool isEnabled;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public DrawShapeTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public DrawShapeTool(PictureBox mainView) : base(mainView)
        {
            isEnabled = false;
        }
    }
}

