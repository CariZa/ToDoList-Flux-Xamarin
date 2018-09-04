using System;
using ToDoListFlux.ActionTypes;
using ToDoListFlux.Models;
using Xamarin.Forms;

namespace ToDoListFlux.Actions
{
    public class ToDoActions: IActions
    {
        public void AddTodo(string text)
        {
            MessagingCenter.Send<IActions, string>(this, ToDoActionTypes.ADD_TODO, text);
        }

        public void DeleteCompletedTodos()
        {
            MessagingCenter.Send<IActions>(this, ToDoActionTypes.DELETE_COMPLETED_TODOS);
        }

        public void DeleteTodo(string id)
        {
            MessagingCenter.Send<IActions, string>(this, ToDoActionTypes.DELETE_TODO, id);
        }

        public void EditTodo(string id, string text)
        {
            MessagingCenter.Send<IActions, Todo>(this, ToDoActionTypes.EDIT_TODO, new Todo {
                Id = id,
                Text = text
            });
        }

        public void ToggleAllTodos()
        {
            MessagingCenter.Send<IActions>(this, ToDoActionTypes.TOGGLE_ALL_TODOS);
        }

        public void ToggleTodo(string id)
        {
            MessagingCenter.Send<IActions, string>(this, ToDoActionTypes.TOGGLE_TODO, id);
        }
    }
}
