using System;
using System.Runtime.InteropServices; // Để sử dụng API di chuyển form
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;

namespace MISUP.WinForms
{
    public partial class LoginForm : Form
    {
        private AuthBLL _authBLL = new AuthBLL();

        public LoginForm()
        {
            InitializeComponent();
        }

        // Xử lý Ẩn/Hiện mật khẩu
        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !chkShowPass.Checked;
        }

        // Thoát ứng dụng
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Xử lý nút Đăng Nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }

            try
            {
                // Gọi tầng BLL kiểm tra đăng nhập
                TaiKhoan user = _authBLL.Login(txtUser.Text.Trim(), txtPass.Text.Trim());

                if (user != null)
                {
                    this.Hide(); // Ẩn form đăng nhập đi

                    // Khởi tạo MainForm và truyền thông tin User vào
                    MainForm main = new MainForm(user);

                    // Lắng nghe sự kiện Form đóng. Khi MainForm bị đóng (hoặc người dùng chọn Đăng xuất)
                    // thì Form Đăng nhập sẽ tự động đóng theo (hoặc hiện lại nếu là Đăng xuất)
                    main.Closed += (s, args) => this.Close();

                    main.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc hệ thống: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- ĐOẠN CODE GIÚP DI CHUYỂN FORM KHI KHÔNG CÓ VIỀN (BORDERLESS) ---
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            // Nếu click chuột trái, giải phóng capture chuột và gửi thông báo hệ thống là "đang kéo thanh tiêu đề"
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}