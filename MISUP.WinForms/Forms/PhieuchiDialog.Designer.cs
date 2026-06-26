namespace MISUP.WinForms
{
    partial class PhieuChiDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader, lblMaPhieu, lblNgayChi, lblNguoiNhan, lblSoTien, lblLyDo;
        private System.Windows.Forms.TextBox txtMaPhieu, txtNguoiNhan, txtSoTien, txtLyDo;
        private System.Windows.Forms.DateTimePicker dtpNgayChi;
        private System.Windows.Forms.Button btnSave, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.lblNgayChi = new System.Windows.Forms.Label();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.txtNguoiNhan = new System.Windows.Forms.TextBox();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.dtpNgayChi = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblHeader
            this.lblHeader.Text = "PHIẾU CHI TIỀN";
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHeader.Height = 50;

            // Labels
            this.lblMaPhieu.Text = "Mã phiếu:"; this.lblMaPhieu.Location = new System.Drawing.Point(20, 70);
            this.lblNgayChi.Text = "Ngày chi:"; this.lblNgayChi.Location = new System.Drawing.Point(20, 110);
            this.lblNguoiNhan.Text = "Người nhận:"; this.lblNguoiNhan.Location = new System.Drawing.Point(20, 150);
            this.lblSoTien.Text = "Số tiền (VNĐ):"; this.lblSoTien.Location = new System.Drawing.Point(20, 190);
            this.lblLyDo.Text = "Lý do chi:"; this.lblLyDo.Location = new System.Drawing.Point(20, 230);

            // Inputs
            this.txtMaPhieu.Location = new System.Drawing.Point(120, 70); this.txtMaPhieu.Width = 230; this.txtMaPhieu.ReadOnly = true;
            this.dtpNgayChi.Location = new System.Drawing.Point(120, 110); this.dtpNgayChi.Width = 230; this.dtpNgayChi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtNguoiNhan.Location = new System.Drawing.Point(120, 150); this.txtNguoiNhan.Width = 230;
            this.txtSoTien.Location = new System.Drawing.Point(120, 190); this.txtSoTien.Width = 230;
            this.txtLyDo.Location = new System.Drawing.Point(120, 230); this.txtLyDo.Width = 230; this.txtLyDo.Multiline = true; this.txtLyDo.Height = 60;

            // btnSave
            this.btnSave.Text = "Lưu Phiếu Chi";
            this.btnSave.Location = new System.Drawing.Point(80, 310);
            this.btnSave.Size = new System.Drawing.Size(110, 40);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Location = new System.Drawing.Point(200, 310);
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // PhieuChiDialog
            this.ClientSize = new System.Drawing.Size(400, 420);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMaPhieu, this.txtMaPhieu, this.lblNgayChi, this.dtpNgayChi,
                this.lblNguoiNhan, this.txtNguoiNhan, this.lblSoTien, this.txtSoTien,
                this.lblLyDo, this.txtLyDo, this.btnSave, this.btnCancel, this.lblHeader
            });
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Phiếu Chi Mới";
            this.Load += new System.EventHandler(this.PhieuChiDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}