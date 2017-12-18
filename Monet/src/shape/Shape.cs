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
    public abstract class Shape: Selectable,ActionParameters
    {
        public abstract bool IsSelectMe(Point point);

        public abstract void ShowAsNotSelected();

        public abstract void ShowAsSelected();
    }
}
