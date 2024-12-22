using Avalonia.Controls;
using ReactiveUI;
using System.Collections.ObjectModel;
using ToDoApp.Models;

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

    public ObservableCollection<TaskModel> Tasks { get; set; } = [];

    public MainViewModel()
    {
        if (Design.IsDesignMode)
        {
            Tasks =
            [
                new()
                {
                    Title = "Sleep"
                },
                new()
                {
                    Title = "Code",
                    IsCompleted = true,
                },
            ];
        }
    }

    public void CreateTask()
    {
        if (string.IsNullOrWhiteSpace(TaskTitle))
        {
            return;
        }

        Tasks.Add(new()
        {
            Title = TaskTitle,
        });
        TaskTitle = string.Empty;
    }
}
