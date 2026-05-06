using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MISUP.Models;

namespace MISUP.WinForms
{
    public partial class LoginForm : Form
    {
        DatabaseHelper db = new DatabaseHelper();
        public LoginForm() { InitializeComponent(); }
        private void chkShowPass_CheckedChanged(object sender, EventArgs e) { txtPass.UseSystemPasswordChar = !chkShowPass.Checked; }
        private void btnExit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            TaiKhoan user = db.KiemTraDangNhap(txtUser.Text, txtPass.Text);
            if (user != null)
            {
                this.Hide();
                MainForm main = new MainForm(user);
                main.Closed += (s, args) => this.Close();
                main.Show();
            }
            else MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
