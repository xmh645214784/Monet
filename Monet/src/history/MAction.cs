///-------------------------------------------------------------------------------------------------
/// \file src\history\MAction.cs.
///
/// \brief Implements the action class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.history
{
    ///-------------------------------------------------------------------------------------------------
    /// \class MAction
    ///
    /// \brief An action.
    ///-------------------------------------------------------------------------------------------------

    public class MAction:ICloneable
    {
        /// \brief The actionable
        Actionable actionable;
        /// \brief Options for controlling the action
        ActionParameters_t actionParameters;
        /// \brief True to show, false to hide
        public bool visible=true;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public MAction(Actionable actionable, ActionParameters_t toolParameters)
        ///
        /// \brief Constructor
        ///
        /// \param actionable     The actionable.
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        public MAction(Actionable actionable,
        ActionParameters_t toolParameters)
        {
            this.actionable = actionable;
            this.actionParameters = toolParameters;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \property public ActionParameters_t ToolParameters
        ///
        /// \brief Gets or sets options for controlling the tool
        ///
        /// \return Options that control the tool.
        ///-------------------------------------------------------------------------------------------------

        public ActionParameters_t ToolParameters { get; internal set; }

        ///-------------------------------------------------------------------------------------------------
        /// \property internal ActionParameters_t ActionParameters
        ///
        /// \brief Gets or sets options for controlling the action
        ///
        /// \return Options that control the action.
        ///-------------------------------------------------------------------------------------------------

        internal ActionParameters_t ActionParameters { get => actionParameters; set => actionParameters = value; }

        ///-------------------------------------------------------------------------------------------------
        /// \property internal Actionable Actionable
        ///
        /// \brief Gets or sets the actionable
        ///
        /// \return The actionable.
        ///-------------------------------------------------------------------------------------------------

        internal Actionable Actionable { get => actionable; set => actionable = value; }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public object Clone()
        ///
        /// \brief 创建作为当前实例副本的新对象。
        ///
        /// \return 作为此实例副本的新对象。.
        ///-------------------------------------------------------------------------------------------------

        public object Clone()
        {
            MAction copy = new MAction(actionable,(ActionParameters_t)actionParameters.Clone());
            return copy;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn internal virtual void Action()
        ///
        /// \brief Actions this object
        ///-------------------------------------------------------------------------------------------------

        internal virtual void Action()
        {
            if(visible)
                actionable.MakeAction(actionParameters);
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \class BackUpMAction
    ///
    /// \brief decorator
    ///-------------------------------------------------------------------------------------------------

    class BackUpMAction : MAction
    {
        /// \brief The original action
        MAction originalAction;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public BackUpMAction(MAction mAction,MAction orginalAction) : base(mAction.Actionable, mAction.ActionParameters)
        ///
        /// \brief Constructor
        ///
        /// \exception ArgumentNullException Thrown when one or more required arguments are null.
        ///
        /// \param mAction       The action.
        /// \param orginalAction The orginal action.
        ///-------------------------------------------------------------------------------------------------

        public BackUpMAction(MAction mAction,MAction orginalAction) : base(mAction.Actionable, mAction.ActionParameters)
        {
            this.originalAction = orginalAction ?? throw new ArgumentNullException(nameof(orginalAction));
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn internal override void Action()
        ///
        /// \brief Actions this object
        ///-------------------------------------------------------------------------------------------------

        internal override void Action()
        {
            //do nothing;
        }

        ///-------------------------------------------------------------------------------------------------
        /// \fn public void ExchangeParameters()
        ///
        /// \brief Exchange parameters
        ///-------------------------------------------------------------------------------------------------

        public void ExchangeParameters()
        {
            ActionParameters_t temp = originalAction.ActionParameters;
            originalAction.ActionParameters = this.ActionParameters;
            this.ActionParameters = temp;
            
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// \interface Actionable
    ///
    /// \brief Interface for actionable.
    ///-------------------------------------------------------------------------------------------------

    public interface Actionable
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn void MakeAction(ActionParameters_t toolParameters);
        ///
        /// \brief Makes an action
        ///
        /// \param toolParameters Options for controlling the tool.
        ///-------------------------------------------------------------------------------------------------

        void MakeAction(ActionParameters_t toolParameters);
    }
}
