namespace MISUP.WinForms
{
    partial class ucTongQuan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpDashboard; // Dùng FlowLayout để không bị đè

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
            this.flpDashboard = new System.Windows.Forms.FlowLayoutPanel();
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
            this.flpDashboard.SuspendLayout();
            this.cardSKU.SuspendLayout();
            this.cardGiaTri.SuspendLayout();
            this.cardCanhBaoTon.SuspendLayout();
            this.cardCanhBaoHSD.SuspendLayout();
            this.pnlCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "TỔNG QUAN HỆ THỐNG";
            // 
            // flpDashboard
            // 
            this.flpDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.flpDashboard.Controls.Add(this.cardSKU);
            this.flpDashboard.Controls.Add(this.cardGiaTri);
            this.flpDashboard.Controls.Add(this.cardCanhBaoTon);
            this.flpDashboard.Controls.Add(this.cardCanhBaoHSD);
            this.flpDashboard.Location = new System.Drawing.Point(20, 70);
            this.flpDashboard.Name = "flpDashboard";
            this.flpDashboard.Size = new System.Drawing.Size(1040, 130);
            // 
            // cardSKU
            // 
            this.cardSKU.BackColor = System.Drawing.Color.White;
            this.cardSKU.Controls.Add(this.lblTongSKUTitle);
            this.cardSKU.Controls.Add(this.lblTongSKUValue);
            this.cardSKU.Location = new System.Drawing.Point(3, 3);
            this.cardSKU.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cardSKU.Size = new System.Drawing.Size(245, 110);
            // 
            // lblTongSKUTitle
            // 
            this.lblTongSKUTitle.AutoSize = true;
            this.lblTongSKUTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongSKUTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTongSKUTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTongSKUTitle.Text = "Tổng Sản phẩm (SKUs)";
            // 
            // lblTongSKUValue
            // 
            this.lblTongSKUValue.AutoSize = true;
            this.lblTongSKUValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTongSKUValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblTongSKUValue.Location = new System.Drawing.Point(15, 45);
            this.lblTongSKUValue.Text = "0";
            // 
            // cardGiaTri
            // 
            this.cardGiaTri.BackColor = System.Drawing.Color.White;
            this.cardGiaTri.Controls.Add(this.lblTongGiaTriTitle);
            this.cardGiaTri.Controls.Add(this.lblTongGiaTriValue);
            this.cardGiaTri.Location = new System.Drawing.Point(260, 3);
            this.cardGiaTri.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cardGiaTri.Size = new System.Drawing.Size(245, 110);
            // 
            // lblTongGiaTriTitle
            // 
            this.lblTongGiaTriTitle.AutoSize = true;
            this.lblTongGiaTriTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongGiaTriTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTongGiaTriTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTongGiaTriTitle.Text = "Tổng Giá Trị Kho";
            // 
            // lblTongGiaTriValue
            // 
            this.lblTongGiaTriValue.AutoSize = true;
            this.lblTongGiaTriValue.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTongGiaTriValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblTongGiaTriValue.Location = new System.Drawing.Point(15, 50);
            this.lblTongGiaTriValue.Text = "0 đ";
            // 
            // cardCanhBaoTon
            // 
            this.cardCanhBaoTon.BackColor = System.Drawing.Color.White;
            this.cardCanhBaoTon.Controls.Add(this.lblCanhBaoTonTitle);
            this.cardCanhBaoTon.Controls.Add(this.lblCanhBaoTonValue);
            this.cardCanhBaoTon.Location = new System.Drawing.Point(517, 3);
            this.cardCanhBaoTon.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cardCanhBaoTon.Size = new System.Drawing.Size(245, 110);
            // 
            // lblCanhBaoTonTitle
            // 
            this.lblCanhBaoTonTitle.AutoSize = true;
            this.lblCanhBaoTonTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCanhBaoTonTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCanhBaoTonTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCanhBaoTonTitle.Text = "Hàng Sắp Hết Tồn";
            // 
            // lblCanhBaoTonValue
            // 
            this.lblCanhBaoTonValue.AutoSize = true;
            this.lblCanhBaoTonValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCanhBaoTonValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblCanhBaoTonValue.Location = new System.Drawing.Point(15, 45);
            this.lblCanhBaoTonValue.Text = "0";
            // 
            // cardCanhBaoHSD
            // 
            this.cardCanhBaoHSD.BackColor = System.Drawing.Color.White;
            this.cardCanhBaoHSD.Controls.Add(this.lblCanhBaoHSDTitle);
            this.cardCanhBaoHSD.Controls.Add(this.lblCanhBaoHSDValue);
            this.cardCanhBaoHSD.Location = new System.Drawing.Point(774, 3);
            this.cardCanhBaoHSD.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cardCanhBaoHSD.Size = new System.Drawing.Size(245, 110);
            // 
            // lblCanhBaoHSDTitle
            // 
            this.lblCanhBaoHSDTitle.AutoSize = true;
            this.lblCanhBaoHSDTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCanhBaoHSDTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCanhBaoHSDTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCanhBaoHSDTitle.Text = "Hàng Cận Date / Hết Hạn";
            // 
            // lblCanhBaoHSDValue
            // 
            this.lblCanhBaoHSDValue.AutoSize = true;
            this.lblCanhBaoHSDValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCanhBaoHSDValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCanhBaoHSDValue.Location = new System.Drawing.Point(15, 45);
            this.lblCanhBaoHSDValue.Text = "0";
            // 
            // pnlCanhBao
            // 
            this.pnlCanhBao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCanhBao.BackColor = System.Drawing.Color.White;
            this.pnlCanhBao.Controls.Add(this.lblCanhBaoTitle);
            this.pnlCanhBao.Controls.Add(this.dgvCanhBao);
            this.pnlCanhBao.Location = new System.Drawing.Point(20, 210);
            this.pnlCanhBao.Name = "pnlCanhBao";
            this.pnlCanhBao.Size = new System.Drawing.Size(1040, 460);
            // 
            // lblCanhBaoTitle
            // 
            this.lblCanhBaoTitle.AutoSize = true;
            this.lblCanhBaoTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCanhBaoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.lblCanhBaoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCanhBaoTitle.Text = "DANH SÁCH SẢN PHẨM CẦN XỬ LÝ (SẮP HẾT HÀNG HOẶC HẾT HẠN)";
            // 
            // dgvCanhBao
            // 
            this.dgvCanhBao.AllowUserToAddRows = false;
            this.dgvCanhBao.AllowUserToDeleteRows = false;

