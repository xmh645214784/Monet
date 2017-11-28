using Monet.src.tools;
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
        public Tool pointerTool;
        /// \brief The line tool
        public Tool lineTool;
        /// \brief The pencil tool
        public Tool pencilTool;
        /// \brief The circle tool
        public Tool circleTool;
        /// \brief The select tool
        public Tool selectTool;

        public Tool fillTool;

        static ToolKit mInstance;

        public static ToolKit GetInstance()
        {
            if (mInstance == null)
                throw new NullReferenceException();
            return mInstance;
        }

        public static ToolKit GetInstance(PictureBox mainView)
        {
            if (mInstance == null)
                mInstance = new ToolKit(mainView);
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

        private ToolKit(PictureBox mainView)
        {
            this.mainView = mainView;
            pointerTool = new PointerTool   (mainView);
            lineTool    = new LineTool      (mainView);
            //pencilTool  = new PencilTool    (mainView);
            circleTool  = new CircleTool    (mainView);
            //selectTool = new SelectTool   (mainView);
            fillTool = new FillTool(mainView);
        }
        
    }
}
