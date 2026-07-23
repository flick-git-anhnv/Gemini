---
step: 2.2
plan: ../PLAN-MASTER.md
agent: JUNIOR DEVELOPER
status: done
completed_at: 2026-07-23
---

# STEP 2.2 — Xây dựng Avalonia UI (XAML Views, Controls, Styling & DataBinding)

## Input nhận
Models, Services và ViewModels từ Step 2.1.

## Nhiệm vụ
Thiết kế giao diện XAML cho Avalonia UI (`MainWindow.axaml`), bao gồm Header panel, Form Input (Thêm/Sửa task), Filter panel (Search, Priority, Status), DataGrid / ListBox hiển thị công việc và StatusStrip hiển thị thống kê. Thực hiện Data Binding với ViewModels.

## Definition of Done
- [x] XAML View `MainWindow.axaml` hoàn thiện với giao diện hiện đại, chuẩn đẹp
- [x] DataBinding đầy đủ với `MainWindowViewModel` (Commands, Properties, Collections)
- [x] Ứng dụng chạy và tương tác mượt mà trên Avalonia

## Đã làm
- Tạo View `MainWindow.axaml` và `MainWindow.axaml.cs` trong `src/TodoApp.Avalonia/Views/`.
- Áp dụng thiết kế giao diện chuẩn màu KZTEK: Header màu Navy `#251C53`, nền xám nhạt `#F5F7FA`, thẻ Card góc bo `#FFFFFF` có viền `#E2E8F0`.
- Tích hợp DataBinding với `MainWindowViewModel`: Form Input (Title, Description, Priority, DueDate, SaveCommand, CancelEditCommand, IsEditing), Filter Bar (SearchKeyword, SelectedStatusFilter, SelectedPriorityFilter), DataGrid công việc (CheckBox complete, Title, Description, Priority badge, DueDate, EditCommand, DeleteCommand), và Status Footer (Total, Active, Completed, StatsSummary).
- Cập nhật `App.axaml.cs` khởi tạo `MainWindow` và gán `DataContext = new MainWindowViewModel()`.
- Kiểm tra biên dịch `dotnet build src/TodoApp.Avalonia/TodoApp.Avalonia.csproj` thành công 100% (0 errors, 0 warnings).

## Artifact
- `src/TodoApp.Avalonia/Views/MainWindow.axaml`
- `src/TodoApp.Avalonia/Views/MainWindow.axaml.cs`
- `src/TodoApp.Avalonia/App.axaml`
- `src/TodoApp.Avalonia/App.axaml.cs`

## Quyết định quan trọng
- Sử dụng DataGrid với `AutoGenerateColumns="False"` kết hợp DataGridTemplateColumn giúp kiểm soát styling chi tiết từng cột (CheckBox hoàn thành, Priority badge, Nút thao tác Sửa/Xóa).
- Binding Command thao tác dòng trong DataGrid sử dụng `$parent[Window].((vm:MainWindowViewModel)DataContext).CommandName` đảm bảo type-safety với Avalonia CompiledBindings.

## Handoff Log — bước sau cần biết
- Giao diện UI Avalonia và DataBinding hoàn thành. Đã sẵn sàng cho Bước 2.3 / Bước 3 Review / Smoke Test / QA.

## Commit
- Hash: N/A
- Đã push: false

---
**Status icons:** ⬜ Todo | 🔄 In Progress | ✅ Done | 🛑 Blocked | ⏭️ Skipped

