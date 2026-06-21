using MISUP.BLL.Services;
using MISUP.ConsoleApp.Views;
using MISUP.Models;
using NStack;
using System;
using Terminal.Gui;

namespace MISUP.ConsoleApp
{
    public class MainWindow
    {
        private HangHoaBLL _db = new HangHoaBLL();
        private TaiKhoan _user;
        private Window _win;
        private FrameView _rightPane;

        public MainWindow(TaiKhoan user)
        {
            _user = user;
        }

        public void Run()
        {
            var top = new Toplevel() { ColorScheme = ThemeManager.HackerScheme };

            // ==========================================
            // 1. MENU BAR TRÊN CÙNG (Gắn quyền Admin)
            // ==========================================
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_Hệ Thống", new MenuItem [] {
                    new MenuItem ("_Thông Tin", "", () => MessageBox.Query("Thông Tin", "MISUP ERP Console v2.0", "OK")),
                    new MenuItem ("_Đăng Xuất", "Đăng xuất khỏi phiên", () => Application.RequestStop())
                }),
                new MenuBarItem ($"_👤 {_user.HoTen} ({_user.Quyen})", new MenuItem [] {
                    new MenuItem ("_Quản lý Nhân Viên", "", () => KiemTraQuyenAdmin("Mở màn hình Quản lý Nhân viên")),
                    new MenuItem ("_Phân quyền hệ thống", "", () => KiemTraQuyenAdmin("Mở màn hình Phân Quyền")),
                    new MenuItem ("_Đổi mật khẩu", "", () => MessageBox.Query("Bảo mật", "Mở màn hình Đổi mật khẩu", "OK"))
                })
            });

            // ==========================================
            // 2. KHUNG LÀM VIỆC CHÍNH (Split Screen)
            // ==========================================
            _win = new Window("MISUP CONSOLE TERMINAL")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1,
                ColorScheme = ThemeManager.HackerScheme
            };

            // CỘT TRÁI: MENU ĐIỀU HƯỚNG
            var leftPane = new FrameView("Danh Mục Chức Năng") { X = 0, Y = 0, Width = 25, Height = Dim.Fill() };
            var menuList = new ListView(new ustring[] {
                " 🏠 Tổng Quan",
                " 🏷️ Sản Phẩm",
                " 🏢 Nhà Cung Cấp",
                " 💳 Thanh Toán NCC",
                " 📋 Kiểm Kê Kho",
                " 📊 Báo Cáo"
            })
            { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            // CỘT PHẢI: NỘI DUNG THAY ĐỔI
            _rightPane = new FrameView("Nội Dung") { X = Pos.Right(leftPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            leftPane.Add(menuList);
            _win.Add(leftPane, _rightPane);

            // Xử lý sự kiện click Menu bên trái
            menuList.OpenSelectedItem += (e) => HandleMenuSelection(e.Item);

            // ==========================================
            // 3. THANH TRẠNG THÁI
            // ==========================================
            var statusBar = new StatusBar(new StatusItem[] {
                new StatusItem(Key.CtrlMask | Key.Q, "~CTRL-Q~ Thoát", () => Application.RequestStop())
            });

            top.Add(menu, _win, statusBar);

            // Mặc định nạp màn hình Tổng quan
            LoadView(new TongQuanView(_db));

            Application.Run(top);
        }

        private void KiemTraQuyenAdmin(string thongBao)
        {
            if (_user.Quyen != "Admin")
            {
                MessageBox.ErrorQuery("Từ chối", "Chỉ Admin mới có quyền thực hiện chức năng này!", "OK");
            }
            else
            {
                MessageBox.Query("Admin", thongBao, "OK");
            }
        }

        // TÍNH ĐA HÌNH CHO UI: Xóa ruột cũ, đắp ruột mới vào Cột Phải
        private void LoadView(View v)
        {
            _rightPane.RemoveAll();
            v.Width = Dim.Fill();
            v.Height = Dim.Fill();
            _rightPane.Add(v);
        }

        private void HandleMenuSelection(int index)
        {
            switch (index)
            {
                case 0: LoadView(new TongQuanView(_db)); break;
                case 1: LoadView(new SanPhamView(_db)); break;
                case 2: LoadView(new NhaCungCapView()); break;
                case 3: LoadView(new ThanhToanView()); break;
                case 4: LoadView(new KiemKeView()); break;
                case 5: LoadView(new BaoCaoView(_db)); break;
            }
        }
    }
}