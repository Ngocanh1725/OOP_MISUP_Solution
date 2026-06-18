using System;
using System.Windows.Forms;
using MISUP.Models;
using MISUP.BLL.Services; // Tầng BLL

namespace MISUP.WinForms
{
    public partial class LoginForm : Form
    {
        private AuthBLL _authBLL = new AuthBLL();

        public LoginForm() { InitializeComponent(); }
        private void chkShowPass_CheckedChanged(object sender, EventArgs e) { txtPass.UseSystemPasswordChar = !chkShowPass.Checked; }
        private void btnExit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi đích danh MISUP.Models.TaiKhoan
                MISUP.Models.TaiKhoan user = _authBLL.Login(txtUser.Text, txtPass.Text);

                if (user != null)
                {
                    this.Hide();
                    MainForm main = new MainForm(user);
                    main.Closed += (s, args) => this.Close();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}