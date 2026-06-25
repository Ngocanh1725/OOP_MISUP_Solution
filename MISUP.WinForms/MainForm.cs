using System;
using System.Drawing;
using System.Windows.Forms;
using MISUP.Models;
using MISUP.WinForms.Forms;

namespace MISUP.WinForms
{
    public partial class MainForm : Form
    {
        private TaiKhoan _user;

        // MÃ MÀU THEO IMAGE 3 (THEME XANH ĐẬM - ROYAL BLUE)
        private Color SapoSidebarHover = Color.FromArgb(58, 100, 204); // Sáng hơn một chút khi hover
        private Color SapoSidebarActive = Color.White; // Nền trắng khi nút được chọn
        private Color SapoSidebarTextActive = Color.FromArgb(37, 84, 199); // Chữ màu Xanh khi nút được chọn
        private Color SapoSidebarTextNormal = Color.White; // Chữ trắng khi bình thường

        private Button _activeButton;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(TaiKhoan user)
        {
            _user = user;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            string userRoleDisplay = _user.Quyen == "Admin" ? "Quản trị viên" : "Nhân viên";
            btnUser.Text = $"{_user.HoTen} ({userRoleDisplay}) ▼";
            btnUser.Click += BtnUser_Click;

            // Bắt đầu vào màn hình Báo Cáo (để test biểu đồ) hoặc Tổng Quan
            SetActiveButton(btnTongQuan);
            OpenControl(new ucTongQuan());
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            ContextMenuStrip adminMenu = new ContextMenuStrip();
            adminMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ToolStripMenuItem mnuThemTaiKhoan = new ToolStripMenuItem("👥 Quản lý nhân viên");
            mnuThemTaiKhoan.BackColor = Color.FromArgb(232, 244, 253);
            mnuThemTaiKhoan.ForeColor = Color.FromArgb(25, 118, 210);
            mnuThemTaiKhoan.Padding = new Padding(10, 5, 10, 5);
            mnuThemTaiKhoan.Click += (s, args) => {
                if (_user.Quyen != "Admin") { MessageBox.Show("Chỉ Admin mới có quyền!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop); return; }
                new QuanLyNhanVienForm().ShowDialog();
            };

            ToolStripMenuItem mnuPhanQuyen = new ToolStripMenuItem("🔐 Phân quyền hệ thống");
            mnuPhanQuyen.BackColor = Color.FromArgb(255, 244, 229);
            mnuPhanQuyen.ForeColor = Color.FromArgb(230, 126, 34);
            mnuPhanQuyen.Padding = new Padding(10, 5, 10, 5);
            mnuPhanQuyen.Click += (s, args) => {
                if (_user.Quyen != "Admin") { MessageBox.Show("Chỉ Admin mới có quyền!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop); return; }
                new PhanQuyenForm().ShowDialog();
            };

            ToolStripMenuItem mnuDoiMatKhau = new ToolStripMenuItem("🔑 Đổi mật khẩu");
            mnuDoiMatKhau.BackColor = Color.FromArgb(237, 247, 237);
            mnuDoiMatKhau.ForeColor = Color.FromArgb(46, 125, 50);
            mnuDoiMatKhau.Padding = new Padding(10, 5, 10, 5);
            mnuDoiMatKhau.Click += (s, args) => {
                new DoiMatKhauForm(_user.TenDangNhap).ShowDialog();
            };

            adminMenu.Items.Add(mnuThemTaiKhoan);
            adminMenu.Items.Add(new ToolStripSeparator());
            adminMenu.Items.Add(mnuPhanQuyen);
            adminMenu.Items.Add(new ToolStripSeparator());
            adminMenu.Items.Add(mnuDoiMatKhau);

            adminMenu.Show(btnUser, new Point(0, btnUser.Height));
        }

        private void OpenControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }

        private void SetActiveButton(Button btn)
        {
            // Trả lại trạng thái cho nút cũ
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.Transparent;
                _activeButton.ForeColor = SapoSidebarTextNormal;
                _activeButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            }

            // Highlight nút mới (Nền trắng, chữ Xanh ngọc) giống Image 2
            _activeButton = btn;
            _activeButton.BackColor = SapoSidebarActive;
            _activeButton.ForeColor = SapoSidebarTextActive;
            _activeButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            if (clickedBtn == null || clickedBtn == _activeButton) return;

            SetActiveButton(clickedBtn);

            if (clickedBtn == btnTongQuan) OpenControl(new ucTongQuan());
            else if (clickedBtn == btnDatHang) OpenControl(new ucDatHang());
            else if (clickedBtn == btnNhapHang) OpenControl(new ucNhapHang());
            else if (clickedBtn == btnSanPham) OpenControl(new ucSanPham());
            else if (clickedBtn == btnNhaCungCap) OpenControl(new ucNhaCungCap());
            else if (clickedBtn == btnThanhToanNCC) OpenControl(new ucThanhToanNCC());
            else if (clickedBtn == btnKiemKeKho) OpenControl(new ucKiemKeKho());
            else if (clickedBtn == btnBaoCao) OpenControl(new ucBaoCao());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}