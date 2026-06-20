namespace MISUP.WinForms
{
    partial class ucTongQuan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlDashboard;

        // Các Panel (Thẻ Card) hiển thị thông số
        private System.Windows.Forms.Panel cardSKU;
        private System.Windows.Forms.Label lblTongSKUTitle;
        private System.Windows.Forms.Label lblTongSKUValue;

        private System.Windows.Forms.Panel cardGiaTri;
        private System.Windows.Forms.Label lblTongGiaTriTitle;
        private System.Windows.Forms.Label lblTongGiaTriValue;

        private System.Windows.Forms.Panel cardCanhBaoTon;
        private System.Windows.Forms.Label lblCanhBaoTonTitle;
        private System.Windows.Forms.Label lblCanhBaoTonValue;

        private System.Windows.Forms.Panel cardCanhBaoHSD;
        private System.Windows.Forms.Label lblCanhBaoHSDTitle;
        private System.Windows.Forms.Label lblCanhBaoHSDValue;

        // Vùng hiển thị chi tiết nhắc nhở
        private System.Windows.Forms.Panel pnlCanhBao;
        private System.Windows.Forms.Label lblCanhBaoTitle;
        private System.Windows.Forms.DataGridView dgvCanhBao;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlDashboard = new System.Windows.Forms.Panel();

            this.cardSKU = new System.Windows.Forms.Panel();
            this.lblTongSKUTitle = new System.Windows.Forms.Label();
            this.lblTongSKUValue = new System.Windows.Forms.Label();

            this.cardGiaTri = new System.Windows.Forms.Panel();
            this.lblTongGiaTriTitle = new System.Windows.Forms.Label();
            this.lblTongGiaTriValue = new System.Windows.Forms.Label();

            this.cardCanhBaoTon = new System.Windows.Forms.Panel();
            this.lblCanhBaoTonTitle = new System.Windows.Forms.Label();
            this.lblCanhBaoTonValue = new System.Windows.Forms.Label();

            this.cardCanhBaoHSD = new System.Windows.Forms.Panel();
            this.lblCanhBaoHSDTitle = new System.Windows.Forms.Label();
            this.lblCanhBaoHSDValue = new System.Windows.Forms.Label();

            this.pnlCanhBao = new System.Windows.Forms.Panel();
            this.lblCanhBaoTitle = new System.Windows.Forms.Label();
            this.dgvCanhBao = new System.Windows.Forms.DataGridView();

            this.pnlDashboard.SuspendLayout();
            this.cardSKU.SuspendLayout();
            this.cardGiaTri.SuspendLayout();
            this.cardCanhBaoTon.SuspendLayout();
            this.cardCanhBaoHSD.SuspendLayout();
            this.pnlCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).BeginInit();
            this.SuspendLayout();

            // Font chuẩn
            System.Drawing.Font fontCardTitle = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontCardValue = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "TỔNG QUAN HỆ THỐNG";

            // pnlDashboard (Khu vực chứa các thẻ Card ngang)
            this.pnlDashboard.Controls.Add(this.cardSKU);
            this.pnlDashboard.Controls.Add(this.cardGiaTri);
            this.pnlDashboard.Controls.Add(this.cardCanhBaoTon);
            this.pnlDashboard.Controls.Add(this.cardCanhBaoHSD);
            this.pnlDashboard.Location = new System.Drawing.Point(35, 80);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(1000, 140);
            this.pnlDashboard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // ==========================================
            // CARD 1: TỔNG SẢN PHẨM (SKU)
            // ==========================================
            this.cardSKU.BackColor = System.Drawing.Color.White;
            this.cardSKU.Location = new System.Drawing.Point(0, 0);
            this.cardSKU.Size = new System.Drawing.Size(230, 120);
            this.cardSKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblTongSKUTitle.AutoSize = true; this.lblTongSKUTitle.Font = fontCardTitle; this.lblTongSKUTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTongSKUTitle.Location = new System.Drawing.Point(15, 20); this.lblTongSKUTitle.Text = "Tổng Sản phẩm (SKUs)";

            this.lblTongSKUValue.AutoSize = true; this.lblTongSKUValue.Font = fontCardValue; this.lblTongSKUValue.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219); // Xanh dương
            this.lblTongSKUValue.Location = new System.Drawing.Point(15, 50); this.lblTongSKUValue.Text = "0";

            this.cardSKU.Controls.Add(this.lblTongSKUTitle); this.cardSKU.Controls.Add(this.lblTongSKUValue);

            // ==========================================
            // CARD 2: TỔNG GIÁ TRỊ KHO
            // ==========================================
            this.cardGiaTri.BackColor = System.Drawing.Color.White;
            this.cardGiaTri.Location = new System.Drawing.Point(250, 0);
            this.cardGiaTri.Size = new System.Drawing.Size(230, 120);
            this.cardGiaTri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblTongGiaTriTitle.AutoSize = true; this.lblTongGiaTriTitle.Font = fontCardTitle; this.lblTongGiaTriTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTongGiaTriTitle.Location = new System.Drawing.Point(15, 20); this.lblTongGiaTriTitle.Text = "Tổng Giá Trị Kho";

            this.lblTongGiaTriValue.AutoSize = true; this.lblTongGiaTriValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold); this.lblTongGiaTriValue.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113); // Xanh lá
            this.lblTongGiaTriValue.Location = new System.Drawing.Point(15, 60); this.lblTongGiaTriValue.Text = "0 đ";

            this.cardGiaTri.Controls.Add(this.lblTongGiaTriTitle); this.cardGiaTri.Controls.Add(this.lblTongGiaTriValue);

            // ==========================================
            // CARD 3: CẢNH BÁO TỒN KHO
            // ==========================================
            this.cardCanhBaoTon.BackColor = System.Drawing.Color.White;
            this.cardCanhBaoTon.Location = new System.Drawing.Point(500, 0);
            this.cardCanhBaoTon.Size = new System.Drawing.Size(230, 120);
            this.cardCanhBaoTon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblCanhBaoTonTitle.AutoSize = true; this.lblCanhBaoTonTitle.Font = fontCardTitle; this.lblCanhBaoTonTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCanhBaoTonTitle.Location = new System.Drawing.Point(15, 20); this.lblCanhBaoTonTitle.Text = "Hàng Sắp Hết Tồn (<ĐM)";

            this.lblCanhBaoTonValue.AutoSize = true; this.lblCanhBaoTonValue.Font = fontCardValue; this.lblCanhBaoTonValue.ForeColor = System.Drawing.Color.FromArgb(243, 156, 18); // Cam
            this.lblCanhBaoTonValue.Location = new System.Drawing.Point(15, 50); this.lblCanhBaoTonValue.Text = "0";

            this.cardCanhBaoTon.Controls.Add(this.lblCanhBaoTonTitle); this.cardCanhBaoTon.Controls.Add(this.lblCanhBaoTonValue);

            // ==========================================
            // CARD 4: CẢNH BÁO HẠN SỬ DỤNG
            // ==========================================
            this.cardCanhBaoHSD.BackColor = System.Drawing.Color.White;
            this.cardCanhBaoHSD.Location = new System.Drawing.Point(750, 0);
            this.cardCanhBaoHSD.Size = new System.Drawing.Size(230, 120);
            this.cardCanhBaoHSD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblCanhBaoHSDTitle.AutoSize = true; this.lblCanhBaoHSDTitle.Font = fontCardTitle; this.lblCanhBaoHSDTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCanhBaoHSDTitle.Location = new System.Drawing.Point(15, 20); this.lblCanhBaoHSDTitle.Text = "Hàng Hết Hạn / Cận Date";

            this.lblCanhBaoHSDValue.AutoSize = true; this.lblCanhBaoHSDValue.Font = fontCardValue; this.lblCanhBaoHSDValue.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Đỏ
            this.lblCanhBaoHSDValue.Location = new System.Drawing.Point(15, 50); this.lblCanhBaoHSDValue.Text = "0";

            this.cardCanhBaoHSD.Controls.Add(this.lblCanhBaoHSDTitle); this.cardCanhBaoHSD.Controls.Add(this.lblCanhBaoHSDValue);

            // ==========================================
            // VÙNG HIỂN THỊ DANH SÁCH CHI TIẾT CẢNH BÁO
            // ==========================================
            this.pnlCanhBao.BackColor = System.Drawing.Color.White;
            this.pnlCanhBao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanhBao.Location = new System.Drawing.Point(35, 240);
            this.pnlCanhBao.Size = new System.Drawing.Size(1000, 400);
            this.pnlCanhBao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            this.lblCanhBaoTitle.AutoSize = true;
            this.lblCanhBaoTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCanhBaoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCanhBaoTitle.Text = "DANH SÁCH SẢN PHẨM CẦN LƯU Ý (SẮP HẾT HÀNG HOẶC HẾT HẠN)";

            this.dgvCanhBao.Location = new System.Drawing.Point(20, 50);
            this.dgvCanhBao.Size = new System.Drawing.Size(960, 330);
            this.dgvCanhBao.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvCanhBao.BackgroundColor = System.Drawing.Color.White;
            this.dgvCanhBao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCanhBao.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCanhBao.EnableHeadersVisualStyles = false;
            this.dgvCanhBao.AllowUserToAddRows = false;
            this.dgvCanhBao.ReadOnly = true;
            this.dgvCanhBao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCanhBao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanhBao.RowHeadersVisible = false;

            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(99, 115, 129);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.Padding = new System.Windows.Forms.Padding(10);
            this.dgvCanhBao.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvCanhBao.ColumnHeadersHeight = 45;

            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            cellStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            cellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCanhBao.DefaultCellStyle = cellStyle;
            this.dgvCanhBao.RowTemplate.Height = 45;

            this.pnlCanhBao.Controls.Add(this.lblCanhBaoTitle);
            this.pnlCanhBao.Controls.Add(this.dgvCanhBao);

            // ucTongQuan
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.pnlCanhBao);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucTongQuan";
            this.Size = new System.Drawing.Size(1080, 700);

            this.pnlDashboard.ResumeLayout(false);
            this.cardSKU.ResumeLayout(false);
            this.cardSKU.PerformLayout();
            this.cardGiaTri.ResumeLayout(false);
            this.cardGiaTri.PerformLayout();
            this.cardCanhBaoTon.ResumeLayout(false);
            this.cardCanhBaoTon.PerformLayout();
            this.cardCanhBaoHSD.ResumeLayout(false);
            this.cardCanhBaoHSD.PerformLayout();
            this.pnlCanhBao.ResumeLayout(false);
            this.pnlCanhBao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}