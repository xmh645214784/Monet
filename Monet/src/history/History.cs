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
using static Monet.src.tools.ResizeCanvasButton;
using Monet.src.shape;

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
        public ArrayList historyArray;


        /// \brief Zero-based index of the last valid element in array. 
        ///        【重要】index为当前最后的Action
        int index =-1;

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

        public int Index { get => index;}

        ///-------------------------------------------------------------------------------------------------
        /// \fn private History()
        ///
        /// \brief Constructor that prevents a default instance of this class from being created
        ///-------------------------------------------------------------------------------------------------

        private History()
        {
            CanUndo = CanRedo = false;
            historyArray = new ArrayList();
            ResizeParam actionParameters = new ResizeParam();
            actionParameters.backgroundColor = Setting.GetInstance().BackgroundColor;
            actionParameters.size = MainWin.GetInstance().MainView().Image.Size;
            PushBackAction(new MAction(MainWin.GetInstance().resizePictureBoxButton, actionParameters));
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
            if(index!=0)
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
            if (Index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (Index == 1) //this is one because we have a empty resize
                CanUndo = false;

            CanRedo = true;

            if(historyArray[index] is BackUpMAction)
            {
                BackUpMAction backUpMAction = (BackUpMAction)historyArray[index];
                backUpMAction.ExchangeParameters();
            }
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
            if (Index == historyArray.Count - 1)
                throw new ArgumentOutOfRangeException();
            else if (Index == historyArray.Count - 2)
                CanRedo = false;
            CanUndo = true;
            ++index;
            if(historyArray[index] is BackUpMAction)
            {
                BackUpMAction ba = (BackUpMAction)historyArray[index];
                ba.ExchangeParameters();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void RedrawAllActions()
        ///
        /// \brief Redraw all actions
        ///-------------------------------------------------------------------------------------------------

        public void Update()
        {
            MainWin.GetInstance().ClearScreen();
            for (int i=0;i<=Index;i++)
            {
                MAction p = (MAction)historyArray[i];
                p.Action();
            }
        }

        public bool FindShapeInHistory(Shape shape,out MAction mActionInPosition)
        {
            for (int i = 0; i <= Index; i++)
            {
                MAction p = (MAction)historyArray[i];
                if (p is BackUpMAction)
                    continue;
                try
                {
                    Shape shapeIter = (Shape)p.ActionParameters;
                    if (shapeIter.Equals(shape))
                    {
                        mActionInPosition = p;
                        return true;
                    }
                }
                catch (InvalidCastException)
                {
                    ;
                }    
            }
            mActionInPosition = null;
            return false;
        }

        public void AddBackUpClone(MAction mAction)
        {
            Shape shape=(Shape)mAction.ActionParameters;
            shape.BaseShowAsNotSelected();
            PushBackAction(new BackUpMAction((MAction)mAction.Clone(), mAction));
            shape.BaseShowAsSelected();
        }

        public void UnSelectAll()
        {
            for (int i = 0; i <= Index; i++)
            {
                if (historyArray[i] is BackUpMAction)
                    continue;
                try
                {
                    ActionParameters_t actionParameters = ((MAction)historyArray[i]).ActionParameters;
                    Shape shape_temp = (Shape)actionParameters;
                    shape_temp.ShowAsNotSelected();
                }
                catch (InvalidCastException)
                {
                    ;
                }
            }
        }
    }
}
