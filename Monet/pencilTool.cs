using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet
{
    class PencilTool : DrawShapeTool
    {
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
        }

    }
}
