using Monet.src.history;
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
        public Pen backUpPen=null;
        public Pen solidPen;

        

        protected bool isResizing = false;
        protected bool isMoving = false;

        public Shape()
        {
            solidPen = new Pen(Color.Blue);
            solidPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            solidPen.DashPattern = new float[] { 1f, 1f };
        }

        public abstract object Clone();

        public abstract bool IsSelectMe(Point point);

        public virtual void ShowAsNotSelected()
        {
            if (backUpPen != null)
            {
                this.pen = backUpPen;
                backUpPen = null;
            }
        }

        public virtual void ShowAsSelected()
        {
             backUpPen = (Pen)pen.Clone();
             pen = solidPen.Clone() as Pen;
        }

        public void BaseShowAsNotSelected()
        {
            if (backUpPen != null)
            {
                this.pen = backUpPen;
                backUpPen = null;
            }
        }

        public void BaseShowAsSelected()
        {
            backUpPen = (Pen)pen.Clone();
            pen = solidPen.Clone() as Pen;
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
