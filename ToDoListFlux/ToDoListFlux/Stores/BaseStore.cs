using System;
using ToDoListFlux.Actions;
using Xamarin.Forms;

namespace ToDoListFlux.Stores
{
    public class BaseStore<T>
    {
        public event EventHandler<StoreEventArgs> OnEmitted;

        public T Data { get; set; }

        public string Error { get; set; }

        protected virtual void Subscribe<TData>(string eventType)
        {
            MessagingCenter.Subscribe<IActions, TData>(this, eventType, (sender, data) => ReceiveEvent(eventType, data));
        }

        protected virtual void Subscribe(string eventType)
        {
            MessagingCenter.Subscribe<IActions>(this, eventType, (sender) => ReceiveEvent(eventType, (object)null));
        }

        protected virtual void ReceiveEvent<TData>(string eventType, TData data)
        {
            OnEmitted?.Invoke(this, new StoreEventArgs(eventType));
        }
    }
}
