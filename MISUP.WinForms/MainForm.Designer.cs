namespace MISUP.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnKiemKeKho;
        private System.Windows.Forms.Button btnThanhToanNCC;
        private System.Windows.Forms.Button btnNhaCungCap;
        private System.Windows.Forms.Button btnSanPham;
        private System.Windows.Forms.Button btnNhapHang;
        private System.Windows.Forms.Button btnDatHang;
        private System.Windows.Forms.Button btnTongQuan;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderBorder;
        private System.Windows.Forms.TextBox txtSearchHeader;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.Panel pnlContent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnTongQuan = new System.Windows.Forms.Button();
            this.btnDatHang = new System.Windows.Forms.Button();
            this.btnNhapHang = new System.Windows.Forms.Button();
            this.btnSanPham = new System.Windows.Forms.Button();
            this.btnNhaCungCap = new System.Windows.Forms.Button();
            this.btnThanhToanNCC = new System.Windows.Forms.Button();
            this.btnKiemKeKho = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderBorder = new System.Windows.Forms.Panel();
            this.txtSearchHeader = new System.Windows.Forms.TextBox();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this.pnlSidebar.Controls.Add(this.btnBaoCao);
            this.pnlSidebar.Controls.Add(this.btnKiemKeKho);
            this.pnlSidebar.Controls.Add(this.btnThanhToanNCC);
            this.pnlSidebar.Controls.Add(this.btnNhaCungCap);
            this.pnlSidebar.Controls.Add(this.btnSanPham);
            this.pnlSidebar.Controls.Add(this.btnNhapHang);
            this.pnlSidebar.Controls.Add(this.btnDatHang);
            this.pnlSidebar.Controls.Add(this.btnTongQuan);
            this.pnlSidebar.Controls.Add(this.lblLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(230, 800);

            // lblLogo
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(230, 80);
            this.lblLogo.Text = "QUẢN LÝ\nNHẬP HÀNG";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Tạo các nút (Dùng chung thuộc tính cơ bản)
            System.Windows.Forms.Button[] menuButtons = { btnTongQuan, btnDatHang, btnNhapHang, btnSanPham, btnNhaCungCap, btnThanhToanNCC, btnKiemKeKho, btnBaoCao };
            string[] btnTexts = { "   🏠 Tổng quan", "   🛒 Đặt hàng", "   📥 Nhập hàng", "   🏷️ Sản phẩm", "   🏢 Nhà cung cấp", "   💳 Thanh toán NCC", "   📋 Kiểm kê kho", "   📊 Báo cáo" };

            int topPadding = 80;
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].Dock = System.Windows.Forms.DockStyle.Top;
                menuButtons[i].FlatAppearance.BorderSize = 0;
                menuButtons[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                menuButtons[i].Font = new System.Drawing.Font("Segoe UI", 11F);
                menuButtons[i].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
                menuButtons[i].BackColor = System.Drawing.Color.Transparent;
                menuButtons[i].Location = new System.Drawing.Point(0, topPadding);
                menuButtons[i].Name = "btn" + i;
                menuButtons[i].Size = new System.Drawing.Size(230, 50);
                menuButtons[i].Text = btnTexts[i];
                menuButtons[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                menuButtons[i].Cursor = System.Windows.Forms.Cursors.Hand;

                // Gắn sự kiện click chung
                menuButtons[i].Click += new System.EventHandler(this.MenuButton_Click);

                topPadding += 50;
            }

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.btnUser);
            this.pnlHeader.Controls.Add(this.txtSearchHeader);
            this.pnlHeader.Controls.Add(this.pnlHeaderBorder);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(230, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1120, 60);

            // pnlHeaderBorder
            this.pnlHeaderBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pnlHeaderBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderBorder.Height = 1;

            // txtSearchHeader
            this.txtSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchHeader.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearchHeader.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchHeader.Location = new System.Drawing.Point(20, 16);
            this.txtSearchHeader.Size = new System.Drawing.Size(350, 25);
            this.txtSearchHeader.Text = "🔍 Tìm kiếm (Ctrl + K)";

            // btnUser
            this.btnUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUser.FlatAppearance.BorderSize = 0;
            this.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnUser.Location = new System.Drawing.Point(670, 0);
            this.btnUser.Size = new System.Drawing.Size(300, 60);
            this.btnUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // btnLogout
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Location = new System.Drawing.Point(980, 0);
            this.btnLogout.Size = new System.Drawing.Size(100, 60);
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(230, 60);
            this.pnlContent.Name = "pnlContent";

            // MainForm
            this.ClientSize = new System.Drawing.Size(1350, 800);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Nhập hàng MISUP";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}