---
step: 1.1
plan: ../PLAN-MASTER.md
agent: CODE-MIGRATOR
status: done
completed_at: 2026-07-23
---

# STEP 1.1 — Khảo sát WinForms & lập plan mapping chuyển đổi Avalonia

## Input nhận
Yêu cầu migrate TodoApp WinForms sang Avalonia UI.

## Nhiệm vụ
Phân tích toàn bộ mã nguồn WinForms hiện tại trong `src/TodoApp/`, khảo sát UI controls, event handlers, Models, Services. Lập tài liệu mapping chi tiết từ WinForms components & patterns sang Avalonia UI & MVVM.

## Definition of Done
- [x] Phân tích xong `src/TodoApp/Form1.cs`, `Form1.Designer.cs`, `Models`, `Services`
- [x] Lập bảng mapping chi tiết WinForms Control -> Avalonia Control
- [x] Xác định các thư viện NuGet cần thiết (Avalonia, CommunityToolkit.Mvvm...)

## Đã làm
- Đọc và phân tích mã nguồn WinForms cũ (`Form1.cs`, `TodoItem.cs`, `TodoService.cs`).
- Lập bảng mapping cho Controls và Event Handlers từ WinForms sang Avalonia UI.
- Quyết định sử dụng kiến trúc MVVM với CommunityToolkit.Mvvm (.NET 8/9).

## Artifact
- `docs/plans/PLAN-todoapp-avalonia-2026-07-23/steps/STEP-1.1-khao-sat-plan.md`

## Quyết định quan trọng
- Framework: Avalonia UI (.NET 8/9).
- Pattern: MVVM.
- Thư viện: CommunityToolkit.Mvvm cho ViewModels (sử dụng ObservableObject, RelayCommand, ObservableCollection).
- Tái sử dụng code: Giữ nguyên Models (`TodoItem.cs`) và Services (`TodoService.cs`), chúng hoạt động tốt và độc lập với giao diện.

## Handoff Log — bước sau cần biết
### 1. Kiến trúc MVVM
- Models: Dùng lại `TodoItem`.
- Services: Dùng lại `TodoService` (vẫn đọc ghi file `todos.json`).
- ViewModels: 
  - Tạo `MainWindowViewModel` kế thừa `ObservableObject`.
  - Properties binding: `TodoList` (ObservableCollection<TodoItem>), `SearchKeyword`, `StatusFilter`, `PriorityFilter`, `SelectedItem` (để sửa).
  - Commands: `SaveCommand`, `CancelEditCommand`, `ToggleCompleteCommand`, `DeleteCommand`.
- Views: Giao diện XAML `MainWindow.axaml` sử dụng `Grid`, `StackPanel`, `Border`.

### 2. Bảng mapping Controls
| WinForms Control (Cũ) | Avalonia UI Control (Mới) | Cách Binding (MVVM) |
|---|---|---|
| Form / panelHeader | Window / DockPanel / Grid / Border | `Window.DataContext` = `MainWindowViewModel` |
| GroupBox (gbInput) | Border + TextBlock Header | |
| TextBox (txtTitle, vv) | TextBox | `Text="{Binding Title, Mode=TwoWay}"`... |
| ComboBox (cbPriority, vv) | ComboBox | `ItemsSource="{Binding Priorities}"`, `SelectedItem="{Binding SelectedPriority, Mode=TwoWay}"` |
| DateTimePicker (dtpDueDate) | CalendarDatePicker / DatePicker | `SelectedDate="{Binding DueDate, Mode=TwoWay}"` |
| Button (btnSave, btnCancelEdit) | Button | `Command="{Binding SaveCommand}"` |
| DataGridView (dgvTodos) | DataGrid (`Avalonia.Controls.DataGrid`) hoặc ItemsControl/ListBox | `ItemsSource="{Binding TodoList}"` |
| DataGridViewCheckBoxColumn | CheckBox trong DataTemplate | `IsChecked="{Binding IsCompleted, Mode=TwoWay}"`, `Command="{Binding ToggleCompleteCommand}"` |
| DataGridViewButtonColumn | Button trong DataTemplate | `Command="{Binding DeleteCommand}"`, `CommandParameter="{Binding Id}"` |
| StatusStrip / ToolStripStatusLabel | Border dưới cùng + TextBlock | `Text="{Binding TotalText}"`, v.v... |

### 3. Thư viện NuGet cần thiết
- `Avalonia` và các gói kèm theo (`Avalonia.Desktop`, `Avalonia.Themes.Fluent`, vv).
- `Avalonia.Controls.DataGrid` (nếu dùng DataGrid).
- `CommunityToolkit.Mvvm`.

## Commit
- Hash: 
- Đã push: false

---
**Status icons:** ⬜ Todo | 🔄 In Progress | ✅ Done | 🛑 Blocked | ⏭️ Skipped
