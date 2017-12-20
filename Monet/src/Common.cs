//Line: Alternative
//  LINE_SYSTEM 
//  LINE_DDA 
//  LINE_MIDPOINT 
//  LINE_BRESENHAM
#define LINE_DDA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Monet
{
    public sealed class Ref<T>
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
        public T Value { get { return getter(); } set { setter(value); } }
    }
    class Common
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn static void DrawPix(Graphics g,Point p,Pen pen)
        ///
        /// \brief Draw pix. This is the only drawing method what I use in my implementing across all the assignment.
        ///
        /// \param g   The Graphics to process.
        /// \param p   A Point to process.
        /// \param pen The pen.
        ///-------------------------------------------------------------------------------------------------
        public static void  DrawPix(Graphics g,Point p,Pen pen)
        {
            g.DrawRectangle(pen, p.X, p.Y, 1, 1);
        }

        public static Rectangle Rectangle(Point p1,Point p2)
        {
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);
            Point start = new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            return new Rectangle(start, new Size(width, height));
        }
    }
}
