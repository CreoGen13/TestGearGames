using System;
using Base;
using Base.Classes;
using Base.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BaseUI
{
    public abstract class BaseMenuView<T> : BaseView<T>, IMenuView
        where T : IMenuPresenter
    {
        public event Action MenuOpened;
        public event Action MenuClosed;
        
        [Header("Main")]
        [SerializeField] protected RectTransform windowTransform;
        [SerializeField] protected Image background;

        protected virtual void InitButtons(){}
        
        public abstract void OpenMenuInstant();
        public abstract void CloseMenuInstant();
        
        protected void OnMenuOpened()
        {
            MenuOpened?.Invoke();
        }
        protected void OnMenuClosed()
        {
            MenuClosed?.Invoke();
        }

    }
}