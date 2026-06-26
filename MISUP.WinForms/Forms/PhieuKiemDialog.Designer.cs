namespace MISUP.WinForms
{
    partial class PhieuKiemDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop, pnlBottom;
        private System.Windows.Forms.DataGridView dgvChiTietKiem;
        private System.Windows.Forms.Label lblMaKiem, lblNguoiKiem, lblGhiChu;
        private System.Windows.Forms.TextBox txtMaKiem, txtNguoiKiem, txtGhiChu;
        private System.Windows.Forms.Button btnSave, btnCancel;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.dgvChiTietKiem = new System.Windows.Forms.DataGridView();
            this.lblMaKiem = new System.Windows.Forms.Label();
            this.lblNguoiKiem = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtMaKiem = new System.Windows.Forms.TextBox();
            this.txtNguoiKiem = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietKiem)).BeginInit();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 100;
            this.pnlTop.Controls.Add(this.lblMaKiem);
            this.pnlTop.Controls.Add(this.txtMaKiem);
            this.pnlTop.Controls.Add(this.lblNguoiKiem);
            this.pnlTop.Controls.Add(this.txtNguoiKiem);
            this.pnlTop.Controls.Add(this.lblGhiChu);
            this.pnlTop.Controls.Add(this.txtGhiChu);

            // Top Controls
            this.lblMaKiem.Text = "Mã kiểm:"; this.lblMaKiem.Location = new System.Drawing.Point(20, 23); this.lblMaKiem.AutoSize = true;
            this.txtMaKiem.Location = new System.Drawing.Point(100, 20); this.txtMaKiem.Width = 150; this.txtMaKiem.ReadOnly = true;

            this.lblNguoiKiem.Text = "Người kiểm:"; this.lblNguoiKiem.Location = new System.Drawing.Point(20, 58); this.lblNguoiKiem.AutoSize = true;
            this.txtNguoiKiem.Location = new System.Drawing.Point(100, 55); this.txtNguoiKiem.Width = 150;

            this.lblGhiChu.Text = "Ghi chú:"; this.lblGhiChu.Location = new System.Drawing.Point(300, 23); this.lblGhiChu.AutoSize = true;
            this.txtGhiChu.Location = new System.Drawing.Point(360, 20); this.txtGhiChu.Width = 250; this.txtGhiChu.Height = 57; this.txtGhiChu.Multiline = true;

            // pnlBottom
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 60;
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Controls.Add(this.btnCancel);

            // btnSave
            this.btnSave.Text = "Xác nhận & Lưu";
            this.btnSave.Location = new System.Drawing.Point(200, 10);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Location = new System.Drawing.Point(340, 10);
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // dgvChiTietKiem
            this.dgvChiTietKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietKiem.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietKiem.AllowUserToAddRows = false;
            this.dgvChiTietKiem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvChiTietKiem.Columns.Add("MaSP", "Mã SP");
            this.dgvChiTietKiem.Columns.Add("TenSP", "Tên SP");
            this.dgvChiTietKiem.Columns.Add("TonHeThong", "Tồn Hệ Thống");
            this.dgvChiTietKiem.Columns.Add("TonThucTe", "Tồn Thực Tế");

            this.dgvChiTietKiem.Columns["MaSP"].ReadOnly = true;
            this.dgvChiTietKiem.Columns["TenSP"].ReadOnly = true;
            this.dgvChiTietKiem.Columns["TonHeThong"].ReadOnly = true;
            this.dgvChiTietKiem.Columns["TonThucTe"].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;

            // PhieuKiemDialog
            this.ClientSize = new System.Drawing.Size(650, 500);
            this.Controls.Add(this.dgvChiTietKiem);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kiểm Kê Kho Hàng";
            this.Load += new System.EventHandler(this.PhieuKiemDialog_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietKiem)).EndInit();
            this.ResumeLayout(false);
        }
    }
}