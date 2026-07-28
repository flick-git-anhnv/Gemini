# Kztek Gemini Agent Workspace

Đây là kho lưu trữ trung tâm dành cho toàn bộ thiết lập cấu hình, Rules, Workflows, và hệ thống Agents của KZTEK sử dụng với **Gemini Agent** (hoặc Antigravity).

Bằng cách sử dụng kho lưu trữ này làm cấu hình gốc (Global Configuration), toàn bộ đội ngũ phát triển (Team) sẽ luôn có chung một tiêu chuẩn lập trình, chung một luồng quy trình (workflow), và tận dụng được các Agent đóng vai trò như Tech Lead, QA, Dev.

## 🚀 Hướng dẫn Cài đặt cho Thành viên Mới

Khi bạn mới clone repo này về máy, bạn cần chạy script thiết lập để đưa cấu hình của Gemini Agent trỏ về đúng repo này.

### Bước 1: Clone kho lưu trữ
Clone repository này về một thư mục cố định trên máy (Ví dụ: `Desktop/GeminiGit`). Đừng thay đổi vị trí của nó sau khi đã cài đặt.

### Bước 2: Thiết lập cấu hình toàn cầu (Global Setup)
Mở **PowerShell** (không cần quyền Admin) và chạy script sau:

```powershell
# Chuyển vào thư mục repo vừa clone
cd C:\Path\To\GeminiGit

# Chạy script cài đặt
.\setup-global-gemini.ps1
```

Script này sẽ tự động tạo các **Junction Links** cho các thư mục cấu hình bên trong `C:\Users\<Tên-Bạn>\.gemini`. Nhờ vậy, thư mục gốc của hệ thống AI sẽ luôn đồng bộ 100% với kho chứa này mà không làm khoá (lock) các file dữ liệu cục bộ.

### Bước 3: Cấu hình Permissions (Tùy chọn)
Nếu bạn chưa có file `settings.json`, hãy sao chép (copy) file mẫu từ `templates\settings-global.json` vào `C:\Users\<Tên-Bạn>\.gemini\settings.json` để cấp quyền chạy các lệnh thông dụng (như PowerShell, Git, Node, Python) mà không bị hệ thống AI hỏi xác nhận quá nhiều.

---

## 🛠 Cách áp dụng AI vào Project Bất Kỳ

Mỗi khi bạn tạo một project làm việc mới và muốn sử dụng hệ thống Gemini Agent, bạn **KHÔNG CẦN** phải copy toàn bộ thư mục `.gemini`.

Bạn chỉ cần thực hiện việc cực kỳ đơn giản sau:
1. **Copy bộ 3 file tài liệu chuẩn** từ gốc repo này sang gốc project của bạn:
   - `GEMINI.md`
   - `RULES.md`
   - `WORKFLOW.md`
2. Mở IDE (hoặc Terminal) tại project mới và gọi Agent AI hoạt động. Agent sẽ tự động đọc `GEMINI.md` và tuân theo mọi luật lệ, đồng thời sử dụng các skill, command từ Global Configuration.

*(Lưu ý: Bạn cũng có thể dùng file `setup-gemini-link.ps1` để tạo thêm một junction `.gemini` ở mức project nếu kiến trúc dự án yêu cầu, nhưng với Global Setup ở bước 2 thì việc này thường là không cần thiết nữa).*

---

## 📂 Cấu trúc Repository

- `agents/`: Khai báo tính cách, model, và công cụ của từng Agent (CTO, Tech Lead, Dev, QA...).
- `commands/`: Các slash command tuỳ chỉnh hỗ trợ điều khiển luồng công việc nhanh chóng.
- `evals/` & `lessons/`: Nhật ký và bài học kinh nghiệm tự học của AI.
- `hooks-kztek/`: Các đoạn mã can thiệp, bảo vệ an toàn (config protection) và nhắc nhở.
- `scripts/`: Chứa script setup và các tool hữu ích chạy tự động.
- `templates/`: Khu vực chứa các file cấu hình gốc mẫu (ví dụ: `settings-global.json`).
- `GEMINI.md`, `RULES.md`, `WORKFLOW.md`: Trái tim của hệ thống phân luồng (Chain of Command). Đọc kỹ để hiểu luồng làm việc BẮT BUỘC.
