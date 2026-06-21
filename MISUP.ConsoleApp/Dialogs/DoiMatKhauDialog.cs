using MISUP.BLL.Services;
using System;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class DoiMatKhauDialog : Dialog
    {
        private TaiKhoanBLL _bll = new TaiKhoanBLL();

        public DoiMatKhauDialog(string username) : base("Đổi Mật Khẩu", 50, 14)
        {
            ColorScheme = ThemeManager.HackerScheme;

            var txtOldPass = new TextField("") { X = 20, Y = 2, Width = 25, Secret = true, ColorScheme = ThemeManager.InputScheme };
            var txtNewPass = new TextField("") { X = 20, Y = 4, Width = 25, Secret = true, ColorScheme = ThemeManager.InputScheme };
            var txtConfirm = new TextField("") { X = 20, Y = 6, Width = 25, Secret = true, ColorScheme = ThemeManager.InputScheme };

            var btnScheme = new ColorScheme() { Normal = new Attribute(Color.Cyan, Color.Black), Focus = new Attribute(Color.Black, Color.Cyan) };

            var btnSave = new Button("Lưu Mật Khẩu") { X = Pos.Center() - 10, Y = 9, IsDefault = true, ColorScheme = btnScheme };
            var btnCancel = new Button("Hủy") { X = Pos.Center() + 6, Y = 9, ColorScheme = btnScheme };

            btnCancel.Clicked += () => Application.RequestStop();
            btnSave.Clicked += () => {
                try
                {
                    _bll.DoiMatKhau(username, txtOldPass.Text.ToString(), txtNewPass.Text.ToString(), txtConfirm.Text.ToString());
                    MessageBox.Query("Thành công", "Đổi mật khẩu thành công! Vui lòng nhớ mật khẩu mới.", "OK");
                    Application.RequestStop();
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Lỗi", ex.Message, "OK");
                }
            };

            Add(new Label("Mật khẩu cũ:") { X = 2, Y = 2 }, txtOldPass,
                new Label("Mật khẩu mới:") { X = 2, Y = 4 }, txtNewPass,
                new Label("Xác nhận MK:") { X = 2, Y = 6 }, txtConfirm,
                btnSave, btnCancel);
        }
    }
}