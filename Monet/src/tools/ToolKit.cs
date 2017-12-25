///-------------------------------------------------------------------------------------------------
/// \file src\tools\ToolKit.cs.
///
/// \brief Implements the tool kit class
///-------------------------------------------------------------------------------------------------

using Monet.src.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class ToolKit
    ///
    /// \brief A tool kit.
    ///-------------------------------------------------------------------------------------------------

    class ToolKit
    {
        /// \brief The main view control
        PictureBox mainView;
        /// \brief The current tool
        public Tool currentTool;
        /// \brief The pointer tool
        public PointerTool pointerTool;
        /// \brief The line tool
        public LineTool lineTool;
        /// \brief The pencil tool
        public Tool pencilTool;
        /// \brief The circle tool
        public CircleTool circleTool;
        /// \brief The select tool
        public ClipTool clipTool;

        /// \brief The fill tool
        public ScanFillTool scanFillTool;


        public FloodFillTool floodFillTool;

        /// \brief The ellipse tool
        public Tool ellipseTool;

        /// \brief The polygon tool
        public Tool polygonTool;

        public ResizeTool resizeTool;

        public RotatingTool rotatingTool;
        public BSplineTool bSplineTool;
        public BezierTool bezierTool;


        /// \brief The instance
        static ToolKit mInstance;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public static ToolKit GetInstance()
        ///
        /// \brief Gets the instance
        ///
        /// \exception NullReferenceException Thrown when a value was unexpectedly null.
        ///
        /// \return The instance.
        ///-------------------------------------------------------------------------------------------------

        public static ToolKit GetInstance()
        {
            if (mInstance == null)
                throw new NullReferenceException();
            return mInstance;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public static ToolKit GetInstance(PictureBox mainView)
        ///
        /// \brief Gets an instance
        ///
        /// \param mainView The main view control.
        ///
        /// \return The instance.
        ///-------------------------------------------------------------------------------------------------

        public static ToolKit GetInstance(PictureBox mainView)
        {
            if (mInstance == null)
                mInstance = new ToolKit(mainView);
            return mInstance;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private ToolKit(PictureBox mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///
        /// ### param pointerButton The pointer control.
        /// ### param lineButton    The line control.
        /// ### param pencilButton  The pencil control.
        /// ### param circleButton  The circle control.
        ///-------------------------------------------------------------------------------------------------

        private ToolKit(PictureBox mainView)
        {
            this.mainView = mainView;
            pointerTool = new PointerTool   (mainView);
            lineTool    = new LineTool      (mainView);
            //pencilTool  = new PencilTool    (mainView);
            circleTool  = new CircleTool    (mainView);
            clipTool = new ClipTool   (mainView);
            scanFillTool = new ScanFillTool(mainView);
            floodFillTool = new FloodFillTool(mainView);
            ellipseTool = new EllipseTool(mainView);
            polygonTool = new PolygonTool(mainView);
            resizeTool = new ResizeTool(mainView);
            rotatingTool = new RotatingTool(mainView);
            bSplineTool = new BSplineTool(mainView);
            bezierTool = new BezierTool(mainView);
        }
        
    }
}
