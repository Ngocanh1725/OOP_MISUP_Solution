using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    partial class NhaCungCapDialog
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle, lblMaNCC, lblTenNCC, lblDienThoai, lblTrangThai;
        private TextBox txtMaNCC, txtTenNCC, txtDienThoai;
        private ComboBox cmbTrangThai;
        private Button btnSave, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblMaNCC = new Label();
            this.txtMaNCC = new TextBox();
            this.lblTenNCC = new Label();
            this.txtTenNCC = new TextBox();
            this.lblDienThoai = new Label();
            this.txtDienThoai = new TextBox();
            this.lblTrangThai = new Label();
            this.cmbTrangThai = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            Font labelFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            Font inputFont = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Thêm Mới Đối Tác";

            // Mã NCC
            this.lblMaNCC.AutoSize = true; this.lblMaNCC.Font = labelFont; this.lblMaNCC.Location = new Point(20, 80); this.lblMaNCC.Text = "Mã Nhà Cung Cấp (*)";
            this.txtMaNCC.Font = inputFont; this.txtMaNCC.Location = new Point(20, 105); this.txtMaNCC.Size = new Size(350, 32);

            // Tên NCC
            this.lblTenNCC.AutoSize = true; this.lblTenNCC.Font = labelFont; this.lblTenNCC.Location = new Point(20, 150); this.lblTenNCC.Text = "Tên Công Ty/Đối tác (*)";
            this.txtTenNCC.Font = inputFont; this.txtTenNCC.Location = new Point(20, 175); this.txtTenNCC.Size = new Size(350, 32);

            // Điện Thoại
            this.lblDienThoai.AutoSize = true; this.lblDienThoai.Font = labelFont; this.lblDienThoai.Location = new Point(20, 220); this.lblDienThoai.Text = "Số điện thoại liên hệ";
            this.txtDienThoai.Font = inputFont; this.txtDienThoai.Location = new Point(20, 245); this.txtDienThoai.Size = new Size(350, 32);

            // Trạng Thái
            this.lblTrangThai.AutoSize = true; this.lblTrangThai.Font = labelFont; this.lblTrangThai.Location = new Point(20, 290); this.lblTrangThai.Text = "Trạng thái hợp tác";
            this.cmbTrangThai.Font = inputFont; this.cmbTrangThai.Location = new Point(20, 315); this.cmbTrangThai.Size = new Size(350, 33);
            this.cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrangThai.Items.AddRange(new object[] { "Đang giao dịch", "Ngừng giao dịch" });

            // Buttons
            this.btnSave.BackColor = Color.FromArgb(0, 136, 255);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(240, 380);
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu đối tác";
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.btnCancel.BackColor = Color.White;
            this.btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.Location = new Point(140, 380);
            this.btnCancel.Size = new Size(90, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // Form Properties
            this.BackColor = Color.White;
            this.ClientSize = new Size(390, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMaNCC); this.Controls.Add(this.txtMaNCC);
            this.Controls.Add(this.lblTenNCC); this.Controls.Add(this.txtTenNCC);
            this.Controls.Add(this.lblDienThoai); this.Controls.Add(this.txtDienThoai);
            this.Controls.Add(this.lblTrangThai); this.Controls.Add(this.cmbTrangThai);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông Tin Đối Tác";
            this.Load += new System.EventHandler(this.NhaCungCapDialog_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}