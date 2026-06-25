using System;
using System.Windows.Forms;
using MISUP.BLL.Services;

namespace MISUP.WinForms.Forms
{
    public partial class DoiMatKhauForm : Form
    {
        private string _username;
        private TaiKhoanBLL _bll = new TaiKhoanBLL();

        public DoiMatKhauForm(string username)
        {
            _username = username;
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi xuống tầng BLL để thực hiện Logic và kiểm tra ràng buộc
                _bll.DoiMatKhau(_username, txtOld.Text, txtNew.Text, txtConfirm.Text);

                MessageBox.Show("Thay đổi mật khẩu thành công!\nVui lòng ghi nhớ mật khẩu mới cho lần đăng nhập sau.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}