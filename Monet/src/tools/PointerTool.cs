using Monet.src.history;
using Monet.src.shape;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    class PointerTool:Tool
    {
        public PointerTool(PictureBox mainView) : base(mainView)
        {

        }

        public override void MakeAction(ActionParameters toolParameters)
        {
            ;
        }

        public override void RegisterTool()
        {
            mainView.Cursor = Cursors.Default;       
            base.RegisterTool();
            mainView.MouseClick += MainView_MouseClick;

        }

        private void MainView_MouseClick(object sender, MouseEventArgs e)
        {
            ArrayList array = History.GetInstance().historyArray;
            for(int i=0;i<=History.GetInstance().Index; i++)
            {
                try
                {
                    ActionParameters actionParameters = ((MAction)array[i]).ActionParameters;
                    Shape shape = (Shape)actionParameters;
                    if (shape.IsSelectMe(e.Location))
                    {
                        shape.ShowAsSelected();
                        break;
                    }
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
            mainView.MouseClick -= MainView_MouseClick;
        }
    }
}
