---
step: 2.1
plan: ../PLAN-MASTER.md
agent: SENIOR DEVELOPER
status: done
completed_at: 2026-07-23
---

# STEP 2.1 — Khởi tạo Avalonia project, thiết kế MVVM & Models/Services

## Input nhận
Kết quả khảo sát và mapping từ Step 1.1.

## Nhiệm vụ
Tạo dự án Avalonia mới `src/TodoApp.Avalonia/TodoApp.Avalonia.csproj`, cấu hình dependencies, định nghĩa Models (`TodoItem`), Services (`TodoService`) và ViewModels (`MainWindowViewModel`, `ObservableObject`, `RelayCommand`).

## Definition of Done
- [x] Dự án `TodoApp.Avalonia.csproj` được khởi tạo và build thành công
- [x] Models và Services được port sang Avalonia MVVM pattern
- [x] ViewModels quản lý state, danh sách TodoItems, filters, statistics commands

## Đã làm
- Khởi tạo cấu trúc dự án `src/TodoApp.Avalonia/` cùng file cấu hình `.csproj` (.NET 8.0) tích hợp đầy đủ NuGet packages: Avalonia (11.1.3), Avalonia.Desktop, Avalonia.Themes.Fluent, Avalonia.Fonts.Inter, Avalonia.Controls.DataGrid, CommunityToolkit.Mvvm (8.2.2).
- Tạo `Program.cs`, `App.axaml`, và `App.axaml.cs` để hoàn thiện khung ứng dụng Avalonia.
- Port Model `TodoItem.cs` sang namespace `TodoApp.Avalonia.Models` và thêm phương thức hỗ trợ `Clone()`.
- Port Service `TodoService.cs` sang namespace `TodoApp.Avalonia.Services`, giữ nguyên logic lưu trữ JSON file `todos.json` và các tính năng CRUD, filter, search, stats.
- Xây dựng `ViewModelBase` kế thừa `ObservableObject` và `MainWindowViewModel` kế thừa `ViewModelBase` sử dụng CommunityToolkit.Mvvm (Source Generators):
  - Quản lý danh sách `ObservableCollection<TodoItem> TodoItems`.
  - Quản lý các thuộc tính form tạo/sửa: `Title`, `Description`, `Priority`, `DueDate`, `IsEditing`, `EditingId`.
  - Quản lý các bộ lọc & tìm kiếm: `SelectedStatusFilter`, `SelectedPriorityFilter`, `SearchKeyword` tự động trigger `ApplyFilters()`.
  - Quản lý thuộc tính thống kê: `TotalCount`, `ActiveCount`, `CompletedCount`, `StatsSummary`.
  - Cung cấp các `RelayCommand`: `SaveCommand`, `CancelEditCommand`, `EditCommand`, `DeleteCommand`, `ToggleCompleteCommand`.
- Thực hiện `dotnet build` xác nhận dự án biên dịch thành công 0 lỗi, 0 cảnh báo.

## Artifact
- `src/TodoApp.Avalonia/TodoApp.Avalonia.csproj`
- `src/TodoApp.Avalonia/Program.cs`
- `src/TodoApp.Avalonia/App.axaml`
- `src/TodoApp.Avalonia/App.axaml.cs`
- `src/TodoApp.Avalonia/Models/TodoItem.cs`
- `src/TodoApp.Avalonia/Services/TodoService.cs`
- `src/TodoApp.Avalonia/ViewModels/ViewModelBase.cs`
- `src/TodoApp.Avalonia/ViewModels/MainWindowViewModel.cs`

## Quyết định quan trọng
- Sử dụng CommunityToolkit.Mvvm (v8.2.2) với Source Generators (`[ObservableProperty]`, `[RelayCommand]`) giúp giảm boiler-plate code và tối ưu hoá hiệu năng binding.
- Kiểu dữ liệu `DueDate` ở Form ViewModel được thiết kế kiểu `DateTimeOffset?` để tương thích tốt với Avalonia DatePicker control, đồng thời chuyển đổi linh hoạt về `DateTime?` cho Model `TodoItem`.
- Giữ nguyên cấu trúc lưu trữ `todos.json` của WinForms để đảm bảo dữ liệu ứng dụng không bị gián đoạn hay bất tương thích khi di chuyển.

## Handoff Log — bước sau cần biết
- Step 2.2 (Junior Developer) sẽ tiến hành tạo `Views/MainWindow.axaml` và `Views/MainWindow.axaml.cs`.
- Trong `MainWindow.axaml`, gán DataContext là `MainWindowViewModel`.
- Các Binding sẵn có từ `MainWindowViewModel`:
  - `TodoItems` cho DataGrid / ItemsControl.
  - `SelectedTodo` cho SelectedItem.
  - `Title`, `Description`, `Priority` (PriorityLevels), `DueDate` cho Form Controls.
  - `StatusFilters`, `SelectedStatusFilter`, `PriorityFilters`, `SelectedPriorityFilter`, `SearchKeyword` cho Controls lọc & tìm kiếm.
  - `SaveCommand`, `CancelEditCommand`, `EditCommand`, `DeleteCommand`, `ToggleCompleteCommand` cho Buttons/Commands.

## Commit
- Hash: 
- Đã push: false

---
**Status icons:** ⬜ Todo | 🔄 In Progress | ✅ Done | 🛑 Blocked | ⏭️ Skipped

