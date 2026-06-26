using System;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class PhieuChiDialog : Form
    {
        public string MaPhieu { get; private set; }
        public DateTime NgayChi { get; private set; }
        public string NguoiNhan { get; private set; }
        public decimal SoTien { get; private set; }
        public string LyDo { get; private set; }

        public PhieuChiDialog()
        {
            InitializeComponent();
        }

        private void PhieuChiDialog_Load(object sender, EventArgs e)
        {
            // Tự động sinh mã phiếu chi khi mở form
            txtMaPhieu.Text = "PC" + DateTime.Now.ToString("yyMMddHHmm");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNguoiNhan.Text) || string.IsNullOrWhiteSpace(txtSoTien.Text))
                    throw new Exception("Vui lòng nhập người nhận và số tiền!");

                this.MaPhieu = txtMaPhieu.Text;
                this.NgayChi = dtpNgayChi.Value;
                this.NguoiNhan = txtNguoiNhan.Text;
                this.SoTien = decimal.Parse(txtSoTien.Text);
                this.LyDo = txtLyDo.Text;

                // TODO: Gọi BLL để lưu phiếu chi vào Database ở đây
                // _phieuChiBLL.ThemPhieuChi(this.MaPhieu, this.NgayChi, ...);

                MessageBox.Show("Tạo phiếu chi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}