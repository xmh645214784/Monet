///-------------------------------------------------------------------------------------------------
/// \file src\ui\MoveButton.cs.
///
/// \brief Implements the move button class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Monet.src.shape;

namespace Monet.src.ui
{
    ///-------------------------------------------------------------------------------------------------
    /// \class MoveButton
    ///
    /// \brief A move button.
    ///-------------------------------------------------------------------------------------------------

    public class MoveButton : MoveableButtonWithDoubleBuffering
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn public MoveButton(PictureBox mainView, Shape shape, Point location, Cursor cursor) : base(mainView, shape, location, cursor)
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        /// \param shape    The shape.
        /// \param location The location.
        /// \param cursor   The cursor.
        ///-------------------------------------------------------------------------------------------------

        public MoveButton(PictureBox mainView, Shape shape, Point location, Cursor cursor) : base(mainView, shape, location, cursor)
        {
        }
    }
}
