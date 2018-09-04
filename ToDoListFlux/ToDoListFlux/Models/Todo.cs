using System;
using Prism.Mvvm;
using ToDoListFlux.ViewModels;

namespace ToDoListFlux.Models
{
    public class Todo : BindableBase
    {
        private string _id;
        private string _text;
        private bool _isCompleted;

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                SetProperty(ref _text, value);
            }
        }

        public bool IsCompleted
        {
            get 
            {
                return _isCompleted;
            }
            set 
            {
                SetProperty(ref _isCompleted, value);
            }
        }
    }
}
