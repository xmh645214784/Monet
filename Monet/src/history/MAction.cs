using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.history
{
    class MAction
    {
        Actionable actionable;
        ActionParameters actionParameters;

        public MAction(Actionable actionable,
        ActionParameters toolParameters)
        {
            this.actionable = actionable;
            this.actionParameters = toolParameters;
        }

        internal ActionParameters ActionParameters { get => actionParameters; set => actionParameters = value; }
        internal Actionable Actionable { get => actionable; set => actionable = value; }

        internal void Action()
        {
            actionable.MakeAction(actionParameters);
        }
    }


    interface Actionable
    {
        void MakeAction(ActionParameters toolParameters);
    }
}
