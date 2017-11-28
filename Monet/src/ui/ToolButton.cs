using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.tools
{
    class ToolButton:Button
    {
        public static ToolButton currentButton;
        Tool bindingTool;

        public ToolButton()
        {
            this.MouseClick += ToolButton_MouseClick;
        }

        private void ToolButton_MouseClick(object sender, MouseEventArgs e)
        {
            ToolKit.GetInstance().currentTool.UnRegisterTool();
            ToolKit.GetInstance().currentTool = BindingTool;
            ToolKit.GetInstance().currentTool.RegisterTool();

            currentButton.BackColor = Color.Transparent;
            this.BackColor = Color.Cornsilk;
            currentButton = this;
        }

        internal Tool BindingTool
        {
            get
            {
                if (bindingTool == null)
                    throw new NullReferenceException();
                return bindingTool;
            }
            set => bindingTool = value;
        }
    }
}
