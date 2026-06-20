using System;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    public partial class PhieuNhapDialog : Form
    {
        public bool IsEditMode { get; set; } = false;
        public string MaPhieu { get; set; }

        // Các Property để lưu dữ liệu trả về cho màn hình chính
        public string ThoiGian { get; set; }
        public string NhaCungCap { get; set; }
        public string ChiNhanh { get; set; }
        public string TongTien { get; set; }
        public string TrangThai { get; set; }

        public PhieuNhapDialog()
        {
            InitializeComponent();
        }

        private void PhieuNhapDialog_Load(object sender, EventArgs e)
        {
            cmbNhaCungCap.SelectedIndex = 0;
            cmbChiNhanh.SelectedIndex = 0;
            cmbTrangThai.SelectedIndex = 0;

            if (IsEditMode)
            {
                this.Text = "Cập nhật Phiếu Nhập Kho";
                lblTitle.Text = $"Cập nhật Phiếu: {MaPhieu}";

                // Đổ dữ liệu cũ lên giao diện
                dtpThoiGian.Value = DateTime.ParseExact(ThoiGian, "dd/MM/yyyy HH:mm", null);
                cmbNhaCungCap.Text = NhaCungCap;
                cmbChiNhanh.Text = ChiNhanh;
                txtTongTien.Text = TongTien.Replace(",", "");
                cmbTrangThai.Text = TrangThai;
            }
            else
            {
                this.Text = "Tạo Phiếu Nhập Kho Mới";
                lblTitle.Text = "Tạo Mới Phiếu Nhập";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTongTien.Text))
            {
                MessageBox.Show("Vui lòng nhập Tổng tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật lại properties
            ThoiGian = dtpThoiGian.Value.ToString("dd/MM/yyyy HH:mm");
            NhaCungCap = cmbNhaCungCap.Text;
            ChiNhanh = cmbChiNhanh.Text;

            if (decimal.TryParse(txtTongTien.Text, out decimal tien))
            {
                TongTien = tien.ToString("N0");
            }
            else
            {
                TongTien = txtTongTien.Text;
            }

            TrangThai = cmbTrangThai.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}