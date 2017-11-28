using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using Monet.src.history;

namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class History
    ///
    /// \brief A singleton class stores all what user draw step by step.
    ///        And it can be used to undo and redo operations
    ///-------------------------------------------------------------------------------------------------

     class History
     {
        /// \brief Array of histories images. 
        ///        We record the image after user's each step.
        private ArrayList historyArray;
        /// \brief Zero-based index of the last valid element in array.
        int index = -1;

        /// \brief The instance.
        static History mInstance;

        private bool canUndo;
        public bool CanUndo { get => canUndo; }

        private bool canRedo;
        public bool CanRedo { get => canRedo; }

        private History()
        {
            canUndo = canRedo = false;
            historyArray = new ArrayList();
        }
        public static History GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new History();
            }
            return mInstance;
        }

        public void PushBackAction(ToolAction action)
        {
            if (index == historyArray.Count - 1)
            {
                historyArray.Add(action);
                index++;
            }
            else
            {
                historyArray[++index] = action;
            }
            if (index >= 1)
                canUndo = true;
        }

        public void UndoAction()
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index == 1)
                canUndo = false;

            canRedo = true;
            --index;
        }

        public void RedoAction()
        {
            if (index == historyArray.Count - 1)
                throw new ArgumentOutOfRangeException();
            else if (index == historyArray.Count - 2)
                canRedo = false;

            ++index;
        }


    }
}
