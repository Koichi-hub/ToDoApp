using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ToDoApp.Extensions;
using ToDoApp.Services;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp;

public partial class App : Application
{
    private TaskPersistenceService taskPersistenceService = null!;
    private MainViewModel mainViewModel = null!;
    private bool canCanceled;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        //DI
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCommonServices();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        taskPersistenceService = serviceProvider.GetRequiredService<TaskPersistenceService>();
        mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs eventArgs)
    {
        eventArgs.Cancel = !canCanceled;

        if (!canCanceled)
        {
            taskPersistenceService.SaveTasks(mainViewModel.TasksViewModels.Select(x => x.Model));

            canCanceled = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}
