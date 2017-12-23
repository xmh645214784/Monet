///-------------------------------------------------------------------------------------------------
/// \file src\ui\MoveableButton.cs.
///
/// \brief Implements the moveable button class
///-------------------------------------------------------------------------------------------------

using Monet.src.history;
using Monet.src.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src.ui
{
    ///-------------------------------------------------------------------------------------------------
    /// \class MoveableButton
    ///
    /// \brief A moveable button.
    ///-------------------------------------------------------------------------------------------------

    public class MoveableButton : Button
    {

        /// \brief The main view control
        PictureBox mainView;

        /// \brief The temporary point
        Point tempPoint;
        /// \brief True if this object is enabled
        bool isEnabled = false;

        /// \brief The start location
        Point startLocation;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public MoveableButton(PictureBox mainView, Point location, Cursor cursor ) : base()
        ///
        /// \brief Constructor
        ///
        /// \param mainView The main view control.
        /// \param location The location.
        /// \param cursor   The cursor.
        ///-------------------------------------------------------------------------------------------------

        public MoveableButton(PictureBox mainView,
                            Point location,
                            Cursor cursor
                            ) : base()
        {

            this.mainView = mainView;
            this.Location = startLocation = location;
            this.Cursor = cursor;
            this.Size = new Size(10, 10);
            mainView.Controls.Add(this);
            this.Show();
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void Disappear()
        ///
        /// \brief Disappears this object
        ///-------------------------------------------------------------------------------------------------

        public void Disappear()
        {
            this.Enabled = false;
            this.Visible = false;
            mainView.Controls.Remove(this);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn protected override void OnMouseDown(MouseEventArgs e)
        ///
        /// \brief 引发 <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEven
        ///     tArgs)" /> 事件。
        ///
        /// \param e 包含事件数据的 <see cref="T:System.Windows.Forms.MouseEventArgs" />。.
        ///-------------------------------------------------------------------------------------------------

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tempPoint = e.Location;
                isEnabled = true;
            }
            base.OnMouseDown(e);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn protected override void OnMouseMove(MouseEventArgs mevent)
        ///
        /// \brief 引发 <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEven
        ///     tArgs)" /> 事件。
        ///
        /// \param mevent 包含事件数据的 <see cref="T:System.Windows.Forms.MouseEventArgs" />。.
        ///-------------------------------------------------------------------------------------------------

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (isEnabled)
            {
                this.Location = new Point(this.Left + (mevent.X - tempPoint.X),
                        this.Top + (mevent.Y - tempPoint.Y));
            }
            base.OnMouseMove(mevent);
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn protected override void OnMouseUp(MouseEventArgs mevent)
        ///
        /// \brief 引发 <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEve
        ///     ntArgs)" /> 事件。
        ///
        /// \param mevent 包含事件数据的 <see cref="T:System.Windows.Forms.MouseEventArgs" />。.
        ///-------------------------------------------------------------------------------------------------

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                isEnabled = false;
            }
            base.OnMouseUp(mevent);
            History.GetInstance().Update();
        }
    }
}
