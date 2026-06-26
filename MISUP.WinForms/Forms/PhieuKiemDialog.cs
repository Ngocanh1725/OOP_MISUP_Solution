using System;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class PhieuKiemDialog : Form
    {
        public PhieuKiemDialog()
        {
            InitializeComponent();
        }

        private void PhieuKiemDialog_Load(object sender, EventArgs e)
        {
            // Sinh mã kiểm kê tự động
            txtMaKiem.Text = "PK" + DateTime.Now.ToString("yyMMddHHmm");

            // TODO: Thay thế dữ liệu mẫu này bằng việc lấy từ DB thông qua HangHoaBLL
            LoadDuLieuMau();
        }

        private void LoadDuLieuMau()
        {
            dgvChiTietKiem.Rows.Add("SP01", "Sữa rửa mặt", "50", "50");
            dgvChiTietKiem.Rows.Add("SP02", "Kem chống nắng", "30", "28"); // Bị hụt 2 sản phẩm
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO: Xử lý lưu dữ liệu kiểm kê xuống Database, cập nhật lại số lượng kho nếu có sai lệch

            MessageBox.Show("Đã lưu kết quả kiểm kê kho!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}