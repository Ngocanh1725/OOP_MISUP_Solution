using System;
using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    public partial class PhieuKiemDialog : Form
    {
        public string MaKiemKe { get; set; }
        public string NgayKiem { get; set; }
        public string NguoiKiem { get; set; }
        public string Kho { get; set; }
        public string ChenhLech { get; set; }
        public string TrangThai { get; set; }

        private ComboBox cmbKho, cmbTrangThai;
        private TextBox txtMa, txtNguoi, txtChenhLech;
        private DateTimePicker dtpNgay;

        public PhieuKiemDialog() { BuildUI(); }

        private void BuildUI()
        {
            this.Text = "Chi Tiết Phiếu Kiểm Kho";
            this.Size = new Size(450, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            Label lblTitle = new Label { Text = "Lập Phiếu Kiểm Kê", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            Label l1 = new Label { Text = "Mã phiếu:", Location = new Point(20, 70), AutoSize = true };
            txtMa = new TextBox { Location = new Point(20, 95), Width = 180, ReadOnly = true };

            Label l2 = new Label { Text = "Ngày kiểm:", Location = new Point(220, 70), AutoSize = true };
            dtpNgay = new DateTimePicker { Location = new Point(220, 95), Width = 180, Format = DateTimePickerFormat.Short };

            Label l3 = new Label { Text = "Người kiểm kê:", Location = new Point(20, 140), AutoSize = true };
            txtNguoi = new TextBox { Location = new Point(20, 165), Width = 380 };

            Label l4 = new Label { Text = "Kho kiểm:", Location = new Point(20, 210), AutoSize = true };
            cmbKho = new ComboBox { Location = new Point(20, 235), Width = 380, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbKho.Items.AddRange(new object[] { "Kho Tổng HN", "Kho Miền Trung", "Kho Miền Nam" });

            Label l5 = new Label { Text = "SL Chênh Lệch:", Location = new Point(20, 280), AutoSize = true };
            txtChenhLech = new TextBox { Location = new Point(20, 305), Width = 180 };

            Label l6 = new Label { Text = "Trạng thái:", Location = new Point(220, 280), AutoSize = true };
            cmbTrangThai = new ComboBox { Location = new Point(220, 305), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTrangThai.Items.AddRange(new object[] { "Đã cân bằng", "Đang xử lý", "Đã hủy" });

            Button btnSave = new Button { Text = "Lưu", Location = new Point(300, 340), Width = 100, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += BtnSave_Click;

            this.Controls.AddRange(new Control[] { lblTitle, l1, txtMa, l2, dtpNgay, l3, txtNguoi, l4, cmbKho, l5, txtChenhLech, l6, cmbTrangThai, btnSave });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtMa.Text = MaKiemKe;
            if (!string.IsNullOrEmpty(NgayKiem)) dtpNgay.Value = DateTime.ParseExact(NgayKiem, "dd/MM/yyyy HH:mm", null);
            txtNguoi.Text = NguoiKiem ?? "Admin";
            cmbKho.Text = Kho ?? "Kho Tổng HN";
            txtChenhLech.Text = ChenhLech;
            cmbTrangThai.Text = TrangThai ?? "Đang xử lý";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MaKiemKe = txtMa.Text; NgayKiem = dtpNgay.Value.ToString("dd/MM/yyyy HH:mm");
            NguoiKiem = txtNguoi.Text; Kho = cmbKho.Text; ChenhLech = txtChenhLech.Text; TrangThai = cmbTrangThai.Text;
            this.DialogResult = DialogResult.OK; this.Close();
        }
    }
}