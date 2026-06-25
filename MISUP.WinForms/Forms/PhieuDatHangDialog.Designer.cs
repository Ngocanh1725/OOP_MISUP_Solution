using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    partial class PhieuDatHangDialog
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle, lblMaDon, lblNgayTao, lblNhaCungCap, lblTongTien, lblTrangThai, lblNguoiTao;
        private TextBox txtMaDon, txtTongTien, txtNguoiTao;
        private DateTimePicker dtpNgayTao;
        private ComboBox cmbNhaCungCap, cmbTrangThai;
        private Button btnSave, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblMaDon = new Label();
            this.txtMaDon = new TextBox();
            this.lblNgayTao = new Label();
            this.dtpNgayTao = new DateTimePicker();
            this.lblNhaCungCap = new Label();
            this.cmbNhaCungCap = new ComboBox();
            this.lblTongTien = new Label();
            this.txtTongTien = new TextBox();
            this.lblTrangThai = new Label();
            this.cmbTrangThai = new ComboBox();
            this.lblNguoiTao = new Label();
            this.txtNguoiTao = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            Font labelFont = new Font("Segoe UI", 10F);
            Font inputFont = new Font("Segoe UI", 11F);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Tạo Đơn Đặt Hàng Mới";

            // Row 1: Mã đơn & Ngày tạo
            this.lblMaDon.AutoSize = true; this.lblMaDon.Font = labelFont; this.lblMaDon.Location = new Point(20, 80); this.lblMaDon.Text = "Mã đơn hàng:";
            this.txtMaDon.Font = inputFont; this.txtMaDon.Location = new Point(20, 105); this.txtMaDon.Size = new Size(200, 32); this.txtMaDon.ReadOnly = true;

            this.lblNgayTao.AutoSize = true; this.lblNgayTao.Font = labelFont; this.lblNgayTao.Location = new Point(240, 80); this.lblNgayTao.Text = "Ngày lập phiếu:";
            this.dtpNgayTao.Font = inputFont; this.dtpNgayTao.Format = DateTimePickerFormat.Short; this.dtpNgayTao.Location = new Point(240, 105); this.dtpNgayTao.Size = new Size(250, 32);

            // Row 2: NCC & Tiền
            this.lblNhaCungCap.AutoSize = true; this.lblNhaCungCap.Font = labelFont; this.lblNhaCungCap.Location = new Point(20, 150); this.lblNhaCungCap.Text = "Nhà cung cấp:";
            this.cmbNhaCungCap.Font = inputFont; this.cmbNhaCungCap.Location = new Point(20, 175); this.cmbNhaCungCap.Size = new Size(200, 33);
            this.cmbNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblTongTien.AutoSize = true; this.lblTongTien.Font = labelFont; this.lblTongTien.Location = new Point(240, 150); this.lblTongTien.Text = "Tổng tiền ước tính (VNĐ):";
            this.txtTongTien.Font = inputFont; this.txtTongTien.Location = new Point(240, 175); this.txtTongTien.Size = new Size(250, 32);

            // Row 3: Trạng thái & Người tạo
            this.lblTrangThai.AutoSize = true; this.lblTrangThai.Font = labelFont; this.lblTrangThai.Location = new Point(20, 220); this.lblTrangThai.Text = "Trạng thái:";
            this.cmbTrangThai.Font = inputFont; this.cmbTrangThai.Location = new Point(20, 245); this.cmbTrangThai.Size = new Size(200, 33);
            this.cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrangThai.Items.AddRange(new object[] { "Phiếu tạm", "Đang giao dịch", "Hoàn thành", "Đã hủy" });

            this.lblNguoiTao.AutoSize = true; this.lblNguoiTao.Font = labelFont; this.lblNguoiTao.Location = new Point(240, 220); this.lblNguoiTao.Text = "Người lập phiếu:";
            this.txtNguoiTao.Font = inputFont; this.txtNguoiTao.Location = new Point(240, 245); this.txtNguoiTao.Size = new Size(250, 32);

            // Buttons
            this.btnSave.BackColor = Color.FromArgb(0, 136, 255);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(370, 310);
            this.btnSave.Size = new Size(120, 40);
            this.btnSave.Text = "Lưu Đơn";
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.btnCancel.BackColor = Color.White;
            this.btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.Location = new Point(260, 310);
            this.btnCancel.Size = new Size(90, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // Dialog Properties
            this.BackColor = Color.White;
            this.ClientSize = new Size(520, 380);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMaDon); this.Controls.Add(this.txtMaDon);
            this.Controls.Add(this.lblNgayTao); this.Controls.Add(this.dtpNgayTao);
            this.Controls.Add(this.lblNhaCungCap); this.Controls.Add(this.cmbNhaCungCap);
            this.Controls.Add(this.lblTongTien); this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.lblTrangThai); this.Controls.Add(this.cmbTrangThai);
            this.Controls.Add(this.lblNguoiTao); this.Controls.Add(this.txtNguoiTao);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chi Tiết Đơn Đặt Hàng";
            this.Load += new System.EventHandler(this.PhieuDatHangDialog_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}