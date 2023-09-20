using Base.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Base.Classes
{
    public abstract class BaseView<T> : MonoBehaviour, IView
        where T : IPresenter
    {
        protected T Presenter;
        protected Sequence Sequence;

        private void OnDestroy()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            if (Presenter == null)
            {
                Debug.LogError("Presenter for " + typeof(T) + " was not assigned");
                    return;
            }
            Presenter.Dispose();
        }
    }
}