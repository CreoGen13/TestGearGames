using Base.Classes;

namespace UI.BaseUI
{
    public abstract class BaseMenuModel<T> : BaseModel<T>
        where T : BaseMenuModel<T>
    {
        public bool IsValidating;
        public bool IsBlocked;

        public bool IsBusy => IsBlocked || IsValidating;
    }
}