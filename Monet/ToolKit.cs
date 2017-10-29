using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    class ToolKit
    {
        /// \brief The main view control
        PictureBox mainView;
        /// \brief The current tool
        public Tool currentTool;

        /// \brief The pointer tool
        static Tool pointerTool;
        /// \brief The line tool
        static Tool lineTool;

        ///-------------------------------------------------------------------------------------------------
        /// \property internal Tool LineTool
        ///
        /// \brief Gets the line tool
        ///
        /// \return The line tool.
        ///-------------------------------------------------------------------------------------------------

        internal Tool LineTool { get => lineTool; }

        ///-------------------------------------------------------------------------------------------------
        /// \property internal Tool PointerTool
        ///
        /// \brief Gets the pointer tool
        ///
        /// \return The pointer tool.
        ///-------------------------------------------------------------------------------------------------

        internal Tool PointerTool { get => pointerTool;}

        ///-------------------------------------------------------------------------------------------------
        /// \fn internal ToolKit(PictureBox mainView, Button pointerButton, Button lineButton)
        ///
        /// \brief Constructor
        ///
        /// \param mainView      The main view control.
        /// \param pointerButton The pointer control.
        /// \param lineButton    The line control.
        ///-------------------------------------------------------------------------------------------------

        internal ToolKit(PictureBox mainView,
                         Button pointerButton,
                         Button lineButton)
        {
            this.mainView = mainView;
            pointerTool = new PointerTool   (mainView,pointerButton);
            lineTool    = new LineTool      (mainView,lineButton);
        }
        
    }
}
