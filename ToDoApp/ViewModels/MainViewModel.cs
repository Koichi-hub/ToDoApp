using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.ViewModels;

public class MainViewModel : ReactiveObject
{
    private string taskTitle = string.Empty;
    public string TaskTitle
    {
        get => taskTitle;
        set
        {
            if (taskTitle != value)
            {
                this.RaiseAndSetIfChanged(ref taskTitle, value);
            }
        }
    }

    public ObservableCollection<TaskViewModel> TasksViewModels { get; set; } = [];

    public MainViewModel()
    {
        if (Design.IsDesignMode)
        {
            var tasks = new List<TaskModel>()
            {
                new()
                {
                    Uid = Guid.NewGuid(),
                    Title = "Sleep"
                },
                new()
                {
                    Uid = Guid.NewGuid(),
                    Title = "Code",
                    IsCompleted = true,
                },
                new()
                {
                    Uid = Guid.NewGuid(),
                    Title = "Very long task title",
                    IsCompleted = true,
                },
            };
            TasksViewModels = new ObservableCollection<TaskViewModel>(tasks.Select(x => new TaskViewModel(x, DeleteTask)));
        }
    }

    public MainViewModel(TaskPersistenceService taskPersistenceService) : this()
    {
        TasksViewModels = new ObservableCollection<TaskViewModel>(taskPersistenceService.LoadTasks().Select(x => new TaskViewModel(x, DeleteTask)));
    }

    public void CreateTask()
    {
        if (string.IsNullOrWhiteSpace(TaskTitle))
        {
            return;
        }

        TasksViewModels.Add(new TaskViewModel(new TaskModel
        {
            Uid = Guid.NewGuid(),
            Title = TaskTitle,
        }, DeleteTask));

        TaskTitle = string.Empty;
    }

    private void DeleteTask(TaskViewModel taskViewModel)
    {
        TasksViewModels.Remove(taskViewModel);
    }
}
