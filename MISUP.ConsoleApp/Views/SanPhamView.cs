using MISUP.BLL.Services;
using MISUP.ConsoleApp.Dialogs; // Gọi AddProductDialog
using MISUP.Models;
using System;
using System.Data;
using System.Linq;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute; // Định danh rõ Attribute của Terminal.Gui

namespace MISUP.ConsoleApp.Views
{
    public class SanPhamView : View
    {
        private HangHoaBLL _db;
        private TableView _table;
        private DataTable _dtSource;

        public SanPhamView(HangHoaBLL db)
        {
            _db = db;

            // ========================================================
            // 1. TẠO HIỆU ỨNG MÀU CHO NÚT BẤM
            // ========================================================
            var actionBtnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),          // Bình thường: Chữ xanh
                Focus = new Attribute(Color.Black, Color.Cyan),           // Trỏ chuột vào: Nền xanh chữ đen
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            // ========================================================
            // 2. KHỞI TẠO CÁC CONTROL
            // ========================================================
            // Dòng 1: Thanh công cụ Tìm kiếm và Lọc
            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 20, ColorScheme = ThemeManager.InputScheme };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0, ColorScheme = actionBtnScheme };

            var lblLoai = new Label("Lọc loại:") { X = Pos.Right(btnTim) + 2, Y = 0 };
            var cmbLoai = new ComboBox() { X = Pos.Right(lblLoai) + 1, Y = 0, Width = 15, Height = 6 };
            cmbLoai.SetSource(new string[] { "Tất cả", "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });
            cmbLoai.SelectedItem = 0; // Mặc định là "Tất cả"

            var btnSortAsc = new Button("↑ Kho") { X = Pos.Right(cmbLoai) + 2, Y = 0, ColorScheme = actionBtnScheme };
            var btnSortDesc = new Button("↓ Kho") { X = Pos.Right(btnSortAsc) + 1, Y = 0, ColorScheme = actionBtnScheme };

            // Dòng 2: Thanh công cụ Thao tác (Thêm, Sửa, Xóa)
            var btnThem = new Button("➕ Tạo SP") { X = 1, Y = 2, ColorScheme = actionBtnScheme };
            var btnSua = new Button("✏️ Sửa") { X = Pos.Right(btnThem) + 2, Y = 2, ColorScheme = actionBtnScheme };
            var btnXoa = new Button("🗑️ Xóa") { X = Pos.Right(btnSua) + 2, Y = 2, ColorScheme = actionBtnScheme };

            // Bảng dữ liệu
            _table = new TableView() { X = 0, Y = 4, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            Add(txtSearch, btnTim, lblLoai, cmbLoai, btnSortAsc, btnSortDesc, btnThem, btnSua, btnXoa, _table);
            LoadData(); // Nạp dữ liệu lần đầu

            // ========================================================
            // 3. GẮN SỰ KIỆN CHO CÁC NÚT (EVENTS)
            // ========================================================

            // Tìm kiếm khi nhấn nút
            btnTim.Clicked += () => { LoadData(txtSearch.Text.ToString(), GetSelectedLoai(cmbLoai.SelectedItem)); };

            // Tự động lọc khi đổi Loại Sản Phẩm trong ComboBox
            cmbLoai.SelectedItemChanged += (e) => { LoadData(txtSearch.Text.ToString(), GetSelectedLoai(cmbLoai.SelectedItem)); };

            // Sắp xếp tăng giảm
            btnSortAsc.Clicked += () => {
                if (_dtSource != null)
                {
                    _dtSource.DefaultView.Sort = "Tồn Kho ASC";
                    _table.Table = _dtSource.DefaultView.ToTable();
                }
            };
            btnSortDesc.Clicked += () => {
                if (_dtSource != null)
                {
                    _dtSource.DefaultView.Sort = "Tồn Kho DESC";
                    _table.Table = _dtSource.DefaultView.ToTable();
                }
            };

            // THÊM SẢN PHẨM: Mở hộp thoại AddProductDialog (để null = thêm mới)
            btnThem.Clicked += () => {
                var dialog = new AddProductDialog(_db, null);
                Application.Run(dialog);

                // Nếu người dùng đã bấm "Lưu" trong Dialog, tải lại bảng
                if (dialog.IsSaved) LoadData(txtSearch.Text.ToString(), GetSelectedLoai(cmbLoai.SelectedItem));
            };

            // SỬA SẢN PHẨM: Lấy mã từ bảng và truyền vào Dialog
            btnSua.Clicked += () => {
                if (_table.SelectedRow < 0) { MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 sản phẩm ở bảng dưới để sửa!", "OK"); return; }

                string ma = _table.Table.Rows[_table.SelectedRow][0].ToString();
                var sp = _db.LayDanhSach().FirstOrDefault(x => x.MaHang == ma); // Truy vấn lấy đối tượng

                if (sp != null)
                {
                    var dialog = new AddProductDialog(_db, sp);
                    Application.Run(dialog);

                    if (dialog.IsSaved) LoadData(txtSearch.Text.ToString(), GetSelectedLoai(cmbLoai.SelectedItem));
                }
            };

            // XÓA SẢN PHẨM: Gọi tầng BLL để xóa
            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0) { MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 sản phẩm để xóa!", "OK"); return; }

                string ma = _table.Table.Rows[_table.SelectedRow][0].ToString();
                string ten = _table.Table.Rows[_table.SelectedRow][1].ToString();

                if (MessageBox.Query("Xác nhận", $"Bạn có chắc chắn xóa vĩnh viễn sản phẩm '{ten}' ({ma})?", "Có", "Không") == 0)
                {
                    try
                    {
                        _db.XoaHang(ma);
                        LoadData(txtSearch.Text.ToString(), GetSelectedLoai(cmbLoai.SelectedItem)); // Load lại Data
                        MessageBox.Query("Thành công", "Đã xóa sản phẩm khỏi hệ thống!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };
        }

        // Hàm ánh xạ từ Index ComboBox sang Code loại trong Database
        private string GetSelectedLoai(int index)
        {
            return index switch
            {
                1 => "ThucPham",
                2 => "DienTu",
                3 => "MyPham",
                4 => "GiaDung",
                5 => "ThoiTrang",
                _ => "" // 0: Tất cả
            };
        }

        // Hàm tải dữ liệu kết hợp Lọc theo Loại và Từ khóa
        private void LoadData(string keyword = "", string loai = "")
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã Hàng");
            _dtSource.Columns.Add("Tên SP");
            _dtSource.Columns.Add("ĐVT");
            _dtSource.Columns.Add("Tồn Kho", typeof(int)); // Ép kiểu Int để Sắp xếp ASC/DESC được chuẩn xác
            _dtSource.Columns.Add("Giá Nhập");

            // 1. Lọc theo Loại (Loại rỗng -> Lấy tất cả)
            var list = string.IsNullOrEmpty(loai) ? _db.LayDanhSach() : _db.LocTheoLoai(loai);

            // 2. Lọc tiếp theo Từ khóa Tìm kiếm
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                list = list.Where(x => x.TenHang.ToLower().Contains(keyword) || x.MaHang.ToLower().Contains(keyword)).ToList();
            }

            // 3. Đổ vào Data Table
            foreach (var h in list)
            {
                _dtSource.Rows.Add(h.MaHang, h.TenHang, h.DonViTinh, h.SoLuongNhap, h.DonGia.ToString("N0"));
            }

            _table.Table = _dtSource;
            _table.Update();
        }
    }
}