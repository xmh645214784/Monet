///-------------------------------------------------------------------------------------------------
/// \file src\shape\shape.cs.
///
/// \brief Implements the shape class
///-------------------------------------------------------------------------------------------------

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
    ///-------------------------------------------------------------------------------------------------
    /// \interface Selectable
    ///
    /// \brief Interface for selectable.
    ///-------------------------------------------------------------------------------------------------

    interface Selectable
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void ShowAsSelected();
        ///
        /// \brief Shows as selected
        ///-------------------------------------------------------------------------------------------------

        void ShowAsSelected();

        ///-------------------------------------------------------------------------------------------------
        /// \fn void ShowAsNotSelected();
        ///
        /// \brief Shows as not selected
        ///-------------------------------------------------------------------------------------------------

        void ShowAsNotSelected();
    }

    ///-------------------------------------------------------------------------------------------------
    /// \interface Resizeable
    ///
    /// \brief Interface for resizeable.
    ///-------------------------------------------------------------------------------------------------

    interface Resizeable
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void ShowAsResizing();
        ///
        /// \brief Shows as resizing
        ///-------------------------------------------------------------------------------------------------

        void ShowAsResizing();

        ///-------------------------------------------------------------------------------------------------
        /// \fn void ShowAsNotResizing();
        ///
        /// \brief Shows as not resizing
        ///-------------------------------------------------------------------------------------------------

        void ShowAsNotResizing();
    }

    ///-------------------------------------------------------------------------------------------------
    /// \interface Rotatable
    ///
    /// \brief Interface for rotatable.
    ///-------------------------------------------------------------------------------------------------

    interface Rotatable
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void Rotate(Point midPoint,double angle);
        ///
        /// \brief Rotates
        ///
        /// \param midPoint The middle point.
        /// \param angle    The angle.
        ///-------------------------------------------------------------------------------------------------

        void Rotate(Point midPoint,double angle);
    }

    ///-------------------------------------------------------------------------------------------------
    /// \interface Clipable
    ///
    /// \brief Interface for clipable.
    ///-------------------------------------------------------------------------------------------------

    interface Clipable
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void Clip(Rectangle rect);
        ///
        /// \brief Clips the given rectangle
        ///
        /// \param rect The rectangle.
        ///-------------------------------------------------------------------------------------------------

        void Clip(Rectangle rect);
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class Shape
    ///
    /// \brief A shape.
    ///-------------------------------------------------------------------------------------------------

    public abstract class Shape: Selectable,ActionParameters_t
    {
        /// \brief The binding action
        MAction bindingAction=null;
        /// \brief The pen
        public Pen pen;

        /// \brief The back color
        public Color backColor=Setting.GetInstance().BackgroundColor;

        /// \brief True if this object is adjusting
        protected bool isAdjusting = false;
        /// \brief True if this object is moving
        protected bool isMoving = false;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public abstract object Clone();
        ///
        /// \brief Makes a deep copy of this object
        ///
        /// \return A copy of this object.
        ///-------------------------------------------------------------------------------------------------

        public abstract object Clone();

        ///-------------------------------------------------------------------------------------------------
        /// \fn public abstract bool IsSelectMe(Point point);
        ///
        /// \brief Query if 'point' is select me
        ///
        /// \param point The point.
        ///
        /// \return True if select me, false if not.
        ///-------------------------------------------------------------------------------------------------

        public abstract bool IsSelectMe(Point point);

        ///-------------------------------------------------------------------------------------------------
        /// \fn public virtual void ShowAsNotSelected()
        ///
        /// \brief Shows as not selected
        ///-------------------------------------------------------------------------------------------------

        public virtual void ShowAsNotSelected()
        {
           
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public virtual void ShowAsSelected()
        ///
        /// \brief Shows as selected
        ///-------------------------------------------------------------------------------------------------

        public virtual void ShowAsSelected()
        {
           
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public MAction RetMAction()
        ///
        /// \brief Ret m action
        ///
        /// \return A MAction.
        ///-------------------------------------------------------------------------------------------------

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
