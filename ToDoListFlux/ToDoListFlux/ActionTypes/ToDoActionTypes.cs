using System;

using Xamarin.Forms;

namespace ToDoListFlux.ActionTypes
{
    public class ToDoActionTypes
    {
        public const string ADD_TODO = "add_todo";
        public const string DELETE_COMPLETED_TODOS = "delete_completed_todos";
        public const string DELETE_TODO = "delete_todo";
        public const string EDIT_TODO = "edit_todo";
        public const string TOGGLE_ALL_TODOS = "toggle_all_todos";
        public const string TOGGLE_TODO = "toggle_todo";
    }
}

