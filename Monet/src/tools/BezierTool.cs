///-------------------------------------------------------------------------------------------------
/// \file src\tools\BezierTool.cs.
///
/// \brief Implements the bezier tool class
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
    /// \class BezierTool
    ///
    /// \brief A bezier tool.
    ///-------------------------------------------------------------------------------------------------

    class BezierTool : Tool
    {
        /// \brief Array of control points
        List<Point> controlPointsArray = new List<Point>();
        /// \brief Buffer for double data
        Image doubleBuffer;
        /// \brief True if this object is drawing
        bool isDrawing = false;
        /// \brief True if this object is first click
        bool isFirstClick = true;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public BezierTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public BezierTool(PictureBox mainView) : base(mainView)
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
                Bezier bezier = (Bezier)toolParameters;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, bezier.pointArray, bezier.pen);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void Draw(Graphics g, List<Point> controlPointsArray,Pen pen)
        ///
        /// \brief Draws
        ///
        /// \param g                  The Graphics to process.
        /// \param controlPointsArray Array of control points.
        /// \param pen                The pen.
        ///-------------------------------------------------------------------------------------------------

        private void Draw(Graphics g, List<Point> controlPointsArray,Pen pen)
        {
            for (int i = 0; i <= 1000; i++)
            {
                double t = i / 1000.0;
                double x = 0, y = 0, Ber;
                int k;
                for (k = 0; k < controlPointsArray.Count; k++)
                {
                    Ber = C(controlPointsArray.Count - 1, k) * Math.Pow(t, k) * Math.Pow(1 - t, controlPointsArray.Count - 1 - k);
                    x += controlPointsArray[k].X * Ber;
                    y += controlPointsArray[k].Y * Ber;
                }
                Common.DrawPix(g, new Point((int)x, (int)y),pen);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private int factorial(int n)
        ///
        /// \brief Factorials
        ///
        /// \param n An int to process.
        ///
        /// \return An int.
        ///-------------------------------------------------------------------------------------------------

        private int factorial(int n)
        {
            if (n == 1 || n == 0)
            {
                return 1;
            }
            else
            {
                int sum = 1;
                for (int i = 2; i <= n; i++)
                    sum *= i;
                return sum;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private double C(int n, int i)
        ///
        /// \brief Create struct
        ///
        /// \param n An int to process.
        /// \param i Zero-based index of the.
        ///
        /// \return A double.
        ///-------------------------------------------------------------------------------------------------

        private double C(int n, int i)
        {
            return ((double)factorial(n)) / ((factorial(i) * factorial(n - i)));
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
            if (isDrawing)
            {
                mainView.Image.Dispose();
                mainView.Image = doubleBuffer.Clone() as Image;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    Draw(g, controlPointsArray, Setting.GetInstance().Pen);
                }
            }
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
            if (isFirstClick)
            {
                doubleBuffer = mainView.Image.Clone() as Image;
                isFirstClick = false;
            }
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                controlPointsArray.Add(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;

                Bezier bezier = new Bezier(controlPointsArray);
                bezier.pen = Setting.GetInstance().Pen;
                History.GetInstance().PushBackAction(
                    new history.MAction(this,bezier)
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
    }
}
