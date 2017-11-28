using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Monet.src.history;

namespace Monet
{
    abstract class Tool: Actionable
    {
        protected PictureBox mainView;

        public Tool(PictureBox mainView)
        {
            this.mainView = mainView;
        }

        abstract public void MakeAction(ToolParameters toolParameters);


        virtual public void RegisterTool()
        {

        }
        //{
        //   ToolKit.GetInstance().currentTool.bindingButton.BackColor = Color.Transparent;
        //   bindingButton.BackColor = Color.Cornsilk;

        //}
        virtual public void UnRegisterTool()
        {

        }
        //{
        //    bindingButton.BackColor = Color.Transparent;
        //}
    }
    abstract class DrawShapeTool:Tool
    {
        protected bool isEnabled;
        public DrawShapeTool(PictureBox mainView) : base(mainView)
        {
            isEnabled = false;
        }
    }
}

