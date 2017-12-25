using Monet.src.history;
using Monet.src.shape;
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
    sealed class ScanFillTool : Tool
    {
        static Queue<Point> queue = new Queue<Point>(capacity: 1000000);

        public ScanFillTool(PictureBox mainView) : base(mainView)
        {
        }

        public override void RegisterTool()
        {
            base.RegisterTool();
            mainView.Cursor = Cursors.Hand;
            mainView.MouseClick += MainView_MouseClick;
        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            Color newColor;
            if (e.Button == MouseButtons.Left)
            {
                newColor = Setting.GetInstance().FrontColor;
            }
            else
            {
                newColor = Setting.GetInstance().BackgroundColor;
            }
            ArrayList array = History.GetInstance().historyArray;
            for (int i = 0; i <= History.GetInstance().Index; i++)
            {
                if (array[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)array[i]).ActionParameters;
                    Shape shape_temp = (Shape)actionParameters;
                    if (shape_temp.IsSelectMe(e.Location))
                    {
                        shape_temp.backColor = newColor;
                        break;
                    }
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }
            History.GetInstance().Update();
        }


        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.Cursor = Cursors.Default;
            mainView.MouseClick -= MainView_MouseClick;
        }



        public override void MakeAction(ActionParameters_t toolParameters)
        {
            ;//No action
        }
    }


}
