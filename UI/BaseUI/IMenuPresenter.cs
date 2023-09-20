using Base.Interfaces;
using UI.MainMenu;

namespace UI.BaseUI
{
    public interface IMenuPresenter : IPresenter
    {
        public void ChangeMenuState(MenuState state);
    }
}