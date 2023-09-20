using System;
using Base.Interfaces;

namespace UI.BaseUI
{
    public interface IMenuView : IView
    {
        public event Action MenuOpened;
        public event Action MenuClosed;
        
        public void OpenMenuInstant();
        public void CloseMenuInstant();
    }
}