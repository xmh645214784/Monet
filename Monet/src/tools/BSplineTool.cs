///-------------------------------------------------------------------------------------------------
/// \file src\tools\BSplineTool.cs.
///
/// \brief Implements the BSpline tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    ///-------------------------------------------------------------------------------------------------
    /// \class BSplineTool
    ///
    /// \brief A BSpline tool.
    ///-------------------------------------------------------------------------------------------------

    class BSplineTool : Tool
    {
        /// \brief Array of control points
        List<Point> controlPointsArray=new List<Point>();
        /// \brief Buffer for double data
        Image doubleBuffer;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public BSplineTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public BSplineTool(PictureBox mainView) : base(mainView)
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
            try
            {
                BSpline bSpline = (BSpline)(toolParameters);
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, bSpline.pointArray,bSpline.pen);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.MouseClick += MainView_MouseClick;
            mainView.MouseMove += MainView_MouseMove;
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
                mainView.Image.Dispose();
                mainView.Image = doubleBuffer.Clone() as Image;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, controlPointsArray,Setting.GetInstance().Pen);
                }
            }
        }
        /// \brief True if this object is drawing
        bool isDrawing = false;
        /// \brief True if this object is first click
        bool isFirstClick = true;

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
            if(isFirstClick)
            {
                doubleBuffer = mainView.Image.Clone() as Image;
                isFirstClick = false;
            }
            if(e.Button==MouseButtons.Left)
            {
                isDrawing = true;
                controlPointsArray.Add(e.Location); 
            }
            else if(e.Button==MouseButtons.Right)
            {
                isDrawing = false;
                BSpline bSpline = new BSpline(controlPointsArray);
                bSpline.pen = Setting.GetInstance().Pen;
                History.GetInstance().PushBackAction(
                    new history.MAction(this, bSpline)
                    );
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void UnRegisterTool()
        ///
        /// \brief Un register tool
        ///-------------------------------------------------------------------------------------------------

        public override void UnRegisterTool()
        {
            mainView.MouseClick -= MainView_MouseClick;
            mainView.MouseMove -= MainView_MouseMove;
            base.UnRegisterTool();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void Draw(Graphics g,List<Point> controlPointsArray,Pen pen)
        ///
        /// \brief Draws
        ///
        /// \param g                  The Graphics to process.
        /// \param controlPointsArray Array of control points.
        /// \param pen                The pen.
        ///-------------------------------------------------------------------------------------------------

        private  void Draw(Graphics g,List<Point> controlPointsArray,Pen pen)
        {
            int n = 1000;
            double deltaT = 1.0 / n;
            for (int num = 0; num < controlPointsArray.Count - 3; num++)
            {
                for (int i = 0; i <= n; i++)
                {
                    double T = i * deltaT;
                    double f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                    double f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                    double f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                    double f4 = (T * T * T) / 6.0;
                   Common.DrawPix(g,
                       (int)(f1 * controlPointsArray[num].X + f2 * controlPointsArray[num + 1].X + f3 * controlPointsArray[num + 2].X + f4 * controlPointsArray[num + 3].X),
                       (int)(f1 * controlPointsArray[num].Y + f2 * controlPointsArray[num + 1].Y + f3 * controlPointsArray[num + 2].Y + f4 * controlPointsArray[num + 3].Y), 
                               pen);
                }
            }
        }
    }
}