            // --- KHÓA KÉO DÃN LƯỚI CHO TỔNG QUAN ---
            this.dgvCanhBao.AllowUserToResizeColumns = false;
            this.dgvCanhBao.AllowUserToResizeRows = false;
            this.dgvCanhBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.dgvCanhBao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCanhBao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanhBao.BackgroundColor = System.Drawing.Color.White;
            this.dgvCanhBao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCanhBao.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCanhBao.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(115)))), ((int)(((byte)(129)))));
            headerStyle.Padding = new System.Windows.Forms.Padding(10);
            this.dgvCanhBao.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvCanhBao.ColumnHeadersHeight = 45;
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = System.Drawing.Color.White;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            cellStyle.ForeColor = System.Drawing.Color.Black;
            cellStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            cellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCanhBao.DefaultCellStyle = cellStyle;
            this.dgvCanhBao.EnableHeadersVisualStyles = false;
            this.dgvCanhBao.Location = new System.Drawing.Point(15, 60);
            this.dgvCanhBao.ReadOnly = true;
            this.dgvCanhBao.RowHeadersVisible = false;
            this.dgvCanhBao.RowTemplate.Height = 45;
            this.dgvCanhBao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCanhBao.Size = new System.Drawing.Size(1010, 380);
            // 
            // ucTongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlCanhBao);
            this.Controls.Add(this.flpDashboard);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucTongQuan";
            this.Size = new System.Drawing.Size(1080, 700);
            this.flpDashboard.ResumeLayout(false);
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