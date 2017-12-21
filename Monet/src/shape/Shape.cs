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
    public abstract class Shape: Selectable,ActionParameters_t
    {
        MAction bindingAction=null;
        /// \brief The pen
        public Pen pen;
        public abstract object Clone();

        public abstract bool IsSelectMe(Point point);

        public virtual void ShowAsNotSelected()
        {
            this.pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //this.pen.Color = Color.Black;
        }

        public virtual void ShowAsSelected()
        {
            //this.pen.Color = Color.DeepSkyBlue;
            this.pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.pen.DashPattern = new float[] { 1f, 1f };
        }

        protected MAction RetMAction()
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
