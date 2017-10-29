﻿using System;
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
        public Tool currentTool;

        static Tool pointerTool;
        static Tool lineTool;
        
        internal Tool LineTool { get => lineTool; }
        internal Tool PointerTool { get => pointerTool;}

        internal ToolKit(PictureBox mainView,
                         Button pointerButton,
                         Button lineButton)
        {
            this.mainView = mainView;
            pointerTool = new PointerTool   (mainView,pointerButton);
            lineTool    = new LineTool      (mainView,lineButton);
        }
        
    }
}
