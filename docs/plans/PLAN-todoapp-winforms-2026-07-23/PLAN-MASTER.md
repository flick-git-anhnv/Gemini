---
task: todoapp-winforms
created: 2026-07-23
updated: 2026-07-23
status: in_progress
workflow: WF-FEATURE
priority: P2
---

# PLAN MASTER: Xây dựng TodoApp đơn giản bằng C# Windows Forms

> File này CHỈ chứa tổng quan + trạng thái. Chi tiết từng bước nằm ở `steps/STEP-[N.M]-[tên].md`.

## Mô tả
Xây dựng ứng dụng quản lý công việc (Todo App) đơn giản, hiện đại bằng C# Windows Forms trên nền .NET. Ứng dụng hỗ trợ các chức năng CRUD (Thêm, Sửa, Xóa, Đánh dấu hoàn thành), lọc danh sách theo trạng thái (Tất cả, Đang làm, Đã xong), tìm kiếm công việc, và lưu trữ dữ liệu bền vững (JSON Local Storage).

## Nguồn yêu cầu
- Yêu cầu gốc: Xây dựng todoapp đơn giản bằng C# windows form
- Workflow: WF-FEATURE — Yêu cầu tính năng mới
- Agent chain: PM → BA → UX → EM → PJM → TL → SD/JD → TL → UXR/QAE → QAL → DOE → DOL

## Phases & Steps

### Phase 1: Phân tích & Thiết kế Spec
| # | Bước | Agent | Status | Step file | Hoàn thành lúc |
|---|------|-------|--------|-----------|-----------------|
| 1.1 | Lập kế hoạch tổng thể | task-planner | ✅ | `steps/STEP-1.1-plan.md` | 2026-07-23 |
| 1.2 | Thu thập & Viết PRD | product-manager | ⬜ | `steps/STEP-1.2-prd.md` | - |
| 1.3 | Chi tiết User Stories & AC | business-analyst | ⬜ | `steps/STEP-1.3-ac.md` | - |
| 1.4 | Thiết kế Wireframe & UI Layout | ui-ux-designer | ⬜ | `steps/STEP-1.4-ui.md` | - |
| 1.5 | Ước lượng tài nguyên & Ưu tiên | engineering-manager | ⬜ | `steps/STEP-1.5-em.md` | - |
| 1.6 | Lên Sprint & Task Breakdown | project-manager | ⬜ | `steps/STEP-1.6-pjm.md` | - |
| 1.7 | Viết Technical Design Doc (TDD) | tech-lead | ⬜ | `steps/STEP-1.7-tdd.md` | - |

### Phase 2: Triển khai Lập trình
| # | Bước | Agent | Status | Step file | Hoàn thành lúc |
|---|------|-------|--------|-----------|-----------------|
| 2.1 | Code Core Architecture & Models | senior-developer | ⬜ | `steps/STEP-2.1-sd-code.md` | - |
| 2.2 | Code UI Controls & Storage Helper | junior-developer | ⬜ | `steps/STEP-2.2-jd-code.md` | - |
| 2.3 | Code Review & Merge | tech-lead | ⬜ | `steps/STEP-2.3-review.md` | - |

### Phase 3: Kiểm thử & Đóng gói Deploy
| # | Bước | Agent | Status | Step file | Hoàn thành lúc |
|---|------|-------|--------|-----------|-----------------|
| 3.1 | Đánh giá trực quan UI | ux-ui-reviewer | ⬜ | `steps/STEP-3.1-uxr.md` | - |
| 3.2 | Kiểm thử chức năng (QA Test) | qa-engineer | ⬜ | `steps/STEP-3.2-qa.md` | - |
| 3.3 | Sign-off chất lượng | qa-lead | ⬜ | `steps/STEP-3.3-qal.md` | - |
| 3.4 | Build & Đóng gói Release | devops-engineer | ⬜ | `steps/STEP-3.4-deploy.md` | - |
| 3.5 | Approve Release | devops-lead | ⬜ | `steps/STEP-3.5-dol.md` | - |

## Artifacts dự kiến
- [ ] `docs/plans/PLAN-todoapp-winforms-2026-07-23/PLAN-MASTER.md`
- [ ] `docs/prd/PRD-todoapp-winforms.md`
- [ ] `docs/specs/TDD-todoapp-winforms.md`
- [ ] `src/TodoApp/TodoApp.csproj`
- [ ] `src/TodoApp/Models/TodoItem.cs`
- [ ] `src/TodoApp/Services/TodoService.cs`
- [ ] `src/TodoApp/Forms/MainForm.cs`
- [ ] `code-graph/CODE-GRAPH.md`

## Blockers
Không có

## Lịch sử cập nhật
| Ngày | Cập nhật | Agent |
|------|----------|-------|
| 2026-07-23 | Tạo kế hoạch triển khai TodoApp C# WinForms | task-planner |
