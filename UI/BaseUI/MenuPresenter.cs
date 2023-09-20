using System;

namespace UI.BaseUI
{
    public abstract class MenuPresenter<TModel, TView> : BaseMenuPresenter<TModel, TView>
        where TModel : BaseMenuModel<TModel>
        where TView : IMenuView
    {
        protected MenuPresenter(TModel model, TView view)
            : base(model, view) {}
        
        public abstract void OpenMenu(Action onComplete = null);
        public abstract void CloseMenu(Action onComplete = null);
    }
}