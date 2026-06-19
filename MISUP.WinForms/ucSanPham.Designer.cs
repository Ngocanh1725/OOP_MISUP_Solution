namespace MISUP.WinForms
{
    partial class ucSanPham
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlDashboard, pnlToolbar;
        private System.Windows.Forms.Label lblTongSP, lblTongGiaTri, lblCanhBaoTon;
        private System.Windows.Forms.Button btnThem, btnSua, btnXoa, btnXuatExcel, btnTim;
        private System.Windows.Forms.ComboBox cmbLocLoai;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvData;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cmbLocLoai = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // Setup Base
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Size = new System.Drawing.Size(850, 600);

            // Dashboard Panel
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDashboard.Height = 90;
            this.pnlDashboard.BackColor = System.Drawing.Color.Transparent;

            // Các Card Thống Kê
            System.Windows.Forms.Panel card1 = CreateCard("📦 TỔNG MẶT HÀNG", System.Drawing.Color.FromArgb(52, 152, 219), out lblTongSP, 20);
            System.Windows.Forms.Panel card2 = CreateCard("💰 TỔNG GIÁ TRỊ KHO", System.Drawing.Color.FromArgb(46, 204, 113), out lblTongGiaTri, 250);
            System.Windows.Forms.Panel card3 = CreateCard("⚠️ SẮP HẾT HÀNG (<10)", System.Drawing.Color.FromArgb(231, 76, 60), out lblCanhBaoTon, 480);
            this.pnlDashboard.Controls.AddRange(new System.Windows.Forms.Control[] { card1, card2, card3 });

            // Toolbar Panel
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 60;
            this.pnlToolbar.BackColor = System.Drawing.Color.White;

            this.btnThem.Text = "➕ Thêm"; this.btnThem.Location = new System.Drawing.Point(20, 12); StyleBtn(this.btnThem, System.Drawing.Color.FromArgb(46, 204, 113));
            this.btnSua.Text = "✏️ Sửa"; this.btnSua.Location = new System.Drawing.Point(120, 12); StyleBtn(this.btnSua, System.Drawing.Color.FromArgb(243, 156, 18));
            this.btnXoa.Text = "🗑️ Xóa"; this.btnXoa.Location = new System.Drawing.Point(220, 12); StyleBtn(this.btnXoa, System.Drawing.Color.FromArgb(231, 76, 60));
            this.btnXuatExcel.Text = "📊 Xuất CSV"; this.btnXuatExcel.Location = new System.Drawing.Point(320, 12); StyleBtn(this.btnXuatExcel, System.Drawing.Color.FromArgb(39, 174, 96)); this.btnXuatExcel.Width = 110;

            this.cmbLocLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocLoai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbLocLoai.Location = new System.Drawing.Point(450, 14);
            this.cmbLocLoai.Size = new System.Drawing.Size(130, 33);
            this.cmbLocLoai.Items.AddRange(new string[] { "Tất cả", "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });

            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new System.Drawing.Point(590, 14);
            this.txtTimKiem.Size = new System.Drawing.Size(150, 32);

            this.btnTim.Text = "🔍 Tìm"; this.btnTim.Location = new System.Drawing.Point(750, 12); StyleBtn(this.btnTim, System.Drawing.Color.FromArgb(52, 152, 219)); this.btnTim.Width = 80;

            this.pnlToolbar.Controls.AddRange(new System.Windows.Forms.Control[] { btnThem, btnSua, btnXoa, btnXuatExcel, cmbLocLoai, txtTimKiem, btnTim });

            // DataGridView
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvData.ColumnHeadersHeight = 40;
            this.dgvData.RowTemplate.Height = 35;
            this.dgvData.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.dgvData.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlDashboard);

            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel CreateCard(string title, System.Drawing.Color bgColor, out System.Windows.Forms.Label lblValue, int left)
        {
            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel() { Width = 210, Height = 70, BackColor = bgColor, Left = left, Top = 10 };
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label() { Text = title, ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold), AutoSize = true, Left = 10, Top = 10 };
            lblValue = new System.Windows.Forms.Label() { Text = "0", ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold), AutoSize = true, Left = 10, Top = 30 };
            p.Controls.Add(lblTitle); p.Controls.Add(lblValue);
            return p;
        }

        private void StyleBtn(System.Windows.Forms.Button btn, System.Drawing.Color color)
        {
            btn.Width = 90; btn.Height = 35;
            btn.BackColor = color; btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
        }
    }
}