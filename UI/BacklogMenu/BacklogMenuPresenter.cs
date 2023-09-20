using System;
using UI.BaseUI;

namespace UI.BacklogMenu
{
    public class BacklogMenuPresenter : MenuPresenter<BacklogMenuModel, BacklogMenuView>
    {
        public BacklogMenuPresenter(BacklogMenuModel model, BacklogMenuView view)
            : base(model, view)
        {
            InitSubscriptions();
        }

        protected sealed override void InitSubscriptions()
        {
            AddSubscriptionWithDistinct<bool>(nameof(Model.IsBusy), (value) =>
            {
                View.SetButtonsInteractable(!value);
            });
        }

        public override void OpenMenu(Action onComplete = null)
        {
            if(Model.IsValidating)
                return;
            
            Model.IsValidating = true;
            Model.Update();
            
            View.OpenMenu(() =>
            {
                Model.IsValidating = false;
                Model.Update();
                
                onComplete?.Invoke();
            });
        }

        public override void CloseMenu(Action onComplete = null)
        {
            if(Model.IsValidating)
                return;
            
            Model.IsValidating = true;
            Model.Update();
            
            View.CloseMenu(() =>
            {
                Model.IsValidating = false;
                Model.Update();
                
                onComplete?.Invoke();
            });
        }

        #region Buttons

        public void OnClickButtonExit()
        {
            CloseMenu();
        }

        #endregion
    }
}