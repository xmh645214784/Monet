using System;
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
            ;
        }

        public override void UnRegisterTool()
        {
            base.UnRegisterTool();
            mainView.MouseClick -= MainView_MouseClick;
        }
    }
}
