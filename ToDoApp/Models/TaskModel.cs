using System;

namespace ToDoApp.Models
{
    public class TaskModel
    {
        public Guid Uid { get; set; }

        public bool IsCompleted { get; set; }

        public string Title { get; set; } = null!;
    }
}
