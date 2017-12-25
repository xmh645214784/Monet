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
using Monet.src.tools;

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
        Button currentSettingColorButton;
        public ResizeCanvasButton resizePictureBoxButton;

        public PictureBox MainView()
        {
            return mainView;
        }

        
        internal void ClearScreen()
        {
            mainView.Image = new Bitmap(mainView.Image.Width, mainView.Image.Height);
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                g.Clear(Color.White);
            }

        }

        internal void ClearAllControlsInMainView()
        {
            mainView.Controls.Clear();
        }

        static MainWin mInstance=null;

        public static MainWin GetInstance()
        {
            if (mInstance == null)
                mInstance = new MainWin();
            return mInstance;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public MainWin()
        ///
        /// \brief Default constructor
        ///-------------------------------------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
            // some initialize progress. 
            // initialize the toolKit
            toolKit = ToolKit.GetInstance(mainView);        
            // set default tool. 
            toolKit.currentTool = toolKit.pointerTool;
            toolKit.currentTool.RegisterTool();
            ToolButton.currentButton = pointerButton;

            // init buttons
            pointerButton.BindingTool = toolKit.pointerTool;
            pencilButton.BindingTool = toolKit.pencilTool;
            lineButton.BindingTool = toolKit.lineTool;
            circleButton.BindingTool = toolKit.circleTool;
            fillButton.BindingTool = toolKit.fillTool;
            clipButton.BindingTool = toolKit.clipTool;
            ellipseButton.BindingTool = toolKit.ellipseTool;
            polygonButton.BindingTool = toolKit.polygonTool;
            resizeButton.BindingTool = toolKit.resizeTool;
            rotatingButton.BindingTool = toolKit.rotatingTool;

            // set default color box button, to emphasize the colorBox which is currently being used. 
            currentSettingColorButton = colorBoxButton1;
            // undo and redo button set unenabled
            redoButton.Enabled=undoButton.Enabled = false;
            redoButton.Cursor = undoButton.Cursor = Cursors.No;
            //image related
            mainView.Image = new Bitmap(mainView.Width, mainView.Height);
            //fill the image with white
            using (Graphics g = Graphics.FromImage(mainView.Image))
            {
                g.FillRectangle(Brushes.White, 0, 0, mainView.Image.Width, mainView.Image.Height);
            }


            //set the resize button to the lower right corner.
            resizePictureBoxButton = new ResizeCanvasButton(mainView);
            resizePictureBoxButton.Size = new Size(7, 7);

            polygonButton.BackgroundImage = Resource1.polygon;
            polygonButton.BackgroundImageLayout = ImageLayout.Stretch;

            resizeButton.BackgroundImage= Resource1.resize;
            rotatingButton.BackgroundImage = Resource1.rotating;
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
            colorDialog.Color = Setting.GetInstance().Pen.Color;
            colorDialog.FullOpen = true;
            colorDialog.AnyColor = true;
            if(colorDialog.ShowDialog()==DialogResult.OK)
            {
                //set the button color.
                currentSettingColorButton.BackColor= colorDialog.Color;

                //set the setting singleton's color
                if(currentSettingColorButton.Equals(colorBoxButton1))
                {
                    Setting.GetInstance().Pen.Color = colorDialog.Color;
                    Setting.GetInstance().FrontColor = colorDialog.Color;
                }
                else
                {
                    Setting.GetInstance().BackgroundColor = colorDialog.Color;
                }
            }
        }
       
        
        private void colorBoxButton1_Click(object sender, EventArgs e)
        {
            currentSettingColorButton = colorBoxButton1;
            colorTableLayoutPanel1.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption);
            colorTableLayoutPanel2.BackColor = Color.FromArgb(245, 246, 247);
        }

        private void colorBoxButton2_Click(object sender, EventArgs e)
        {
            currentSettingColorButton = colorBoxButton2;
            colorTableLayoutPanel2.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption);
            colorTableLayoutPanel1.BackColor = Color.FromArgb(245, 246, 247);
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            History.GetInstance().UnSelectAll();
            History.GetInstance().UndoAction();
            ClearAllControlsInMainView();
            History.GetInstance().Update();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            History.GetInstance().UnSelectAll();
            History.GetInstance().RedoAction();
            ClearAllControlsInMainView();
            History.GetInstance().Update();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mainView.Image.Dispose();
                mainView.Image=new Bitmap(openFileDialog.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                mainView.Image.Save(saveFileDialog.FileName);
            }
        }
    }
}
