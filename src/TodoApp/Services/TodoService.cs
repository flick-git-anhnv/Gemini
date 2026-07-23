using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly string _filePath;
        private List<TodoItem> _items = new();

        public TodoService(string? customPath = null)
        {
            _filePath = customPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todos.json");
            LoadData();
        }

        public List<TodoItem> GetAll()
        {
            return _items.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public List<TodoItem> Filter(string statusFilter, string priorityFilter, string searchKeyword)
        {
            var query = _items.AsEnumerable();

            // Status filter
            if (statusFilter == "Đang làm")
            {
                query = query.Where(x => !x.IsCompleted);
            }
            else if (statusFilter == "Đã hoàn thành")
            {
                query = query.Where(x => x.IsCompleted);
            }

            // Priority filter
            if (priorityFilter == "Cao")
            {
                query = query.Where(x => x.Priority == PriorityLevel.High);
            }
            else if (priorityFilter == "Trung bình")
            {
                query = query.Where(x => x.Priority == PriorityLevel.Medium);
            }
            else if (priorityFilter == "Thấp")
            {
                query = query.Where(x => x.Priority == PriorityLevel.Low);
            }

            // Search keyword
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                string kw = searchKeyword.Trim().ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(kw) || x.Description.ToLower().Contains(kw));
            }

            return query.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public void Add(TodoItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
            SaveData();
        }

        public void Update(TodoItem updatedItem)
        {
            var existing = _items.FirstOrDefault(x => x.Id == updatedItem.Id);
            if (existing != null)
            {
                existing.Title = updatedItem.Title;
                existing.Description = updatedItem.Description;
                existing.IsCompleted = updatedItem.IsCompleted;
                existing.Priority = updatedItem.Priority;
                existing.DueDate = updatedItem.DueDate;
                SaveData();
            }
        }

        public void Delete(string id)
        {
            _items.RemoveAll(x => x.Id == id);
            SaveData();
        }

        public void ToggleComplete(string id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                SaveData();
            }
        }

        public (int Total, int Active, int Completed) GetStats()
        {
            int total = _items.Count;
            int completed = _items.Count(x => x.IsCompleted);
            int active = total - completed;
            return (total, active, completed);
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    _items = JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
                }
            }
            catch
            {
                _items = new List<TodoItem>();
            }
        }

        private void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_items, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error Saving Todos]: {ex.Message}");
            }
        }
    }
}
