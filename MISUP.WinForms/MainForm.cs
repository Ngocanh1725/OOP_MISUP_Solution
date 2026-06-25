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
        private Color SapoSidebarHover = Color.FromArgb(41, 56, 70);
        private Color SapoSidebarActive = Color.FromArgb(32, 45, 58); // Màu tối hơn khi đang chọn
        private Button _activeButton; // Lưu trữ nút đang được chọn

        // Constructor mặc định (Bắt buộc cho Designer)
        public MainForm()
        {
            InitializeComponent();
        }

        // Constructor có truyền tải khoản (Khi đăng nhập thành công)
        public MainForm(TaiKhoan user)
        {
            _user = user;
            InitializeComponent();

            // Khóa kích thước Windows, không cho kéo thả thu nhỏ/phóng to
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Hiển thị thông tin User đăng nhập
            string userRoleDisplay = _user.Quyen == "Admin" ? "Quản trị viên" : "Nhân viên";
            btnUser.Text = $"👤 {userRoleDisplay}: {_user.HoTen}";

            // Gắn sự kiện cho nút Admin để hiện Menu
            btnUser.Click += BtnUser_Click;

            // --- BẮT ĐẦU: LOGIC PHÂN QUYỀN TRUY CẬP (RBAC) ---
            if (_user.Quyen != "Admin")
            {
                // Quyen chứa danh sách các module được phép, ví dụ: "TongQuan,SanPham,NhapHang"
                string q = _user.Quyen ?? "";
                btnTongQuan.Visible = q.Contains("TongQuan");
                btnDatHang.Visible = q.Contains("DatHang");
                btnNhapHang.Visible = q.Contains("NhapHang");
                btnSanPham.Visible = q.Contains("SanPham");
                btnNhaCungCap.Visible = q.Contains("NhaCungCap");
                btnThanhToanNCC.Visible = q.Contains("ThanhToan");
                btnKiemKeKho.Visible = q.Contains("KiemKe");
                btnBaoCao.Visible = q.Contains("BaoCao");
            }
            // --- KẾT THÚC: LOGIC PHÂN QUYỀN ---

            // Tự động mở Tab đầu tiên mà User được phép thấy
            Button[] allMenuButtons = { btnTongQuan, btnDatHang, btnNhapHang, btnSanPham, btnNhaCungCap, btnThanhToanNCC, btnKiemKeKho, btnBaoCao };
            foreach (var btn in allMenuButtons)
            {
                if (btn.Visible)
                {
                    MenuButton_Click(btn, EventArgs.Empty);
                    break;
                }
            }
        }

        // =========================================================
        // TẠO MENU XỔ XUỐNG KHI CLICK VÀO NÚT QUẢN TRỊ VIÊN
        // =========================================================
        private void BtnUser_Click(object sender, EventArgs e)
        {
            ContextMenuStrip adminMenu = new ContextMenuStrip();
            adminMenu.Font = new Font("Segoe UI", 11F, FontStyle.Bold); // Chữ to và đậm hơn chút

            // Item 1: Quản lý nhân viên
            ToolStripMenuItem mnuThemTaiKhoan = new ToolStripMenuItem("👥 Quản lý nhân viên");
            mnuThemTaiKhoan.BackColor = Color.FromArgb(232, 244, 253);   // Nền xanh nhạt
            mnuThemTaiKhoan.ForeColor = Color.FromArgb(25, 118, 210);    // Chữ xanh đậm
            mnuThemTaiKhoan.Padding = new Padding(10, 5, 10, 5);
            mnuThemTaiKhoan.Click += (s, args) => {
                if (_user.Quyen != "Admin")
                {
                    MessageBox.Show("Chỉ Admin mới có quyền thực hiện chức năng này!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                new QuanLyNhanVienForm().ShowDialog();
            };

            // Item 2: Phân quyền hệ thống
            ToolStripMenuItem mnuPhanQuyen = new ToolStripMenuItem("🔐 Phân quyền hệ thống");
            mnuPhanQuyen.BackColor = Color.FromArgb(255, 244, 229);      // Nền cam nhạt
            mnuPhanQuyen.ForeColor = Color.FromArgb(230, 126, 34);       // Chữ cam đậm
            mnuPhanQuyen.Padding = new Padding(10, 5, 10, 5);
            mnuPhanQuyen.Click += (s, args) => {
                if (_user.Quyen != "Admin")
                {
                    MessageBox.Show("Chỉ Admin mới có quyền thực hiện chức năng này!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                new PhanQuyenForm().ShowDialog();
            };

            // Item 3: Đổi mật khẩu
            ToolStripMenuItem mnuDoiMatKhau = new ToolStripMenuItem("🔑 Đổi mật khẩu");
            mnuDoiMatKhau.BackColor = Color.FromArgb(237, 247, 237);     // Nền xanh lá nhạt
            mnuDoiMatKhau.ForeColor = Color.FromArgb(46, 125, 50);       // Chữ xanh lá đậm
            mnuDoiMatKhau.Padding = new Padding(10, 5, 10, 5);
            mnuDoiMatKhau.Click += (s, args) => {
                new DoiMatKhauForm(_user.TenDangNhap).ShowDialog();
            };

            adminMenu.Items.Add(mnuThemTaiKhoan);
            adminMenu.Items.Add(new ToolStripSeparator());
            adminMenu.Items.Add(mnuPhanQuyen);
            adminMenu.Items.Add(new ToolStripSeparator());
            adminMenu.Items.Add(mnuDoiMatKhau);

            // Hiển thị Menu ngay bên dưới nút btnUser
            adminMenu.Show(btnUser, new Point(0, btnUser.Height));
        }

        // =========================================================
        // LOGIC ĐIỀU HƯỚNG CÁC TAB
        // =========================================================

        // Mở UserControl tương ứng vào panel Content
        private void OpenControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }

        // Đổi màu nút đang chọn (Active State)
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

        // Sự kiện Click dùng chung cho toàn bộ Menu bên trái
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            if (clickedBtn == null || clickedBtn == _activeButton) return;

            SetActiveButton(clickedBtn);

            // Điều hướng dựa vào nút được click
            if (clickedBtn == btnTongQuan) OpenControl(new ucTongQuan());
            else if (clickedBtn == btnDatHang) OpenControl(new ucDatHang());
            else if (clickedBtn == btnNhapHang) OpenControl(new ucNhapHang());
            else if (clickedBtn == btnSanPham) OpenControl(new ucSanPham());
            else if (clickedBtn == btnNhaCungCap) OpenControl(new ucNhaCungCap());
            else if (clickedBtn == btnThanhToanNCC) OpenControl(new ucThanhToanNCC());
            else if (clickedBtn == btnKiemKeKho) OpenControl(new ucKiemKeKho());
            else if (clickedBtn == btnBaoCao) OpenControl(new ucBaoCao());
        }

        // Sự kiện đăng xuất
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}