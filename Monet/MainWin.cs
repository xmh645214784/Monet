using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class MainWin
    ///
    /// \brief The application's main form.
    ///-------------------------------------------------------------------------------------------------

    public partial class MainWin : Form
    {
        /// \brief The tool kit which contains all tools(line,rectangle,circle etc.)
        ToolKit toolKit;
        /// \brief This variable shows which colorBox is now setting.
        PictureBox currentSettingColorBox;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public MainWin()
        ///
        /// \brief Default constructor
        ///-------------------------------------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
            // some initialize progress. 
            toolKit = ToolKit.GetInstance(mainView,
                                  pointerButton,
                                  lineButton,
                                  pencilButton
                                  );
            // set default tool. 
            toolKit.currentTool = toolKit.PointerTool;
            toolKit.currentTool.RegisterTool();
            // set default color box button, to emphasize the colorBox which is currently being used. 
            currentSettingColorBox = colorBoxButton1;

        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void mainView_MouseMove(object sender, MouseEventArgs e)
        ///
        /// \brief Event handler. Called by mainView for mouse move events to show current
        ///        mouse position in the status bar.
        ///
        /// \param sender Source of the event.
        /// \param e      Mouse event information.
        ///-------------------------------------------------------------------------------------------------

        private void mainView_MouseMove(object sender, MouseEventArgs e)
        {
            statusLabelText1.Text = String.Format("({0},{1})pix", e.X.ToString(), e.Y.ToString());
        }



        ///-------------------------------------------------------------------------------------------------
        /// \fn private void changeTool(Tool newTool)
        ///
        /// \brief Change the current tool to the new tool specified by param1
        ///
        /// \param newTool The new tool.
        ///-------------------------------------------------------------------------------------------------

        private void changeTool(Tool newTool)
        {
            toolKit.currentTool.UnRegisterTool();
            toolKit.currentTool = newTool;
            toolKit.currentTool.RegisterTool();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void lineButton_Click(object sender, EventArgs e)
        ///
        /// \brief Event handler. Called by lineButton for click events to use the TOOL line.
        ///
        /// \param sender Source of the event.
        /// \param e      Event information.
        ///-------------------------------------------------------------------------------------------------

        private void lineButton_Click(object sender, EventArgs e) => changeTool(toolKit.LineTool);

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void pointerButton_Click(object sender, EventArgs e)
        ///
        /// \brief Event handler. Called by pointerButton for click events to use the TOOL pointer.
        ///
        /// \param sender Source of the event.
        /// \param e      Event information.
        ///-------------------------------------------------------------------------------------------------

        private void pointerButton_Click(object sender, EventArgs e) => changeTool(toolKit.PointerTool);

        private void pencilButton_Click(object sender, EventArgs e) => changeTool(toolKit.PencilTool);

        ///-------------------------------------------------------------------------------------------------
        /// \fn private void colorButton_Click(object sender, EventArgs e)
        ///
        /// \brief Event handler. Called by colorButton for click events to set the front and
        ///        background color.
        ///
        /// \param sender Source of the event.
        /// \param e      Event information.
        ///-------------------------------------------------------------------------------------------------

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = SettingPanel.GetInstance().Pen.Color;
            colorDialog.FullOpen = true;
            colorDialog.AnyColor = true;
            if(colorDialog.ShowDialog()==DialogResult.OK)
            {
                SettingPanel.GetInstance().Pen.Color = colorDialog.Color;
                currentSettingColorBox.BackColor= colorDialog.Color;
            }
        }


    }
}
