using System;

namespace TodoApp.Avalonia.Models
{
    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public class TodoItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime? DueDate { get; set; }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public TodoItem Clone()
        {
            return new TodoItem
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                IsCompleted = this.IsCompleted,
                DueDate = this.DueDate,
                Priority = this.Priority,
                CreatedAt = this.CreatedAt
            };
        }
    }
}
