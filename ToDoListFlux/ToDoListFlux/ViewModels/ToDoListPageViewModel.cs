using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using ToDoListFlux.Actions;
using ToDoListFlux.ActionTypes;
using ToDoListFlux.Models;
using ToDoListFlux.Stores;
using Xamarin.Forms;

namespace ToDoListFlux.ViewModels
{
    public class ToDoListPageViewModel : ViewModelBase
    {
        private readonly ToDoActions _toDoActions;
        private readonly ToDoStore _toDoStore;
        private ICommand _createCommand;
        private ICommand _toggleCommand;
        private ICommand _toggleAllCommand;
        private ICommand _deleteCommand;
        private ICommand _deleteCompletedCommand;
        private ICommand _editCommand;
        private ICommand _populateCommand;

        public ObservableCollection<Todo> Items
        {
            get
            {
                return _toDoStore.Data;
            }
        }

        public ICommand CreateCommand
        {
            get
            {
                return _createCommand ??
                    (_createCommand = new Command(async () =>
                    {
                        var result = await UserDialogs.Instance.PromptAsync("", "New","Done","Cancel","Todo...");
                        if (result.Ok)
                        {
                            _toDoActions.AddTodo(result.Text);
                        }
                    }));
            }
        }

        public ICommand ToggleCommand
        {
            get
            {
                return _toggleCommand ??
                    (_toggleCommand = new Command<Todo>((todo) =>
                    {
                        _toDoActions.ToggleTodo(todo.Id);
                    }));
            }
        }

        public ICommand ToggleAllCommand
        {
            get
            {
                return _toggleAllCommand ??
                    (_toggleAllCommand = new Command(() =>
                    {
                        _toDoActions.ToggleAllTodos();
                    }));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new Command<Todo>((todo) =>
                    {
                        _toDoActions.DeleteTodo(todo.Id);
                    }));
            }
        }

        public ICommand DeleteCompletedCommand
        {
            get
            {
                return _deleteCompletedCommand ??
                    (_deleteCompletedCommand = new Command(() =>
                    {
                        _toDoActions.DeleteCompletedTodos();
                    }));
            }
        }

        public ICommand EditCommand
        {
            get 
            {
                return _editCommand ??
                    (_editCommand = new Command<Todo>( async (todo) =>
                    {
                        var result = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                                                .SetText(todo.Text)
                                                .SetTitle("Edit")
                                                .SetOkText("Done")
                                                .SetCancelText("Cancel")
                                                .SetPlaceholder("Todo...")
                                           );
                        if (result.Ok)
                        {
                            _toDoActions.EditTodo(todo.Id, result.Text);
                        }
                    }));
            }
        }

        public ICommand PopulateCommand
        {
            get 
            {
                return _populateCommand ??
                    (_populateCommand = new Command(() =>
                    {
                        for (var i = 0; i < 24; i++) {
                            _toDoActions.AddTodo($"New Item {i}");
                        }
                    }));
            }
        }


        public ToDoListPageViewModel(ToDoStore todoStore, ToDoActions todoActions, INavigationService navigationService)
            : base(navigationService)
        {
            _toDoStore = todoStore;
            _toDoActions = todoActions;
            _toDoStore.OnEmitted +=  TodoStore_OnEmitted;

        }

        private void TodoStore_OnEmitted(object sender, StoreEventArgs e)
        {
            switch(e.EventType) {
                case ToDoActionTypes.ADD_TODO:
                    UserDialogs.Instance.Toast("Item added!");
                    break;
                case ToDoActionTypes.DELETE_TODO:
                    UserDialogs.Instance.Toast("Item deleted!");
                    break;
                case ToDoActionTypes.DELETE_COMPLETED_TODOS:
                    UserDialogs.Instance.Toast("Completed items deleted");
                    break;
                case ToDoActionTypes.EDIT_TODO:
                    UserDialogs.Instance.Toast("Edited todo");
                    break;
                case ToDoActionTypes.TOGGLE_ALL_TODOS:
                    UserDialogs.Instance.Toast("Todos toggled");
                    break;
                case ToDoActionTypes.TOGGLE_TODO:
                    UserDialogs.Instance.Toast("Toggled todo");
                    break;
            }
        }
    }
}
