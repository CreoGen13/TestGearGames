using System;
using Base.Classes;
using UI.MainMenu;
using UnityEngine;

namespace UI.BaseUI
{
    public abstract class BaseMenuPresenter<TModel, TView> : BasePresenter<TModel, TView>, IMenuPresenter
        where TModel : BaseMenuModel<TModel>
        where TView : IMenuView
    {
        public event Action<MenuType> OtherMenuCall;
        public event Action OnMenuOpened
        {
            add => View.MenuOpened += value;
            remove => View.MenuOpened -= value;
        }
        public event Action OnMenuClosed
        {
            add => View.MenuClosed += value;
            remove => View.MenuClosed -= value;
        }
        protected BaseMenuPresenter(TModel model, TView view)
            : base(model, view) {}
        
        public virtual void ChangeMenuState(MenuState state)
        {
            switch (state)
            {
                case MenuState.Active:
                {
                    Model.IsBlocked = false;
                    Model.Update();

                    break;
                }
                case MenuState.Blocked:
                {
                    Model.IsBlocked = true;
                    Model.Update();
                    
                    break;
                }
                case MenuState.Inactive:
                {
                    Model.IsValidating = false;
                    Model.IsBlocked = false;
                    Model.Update();
                    View.CloseMenuInstant();
                    
                    break;
                }
            }
        }

        protected void OnOtherMenuCall(MenuType menuType)
        {
            if (OtherMenuCall == null)
            {
                Debug.LogError("Action \"OtherMenuCall\" is empty");
                return;
            }
            
            OtherMenuCall.Invoke(menuType);
        }
    }
}