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

    ///-------------------------------------------------------------------------------------------------
    /// \class History
    ///
    /// \brief A singleton class stores all what user draw step by step.
    ///        And it can be used to undo and redo operations 
    ///-------------------------------------------------------------------------------------------------

    class History
    {
        private ArrayList actionsArray;
        /// \brief Zero-based index of the last valid element in array.
        int index;
        /// \brief The instance.
        static History mInstance;

        private bool canUndo;
        public bool CanUndo { get => canUndo; }

        private bool canRedo;     
        public bool CanRedo { get => canRedo;}

        private History()
        {
            index = 0 ;
            canUndo = canRedo = false;
            actionsArray = new ArrayList();
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
            if (index == actionsArray.Capacity - 1)
            {
                actionsArray.Add(action);
                index++;
            }
            else
            {
                actionsArray[++index] = action;
            }
            canUndo = true;
        }

        public Action UndoAction()
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index == 1)
                canUndo = false;

            canRedo = true;
            return actionsArray[--index] as Action;
        }

        public Action RedoAction()
        {
            if (index == actionsArray.Count - 1)
                throw new ArgumentOutOfRangeException();
            else if (index == actionsArray.Count - 2)
                canRedo = false;

            return actionsArray[++index] as Action;
        }
    }
}
