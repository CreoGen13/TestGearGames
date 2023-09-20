using System;
using UniRx;

namespace Base.Classes
{
    public abstract class BaseModel<T>
        where T : BaseModel<T>
    {
        protected BehaviorSubject<T> Subject;

        public IObservable<T> Observe()
        {
            return Subject.AsObservable();
        }

        public abstract void Update();
    }
}