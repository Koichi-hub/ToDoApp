using ReactiveUI;
using System;
using System.Reactive;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class TaskViewModel : ReactiveObject
    {
        public TaskModel Model { get; }

        //Commands
        public ReactiveCommand<TaskViewModel, Unit> DeleteTaskCommand { get; }

        public TaskViewModel(TaskModel model, Action<TaskViewModel> deleteTask)
        {
            Model = model;
            DeleteTaskCommand = ReactiveCommand.Create(deleteTask);
        }
    }
}
