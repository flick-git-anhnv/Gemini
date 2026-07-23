using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp
{
    public partial class Form1 : Form
    {
        private readonly TodoService _todoService;
        private TodoItem? _selectedEditItem = null;

        // UI Controls
        private Panel panelHeader = null!;
        private Label lblHeaderTitle = null!;
        private Label lblHeaderSubtitle = null!;

        private GroupBox gbInput = null!;
        private Label lblTitle = null!;
        private TextBox txtTitle = null!;
        private Label lblDescription = null!;
        private TextBox txtDescription = null!;
        private Label lblPriority = null!;
        private ComboBox cbPriority = null!;
        private Label lblDueDate = null!;
        private DateTimePicker dtpDueDate = null!;
        private Button btnSave = null!;
        private Button btnCancelEdit = null!;

        private Panel panelFilter = null!;
        private Label lblSearch = null!;
        private TextBox txtSearch = null!;
        private Label lblStatusFilter = null!;
        private ComboBox cbStatusFilter = null!;
        private Label lblPriorityFilter = null!;
        private ComboBox cbPriorityFilter = null!;

        private DataGridView dgvTodos = null!;
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel lblStatTotal = null!;
        private ToolStripStatusLabel lblStatActive = null!;
        private ToolStripStatusLabel lblStatCompleted = null!;

        public Form1()
        {
            InitializeComponent();
            _todoService = new TodoService();
            SetupCustomUI();
            LoadDataToGrid();
        }

        private void SetupCustomUI()
        {
            // Form setup
            this.Text = "KZTEK Todo Manager - Gemini Agent Framework";
            this.Size = new Size(950, 680);
            this.MinimumSize = new Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);

            // 1. Header Panel
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(37, 28, 83) // KZTEK Navy Dark #251C53
            };

            lblHeaderTitle = new Label
            {
                Text = "📋 KZTEK Todo Manager",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                Location = new Point(20, 12),
                AutoSize = true
            };

            lblHeaderSubtitle = new Label
            {
                Text = "Ứng dụng quản lý công việc đơn giản - Gemini Agent C# Windows Forms Framework",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(184, 179, 214), // KZTEK Pale
                Location = new Point(23, 42),
                AutoSize = true
            };

            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Controls.Add(lblHeaderSubtitle);
            this.Controls.Add(panelHeader);

            // 2. Input GroupBox
            gbInput = new GroupBox
            {
                Text = "Thêm / Chỉnh sửa Công việc",
                Location = new Point(20, 85),
                Size = new Size(900, 140),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(37, 28, 83),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            lblTitle = new Label { Text = "Tên công việc (*):", Location = new Point(15, 30), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Regular) };
            txtTitle = new TextBox { Location = new Point(15, 52), Size = new Size(250, 26), Font = new Font("Segoe UI", 9.5F) };

            lblDescription = new Label { Text = "Mô tả chi tiết:", Location = new Point(280, 30), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Regular) };
            txtDescription = new TextBox { Location = new Point(280, 52), Size = new Size(280, 26), Font = new Font("Segoe UI", 9.5F) };

            lblPriority = new Label { Text = "Mức ưu tiên:", Location = new Point(575, 30), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Regular) };
            cbPriority = new ComboBox { Location = new Point(575, 52), Size = new Size(120, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cbPriority.Items.AddRange(new object[] { "Thấp", "Trung bình", "Cao" });
            cbPriority.SelectedIndex = 1; // Default Medium

            lblDueDate = new Label { Text = "Hạn chót:", Location = new Point(710, 30), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Regular) };
            dtpDueDate = new DateTimePicker { Location = new Point(710, 52), Size = new Size(170, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };

            btnSave = new Button
            {
                Text = "➕ Thêm Công Việc",
                Location = new Point(15, 92),
                Size = new Size(160, 34),
                BackColor = Color.FromArgb(240, 89, 34), // KZTEK Orange #F05922
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancelEdit = new Button
            {
                Text = "❌ Hủy chỉnh sửa",
                Location = new Point(185, 92),
                Size = new Size(130, 34),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                Visible = false,
                Cursor = Cursors.Hand
            };
            btnCancelEdit.FlatAppearance.BorderSize = 0;
            btnCancelEdit.Click += BtnCancelEdit_Click;

            gbInput.Controls.AddRange(new Control[] { lblTitle, txtTitle, lblDescription, txtDescription, lblPriority, cbPriority, lblDueDate, dtpDueDate, btnSave, btnCancelEdit });
            this.Controls.Add(gbInput);

            // 3. Filter & Search Panel
            panelFilter = new Panel
            {
                Location = new Point(20, 235),
                Size = new Size(900, 45),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            lblSearch = new Label { Text = "🔍 Tìm kiếm:", Location = new Point(0, 12), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            txtSearch = new TextBox { Location = new Point(90, 8), Size = new Size(220, 26) };
            txtSearch.TextChanged += FilterChanged;

            lblStatusFilter = new Label { Text = "Trạng thái:", Location = new Point(330, 12), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            cbStatusFilter = new ComboBox { Location = new Point(410, 8), Size = new Size(140, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cbStatusFilter.Items.AddRange(new object[] { "Tất cả", "Đang làm", "Đã hoàn thành" });
            cbStatusFilter.SelectedIndex = 0;
            cbStatusFilter.SelectedIndexChanged += FilterChanged;

            lblPriorityFilter = new Label { Text = "Ưu tiên:", Location = new Point(570, 12), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            cbPriorityFilter = new ComboBox { Location = new Point(630, 8), Size = new Size(130, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cbPriorityFilter.Items.AddRange(new object[] { "Tất cả", "Cao", "Trung bình", "Thấp" });
            cbPriorityFilter.SelectedIndex = 0;
            cbPriorityFilter.SelectedIndexChanged += FilterChanged;

            panelFilter.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblStatusFilter, cbStatusFilter, lblPriorityFilter, cbPriorityFilter });
            this.Controls.Add(panelFilter);

            // 4. DataGridView
            dgvTodos = new DataGridView
            {
                Location = new Point(20, 290),
                Size = new Size(900, 310),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                RowHeadersVisible = false
            };

            dgvTodos.EnableHeadersVisualStyles = false;
            dgvTodos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(74, 63, 140); // KZTEK Navy Light
            dgvTodos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTodos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvTodos.ColumnHeadersHeight = 35;
            dgvTodos.RowTemplate.Height = 32;

            // Add Columns
            dgvTodos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvTodos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsCompleted", HeaderText = "Done", DataPropertyName = "IsCompleted", Width = 50 });
            dgvTodos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Tên Công Việc", DataPropertyName = "Title", Width = 220 });
            dgvTodos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Mô Tả", DataPropertyName = "Description", Width = 250 });
            dgvTodos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Priority", HeaderText = "Mức Ưu Tiên", DataPropertyName = "PriorityDisplay", Width = 110 });
            dgvTodos.Columns.Add(new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Hạn Chót", DataPropertyName = "DueDateDisplay", Width = 110 });
            
            var btnDeleteCol = new DataGridViewButtonColumn
            {
                Name = "ColDelete",
                HeaderText = "Thao tác",
                Text = "🗑️ Xóa",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvTodos.Columns.Add(btnDeleteCol);

            dgvTodos.CellClick += DgvTodos_CellClick;
            dgvTodos.CellDoubleClick += DgvTodos_CellDoubleClick;

            this.Controls.Add(dgvTodos);

            // 5. Status Strip
            statusStrip = new StatusStrip { BackColor = Color.FromArgb(230, 233, 240) };
            lblStatTotal = new ToolStripStatusLabel { Text = "Tổng số: 0 công việc | " };
            lblStatActive = new ToolStripStatusLabel { Text = "Đang làm: 0 | ", ForeColor = Color.DarkOrange };
            lblStatCompleted = new ToolStripStatusLabel { Text = "Đã hoàn thành: 0", ForeColor = Color.Green };

            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatTotal, lblStatActive, lblStatCompleted });
            this.Controls.Add(statusStrip);
        }

        private void LoadDataToGrid()
        {
            string status = cbStatusFilter.SelectedItem?.ToString() ?? "Tất cả";
            string priority = cbPriorityFilter.SelectedItem?.ToString() ?? "Tất cả";
            string keyword = txtSearch.Text;

            var list = _todoService.Filter(status, priority, keyword);

            var bindList = list.Select(x => new
            {
                x.Id,
                x.IsCompleted,
                x.Title,
                x.Description,
                PriorityDisplay = GetPriorityText(x.Priority),
                DueDateDisplay = x.DueDate.HasValue ? x.DueDate.Value.ToString("dd/MM/yyyy") : "Không có",
                RawItem = x
            }).ToList();

            dgvTodos.DataSource = bindList;

            // Formatting row colors
            foreach (DataGridViewRow row in dgvTodos.Rows)
            {
                bool isDone = row.Cells["IsCompleted"].Value is bool b && b;
                if (isDone)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Strikeout);
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                }
            }

            // Update status bar stats
            var stats = _todoService.GetStats();
            lblStatTotal.Text = $"Tổng số: {stats.Total} công việc | ";
            lblStatActive.Text = $"Đang làm: {stats.Active} | ";
            lblStatCompleted.Text = $"Đã hoàn thành: {stats.Completed}";
        }

        private string GetPriorityText(PriorityLevel p)
        {
            return p switch
            {
                PriorityLevel.High => "🔴 Cao",
                PriorityLevel.Medium => "🟡 Trung bình",
                PriorityLevel.Low => "🟢 Thấp",
                _ => "Trung bình"
            };
        }

        private PriorityLevel GetPriorityFromText(string text)
        {
            return text switch
            {
                "Cao" => PriorityLevel.High,
                "Thấp" => PriorityLevel.Low,
                _ => PriorityLevel.Medium
            };
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tên công việc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            string pText = cbPriority.SelectedItem?.ToString() ?? "Trung bình";
            PriorityLevel priority = GetPriorityFromText(pText);

            if (_selectedEditItem == null)
            {
                // Add new
                var newItem = new TodoItem
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Priority = priority,
                    DueDate = dtpDueDate.Value.Date
                };
                _todoService.Add(newItem);
                MessageBox.Show("Đã thêm công việc mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Update existing
                _selectedEditItem.Title = txtTitle.Text.Trim();
                _selectedEditItem.Description = txtDescription.Text.Trim();
                _selectedEditItem.Priority = priority;
                _selectedEditItem.DueDate = dtpDueDate.Value.Date;
                _todoService.Update(_selectedEditItem);
                MessageBox.Show("Đã cập nhật công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetFormInput();
            }

            txtTitle.Clear();
            txtDescription.Clear();
            LoadDataToGrid();
        }

        private void BtnCancelEdit_Click(object? sender, EventArgs e)
        {
            ResetFormInput();
        }

        private void ResetFormInput()
        {
            _selectedEditItem = null;
            txtTitle.Clear();
            txtDescription.Clear();
            cbPriority.SelectedIndex = 1;
            dtpDueDate.Value = DateTime.Now;
            btnSave.Text = "➕ Thêm Công Việc";
            btnSave.BackColor = Color.FromArgb(240, 89, 34); // KZTEK Orange
            btnCancelEdit.Visible = false;
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            LoadDataToGrid();
        }

        private void DgvTodos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? id = dgvTodos.Rows[e.RowIndex].Cells["Id"].Value?.ToString();
            if (string.IsNullOrEmpty(id)) return;

            // Handle Checkbox Toggle
            if (dgvTodos.Columns[e.ColumnIndex].Name == "IsCompleted")
            {
                _todoService.ToggleComplete(id);
                LoadDataToGrid();
                return;
            }

            // Handle Delete Button
            if (dgvTodos.Columns[e.ColumnIndex].Name == "ColDelete")
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa công việc này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    _todoService.Delete(id);
                    LoadDataToGrid();
                }
            }
        }

        private void DgvTodos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? id = dgvTodos.Rows[e.RowIndex].Cells["Id"].Value?.ToString();
            if (string.IsNullOrEmpty(id)) return;
            var item = _todoService.GetAll().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _selectedEditItem = item;
                txtTitle.Text = item.Title;
                txtDescription.Text = item.Description;
                cbPriority.SelectedItem = item.Priority switch
                {
                    PriorityLevel.High => "Cao",
                    PriorityLevel.Low => "Thấp",
                    _ => "Trung bình"
                };
                if (item.DueDate.HasValue) dtpDueDate.Value = item.DueDate.Value;

                btnSave.Text = "💾 Cập Nhật";
                btnSave.BackColor = Color.FromArgb(74, 63, 140); // KZTEK Navy Light
                btnCancelEdit.Visible = true;
            }
        }
    }
}
