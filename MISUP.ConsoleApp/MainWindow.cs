using MISUP.BLL.Services;
using MISUP.ConsoleApp.Views;
using MISUP.Models;
using NStack;
using System;
using System.Collections.Generic;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute; // Định danh rõ Attribute để dùng màu sắc

namespace MISUP.ConsoleApp
{
    public class MainWindow
    {
        // ==========================================
        // 1. TẦNG DỮ LIỆU & BỘ NHỚ ĐỆM (LOGIC DATA)
        // ==========================================
        private HangHoaBLL _db = new HangHoaBLL();
        private TaiKhoan _user;

        // Caching: Lưu lại các màn hình đã mở để không phải tạo lại (Chống giật lag)
        private Dictionary<int, View> _viewCache;
        private string[] _menuItems = new string[] {
            " 1. Tổng Quan",
            " 2. Sản Phẩm",
            " 3. Nhà Cung Cấp",
            " 4. Thanh Toán NCC",
            " 5. Kiểm Kê Kho",
            " 6. Báo Cáo"
        };

        // ==========================================
        // 2. CÁC THÀNH PHẦN GIAO DIỆN (UI COMPONENTS)
        // ==========================================
        private Toplevel _top;
        private Window _win;
        private FrameView _rightPane;
        private ListView _menuList;
        private MenuBar _menuBar;
        private StatusBar _statusBar;

        public MainWindow(TaiKhoan user)
        {
            _user = user;
            _viewCache = new Dictionary<int, View>();
        }

        public void Run()
        {
            // Khởi tạo các thành phần giao diện
            InitializeUI();

            // Gắn các sự kiện (Events)
            BindEvents();

            // Mặc định nạp màn hình Tổng quan (Index = 0) khi vừa đăng nhập thành công
            SwitchView(0);

            // Chạy vòng lặp ứng dụng Terminal
            Application.Run(_top);
        }

