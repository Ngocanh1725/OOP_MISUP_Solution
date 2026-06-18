using System;
using System.Drawing;
using System.Windows.Forms;
using MISUP.Models.Entities;

namespace MISUP.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm() { InitializeComponent(); }

        public MainForm(TaiKhoan user)
        {
            InitializeComponent();
            btnTaiKhoan.Text = "  👥 Xin chào: " + user.HoTen;
            AddHoverEffect(btnSanPham); AddHoverEffect(btnTaiKhoan); AddHoverEffect(btnLogout);
            OpenControl(new ucSanPham(), "QUẢN LÝ SẢN PHẨM");
        }

        private void AddHoverEffect(Button btn)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(255, 180, 195);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.Transparent;
        }

        private void OpenControl(UserControl uc, string title)
        {
            pnlContent.Controls.Clear(); uc.Dock = DockStyle.Fill; pnlContent.Controls.Add(uc); lblTitle.Text = title;
        }

        private void btnSanPham_Click(object sender, EventArgs e) => OpenControl(new ucSanPham(), "QUẢN LÝ SẢN PHẨM");

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn đăng xuất để đổi tài khoản khác?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Restart();
        }
    }
}