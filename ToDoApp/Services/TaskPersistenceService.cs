using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ToDoApp.Configuration;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class TaskPersistenceService
    {
        private readonly string appFolderPath;

        private readonly string tasksFilePath;

        public TaskPersistenceService()
        {
            appFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Constants.AppFolderName);

            tasksFilePath = Path.Combine(appFolderPath, Constants.TasksJsonFileName);
        }

        public IEnumerable<TaskModel> LoadTasks()
        {
            if (!File.Exists(tasksFilePath))
            {
                return [];
            }

            using var streamReader = new StreamReader(tasksFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(streamReader.ReadToEnd()) ?? [];
        }

        public void SaveTasks(IEnumerable<TaskModel> tasks)
        {
            if (!Directory.Exists(appFolderPath))
            {
                Directory.CreateDirectory(appFolderPath);
            }

            using var streamWriter = new StreamWriter(tasksFilePath);
            streamWriter.Write(JsonConvert.SerializeObject(tasks));
        }
    }
}
