using UI.BaseUI;
using UniRx;

namespace UI.BacklogMenu
{
    public class BacklogMenuModel : BaseMenuModel<BacklogMenuModel>
    {
        public BacklogMenuModel()
        {
            Subject = new BehaviorSubject<BacklogMenuModel>(this);
        }
        public override void Update()
        {
            Subject.OnNext(this);
        }
    }
}