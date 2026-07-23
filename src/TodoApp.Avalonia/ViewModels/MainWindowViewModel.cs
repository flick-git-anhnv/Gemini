using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Avalonia.Models;
using TodoApp.Avalonia.Services;

namespace TodoApp.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly TodoService _todoService;

        public List<string> StatusFilters { get; } = new() { "Tất cả", "Đang làm", "Đã hoàn thành" };
        public List<string> PriorityFilters { get; } = new() { "Tất cả", "Cao", "Trung bình", "Thấp" };
        public List<PriorityLevel> PriorityLevels { get; } = Enum.GetValues<PriorityLevel>().ToList();

        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems = new();

        [ObservableProperty]
        private TodoItem? _selectedTodo;

        // Form properties
        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private PriorityLevel _priority = PriorityLevel.Medium;

        [ObservableProperty]
        private DateTimeOffset? _dueDate;

        [ObservableProperty]
        private bool _isEditing;

        [ObservableProperty]
        private string? _editingId;

        // Filter / Search properties
        [ObservableProperty]
        private string _selectedStatusFilter = "Tất cả";

        [ObservableProperty]
        private string _selectedPriorityFilter = "Tất cả";

        [ObservableProperty]
        private string _searchKeyword = string.Empty;

        // Statistics properties
        [ObservableProperty]
        private int _totalCount;

        [ObservableProperty]
        private int _activeCount;

        [ObservableProperty]
        private int _completedCount;

        [ObservableProperty]
        private string _statsSummary = string.Empty;

        public MainWindowViewModel() : this(new TodoService())
        {
        }

        public MainWindowViewModel(TodoService todoService)
        {
            _todoService = todoService;
            RefreshData();
        }

        partial void OnSelectedStatusFilterChanged(string value) => ApplyFilters();
        partial void OnSelectedPriorityFilterChanged(string value) => ApplyFilters();
        partial void OnSearchKeywordChanged(string value) => ApplyFilters();

        public void ApplyFilters()
        {
            var filtered = _todoService.Filter(SelectedStatusFilter, SelectedPriorityFilter, SearchKeyword);
            TodoItems = new ObservableCollection<TodoItem>(filtered);
            UpdateStats();
        }

        public void RefreshData()
        {
            ApplyFilters();
        }

        private void UpdateStats()
        {
            var (total, active, completed) = _todoService.GetStats();
            TotalCount = total;
            ActiveCount = active;
            CompletedCount = completed;
            StatsSummary = $"Tổng số: {total} | Đang làm: {active} | Đã hoàn thành: {completed}";
        }

        [RelayCommand]
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return;
            }

            if (IsEditing && !string.IsNullOrEmpty(EditingId))
            {
                var existingItem = new TodoItem
                {
                    Id = EditingId,
                    Title = Title.Trim(),
                    Description = Description.Trim(),
                    Priority = Priority,
                    DueDate = DueDate?.DateTime,
                    IsCompleted = SelectedTodo?.IsCompleted ?? false
                };
                _todoService.Update(existingItem);
            }
            else
            {
                var newItem = new TodoItem
                {
                    Title = Title.Trim(),
                    Description = Description.Trim(),
                    Priority = Priority,
                    DueDate = DueDate?.DateTime,
                    IsCompleted = false
                };
                _todoService.Add(newItem);
            }

            ResetForm();
            RefreshData();
        }

        [RelayCommand]
        private void CancelEdit()
        {
            ResetForm();
        }

        [RelayCommand]
        private void Edit(TodoItem? item)
        {
            var target = item ?? SelectedTodo;
            if (target == null) return;

            EditingId = target.Id;
            Title = target.Title;
            Description = target.Description;
            Priority = target.Priority;
            DueDate = target.DueDate.HasValue ? new DateTimeOffset(target.DueDate.Value) : null;
            IsEditing = true;
        }

        [RelayCommand]
        private void Delete(TodoItem? item)
        {
            var target = item ?? SelectedTodo;
            if (target == null) return;

            _todoService.Delete(target.Id);
            if (EditingId == target.Id)
            {
                ResetForm();
            }
            RefreshData();
        }

        [RelayCommand]
        private void ToggleComplete(TodoItem? item)
        {
            var target = item ?? SelectedTodo;
            if (target == null) return;

            _todoService.ToggleComplete(target.Id);
            RefreshData();
        }

        private void ResetForm()
        {
            Title = string.Empty;
            Description = string.Empty;
            Priority = PriorityLevel.Medium;
            DueDate = null;
            IsEditing = false;
            EditingId = null;
            SelectedTodo = null;
        }
    }
}
