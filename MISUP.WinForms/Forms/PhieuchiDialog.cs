using System;
using System.Drawing;
using System.Windows.Forms;
using MISUP.BLL.Services;

namespace MISUP.WinForms.Forms
{
    public partial class PhieuChiDialog : Form
    {
        private NhaCungCapBLL _nccBLL = new NhaCungCapBLL();

        public string MaPhieu { get; set; }
        public string NgayChi { get; set; }
        public string NhaCungCap { get; set; }
        public string SoTien { get; set; }
        public string PhuongThuc { get; set; }
        public string TrangThai { get; set; }

        private ComboBox cmbNCC, cmbPhuongThuc, cmbTrangThai;
        private TextBox txtMa, txtTien;
        private DateTimePicker dtpNgay;

        public PhieuChiDialog()
        {
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Lập Phiếu Chi";
            this.Size = new Size(450, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            Label lblTitle = new Label { Text = "Phiếu Thanh Toán NCC", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            Label l1 = new Label { Text = "Mã phiếu:", Location = new Point(20, 70), AutoSize = true };
            txtMa = new TextBox { Location = new Point(20, 95), Width = 180, ReadOnly = true };

            Label l2 = new Label { Text = "Ngày thanh toán:", Location = new Point(220, 70), AutoSize = true };
            dtpNgay = new DateTimePicker { Location = new Point(220, 95), Width = 180, Format = DateTimePickerFormat.Short };

            Label l3 = new Label { Text = "Nhà cung cấp:", Location = new Point(20, 140), AutoSize = true };
            cmbNCC = new ComboBox { Location = new Point(20, 165), Width = 380, DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (var ncc in _nccBLL.LayDanhSach()) cmbNCC.Items.Add(ncc.TenNCC);

            Label l4 = new Label { Text = "Số tiền chi (VNĐ):", Location = new Point(20, 210), AutoSize = true };
            txtTien = new TextBox { Location = new Point(20, 235), Width = 380 };

            Label l5 = new Label { Text = "Phương thức:", Location = new Point(20, 280), AutoSize = true };
            cmbPhuongThuc = new ComboBox { Location = new Point(20, 305), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPhuongThuc.Items.AddRange(new object[] { "Chuyển khoản", "Tiền mặt" });

            Label l6 = new Label { Text = "Trạng thái:", Location = new Point(220, 280), AutoSize = true };
            cmbTrangThai = new ComboBox { Location = new Point(220, 305), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTrangThai.Items.AddRange(new object[] { "Đã thanh toán", "Kỳ hạn nợ" });

            Button btnSave = new Button { Text = "Lưu", Location = new Point(300, 340), Width = 100, BackColor = Color.FromArgb(0, 136, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += BtnSave_Click;

            this.Controls.AddRange(new Control[] { lblTitle, l1, txtMa, l2, dtpNgay, l3, cmbNCC, l4, txtTien, l5, cmbPhuongThuc, l6, cmbTrangThai, btnSave });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtMa.Text = MaPhieu;
            if (!string.IsNullOrEmpty(NgayChi)) dtpNgay.Value = DateTime.ParseExact(NgayChi, "dd/MM/yyyy", null);
            cmbNCC.Text = NhaCungCap;
            txtTien.Text = SoTien?.Replace(",", "");
            cmbPhuongThuc.Text = string.IsNullOrEmpty(PhuongThuc) ? "Chuyển khoản" : PhuongThuc;
            cmbTrangThai.Text = string.IsNullOrEmpty(TrangThai) ? "Đã thanh toán" : TrangThai;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MaPhieu = txtMa.Text; NgayChi = dtpNgay.Value.ToString("dd/MM/yyyy");
            NhaCungCap = cmbNCC.Text;
            if (decimal.TryParse(txtTien.Text, out decimal t)) SoTien = t.ToString("N0"); else SoTien = txtTien.Text;
            PhuongThuc = cmbPhuongThuc.Text; TrangThai = cmbTrangThai.Text;
            this.DialogResult = DialogResult.OK; this.Close();
        }
    }
}