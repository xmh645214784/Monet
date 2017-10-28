using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    class ToolKit
    {
        PictureBox mainView;
        static Tool lineTool;
        static Tool pointerTool;
        internal Tool LineTool { get => lineTool; }
        internal Tool PointerTool { get => pointerTool;}

        internal ToolKit(PictureBox mainView)
        {
            this.mainView = mainView;
            lineTool = new LineTool(mainView);
            pointerTool = new PointerTool(mainView);
        }
        
    }
}
