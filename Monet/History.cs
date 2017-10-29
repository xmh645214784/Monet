using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

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
            historyArray = new ArrayList();
        }
        public static History GetInstance()
        {
            if(mInstance==null)
            {
                mInstance = new History();
            }
            return mInstance;
        }

        public void PushBackAction(Image action)
        {
            if (index == historyArray.Capacity - 1)
            {
                historyArray.Add(action);
                index++;
            }
            else
            {
                historyArray[++index] = action;
            }
            canUndo = true;
        }

        public Image UndoAction()
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index == 1)
                canUndo = false;

            canRedo = true;
            return historyArray[--index] as Image;
        }

        public Image RedoAction()
        {
            if (index == historyArray.Count - 1)
                throw new ArgumentOutOfRangeException();
            else if (index == historyArray.Count - 2)
                canRedo = false;

            return historyArray[++index] as Image;
        }
    }
}
