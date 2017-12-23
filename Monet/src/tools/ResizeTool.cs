using Monet.src.history;
using Monet.src.shape;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    sealed class ResizeTool : Tool
    {
        public ResizeTool(PictureBox mainView) : base(mainView)
        {
        }

        public override void MakeAction(ActionParameters_t toolParameters)
        {
            ;
        }

        public override void RegisterTool()
        {
            mainView.Cursor = Cursors.Hand;
            base.RegisterTool();
            mainView.MouseClick += MainView_MouseClick;
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            ArrayList array = History.GetInstance().historyArray;
            for (int i = 0; i <= History.GetInstance().Index; i++)
            {
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    Resizeable resizeable = (Resizeable)actionParameters;
                    resizeable.ShowAsNotResizing();
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }
            
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
        }
    }
}
