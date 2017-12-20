using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.history
{
    public class MAction:ICloneable
    {
        Actionable actionable;
        ActionParameters_t actionParameters;
        public bool visible=true;

        public MAction(Actionable actionable,
        ActionParameters_t toolParameters)
        {
            this.actionable = actionable;
            this.actionParameters = toolParameters;
        }

        public ActionParameters_t ToolParameters { get; internal set; }
        internal ActionParameters_t ActionParameters { get => actionParameters; set => actionParameters = value; }
        internal Actionable Actionable { get => actionable; set => actionable = value; }

        public object Clone()
        {
            MAction copy = new MAction(actionable,actionParameters);
            return copy;
        }

        internal virtual void Action()
        {
            if(visible)
                actionable.MakeAction(actionParameters);
        }
    }

    //  decorator
    class BackUpMAction : MAction
    {
        MAction originalAction;

        public BackUpMAction(MAction mAction) : base(mAction.Actionable, mAction.ToolParameters)
        {
            originalAction = mAction;
        }

        internal override void Action()
        {
            //do nothing;
        }

        public void ExchangeParameters()
        {
            ActionParameters_t temp = originalAction.ActionParameters;
            originalAction.ActionParameters = this.ActionParameters;
            this.ActionParameters = temp;
            
        }
    }


    public interface Actionable
    {
        void MakeAction(ActionParameters_t toolParameters);
    }
}