        // ==========================================
        // PHẦN 1: KHỞI TẠO GIAO DIỆN (UI SETUP)
        // ==========================================
        private void InitializeUI()
        {
            _top = new Toplevel() { ColorScheme = ThemeManager.HackerScheme };

            // 1. Menu Bar (Thanh menu trên cùng) - GẮN QUYỀN ADMIN & CHỨC NĂNG
            _menuBar = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_Hệ Thống", new MenuItem [] {
                    new MenuItem ("_Thông Tin", "", () => MessageBox.Query("Thông Tin", "MISUP ERP Console v2.0\nHệ thống Quản lý Vận hành Siêu thị", "OK")),
                    new MenuItem ("_Đăng Xuất", "Quay lại màn hình đăng nhập", () => Application.RequestStop())
                }),
                new MenuBarItem ($"| {_user.HoTen} ({_user.Quyen})", new MenuItem [] {
                    new MenuItem ("_Quản lý Nhân Viên & Phân Quyền", "", () => {
                        // Chỉ cho phép Admin truy cập
                        if (_user.Quyen != "Admin")
                        {
                            MessageBox.ErrorQuery("Từ chối", "Chỉ Admin mới có quyền truy cập khu vực này!", "OK");
                        }
                        else
                        {
                            Application.Run(new MISUP.ConsoleApp.Dialogs.QuanLyNhanVienDialog());
                        }
                    }),
                    new MenuItem ("_Đổi mật khẩu", "", () => {
                        // Bất kỳ ai cũng có thể đổi mật khẩu của mình
                        Application.Run(new MISUP.ConsoleApp.Dialogs.DoiMatKhauDialog(_user.TenDangNhap));
                    })
                })
            });

            // 2. Cửa sổ chính (Khung viền to nhất)
            _win = new Window("MISUP CONSOLE TERMINAL")
            {
                X = 0,
                Y = 1, // Dịch xuống 1 dòng để nhường chỗ cho MenuBar
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1, // Bớt 1 dòng cuối cho StatusBar
                ColorScheme = ThemeManager.HackerScheme
            };

            // 3. Khung bên trái: Danh mục chức năng (Sidebar)
            var leftPane = new FrameView("Danh Mục Chức Năng")
            {
                X = 0,
                Y = 0,
                Width = 25,
                Height = Dim.Fill()
            };

            // TẠO COLOR SCHEME RIÊNG CHO MENU ĐỂ HIGHLIGHT KHI TRỎ VÀO
            var sidebarScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.White, Color.Black),         // Bình thường: Chữ trắng nền đen
                Focus = new Attribute(Color.Black, Color.White),          // Khi trỏ vào/Chọn: Chữ đen nền trắng nổi bật
                HotNormal = new Attribute(Color.Cyan, Color.Black),
                HotFocus = new Attribute(Color.Blue, Color.White)
            };

            _menuList = new ListView(_menuItems)
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                AllowsMarking = false, // Tắt chế độ tick chọn nhiều mục
                ColorScheme = sidebarScheme // Áp dụng theme highlight
            };
            leftPane.Add(_menuList);

            // 4. Khung bên phải: Chứa nội dung hiển thị sẽ thay đổi
            _rightPane = new FrameView("Nội Dung")
            {
                X = Pos.Right(leftPane),
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            _win.Add(leftPane, _rightPane);

            // 5. Thanh trạng thái (Status Bar) dưới cùng
            _statusBar = new StatusBar(new StatusItem[] {
                new StatusItem(Key.CtrlMask | Key.Q, "~CTRL-Q~ Thoát", () => Application.RequestStop())
            });

            // Lắp ráp vào lớp cao nhất (Toplevel)
            _top.Add(_menuBar, _win, _statusBar);
        }

        // ==========================================
        // PHẦN 2: XỬ LÝ SỰ KIỆN (EVENTS)
        // ==========================================
        private void BindEvents()
        {
            // Bắt sự kiện khi ấn mũi tên Lên/Xuống hoặc Click chuột vào danh mục
            // Đổi màn hình ngay lập tức mà không cần nhấn Enter
            _menuList.SelectedItemChanged += (e) => SwitchView(e.Item);
        }

        // ==========================================
        // PHẦN 3: LOGIC CHUYỂN ĐỔI MÀN HÌNH MƯỢT MÀ
        // ==========================================
        private void SwitchView(int menuIndex)
        {
            // 1. Xóa toàn bộ View cũ để tránh lỗi chồng lấn đồ họa (overlapping)
            _rightPane.RemoveAll();

            View targetView = null;

            // 2. Lấy View từ Cache (nếu đã tạo) hoặc khởi tạo mới
            if (_viewCache.ContainsKey(menuIndex))
            {
                targetView = _viewCache[menuIndex];
            }
            else
            {
                targetView = CreateViewFromIndex(menuIndex);
                if (targetView != null)
                {
                    targetView.X = 0;
                    targetView.Y = 0;
                    targetView.Width = Dim.Fill();
                    targetView.Height = Dim.Fill();

                    // Thêm vào Cache để lần sau load tức thì
                    _viewCache.Add(menuIndex, targetView);
                }
            }

            // 3. Thêm View mục tiêu vào khung bên phải
            if (targetView != null)
            {
                _rightPane.Add(targetView);

                // Cắt bỏ các icon Emoji khi đặt làm tiêu đề khung
                string menuTitle = _menuItems[menuIndex].Trim()
                    .Replace(" 🏠 ", "")
                    .Replace(" 🏷️ ", "")
                    .Replace(" 🏢 ", "")
                    .Replace(" 💳 ", "")
                    .Replace(" 📋 ", "")
                    .Replace(" 📊 ", "");

                _rightPane.Title = $"Nội Dung: {menuTitle}";

                // 4. BẮT BUỘC: Ép Terminal vẽ lại (Redraw) khung này ngay lập tức để không bị sót chữ cũ
                _rightPane.SetNeedsDisplay();
            }
        }

        // Factory Logic: Phân tuyến gọi tới màn hình tương ứng dựa vào Index
        private View CreateViewFromIndex(int index)
        {
            switch (index)
            {
                case 0: return new TongQuanView(_db);
                case 1: return new SanPhamView(_db);
                case 2: return new NhaCungCapView();
                case 3: return new ThanhToanView();
                case 4: return new KiemKeView();
                case 5: return new BaoCaoView(_db);
                default:
                    return new Label("Chức năng đang xây dựng...") { X = Pos.Center(), Y = Pos.Center() };
            }
        }
    }
}