namespace MISUP.WinForms.Forms
{
    partial class ProductDialog
    {
        private System.ComponentModel.IContainer components = null;

        // BẮT BUỘC: Khai báo tất cả các control ở đây để file Logic nhìn thấy
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Panel pnlPrice;
        private System.Windows.Forms.Panel pnlStock;

        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.TextBox txtMaVach;
        private System.Windows.Forms.TextBox txtNsx;
        private System.Windows.Forms.TextBox txtSl;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.TextBox txtTonMin;

        private System.Windows.Forms.ComboBox cmbLoai;
        private System.Windows.Forms.ComboBox cmbDvt;

        private System.Windows.Forms.CheckBox chkCoHSD;
        private System.Windows.Forms.DateTimePicker dtpHsd;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.pnlPrice = new System.Windows.Forms.Panel();
            this.pnlStock = new System.Windows.Forms.Panel();

            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtMaVach = new System.Windows.Forms.TextBox();
            this.txtNsx = new System.Windows.Forms.TextBox();
            this.txtSl = new System.Windows.Forms.TextBox();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.txtTonMin = new System.Windows.Forms.TextBox();

            this.cmbLoai = new System.Windows.Forms.ComboBox();
            this.cmbDvt = new System.Windows.Forms.ComboBox();
            this.chkCoHSD = new System.Windows.Forms.CheckBox();
            this.dtpHsd = new System.Windows.Forms.DateTimePicker();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();

            // Font chuẩn
            System.Drawing.Font fontLabel = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontInput = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontTitle = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);

            this.SuspendLayout();

            // --- FORM CHÍNH ---
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(800, 750);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Nhập thông tin sản phẩm";

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Chi tiết sản phẩm";

            // ==========================================
            // BOX 1: THÔNG TIN CHUNG
            // ==========================================
            this.pnlInfo.BackColor = System.Drawing.Color.White;
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfo.Location = new System.Drawing.Point(20, 70);
            this.pnlInfo.Size = new System.Drawing.Size(740, 230);

            System.Windows.Forms.Label lblInfoTitle = new System.Windows.Forms.Label() { Text = "Thông tin chung", Font = fontTitle, AutoSize = true, Location = new System.Drawing.Point(15, 15) };

            System.Windows.Forms.Label lblTen = new System.Windows.Forms.Label() { Text = "Tên sản phẩm", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(20, 50) };
            this.txtTen.Location = new System.Drawing.Point(20, 75); this.txtTen.Size = new System.Drawing.Size(450, 30); this.txtTen.Font = fontInput;

            System.Windows.Forms.Label lblMa = new System.Windows.Forms.Label() { Text = "Mã SKU", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(500, 50) };
            this.txtMa.Location = new System.Drawing.Point(500, 75); this.txtMa.Size = new System.Drawing.Size(220, 30); this.txtMa.Font = fontInput;

            System.Windows.Forms.Label lblLoai = new System.Windows.Forms.Label() { Text = "Nhóm quản lý", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(20, 115) };
            this.cmbLoai.Location = new System.Drawing.Point(20, 140); this.cmbLoai.Size = new System.Drawing.Size(220, 30); this.cmbLoai.Font = fontInput; this.cmbLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            System.Windows.Forms.Label lblMaVach = new System.Windows.Forms.Label() { Text = "Mã vạch (Barcode)", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(260, 115) };
            this.txtMaVach.Location = new System.Drawing.Point(260, 140); this.txtMaVach.Size = new System.Drawing.Size(210, 30); this.txtMaVach.Font = fontInput;

            System.Windows.Forms.Label lblNsx = new System.Windows.Forms.Label() { Text = "Nhà cung cấp", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(500, 115) };
            this.txtNsx.Location = new System.Drawing.Point(500, 140); this.txtNsx.Size = new System.Drawing.Size(220, 30); this.txtNsx.Font = fontInput;

            System.Windows.Forms.Label lblDvt = new System.Windows.Forms.Label() { Text = "Đơn vị tính", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(20, 180) };
            this.cmbDvt.Location = new System.Drawing.Point(100, 178); this.cmbDvt.Size = new System.Drawing.Size(140, 30); this.cmbDvt.Font = fontInput;

