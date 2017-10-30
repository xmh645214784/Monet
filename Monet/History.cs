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

        public void PushBackAction(Image action)
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

        public Image TopAction()
        {
            return historyArray[index] as Image;
        }
    }
}

//    class History
//    {
//        public ArrayList historyArray ;
//        static History mInstance;
//        private History()
//        {
//            historyArray = new ArrayList();
//        }
//        public static History GetInstance()
//        {
//            if (mInstance == null)
//            {
//                mInstance = new History();
//            }
//            return mInstance;
//        }
//        public void PushBackAction(Image action)
//        {
//            historyArray.Add(action);
//        }

//        public Image UndoAction()
//        {
//            Image result=historyArray[historyArray.Count - 1] as Image;
//            historyArray.RemoveAt(historyArray.Count - 1);
//            return result;
//        }


//        public Image TopAction()
//        {
//            return historyArray[historyArray.Count - 1] as Image;
//        }
//    }
//}