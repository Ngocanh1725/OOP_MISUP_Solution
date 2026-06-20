using System;
using System.Drawing;
using System.Windows.Forms;
using MISUP.Models;

namespace MISUP.WinForms
{
    public partial class MainForm : Form
    {
        private TaiKhoan _user;
        private Color SapoSidebarHover = Color.FromArgb(41, 56, 70);
        private Color SapoSidebarActive = Color.FromArgb(32, 45, 58); // Màu tối hơn khi đang chọn
        private Button _activeButton; // Lưu trữ nút đang được chọn

        public MainForm() { InitializeComponent(); }

        public MainForm(TaiKhoan user)
        {
            _user = user;
            InitializeComponent();

            // --- KHÓA KÍCH THƯỚC WINDOWS BÊN NGOÀI ---
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Khóa viền, không cho kéo thả thu nhỏ/phóng to
            this.MaximizeBox = false; // Tắt nút Phóng to toàn màn hình (Ô vuông trên cùng bên phải)

            btnUser.Text = $"👤 {_user.Quyen} : {_user.HoTen} ▼";

            // GẮN SỰ KIỆN CHO NÚT ADMIN ĐỂ HIỆN MENU
            btnUser.Click += BtnUser_Click;

            // Mặc định nạp màn hình Quản lý Sản phẩm vào khung Content
            SetActiveButton(btnSanPham);
            OpenControl(new ucSanPham());
        }

        // =========================================================
        // TẠO MENU XỔ XUỐNG KHI CLICK VÀO NÚT ADMIN
        // =========================================================
        private void BtnUser_Click(object sender, EventArgs e)
        {
            ContextMenuStrip adminMenu = new ContextMenuStrip();
            adminMenu.Font = new Font("Segoe UI", 10F);

            // Item 1: Thêm tài khoản nhân viên
            ToolStripMenuItem mnuThemTaiKhoan = new ToolStripMenuItem("👥 Quản lý & Thêm Nhân viên");
            mnuThemTaiKhoan.Click += (s, args) => {
                if (_user.Quyen != "Admin")
                {
                    MessageBox.Show("Chỉ Admin mới có quyền thực hiện chức năng này!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                MessageBox.Show("Mở cửa sổ Quản lý Nhân Viên... (Chức năng đang phát triển)", "Quản trị hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Item 2: Phân quyền hệ thống
            ToolStripMenuItem mnuPhanQuyen = new ToolStripMenuItem("🔐 Phân quyền hệ thống");
            mnuPhanQuyen.Click += (s, args) => {
                if (_user.Quyen != "Admin")
                {
                    MessageBox.Show("Chỉ Admin mới có quyền phân quyền!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                MessageBox.Show("Mở cửa sổ Phân Quyền... (Chức năng đang phát triển)", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Item 3: Đổi mật khẩu cá nhân
            ToolStripMenuItem mnuDoiMatKhau = new ToolStripMenuItem("🔑 Đổi mật khẩu cá nhân");
            mnuDoiMatKhau.Click += (s, args) => {
                MessageBox.Show("Mở cửa sổ Đổi Mật Khẩu...", "Bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            adminMenu.Items.Add(mnuThemTaiKhoan);
            adminMenu.Items.Add(mnuPhanQuyen);
            adminMenu.Items.Add(new ToolStripSeparator()); // Đường gạch ngang
            adminMenu.Items.Add(mnuDoiMatKhau);

            // Hiển thị Menu ngay bên dưới nút btnUser
            adminMenu.Show(btnUser, new Point(0, btnUser.Height));
        }

        // TÍNH ĐA HÌNH: Hàm này chấp nhận mọi Control kế thừa từ UserControl
        private void OpenControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }

        // Hàm xử lý đổi màu nút đang chọn (Active State)
        private void SetActiveButton(Button btn)
        {
            // Reset nút cũ
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.Transparent;
                _activeButton.ForeColor = Color.FromArgb(170, 180, 190);
                _activeButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            }

            // Set nút mới
            _activeButton = btn;
            _activeButton.BackColor = SapoSidebarActive;
            _activeButton.ForeColor = Color.White;
            _activeButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }

        // Sự kiện Click dùng chung cho toàn bộ Menu
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            if (clickedBtn == null || clickedBtn == _activeButton) return;

            SetActiveButton(clickedBtn);

            // Điều hướng dựa vào Text của nút
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