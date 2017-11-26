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
        protected Button bindingButton;

        public Tool(PictureBox mainView,Button button)
        {
            this.mainView = mainView;
            this.bindingButton = button;
        }

        public Button BindingButton { get => bindingButton;}

        virtual public void RegisterTool()
        {
           ToolKit.GetInstance().currentTool.bindingButton.BackColor = Color.Transparent;
           bindingButton.BackColor = Color.Cornsilk;

        }
        virtual public void UnRegisterTool()
        {
            bindingButton.BackColor = Color.Transparent;
        }
    }
    abstract class DrawShapeTool:Tool
    {
        protected bool isEnabled;
        public DrawShapeTool(PictureBox mainView, Button button) : base(mainView,button)
        {
            isEnabled = false;
        }
    }
}

