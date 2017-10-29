using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    class PencilTool : DrawShapeTool
    {
        Point oldLocation;
        public PencilTool(PictureBox mainView, Button button) : base(mainView, button)
        {
        }

        public override void Draw(ToolParameters toolParameters)
        {
            throw new NotImplementedException();
        }

        public override void RegisterTool()
        {    
            base.RegisterTool();
            mainView.Cursor = new Cursor(GetType(), @"pencilCursor.cur");
            mainView.MouseDown += MainView_MouseDown;
            mainView.MouseMove += MainView_MouseMove;
            mainView.MouseUp += MainView_MouseUp;
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.MouseDown -= MainView_MouseDown;
            mainView.MouseMove -= MainView_MouseMove;
            mainView.MouseUp -= MainView_MouseUp;
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {    
            if(isDrawing)
            {
                LineTool lt = (LineTool)(ToolKit.GetInstance().LineTool);
                lt.lineAgent.DrawLine(g, SettingPanel.GetInstance().Pen, oldLocation, e.Location);
                oldLocation = e.Location;
            }
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                isDrawing = true;
                oldLocation = e.Location;
            }
            
        }
    }
}
