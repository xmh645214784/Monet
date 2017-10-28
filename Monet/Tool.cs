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
        protected PictureBox mainView;
        protected Graphics g;


        public Tool(PictureBox mainView)
        {
            this.mainView = mainView;
            this.g = mainView.CreateGraphics();
        }
        abstract public void RegisterTool();
        abstract public void UnRegisterTool();
    }
    abstract class DrawShapeTool:Tool
    {
        public DrawShapeTool(PictureBox mainView) : base(mainView)
        {
            
        }
        abstract public void Draw(ToolParameters toolParameters);
    }
}

