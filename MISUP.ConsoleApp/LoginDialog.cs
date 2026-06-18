using MISUP.BLL.Services; // Gọi BLL
using MISUP.Models;
using System;
using Terminal.Gui;

namespace MISUP.ConsoleApp
{
    public class LoginDialog : Dialog
    {
        private AuthBLL _authBLL = new AuthBLL();
        public TaiKhoan AuthenticatedUser { get; private set; } = null;

        public LoginDialog() : base("HỆ THỐNG QUẢN LÝ KHO SIÊU THỊ MISUP - ĐĂNG NHẬP", 65, 12)
        {
            this.ColorScheme = ThemeManager.HackerScheme;

            var txtUser = new TextField("") { X = 18, Y = 2, Width = 40, ColorScheme = ThemeManager.InputScheme };
            var txtPass = new TextField("") { X = 18, Y = 4, Width = 40, Secret = true, ColorScheme = ThemeManager.InputScheme };

            var btnLogin = new Button("• Đăng nhập •") { X = 10, Y = 7, IsDefault = true };
            var btnRegister = new Button("Đăng ký") { X = Pos.Right(btnLogin) + 3, Y = 7 };
            var btnExit = new Button("Thoát") { X = Pos.Right(btnRegister) + 3, Y = 7 };

            btnLogin.Clicked += () => ProcessLogin(txtUser.Text.ToString(), txtPass.Text.ToString());
            btnRegister.Clicked += () => MessageBox.Query("Thông báo", "Vui lòng liên hệ Admin!", "OK");
            btnExit.Clicked += () => Application.RequestStop();

            this.Add(new Label("Tài khoản:") { X = 5, Y = 2 }, txtUser, new Label("Mật khẩu:") { X = 5, Y = 4 }, txtPass, btnLogin, btnRegister, btnExit);
        }

        private void ProcessLogin(string username, string password)
        {
            try
            {
                var user = _authBLL.Login(username, password);
                if (user != null) { AuthenticatedUser = user; Application.RequestStop(); }
                else MessageBox.ErrorQuery("Lỗi", "Tài khoản hoặc mật khẩu sai!", "OK");
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Lỗi nghiệp vụ", ex.Message, "OK");
            }
        }
    }
}
