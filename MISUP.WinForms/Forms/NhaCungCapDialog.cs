using System;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;

namespace MISUP.WinForms.Forms
{
    public partial class NhaCungCapDialog : Form
    {
        private NhaCungCapBLL _bll = new NhaCungCapBLL();
        private bool _isEdit;
        private NhaCungCap _ncc;

        public NhaCungCapDialog(NhaCungCap ncc = null)
        {
            InitializeComponent();
            _ncc = ncc;
            _isEdit = (ncc != null);
        }

        private void NhaCungCapDialog_Load(object sender, EventArgs e)
        {
            cmbTrangThai.SelectedIndex = 0; // Mặc định Đang giao dịch

            if (_isEdit)
            {
                lblTitle.Text = "Cập Nhật Đối Tác";
                txtMaNCC.Text = _ncc.MaNCC;
                txtMaNCC.ReadOnly = true; // Không cho phép sửa mã
                txtTenNCC.Text = _ncc.TenNCC;
                txtDienThoai.Text = _ncc.DienThoai;
                cmbTrangThai.Text = _ncc.TrangThai;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text) || string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng điền đủ Mã và Tên Nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_isEdit)
                {
                    _ncc.TenNCC = txtTenNCC.Text.Trim();
                    _ncc.DienThoai = txtDienThoai.Text.Trim();
                    _ncc.TrangThai = cmbTrangThai.Text;

                    _bll.SuaNCC(_ncc);
                    MessageBox.Show("Cập nhật thông tin đối tác thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Tổng mua và Công nợ mặc định = 0 đối với NCC mới
                    var newNcc = new NhaCungCap(txtMaNCC.Text.Trim(), txtTenNCC.Text.Trim(), txtDienThoai.Text.Trim(), 0, 0, cmbTrangThai.Text);
                    _bll.ThemNCC(newNcc);
                    MessageBox.Show("Thêm đối tác mới thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}