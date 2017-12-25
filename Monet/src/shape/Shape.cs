using Monet.src.history;
using Monet.src.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.shape
{
    interface Selectable
    {
        void ShowAsSelected();
        void ShowAsNotSelected();
    }

    interface Resizeable
    {
        void ShowAsResizing();
        void ShowAsNotResizing();
    }

    interface Rotatable
    {
        void Rotate(Point midPoint,double angle);
    }

    public abstract class Shape: Selectable,ActionParameters_t
    {
        MAction bindingAction=null;
        /// \brief The pen
        public Pen pen;        

        protected bool isResizing = false;
        protected bool isMoving = false;

        public abstract object Clone();

        public abstract bool IsSelectMe(Point point);

        public virtual void ShowAsNotSelected()
        {
           
        }

        public virtual void ShowAsSelected()
        {
           
        }



        public MAction RetMAction()
        {
            if (bindingAction==null)
            {
                History his = History.GetInstance();
                his.FindShapeInHistory(this, out bindingAction);
            }
            return bindingAction;
        }
    }
}
