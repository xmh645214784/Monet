///-------------------------------------------------------------------------------------------------
/// \file src\history\History.cs.
///
/// \brief Implements the history class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using Monet.src.history;
using System.Windows.Forms;

namespace Monet
{
     ///-------------------------------------------------------------------------------------------------
     /// \class History
     ///
     /// \brief A singleton class stores all what user draw step by step. And it can be used to undo
     ///    and redo operations
     ///-------------------------------------------------------------------------------------------------

     class History
     {
        /// \brief Array of histories images. We record the image after user's each step.
        private ArrayList historyArray;
        public ArrayList shapeArray;


        /// \brief Zero-based index of the last valid element in array. 
        ///        【重要】index为当前最后的Action
        int index = -1;

        /// \brief The instance.
        static History mInstance;

        /// \brief True if this object can undo
        private bool canUndo;

        /// \brief True if this object can redo
        private bool canRedo;

        ///-------------------------------------------------------------------------------------------------
        /// \property public bool CanUndo
        ///
        /// \brief Gets or sets a value indicating whether we can undo
        ///
        /// \return True if we can undo, false if not.
        ///-------------------------------------------------------------------------------------------------

        public bool CanUndo
        {
            get => canUndo;
            set
            {
                canUndo = value;
                MainWin.GetInstance().undoButton.Enabled = value;
                if(value==false)
                {
                    MainWin.GetInstance().undoButton.Cursor = Cursors.No;
                }
                else
                    MainWin.GetInstance().undoButton.Cursor = Cursors.Default;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \property public bool CanRedo
        ///
        /// \brief Gets or sets a value indicating whether we can redo
        ///
        /// \return True if we can redo, false if not.
        ///-------------------------------------------------------------------------------------------------

        public bool CanRedo
        {
            get => canRedo;
            set
            {
                canRedo = value;
                MainWin.GetInstance().redoButton.Enabled = value;
                if (value == false)
                {
                    MainWin.GetInstance().redoButton.Cursor = Cursors.No;
                }
                else
                    MainWin.GetInstance().redoButton.Cursor = Cursors.Default;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn private History()
        ///
        /// \brief Constructor that prevents a default instance of this class from being created
        ///-------------------------------------------------------------------------------------------------

        private History()
        {
            CanUndo = CanRedo = false;
            historyArray = new ArrayList();
            shapeArray = new ArrayList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public static History GetInstance()
        ///
        /// \brief Gets the instance
        ///
        /// \return The instance.
        ///-------------------------------------------------------------------------------------------------

        public static History GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new History();
            }
            return mInstance;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void PushBackAction(src.history.MAction action)
        ///
        /// \brief Pushes a back action
        ///
        /// \param action The action.
        ///-------------------------------------------------------------------------------------------------

        public void PushBackAction(src.history.MAction action)
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
            CanUndo = true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void UndoAction()
        ///
        /// \brief Undo action
        ///
        /// \exception ArgumentOutOfRangeException
        /// Thrown when one or more arguments are outside the
        /// required range.
        ///-------------------------------------------------------------------------------------------------

        public void UndoAction()
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (index == 0)
                CanUndo = false;

            CanRedo = true;

            --index;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void RedoAction()
        ///
        /// \brief Redo action
        ///
        /// \exception ArgumentOutOfRangeException
        /// Thrown when one or more arguments are outside the
        /// required range.
        ///-------------------------------------------------------------------------------------------------

        public void RedoAction()
        {
            if (index == historyArray.Count - 1)
                throw new ArgumentOutOfRangeException();
            else if (index == historyArray.Count - 2)
                CanRedo = false;
            CanUndo = true;
            ++index;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void RedrawAllActions()
        ///
        /// \brief Redraw all actions
        ///-------------------------------------------------------------------------------------------------

        public void RedrawAllActions()
        {
            for (int i=0;i<=index;i++)
            {
                src.history.MAction p = (src.history.MAction)historyArray[i];
                p.Action();
            }
        }
    }
}
