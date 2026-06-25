using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    partial class QuanLyNhanVienForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle, lblUser, lblName, lblRole;
        private TextBox txtUser, txtName;
        private ComboBox cmbRole;
        private Button btnAdd, btnEdit, btnDelete, btnClose;
        private DataGridView dgvData;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblUser = new Label();
            this.txtUser = new TextBox();
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnClose = new Button();
            this.dgvData = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            Font labelFont = new Font("Segoe UI", 10F);
            Font inputFont = new Font("Segoe UI", 11F);

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Quản lý nhân viên";

            // Dòng nhập liệu: User
            this.lblUser.AutoSize = true; this.lblUser.Font = labelFont; this.lblUser.Location = new Point(20, 70); this.lblUser.Text = "Tên đăng nhập:";
            this.txtUser.Font = inputFont; this.txtUser.Location = new Point(20, 95); this.txtUser.Size = new Size(160, 32);

            // Dòng nhập liệu: Họ Tên
            this.lblName.AutoSize = true; this.lblName.Font = labelFont; this.lblName.Location = new Point(190, 70); this.lblName.Text = "Họ và Tên:";
            this.txtName.Font = inputFont; this.txtName.Location = new Point(190, 95); this.txtName.Size = new Size(180, 32);

            // Dòng nhập liệu: Quyền (Vai trò)
            this.lblRole.AutoSize = true; this.lblRole.Font = labelFont; this.lblRole.Location = new Point(380, 70); this.lblRole.Text = "Vai trò (Quyền):";
            this.cmbRole.Font = inputFont; this.cmbRole.Location = new Point(380, 95); this.cmbRole.Size = new Size(130, 33);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Nhân viên Kho", "Kế toán", "Admin" });
            this.cmbRole.SelectedIndex = 0;

            // Nút Thêm
            this.btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(520, 93);
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.Text = "➕ Thêm";
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            // Nút Sửa
            this.btnEdit.BackColor = Color.FromArgb(243, 156, 18);
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Location = new Point(630, 93);
            this.btnEdit.Size = new Size(100, 35);
            this.btnEdit.Text = "✏️ Sửa";
            this.btnEdit.Cursor = Cursors.Hand;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // Nút Xóa
            this.btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(740, 93);
            this.btnDelete.Size = new Size(110, 35);
            this.btnDelete.Text = "🗑️ Xóa NV";
            this.btnDelete.Cursor = Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // Lưới hiển thị danh sách NV
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = Color.White;
            this.dgvData.BorderStyle = BorderStyle.FixedSingle;
            this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(249, 250, 251);
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.ForeColor = Color.FromArgb(99, 115, 129);
            headerStyle.Padding = new Padding(10);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvData.ColumnHeadersHeight = 45;
            this.dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 10F);
            cellStyle.ForeColor = Color.Black;
            cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255);
            cellStyle.SelectionForeColor = Color.Black;
            cellStyle.Padding = new Padding(10, 0, 10, 0);
            this.dgvData.DefaultCellStyle = cellStyle;

            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.Location = new Point(20, 150);
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 45;
            this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new Size(830, 320);
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellClick); // Sự kiện Tự Động Điền

            // Nút Đóng
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.BackColor = Color.White;
            this.btnClose.FlatAppearance.BorderColor = Color.LightGray;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.Location = new Point(750, 490);
            this.btnClose.Size = new Size(100, 40);
            this.btnClose.Text = "Đóng";
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += (s, e) => this.Close();

            // Properties Form
            this.BackColor = Color.White;
            this.ClientSize = new Size(870, 550);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser); this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblRole); this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnEdit); this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý nhân viên";

            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}