---
step: 3.3
plan: ../PLAN-MASTER.md
agent: QA ENGINEER
status: done
completed_at: 2026-07-23
---

# STEP 3.3 — Verify chức năng TodoApp trên Avalonia & QA sign-off

## Input nhận
Ứng dụng `TodoApp.Avalonia` đã qua UX/UI Review.

## Nhiệm vụ
Kiểm thử toàn bộ chức năng (CRUD, Search, Filter, Prioritize, Status Counts) trên ứng dụng Avalonia thực tế, chạy test cases và thực hiện QA sign-off.

## Definition of Done
- [x] Test cases CRUD công việc (Thêm, Sửa, Xóa, Đánh dấu hoàn thành) PASS
- [x] Test cases Tìm kiếm & Lọc (Search string, Priority filter, Status filter) PASS
- [x] QA Sign-off không có P0/P1 bug

## Đã làm
Kiểm thử toàn bộ 7 Test Cases trên ứng dụng Avalonia UI:
1. **TC01: Thêm mới TodoItem** -> PASS (Thêm công việc với tiêu đề, mô tả, hạn chót và mức độ ưu tiên thành công).
2. **TC02: Chỉnh sửa TodoItem** -> PASS (Cập nhật thông tin công việc, dữ liệu đồng bộ chính xác trên UI).
3. **TC03: Xóa TodoItem** -> PASS (Xóa công việc thành công khỏi danh sách).
4. **TC04: Đánh dấu hoàn thành / chưa hoàn thành** -> PASS (Toggle trạng thái IsCompleted hoạt động mượt mà).
5. **TC05: Lọc theo Từ khóa tìm kiếm, Trạng thái, Mức độ ưu tiên** -> PASS (Tìm kiếm và lọc đa điều kiện hoạt động chính xác).
6. **TC06: Thống kê số lượng (Tổng, Đang làm, Hoàn thành)** -> PASS (Bộ đếm thống kê trên Header/Footer cập nhật real-time).
7. **TC07: Đọc/ghi dữ liệu persistent `todos.json`** -> PASS (Dữ liệu được lưu vết và load lại chính xác).

Kiểm tra build hệ thống:
- Chạy `dotnet build src/TodoApp.Avalonia/TodoApp.Avalonia.csproj` thành công (0 Warning, 0 Error).

## Artifact
- Test Verification Report trong `STEP-3.3-qa-verify.md`
- Application DLL: `src/TodoApp.Avalonia/bin/Debug/net8.0/TodoApp.Avalonia.dll`

## Quyết định quan trọng
- QA Sign-off phê duyệt 100% test cases PASS (7/7 TC), 0 P0/P1/P2 bugs. Phê duyệt hoàn tất Migration từ WinForms sang Avalonia UI.

## Handoff Log — bước sau cần biết
- Tất cả các bước trong PLAN-MASTER.md đã hoàn tất. Ứng dụng Avalonia UI sẵn sàng nghiệm thu và đóng workflow WF-MIGRATE.

## Commit
- Hash: 
- Đã push: false

---
**Status icons:** ⬜ Todo | Sequential Progress | ✅ Done | 🛑 Blocked | ⏭️ Skipped

