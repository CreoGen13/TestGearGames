using System;
using System.Reflection;
using Base.Interfaces;
using UniRx;
using UnityEngine;

namespace Base.Classes
{
    public abstract class BasePresenter<TModel, TView> : IPresenter
        where TModel : BaseModel<TModel>
        where TView : IView
    {
        protected readonly TModel Model;
        protected readonly TView View;

        protected readonly CompositeDisposable Disposable = new CompositeDisposable();

        protected BasePresenter(TModel model, TView view)
        {
            View = view;
            Model = model;
        }
        
        protected abstract void InitSubscriptions();
        protected virtual void InitActions(){}

        protected void AddSubscription<T>(string fieldName, Action<T> onValueChanged)
        {
            try
            {
                Model
                    .Observe()
                    .Select(model => (T)typeof(TModel).InvokeMember (fieldName, BindingFlags.GetField | BindingFlags.GetProperty, null, model, null))
                    .Subscribe(value =>
                    {
                        onValueChanged?.Invoke(value);
                    })
                    .AddTo(Disposable);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("No such field or property as \"" + fieldName + "\" in class " + Model);
            }
        }
        protected void AddSubscriptionWithDistinct<T>(string fieldName, Action<T> onValueChanged)
        {
            try
            {
                Model
                    .Observe()
                    .Select(model => (T)typeof(TModel).InvokeMember(fieldName,
                        BindingFlags.GetField | BindingFlags.GetProperty, null, model, null))
                    .DistinctUntilChanged(state => state.GetHashCode())
                    .Subscribe(value => { onValueChanged?.Invoke(value); })
                    .AddTo(Disposable);
            }
            catch (MissingMethodException)
            {
                Debug.LogError("No such field or property as \"" + fieldName + "\" in class " + Model);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("One of the parameters in subscription is null");
            }
        }
        protected void AddCollectionSubscription<T>(string fieldName, Action<T, T> onValueChanged)
        {
            try
            {
                ((ReactiveCollection<T>)typeof(TModel).InvokeMember (fieldName, BindingFlags.GetField | BindingFlags.GetProperty, null, Model, null))
                    .ObserveReplace()
                    .Subscribe((value) =>
                    {
                        onValueChanged?.Invoke(value.OldValue, value.NewValue);
                    })
                    .AddTo(Disposable);

            }
            catch (NullReferenceException)
            {
                Debug.LogError("No such field or property as \"" + fieldName + "\" in class " + Model);
            }
        }
        protected void AddCollectionSubscriptionWithDistinct<T>(string fieldName, Action<T, T> onValueChanged)
        {
            try
            {
                ((ReactiveCollection<T>)typeof(TModel).InvokeMember (fieldName, BindingFlags.GetField | BindingFlags.GetProperty, null, Model, null))
                    .ObserveReplace()
                    .DistinctUntilChanged(state => state.GetHashCode())
                    .Subscribe((value) =>
                    {
                        onValueChanged?.Invoke(value.OldValue, value.NewValue);
                    })
                    .AddTo(Disposable);

            }
            catch (NullReferenceException)
            {
                Debug.LogError("No such field or property as \"" + fieldName + "\" in class " + Model);
            }
        }
        protected void AddDictionarySubscription<TKey, TValue>(string fieldName, Action<TKey, TValue, TValue> onValueChanged)
        {
            try
            {
                ((ReactiveDictionary<TKey, TValue>)typeof(TModel).InvokeMember (fieldName, BindingFlags.GetField | BindingFlags.GetProperty, null, Model, null))
                    .ObserveReplace()
                    .Subscribe((value) =>
                    {
                        onValueChanged?.Invoke(value.Key, value.OldValue, value.NewValue);
                    })
                    .AddTo(Disposable);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("No such field or property as \"" + fieldName + "\" in class " + Model);
            }
        }
        protected void AddDictionarySubscriptionWithDistinct<TKey, TValue>(string fieldName, Action<TKey, TValue, TValue> onValueChanged)
        {
            try
            {
                ((ReactiveDictionary<TKey, TValue>)typeof(TModel).InvokeMember (fieldName, BindingFlags.GetField | BindingFlags.GetProperty, null, Model, null))
                    .ObserveReplace()
                    .DistinctUntilChanged(state => state.GetHashCode())
                    .Subscribe((value) =>
                    {
                        onValueChanged?.Invoke(value.Key, value.OldValue, value.NewValue);
                    })
                    .AddTo(Disposable);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("No such field or property as \"" + fieldName + "\" in class " + Model);
            }
        }

        public virtual void Dispose()
        {
            Disposable.Dispose();
        }
    }
}