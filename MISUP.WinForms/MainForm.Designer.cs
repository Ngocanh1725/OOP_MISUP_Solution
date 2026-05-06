namespace MISUP.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlSidebar, pnlTop, pnlContent;
        private System.Windows.Forms.Label lblLogo, lblTitle;
        private System.Windows.Forms.Button btnSanPham, btnTaiKhoan, btnLogout;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel(); this.btnLogout = new System.Windows.Forms.Button(); this.btnTaiKhoan = new System.Windows.Forms.Button(); this.btnSanPham = new System.Windows.Forms.Button(); this.lblLogo = new System.Windows.Forms.Label(); this.pnlTop = new System.Windows.Forms.Panel(); this.lblTitle = new System.Windows.Forms.Label(); this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout(); this.pnlTop.SuspendLayout(); this.SuspendLayout();

            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(168))))); this.pnlSidebar.Controls.Add(this.btnTaiKhoan); this.pnlSidebar.Controls.Add(this.btnSanPham); this.pnlSidebar.Controls.Add(this.btnLogout); this.pnlSidebar.Controls.Add(this.lblLogo); this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left; this.pnlSidebar.Location = new System.Drawing.Point(0, 0); this.pnlSidebar.Name = "pnlSidebar"; this.pnlSidebar.Size = new System.Drawing.Size(220, 600);
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top; this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold); this.lblLogo.ForeColor = System.Drawing.Color.White; this.lblLogo.Location = new System.Drawing.Point(0, 0); this.lblLogo.Name = "lblLogo"; this.lblLogo.Size = new System.Drawing.Size(220, 100); this.lblLogo.Text = "MISUP\nCOSMETICS"; this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSanPham.Dock = System.Windows.Forms.DockStyle.Top; this.btnSanPham.FlatAppearance.BorderSize = 0; this.btnSanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSanPham.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.btnSanPham.Location = new System.Drawing.Point(0, 100); this.btnSanPham.Name = "btnSanPham"; this.btnSanPham.Size = new System.Drawing.Size(220, 60); this.btnSanPham.Text = "  📦 Quản lý Sản phẩm"; this.btnSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnSanPham.UseVisualStyleBackColor = true; this.btnSanPham.Cursor = System.Windows.Forms.Cursors.Hand; this.btnSanPham.Click += new System.EventHandler(this.btnSanPham_Click);
            this.btnTaiKhoan.Dock = System.Windows.Forms.DockStyle.Top; this.btnTaiKhoan.FlatAppearance.BorderSize = 0; this.btnTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.btnTaiKhoan.Location = new System.Drawing.Point(0, 160); this.btnTaiKhoan.Name = "btnTaiKhoan"; this.btnTaiKhoan.Size = new System.Drawing.Size(220, 60); this.btnTaiKhoan.Text = "  👥 Tài khoản"; this.btnTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnTaiKhoan.UseVisualStyleBackColor = true;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom; this.btnLogout.FlatAppearance.BorderSize = 0; this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.btnLogout.Location = new System.Drawing.Point(0, 540); this.btnLogout.Name = "btnLogout"; this.btnLogout.Size = new System.Drawing.Size(220, 60); this.btnLogout.Text = "  🔴 Đăng xuất"; this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnLogout.UseVisualStyleBackColor = true; this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand; this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke; this.pnlTop.Controls.Add(this.lblTitle); this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top; this.pnlTop.Location = new System.Drawing.Point(220, 0); this.pnlTop.Name = "pnlTop"; this.pnlTop.Size = new System.Drawing.Size(780, 60);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold); this.lblTitle.ForeColor = System.Drawing.Color.DimGray; this.lblTitle.Location = new System.Drawing.Point(0, 0); this.lblTitle.Name = "lblTitle"; this.lblTitle.Size = new System.Drawing.Size(780, 60); this.lblTitle.Text = "TRANG CHỦ"; this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlContent.BackColor = System.Drawing.Color.White; this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill; this.pnlContent.Location = new System.Drawing.Point(220, 60); this.pnlContent.Name = "pnlContent"; this.pnlContent.Size = new System.Drawing.Size(780, 540);

            this.ClientSize = new System.Drawing.Size(1000, 600); this.Controls.Add(this.pnlContent); this.Controls.Add(this.pnlTop); this.Controls.Add(this.pnlSidebar); this.Name = "MainForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Hệ thống Quản lý MISUP";
            this.pnlSidebar.ResumeLayout(false); this.pnlTop.ResumeLayout(false); this.ResumeLayout(false);
        }
    }
}
