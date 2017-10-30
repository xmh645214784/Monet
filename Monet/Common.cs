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
    }
}
