using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.history
{
    class ToolAction
    {
        Tool tool;
        ToolParameters toolParameters;

        public ToolAction(Tool tool,
        ToolParameters toolParameters)
        {
            this.tool = tool;
            this.toolParameters = toolParameters;
        }

        internal ToolParameters ToolParameters { get => toolParameters; set => toolParameters = value; }
        internal Tool Tool { get => tool; set => tool = value; }

        internal void Action()
        {
            tool.MakeAction(toolParameters);
        }
    }


    interface Actionable
    {
        void MakeAction(ToolParameters toolParameters);
    }
}
