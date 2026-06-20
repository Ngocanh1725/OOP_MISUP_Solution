namespace MISUP.WinForms
{
    partial class ucDatHang
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTaoDonHang;
        private System.Windows.Forms.Button btnXuatFile;

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ComboBox cmbTrangThai;
        private System.Windows.Forms.ComboBox cmbNhaCungCap;
        private System.Windows.Forms.Button btnTim;

        private System.Windows.Forms.DataGridView dgvDonHang;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTaoDonHang = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.dgvDonHang = new System.Windows.Forms.DataGridView();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnTim = new System.Windows.Forms.Button();
            this.cmbTrangThai = new System.Windows.Forms.ComboBox();
            this.cmbNhaCungCap = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlHeader.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnXuatFile);
            this.pnlHeader.Controls.Add(this.btnTaoDonHang);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(20, 20);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1080, 60);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Quản Lý Đặt Hàng Nhập";
            // 
            // btnTaoDonHang
            // 
            this.btnTaoDonHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaoDonHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(255)))));
            this.btnTaoDonHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaoDonHang.FlatAppearance.BorderSize = 0;
            this.btnTaoDonHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoDonHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoDonHang.ForeColor = System.Drawing.Color.White;
            this.btnTaoDonHang.Location = new System.Drawing.Point(920, 10);
            this.btnTaoDonHang.Name = "btnTaoDonHang";
            this.btnTaoDonHang.Size = new System.Drawing.Size(160, 40);
            this.btnTaoDonHang.Text = "➕ Tạo đơn nhập";
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatFile.BackColor = System.Drawing.Color.White;
            this.btnXuatFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnXuatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnXuatFile.Location = new System.Drawing.Point(800, 10);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(110, 40);
            this.btnXuatFile.Text = "📥 Xuất file";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.dgvDonHang);
            this.pnlCard.Controls.Add(this.pnlToolbar);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(20, 80);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCard.Size = new System.Drawing.Size(1080, 640);
            // 
            // dgvDonHang
            // 
            this.dgvDonHang.AllowUserToAddRows = false;
            this.dgvDonHang.AllowUserToDeleteRows = false;
            this.dgvDonHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonHang.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDonHang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(115)))), ((int)(((byte)(129)))));
            headerStyle.Padding = new System.Windows.Forms.Padding(15, 10, 10, 10);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dgvDonHang.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvDonHang.ColumnHeadersHeight = 50;
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = System.Drawing.Color.White;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(43)))), ((int)(((byte)(54)))));
            cellStyle.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            cellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDonHang.DefaultCellStyle = cellStyle;
            this.dgvDonHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonHang.EnableHeadersVisualStyles = false;
            this.dgvDonHang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvDonHang.Location = new System.Drawing.Point(5, 75);
            this.dgvDonHang.ReadOnly = true;
            this.dgvDonHang.RowHeadersVisible = false;
            this.dgvDonHang.RowTemplate.Height = 50;
            this.dgvDonHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.btnTim);
            this.pnlToolbar.Controls.Add(this.cmbTrangThai);
            this.pnlToolbar.Controls.Add(this.cmbNhaCungCap);
            this.pnlToolbar.Controls.Add(this.txtTimKiem);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(5, 5);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1070, 70);
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.Color.White;
            this.btnTim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTim.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.btnTim.Location = new System.Drawing.Point(740, 18);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(100, 36);
            this.btnTim.Text = "Lọc";
            // 
            // cmbTrangThai
            // 
            this.cmbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrangThai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTrangThai.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Phiếu tạm",
            "Đang giao dịch",
            "Hoàn thành",
            "Đã hủy"});
            this.cmbTrangThai.Location = new System.Drawing.Point(540, 20);
            this.cmbTrangThai.Name = "cmbTrangThai";
            this.cmbTrangThai.Size = new System.Drawing.Size(180, 33);
            // 
            // cmbNhaCungCap
            // 
            this.cmbNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbNhaCungCap.Items.AddRange(new object[] {
            "Tất cả NCC",
            "Công ty CP Acecook",
            "Samsung Việt Nam",
            "Nhà phân phối Unilever",
            "Công ty TNHH Panasonic"});
            this.cmbNhaCungCap.Location = new System.Drawing.Point(340, 20);
            this.cmbNhaCungCap.Name = "cmbNhaCungCap";
            this.cmbNhaCungCap.Size = new System.Drawing.Size(180, 33);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            this.txtTimKiem.Location = new System.Drawing.Point(20, 20);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 34);
            this.txtTimKiem.Text = "🔍 Tìm mã đơn, tên NCC...";
            // 
            // ucDatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucDatHang";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1120, 740);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}