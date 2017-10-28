using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Monet
{
    abstract class Tool
    {
        protected Panel panel;

        abstract public void RegisterTool();
        abstract public void UnRegisterTool();
    }
    abstract class DrawShapeTool:Tool
    {

        abstract public void Draw();
    }
}

