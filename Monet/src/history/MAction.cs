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

        internal virtual void Action()
        {
            actionable.MakeAction(actionParameters);
        }
    }

    class BackUpMAction : MAction
    {
        public BackUpMAction(Actionable actionable, ActionParameters toolParameters) : base(actionable, toolParameters)
        {
        }

        internal override void Action()
        {
            //do nothing;
        }
    }


    interface Actionable
    {
        void MakeAction(ActionParameters toolParameters);
    }
}
