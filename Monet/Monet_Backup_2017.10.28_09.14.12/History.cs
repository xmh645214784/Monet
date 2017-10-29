using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Monet
{
    class Action
    {
        Tool tool;
        ToolParameters toolParameters;
        internal Tool Tool { get => tool; set => tool = value; }
        internal ToolParameters ToolParameters { get => toolParameters; set => toolParameters = value; }
    }
    class History
    {
        private ArrayList actionsArray;

        static History mInstance;
        private History()
        {
            ;
        }
        public static History GetInstance()
        {
            if(mInstance==null)
            {
                mInstance = new History();
            }
            return mInstance;
        }

        public void PushBackAction(Action action)
        {
            actionsArray.Add(action);
        }
    }
}
