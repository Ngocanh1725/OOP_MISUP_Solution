using MISUP.BLL.Services;
using MISUP.Models;
using System;
using System.Data;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class QuanLyNhanVienDialog : Dialog
    {
        private TaiKhoanBLL _bll = new TaiKhoanBLL();
        private TableView _table;
        private DataTable _dt;

        public QuanLyNhanVienDialog() : base("Quản Lý Phân Quyền & Nhân Viên", 75, 20)
        {
            ColorScheme = ThemeManager.HackerScheme;
            var btnScheme = new ColorScheme() { Normal = new Attribute(Color.Cyan, Color.Black), Focus = new Attribute(Color.Black, Color.Cyan) };

            var txtUser = new TextField("") { X = 15, Y = 1, Width = 15, ColorScheme = ThemeManager.InputScheme };
            var txtTen = new TextField("") { X = 40, Y = 1, Width = 15, ColorScheme = ThemeManager.InputScheme };
            var cmbQuyen = new ComboBox() { X = 15, Y = 3, Width = 15, Height = 3 };
            cmbQuyen.SetSource(new string[] { "Nhân viên Kho", "Kế toán", "Admin" });
            cmbQuyen.SelectedItem = 0;

            var btnAdd = new Button("Thêm NV") { X = 40, Y = 3, ColorScheme = btnScheme };
            var btnDel = new Button("Xóa NV") { X = Pos.Right(btnAdd) + 2, Y = 3, ColorScheme = btnScheme };

            _table = new TableView() { X = 1, Y = 6, Width = Dim.Fill() - 1, Height = Dim.Fill() - 3, FullRowSelect = true };
            LoadData();

            btnAdd.Clicked += () => {
                try
                {
                    string q = cmbQuyen.SelectedItem == 2 ? "Admin" : (cmbQuyen.SelectedItem == 1 ? "Kế toán" : "Nhân viên Kho");
                    _bll.ThemTaiKhoan(new TaiKhoan(txtUser.Text.ToString(), "123456", txtTen.Text.ToString(), q));
                    MessageBox.Query("Thành công", "Đã thêm nhân viên! Mật khẩu mặc định là: 123456", "OK");
                    LoadData();
                }
                catch (Exception ex) { MessageBox.ErrorQuery("Lỗi", ex.Message, "OK"); }
            };

            btnDel.Clicked += () => {
                if (_table.SelectedRow < 0) return;
                string u = _dt.DefaultView[_table.SelectedRow]["Tên Đăng Nhập"].ToString();
                if (MessageBox.Query("Xác nhận", $"Xóa tài khoản {u}?", "Có", "Không") == 0)
                {
                    try { _bll.XoaTaiKhoan(u); LoadData(); } catch (Exception ex) { MessageBox.ErrorQuery("Lỗi", ex.Message, "OK"); }
                }
            };

            var btnClose = new Button("Đóng") { X = Pos.Center(), Y = Pos.Bottom(_table) + 1, ColorScheme = btnScheme };
            btnClose.Clicked += () => Application.RequestStop();

            Add(new Label("Tên Đăng nhập:") { X = 1, Y = 1 }, txtUser, new Label("Họ Tên:") { X = 32, Y = 1 }, txtTen,
                new Label("Quyền (Vai trò):") { X = 1, Y = 3 }, cmbQuyen, btnAdd, btnDel, _table, btnClose);
        }

        private void LoadData()
        {
            _dt = new DataTable();
            _dt.Columns.Add("Tên Đăng Nhập"); _dt.Columns.Add("Họ Tên"); _dt.Columns.Add("Quyền Hạn");
            foreach (var tk in _bll.LayDanhSach()) _dt.Rows.Add(tk.TenDangNhap, tk.HoTen, tk.Quyen);
            _table.Table = _dt;
            _table.Update();
        }
    }
}