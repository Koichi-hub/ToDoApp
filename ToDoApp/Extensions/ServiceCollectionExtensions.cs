using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Services;
using ToDoApp.ViewModels;

namespace ToDoApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MainViewModel>();

            serviceCollection.AddSingleton<TaskPersistenceService>();
        }
    }
}
