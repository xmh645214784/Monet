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
        public PointerTool(PictureBox mainView, Button button) : base(mainView,button)
        {

        }

        public override void RegisterTool()
        {
            mainView.Cursor = Cursors.Default;
            base.RegisterTool();
        }
    }
}
