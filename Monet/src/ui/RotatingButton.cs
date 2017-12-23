///-------------------------------------------------------------------------------------------------
/// \file src\ui\RotatingButton.cs.
///
/// \brief Implements the rotating button class
///-------------------------------------------------------------------------------------------------

using Monet.src.ui;
using System.Windows.Forms;
using Monet.src.shape;
using System.Drawing;

namespace Monet.src.tools
{
    ///-------------------------------------------------------------------------------------------------
    /// \class RotatingButton
    ///
    /// \brief A rotating button.
    ///-------------------------------------------------------------------------------------------------

    class RotatingButton : MoveableButton
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn public RotatingButton(PictureBox pictureBox, Point location,Cursor cursor) : base(pictureBox,location,cursor)
        ///
        /// \brief Constructor
        ///
        /// \param pictureBox The picture box control.
        /// \param location   The location.
        /// \param cursor     The cursor.
        ///-------------------------------------------------------------------------------------------------

        public RotatingButton(PictureBox pictureBox, Point location,Cursor cursor) : base(pictureBox,location,cursor)
        {
            this.Size = new Size(10, 10);
            this.BackColor = Color.Green;
            this.ForeColor = Color.Green;
        }
    }
}
