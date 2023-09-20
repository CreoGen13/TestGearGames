using System;
using Base;
using Base.Interfaces;

namespace UI.BaseUI
{
    public abstract class MenuView<T> : BaseMenuView<T>
        where T : IMenuPresenter
    {
        public abstract void OpenMenu(Action onComplete = null);
        public abstract void CloseMenu(Action onComplete = null);
    }
}