            this.pnlInfo.Controls.AddRange(new System.Windows.Forms.Control[] { lblInfoTitle, lblTen, this.txtTen, lblMa, this.txtMa, lblLoai, this.cmbLoai, lblMaVach, this.txtMaVach, lblNsx, this.txtNsx, lblDvt, this.cmbDvt });

            // ==========================================
            // BOX 2: GIÁ SẢN PHẨM
            // ==========================================
            this.pnlPrice.BackColor = System.Drawing.Color.White;
            this.pnlPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPrice.Location = new System.Drawing.Point(20, 320);
            this.pnlPrice.Size = new System.Drawing.Size(740, 110);

            System.Windows.Forms.Label lblPriceTitle = new System.Windows.Forms.Label() { Text = "Giá sản phẩm", Font = fontTitle, AutoSize = true, Location = new System.Drawing.Point(15, 15) };
            System.Windows.Forms.Label lblGia = new System.Windows.Forms.Label() { Text = "Giá nhập (VNĐ)", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(20, 45) };
            this.txtGia.Location = new System.Drawing.Point(20, 70); this.txtGia.Size = new System.Drawing.Size(220, 30); this.txtGia.Font = fontInput;
            System.Windows.Forms.Label lblThueGhiChu = new System.Windows.Forms.Label() { Text = "Thuế nhập hàng: Tính tự động theo Ngành hàng", ForeColor = System.Drawing.Color.Gray, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic), AutoSize = true, Location = new System.Drawing.Point(260, 75) };

            this.pnlPrice.Controls.AddRange(new System.Windows.Forms.Control[] { lblPriceTitle, lblGia, this.txtGia, lblThueGhiChu });

            // ==========================================
            // BOX 3: TỒN KHO & HSD
            // ==========================================
            this.pnlStock.BackColor = System.Drawing.Color.White;
            this.pnlStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStock.Location = new System.Drawing.Point(20, 450);
            this.pnlStock.Size = new System.Drawing.Size(740, 140);

            System.Windows.Forms.Label lblStockTitle = new System.Windows.Forms.Label() { Text = "Quản lý tồn kho", Font = fontTitle, AutoSize = true, Location = new System.Drawing.Point(15, 15) };

            System.Windows.Forms.Label lblSl = new System.Windows.Forms.Label() { Text = "Số lượng nhập kho", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(20, 45) };
            this.txtSl.Location = new System.Drawing.Point(20, 70); this.txtSl.Size = new System.Drawing.Size(220, 30); this.txtSl.Font = fontInput;

            System.Windows.Forms.Label lblTonMin = new System.Windows.Forms.Label() { Text = "Định mức tồn thấp nhất (Cảnh báo)", Font = fontLabel, AutoSize = true, Location = new System.Drawing.Point(260, 45) };
            this.txtTonMin.Location = new System.Drawing.Point(260, 70); this.txtTonMin.Size = new System.Drawing.Size(210, 30); this.txtTonMin.Font = fontInput;

            this.chkCoHSD.Text = "Sản phẩm có Hạn sử dụng"; this.chkCoHSD.Font = fontLabel; this.chkCoHSD.Location = new System.Drawing.Point(500, 45); this.chkCoHSD.Size = new System.Drawing.Size(220, 25);
            this.dtpHsd.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpHsd.Font = fontInput; this.dtpHsd.Location = new System.Drawing.Point(500, 70); this.dtpHsd.Size = new System.Drawing.Size(210, 30); this.dtpHsd.Enabled = false;

            this.pnlStock.Controls.AddRange(new System.Windows.Forms.Control[] { lblStockTitle, lblSl, this.txtSl, lblTonMin, this.txtTonMin, this.chkCoHSD, this.dtpHsd });

            // ==========================================
            // BUTTONS CHỨC NĂNG
            // ==========================================
            this.btnSave.Text = "Lưu cập nhật";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 136, 255);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(600, 620);
            this.btnSave.Size = new System.Drawing.Size(160, 40);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(500, 620);
            this.btnCancel.Size = new System.Drawing.Size(90, 40);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnDelete.Text = "🗑 Xóa hàng";
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(20, 620);
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Visible = false; // Mặc định ẩn, file Logic sẽ hiện lên nếu là Sửa

            // THÊM VÀO FORM
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlPrice);
            this.Controls.Add(this.pnlStock);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}