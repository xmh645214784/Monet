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
        /// \brief The pencil tool
        static Tool pencilTool;
        /// \brief The circle tool
        static Tool circleTool;

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

        internal Tool PointerTool { get => pointerTool; }

        ///-------------------------------------------------------------------------------------------------
        /// \property public Tool PencilTool
        ///
        /// \brief Gets the pencil tool
        ///
        /// \return The pencil tool.
        ///-------------------------------------------------------------------------------------------------

        internal Tool PencilTool { get => pencilTool; }

        ///-------------------------------------------------------------------------------------------------
        /// \property internal static Tool CircleTool
        ///
        /// \brief Gets the circle tool
        ///
        /// \return The circle tool.
        ///-------------------------------------------------------------------------------------------------

        internal Tool CircleTool { get => circleTool;}

        static ToolKit mInstance;

        public static ToolKit GetInstance()
        {
            if (mInstance == null)
                throw new NullReferenceException();
            return mInstance;
        }

        public static ToolKit GetInstance(PictureBox mainView,
                         Button pointerButton,
                         Button lineButton,
                         Button pencilButton,
                         Button circleButton)
        {
            if (mInstance == null)
                mInstance = new ToolKit(mainView, 
                                        pointerButton, 
                                        lineButton, 
                                        pencilButton,
                                        circleButton);
            return mInstance;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private ToolKit(PictureBox mainView, Button pointerButton, Button lineButton, Button pencilButton, Button circleButton)
        ///
        /// \brief Constructor
        ///
        /// \param mainView      The main view control.
        /// \param pointerButton The pointer control.
        /// \param lineButton    The line control.
        /// \param pencilButton  The pencil control.
        /// \param circleButton  The circle control.
        ///-------------------------------------------------------------------------------------------------

        private ToolKit(PictureBox mainView,
                         Button pointerButton,
                         Button lineButton,
                         Button pencilButton,
                         Button circleButton)
        {
            this.mainView = mainView;
            pointerTool = new PointerTool   (mainView,pointerButton);
            lineTool    = new LineTool      (mainView,lineButton);
            pencilTool  = new PencilTool    (mainView, pencilButton);
            circleTool  = new CircleTool    (mainView, circleButton);
        }
        
    }
}
