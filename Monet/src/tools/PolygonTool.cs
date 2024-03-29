﻿///-------------------------------------------------------------------------------------------------
/// \file src\tools\PolygonTool.cs.
///
/// \brief Implements the polygon tool class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
using Monet.src.ui;
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
    /// \class PolygonTool
    ///
    /// \brief A polygon tool.
    ///-------------------------------------------------------------------------------------------------

    class PolygonTool: DrawShapeTool
    {
        /// \brief List of arrays
        List<Point> arrayList;
        /// \brief Buffer for double data
        Image doubleBuffer;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public PolygonTool(PictureBox mainView) : base(mainView)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        ///-------------------------------------------------------------------------------------------------

        public PolygonTool(PictureBox mainView) : base(mainView)
        {
            arrayList = new List<Point>();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public override void RegisterTool()
        ///
        /// \brief Registers the tool
        ///-------------------------------------------------------------------------------------------------

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Cross;
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
            if(isEnabled)
            {
                mainView.Image.Dispose();
                mainView.Image = (Image)doubleBuffer.Clone();

                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    LineTool linetool = (LineTool)ToolKit.GetInstance().lineTool;
                    linetool.Draw(g, Setting.GetInstance().Pen, arrayList[arrayList.Count - 1], e.Location);
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
            if (e.Button == MouseButtons.Left)
            {
                if (arrayList.Count == 0)
                {
                    doubleBuffer = (Image)mainView.Image.Clone();
                    arrayList.Add(e.Location);
                    this.isEnabled = true;
                    return;
                }
                doubleBuffer = (Image)mainView.Image.Clone();
                arrayList.Add(e.Location);      
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    // g.DrawLine(Setting.GetInstance().Pen, (Point)arrayList[arrayList.Count - 2], e.Location);
                    LineTool lineTool = (LineTool)ToolKit.GetInstance().lineTool;
                    lineTool.Draw(g, Setting.GetInstance().Pen, arrayList[arrayList.Count - 2], e.Location);
                }
            }
            else
            {
                this.isEnabled = false;

                mainView.Image = (Image)doubleBuffer.Clone();

                //save into actions array.
                Polygon polygon = new Polygon();
                polygon.pen= Setting.GetInstance().Pen.Clone() as Pen;
                polygon.pointArray = new List<Point>(arrayList.ToArray());
                Log.LogText(String.Format("Create Polygon"));
                History.GetInstance().PushBackAction(
                   new MAction(this, polygon));
                MakeAction(polygon);

                //some termination
                arrayList.Clear();
            }
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
        /// \exception InvalidCastException Thrown when an object cannot be cast to a required type.
        ///
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            try
            {
                Polygon polygon = (Polygon)toolParameters;
                using (Graphics g = Graphics.FromImage(mainView.Image))
                {
                    LineTool lineTool = (LineTool)ToolKit.GetInstance().lineTool;
                    int length = polygon.pointArray.Count;
                    for (int i = 0; i < length; i++)
                    {
                        lineTool.Draw(g, polygon.pen, polygon.pointArray[i % length], polygon.pointArray[(i + 1) % length]);
                    }
                    if(polygon.backColor!=Color.White)
                        g.FillPolygon(new SolidBrush(polygon.backColor), polygon.pointArray.ToArray());
                }
            }
            catch (InvalidCastException)
            {
                throw;
            }
        }
    }
}
