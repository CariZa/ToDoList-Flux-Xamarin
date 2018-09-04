using System;
using System.Collections.ObjectModel;
using ToDoListFlux.Models;
using ToDoListFlux.ActionTypes;
using Xamarin.Forms;
using System.Linq;

namespace ToDoListFlux.Stores
{
    // base data type is an obs col
    public class ToDoStore : BaseStore<ObservableCollection<Todo>>
    {
        public ToDoStore()
        {
            Subscribe<string>(ToDoActionTypes.ADD_TODO);
            Subscribe(ToDoActionTypes.DELETE_COMPLETED_TODOS);
            Subscribe<string>(ToDoActionTypes.DELETE_TODO);
            Subscribe<Todo>(ToDoActionTypes.EDIT_TODO);
            Subscribe<string>(ToDoActionTypes.TOGGLE_TODO);
            Subscribe(ToDoActionTypes.TOGGLE_ALL_TODOS);

            Data = new ObservableCollection<Todo>();
        }

        protected override void ReceiveEvent<TData>(string eventType, TData data)
        {
            switch(eventType) {
                case ToDoActionTypes.ADD_TODO:
                    Data.Add(new Todo {
                        Id = Guid.NewGuid().ToString(),
                        Text = data as string,
                        IsCompleted = false
                    });
                    break;
                case ToDoActionTypes.DELETE_COMPLETED_TODOS:
                    var completedItems = Data.Where(t => t.IsCompleted);
                    foreach(var item in completedItems.ToList()) {
                        Data.Remove(item);
                    }
                    break;
                case ToDoActionTypes.DELETE_TODO:
                    var removeItem = Data.FirstOrDefault(t => t.Id == data as string);
                    if (removeItem != null) {
                        Data.Remove(removeItem);
                    }
                    break;
                case ToDoActionTypes.EDIT_TODO:
                    var editItem = Data.FirstOrDefault(t => t.Id == data as string);
                    if (editItem != null) {
                        editItem.Text = (data as Todo).Text;
                    }
                    break;
                case ToDoActionTypes.TOGGLE_ALL_TODOS:
                    var areAllComplete = !Data.Any(t => !t.IsCompleted);
                    foreach (var item in Data) {
                        item.IsCompleted = !areAllComplete;
                    }
                    break;
                case ToDoActionTypes.TOGGLE_TODO:
                    var toggleItem = Data.FirstOrDefault(t => t.Id == data as string);
                    if (toggleItem != null)
                    {
                        toggleItem.IsCompleted = !toggleItem.IsCompleted;
                    }
                    break;
            }
            base.ReceiveEvent(eventType, data);
        }
    }
}
