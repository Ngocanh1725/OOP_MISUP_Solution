using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    partial class PhanQuyenForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle, lblUserList, lblModules;
        private ListBox lstUsers;
        private CheckedListBox clbModules;
        private Button btnSave, btnClose;

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
            this.lblUserList = new Label();
            this.lblModules = new Label();
            this.lstUsers = new ListBox();
            this.clbModules = new CheckedListBox();
            this.btnSave = new Button();
            this.btnClose = new Button();

            this.SuspendLayout();

            Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold);
            Font labelFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font regularFont = new Font("Segoe UI", 11F);

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = titleFont;
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Phân Quyền Truy Cập Hệ Thống";

            // Users List
            this.lblUserList.AutoSize = true;
            this.lblUserList.Font = labelFont;
            this.lblUserList.Location = new Point(20, 70);
            this.lblUserList.Text = "1. Chọn Nhân Viên:";

            this.lstUsers.Font = regularFont;
            this.lstUsers.Location = new Point(20, 100);
            this.lstUsers.Size = new Size(250, 300);
            this.lstUsers.BorderStyle = BorderStyle.FixedSingle;

            // Modules CheckList
            this.lblModules.AutoSize = true;
            this.lblModules.Font = labelFont;
            this.lblModules.Location = new Point(300, 70);
            this.lblModules.Text = "2. Phân Quyền Danh Mục:";

            this.clbModules.Font = regularFont;
            this.clbModules.Location = new Point(300, 100);
            this.clbModules.Size = new Size(350, 300);
            this.clbModules.BorderStyle = BorderStyle.FixedSingle;
            this.clbModules.CheckOnClick = true;
            this.clbModules.Enabled = false; // Disable until a user is selected

            // Add Module Definitions
            this.clbModules.Items.AddRange(new object[] {
                new ModuleItem { TenModule = "🏠 Tổng quan hệ thống", MaModule = "TongQuan" },
                new ModuleItem { TenModule = "🛒 Đặt hàng", MaModule = "DatHang" },
                new ModuleItem { TenModule = "📥 Nhập hàng", MaModule = "NhapHang" },
                new ModuleItem { TenModule = "🏷️ Sản phẩm", MaModule = "SanPham" },
                new ModuleItem { TenModule = "🏢 Nhà cung cấp", MaModule = "NhaCungCap" },
                new ModuleItem { TenModule = "💳 Thanh toán NCC", MaModule = "ThanhToan" },
                new ModuleItem { TenModule = "📋 Kiểm kê kho", MaModule = "KiemKe" },
                new ModuleItem { TenModule = "📊 Báo cáo thống kê", MaModule = "BaoCao" }
            });

            // Save Button
            this.btnSave.BackColor = Color.FromArgb(0, 136, 255);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(480, 420);
            this.btnSave.Size = new Size(170, 40);
            this.btnSave.Text = "💾 Lưu Phân Quyền";
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // Close Button
            this.btnClose.BackColor = Color.White;
            this.btnClose.FlatAppearance.BorderColor = Color.LightGray;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.Location = new Point(370, 420);
            this.btnClose.Size = new Size(100, 40);
            this.btnClose.Text = "Đóng";
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += (s, e) => this.Close();

            // Form Properties
            this.BackColor = Color.White;
            this.ClientSize = new Size(680, 490);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUserList); this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.lblModules); this.Controls.Add(this.clbModules);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Phân Quyền Truy Cập (RBAC)";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}