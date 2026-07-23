---
task: PLAN-todoapp-avalonia-2026-07-23
created: 2026-07-23
updated: 2026-07-23
status: completed
workflow: WF-MIGRATE
priority: P2
---

# PLAN MASTER: Chuyển đổi TodoApp (WinForms) sang Avalonia UI

> File này CHỈ chứa tổng quan + trạng thái. Chi tiết từng bước (mô tả đầy đủ, Handoff Log, artifact chi tiết) nằm ở `steps/STEP-[N.M]-[tên].md` tương ứng — xem cột "Step file" bên dưới.

## Mô tả
Chuyển đổi ứng dụng TodoApp từ Windows Forms (WinForms) sang Avalonia UI (Cross-platform C# framework), áp dụng kiến trúc MVVM (Model-View-ViewModel), tách biệt logic nghiệp vụ và giao diện người dùng.

## Nguồn yêu cầu
- Yêu cầu gốc: Migrate todoapp sang Avalonia
- Workflow: WF-MIGRATE — Chuyển đổi framework/ngôn ngữ/UI stack
- Agent chain: CODE-MIGRATOR → SENIOR DEVELOPER → JUNIOR DEVELOPER → CODE-MIGRATOR → UX/UI REVIEWER → QA ENGINEER

## Phases & Steps

> **Session isolation (GEMINI.md §16.5):** Mỗi bước ⬜/🔄 PHẢI chạy tách session — LOCAL dùng `Agent` subagent, WEB dùng `RemoteTrigger`. Agent/trigger tự tạo/cập nhật step file riêng, commit+push, rồi cập nhật đúng 1 dòng status ở bảng dưới đây.

### Phase 1: Phân tích & Lập kế hoạch chuyển đổi
| # | Bước | Agent | Status | Step file | Hoàn thành lúc |
|---|------|-------|--------|-----------|-----------------|
| 1.1 | Khảo sát WinForms & lập plan mapping chuyển đổi Avalonia | CODE-MIGRATOR | ✅ | `steps/STEP-1.1-khao-sat-plan.md` | 2026-07-23 |

### Phase 2: Triển khai chuyển đổi (Migrate)
| # | Bước | Agent | Status | Step file | Hoàn thành lúc |
|---|------|-------|--------|-----------|-----------------|
| 2.1 | Khởi tạo Avalonia project, thiết kế MVVM & Models/Services | SENIOR DEVELOPER | ✅ | `steps/STEP-2.1-setup-mvvm.md` | 2026-07-23 |
| 2.2 | Xây dựng Avalonia UI (XAML Views, Controls, Styling & DataBinding) | JUNIOR DEVELOPER | ✅ | `steps/STEP-2.2-build-ui.md` | 2026-07-23 |

### Phase 3: Review, Đánh giá UI & Verification
| # | Bước | Agent | Status | Step file | Hoàn thành lúc |
|---|------|-------|--------|-----------|-----------------|
| 3.1 | Review kiến trúc & chất lượng code Avalonia | CODE-MIGRATOR | ✅ | `steps/STEP-3.1-code-review.md` | 2026-07-23 |
| 3.2 | Chạy app Avalonia, kiểm tra trực quan UI & chụp screenshot (C1-C7) | UX/UI REVIEWER | ✅ | `steps/STEP-3.2-uxui-review.md` | 2026-07-23 |
| 3.3 | Verify chức năng TodoApp trên Avalonia & QA sign-off | QA ENGINEER | ✅ | `steps/STEP-3.3-qa-verify.md` | 2026-07-23 |

## Artifacts dự kiến (tổng)
- [ ] `docs/plans/PLAN-todoapp-avalonia-2026-07-23/PLAN-MASTER.md`
- [ ] `src/TodoApp.Avalonia/TodoApp.Avalonia.csproj`
- [ ] `src/TodoApp.Avalonia/ViewModels/MainWindowViewModel.cs`
- [ ] `src/TodoApp.Avalonia/Views/MainWindow.axaml`
- [ ] `src/TodoApp.Avalonia/Models/TodoItem.cs`
- [ ] `src/TodoApp.Avalonia/Services/TodoService.cs`

## Blockers
Không có

## Quyết định / Ghi chú tổng
- Áp dụng Avalonia UI với kiến trúc MVVM (CommunityToolkit.Mvvm).
- Giữ nguyên và đảm bảo tương thích full tính năng CRUD TodoItem, tìm kiếm, lọc theo trạng thái/mức độ ưu tiên, thống kê từ TodoApp WinForms hiện tại.

## Lịch sử cập nhật
| Ngày | Cập nhật | Agent |
|------|----------|-------|
| 2026-07-23 | Plan tạo mới | task-planner |

---
**Status icons:** ⬜ Todo | 🔄 In Progress | ✅ Done | 🛑 Blocked | ⏭️ Skipped
**Cách đọc nhanh:** đọc MASTER trước → nếu cần chi tiết bước cụ thể mới mở step file tương ứng.
