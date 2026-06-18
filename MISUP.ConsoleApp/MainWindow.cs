using MISUP.BLL.Services; // Gọi BLL
using MISUP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Terminal.Gui;

namespace MISUP.ConsoleApp
{
    public class MainWindow
    {
        private HangHoaBLL _db = new HangHoaBLL(); // Dùng BLL thay cho DatabaseHelper
        private TaiKhoan _user;
        private TableView _tableView;

        public MainWindow(TaiKhoan user) { _user = user; }

        public void Run()
        {
            var top = new Toplevel() { ColorScheme = ThemeManager.HackerScheme };

            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_Hệ thống", new MenuItem [] {
                    new MenuItem ("_Đăng xuất đổi tài khoản", "", () => { Application.RequestStop(); })
                }),
                new MenuBarItem ("_Hành động", new MenuItem [] {
                    new MenuItem ("_Thêm hàng mới", "", () => ShowProductDialog(null), null, null, Key.CtrlMask | Key.I),
                    new MenuItem ("_Sửa hàng", "", () => EditSelectedProduct(), null, null, Key.CtrlMask | Key.E),
                    new MenuItem ("_Xóa hàng", "", () => DeleteSelectedProduct(), null, null, Key.Delete)
                }),
                new MenuBarItem ("_Báo cáo", new MenuItem [] {
                    new MenuItem ("_Thống kê tổng tài sản", "", () => {
                        MessageBox.Query("Thống Kê", $"Tổng giá trị kho: {_db.TinhTongGiaTriKho():N0} VNĐ", "OK");
                    })
                })
            });

            var win = new Window($"BẢNG ĐIỀU KHIỂN - Xin chào: {_user.HoTen} ({_user.Quyen})") { X = 0, Y = 1, Width = Dim.Fill(), Height = Dim.Fill() };
            var leftPane = new FrameView("Chức năng") { X = 0, Y = 0, Width = Dim.Percent(20), Height = Dim.Fill() };
            var rightPane = new FrameView("Dữ Liệu Kho Hàng") { X = Pos.Right(leftPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            var menuList = new ListView(new ustring[] { "1. Xem tất cả", "2. Thêm mới", "3. Sửa SP", "4. Xóa SP", "5. Tìm kiếm", "0. Đăng xuất" }) { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            menuList.OpenSelectedItem += (e) => {
                if (e.Item == 0) RefreshData();
                if (e.Item == 1) ShowProductDialog(null);
                if (e.Item == 2) EditSelectedProduct();
                if (e.Item == 3) DeleteSelectedProduct();
                if (e.Item == 4) SearchProduct();
                if (e.Item == 5)
                {
                    if (MessageBox.Query("Xác nhận", "Bạn muốn đăng xuất?", "Đồng ý", "Hủy") == 0)
                        Application.RequestStop();
                }
            };

            _tableView = new TableView() { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            leftPane.Add(menuList); rightPane.Add(_tableView); win.Add(leftPane, rightPane); top.Add(menu, win);
            RefreshData(); Application.Run(top);
        }

        private void RefreshData() => LoadDataToTable(_db.LayDanhSach());

        private void SearchProduct()
        {
            var dialog = new Dialog("Tìm kiếm", 50, 10) { ColorScheme = ThemeManager.HackerScheme };
            var txtTuKhoa = new TextField("") { X = 15, Y = 2, Width = 30, ColorScheme = ThemeManager.InputScheme };

            var btnTim = new Button("Tìm") { X = Pos.Center() - 8, Y = 5, IsDefault = true };
            var btnBack = new Button("Quay lại") { X = Pos.Center() + 4, Y = 5 };

            btnTim.Clicked += () => { LoadDataToTable(_db.TimKiem(txtTuKhoa.Text.ToString())); Application.RequestStop(); };
            btnBack.Clicked += () => Application.RequestStop();

            dialog.Add(new Label("Từ khóa:") { X = 2, Y = 2 }, txtTuKhoa, btnTim, btnBack);
            Application.Run(dialog);
        }

        private void DeleteSelectedProduct()
        {
            if (_tableView.SelectedRow < 0) return;
            string ma = _tableView.Table.Rows[_tableView.SelectedRow][0].ToString();
            if (MessageBox.Query("Xác nhận", $"Xóa SP {ma}?", "Có", "Quay lại") == 0)
            {
                try { _db.XoaHang(ma); RefreshData(); } catch (Exception ex) { MessageBox.ErrorQuery("Lỗi", ex.Message, "OK"); }
            }
        }

        private void EditSelectedProduct()
        {
            if (_tableView.SelectedRow < 0) return;
            string ma = _tableView.Table.Rows[_tableView.SelectedRow][0].ToString(), ten = _tableView.Table.Rows[_tableView.SelectedRow][1].ToString();
            string nsx = _tableView.Table.Rows[_tableView.SelectedRow][2].ToString(), loai = _tableView.Table.Rows[_tableView.SelectedRow][5].ToString();
            int sl = Convert.ToInt32(_tableView.Table.Rows[_tableView.SelectedRow][3]);
            decimal gia = Convert.ToDecimal(_tableView.Table.Rows[_tableView.SelectedRow][4].ToString().Replace(",", ""));

            HangHoa sp = loai switch { "Thực Phẩm" => new HangThucPham(ma, ten, nsx, sl, gia), "Điện Tử" => new HangDienTu(ma, ten, nsx, sl, gia), "Mỹ Phẩm" => new HangMyPham(ma, ten, nsx, sl, gia), "GiaDung" => new HangGiaDung(ma, ten, nsx, sl, gia), "Thời Trang" => new HangThoiTrang(ma, ten, nsx, sl, gia), _ => null };
            ShowProductDialog(sp);
        }

        private void ShowProductDialog(HangHoa sp)
        {
            var dialog = new AddProductDialog(_db, sp); Application.Run(dialog);
            if (dialog.IsSaved) RefreshData();
        }

        private void LoadDataToTable(List<HangHoa> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã"); dt.Columns.Add("Tên SP"); dt.Columns.Add("Nhà SX"); dt.Columns.Add("Số Lượng"); dt.Columns.Add("Đơn Giá"); dt.Columns.Add("Loại");
            foreach (var item in list)
            {
                string loai = item switch { HangThucPham _ => "Thực Phẩm", HangDienTu _ => "Điện Tử", HangMyPham _ => "Mỹ Phẩm", HangGiaDung _ => "Gia Dụng", HangThoiTrang _ => "Thời Trang", _ => "Khác" };
                dt.Rows.Add(item.MaHang, item.TenHang, item.NhaSanXuat, item.SoLuongNhap, item.DonGia.ToString("N0"), loai);
            }
            _tableView.Table = dt; _tableView.Update();
        }
    }
}
