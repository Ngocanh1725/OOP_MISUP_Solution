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

            // Mặc định nạp màn hình Quản lý Sản phẩm vào khung Content
            SetActiveButton(btnSanPham);
            OpenControl(new ucSanPham());
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
            else if (clickedBtn == btnDatHang) OpenControl(new ucDatHang()); // Bạn cần tạo ucDatHang
            else if (clickedBtn == btnNhapHang) OpenControl(new ucNhapHang()); // Bạn cần tạo ucNhapHang
            else if (clickedBtn == btnSanPham) OpenControl(new ucSanPham());
            else if (clickedBtn == btnNhaCungCap) OpenControl(new ucNhaCungCap()); // Bạn cần tạo ucNhaCungCap
            else if (clickedBtn == btnThanhToanNCC) OpenControl(new ucThanhToanNCC()); // Bạn cần tạo ucThanhToanNCC
            else if (clickedBtn == btnKiemKeKho) OpenControl(new ucKiemKeKho()); // Bạn cần tạo ucKiemKeKho
            else if (clickedBtn == btnBaoCao) OpenControl(new ucBaoCao()); // Bạn cần tạo ucBaoCao
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