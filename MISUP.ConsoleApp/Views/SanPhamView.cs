using MISUP.BLL.Services;
using MISUP.Models;
using System.Data;
using System.Linq;
using Terminal.Gui;

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

            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 20 };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0 };
            var btnSortAsc = new Button("↑ Kho") { X = Pos.Right(btnTim) + 1, Y = 0 };
            var btnSortDesc = new Button("↓ Kho") { X = Pos.Right(btnSortAsc) + 1, Y = 0 };

            var btnThem = new Button("➕ Tạo SP") { X = Pos.Right(btnSortDesc) + 2, Y = 0 };
            var btnSua = new Button("✏️ Sửa") { X = Pos.Right(btnThem) + 1, Y = 0 };
            var btnXoa = new Button("🗑️ Xóa") { X = Pos.Right(btnSua) + 1, Y = 0 };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };
            LoadData();

            btnTim.Clicked += () => { LoadData(txtSearch.Text.ToString()); };

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

            btnThem.Clicked += () => {
                MessageBox.Query("Tạo SP", "Mở hộp thoại tạo Sản phẩm mới...", "OK");
                // Giả lập thêm
                LoadData();
            };

            btnSua.Clicked += () => {
                if (_table.SelectedRow < 0) { MessageBox.ErrorQuery("Lỗi", "Chọn 1 sản phẩm để sửa!", "OK"); return; }
                string ma = _table.Table.Rows[_table.SelectedRow][0].ToString();
                MessageBox.Query("Sửa", $"Mở hộp thoại sửa cho mã: {ma}", "OK");
            };

            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0) { MessageBox.ErrorQuery("Lỗi", "Chọn 1 sản phẩm để xóa!", "OK"); return; }
                string ma = _table.Table.Rows[_table.SelectedRow][0].ToString();
                if (MessageBox.Query("Xác nhận", $"Chắc chắn xóa sản phẩm {ma}?", "Có", "Không") == 0)
                {
                    _db.XoaHang(ma);
                    LoadData();
                }
            };

            Add(txtSearch, btnTim, btnSortAsc, btnSortDesc, btnThem, btnSua, btnXoa, _table);
        }

        private void LoadData(string keyword = "")
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã Hàng"); _dtSource.Columns.Add("Tên SP");
            _dtSource.Columns.Add("ĐVT"); _dtSource.Columns.Add("Tồn Kho", typeof(int)); // Kiểu int để sort
            _dtSource.Columns.Add("Giá Nhập");

            var list = string.IsNullOrEmpty(keyword) ? _db.LayDanhSach() : _db.TimKiem(keyword);
            foreach (var h in list)
            {
                _dtSource.Rows.Add(h.MaHang, h.TenHang, h.DonViTinh, h.SoLuongNhap, h.DonGia.ToString("N0"));
            }
            _table.Table = _dtSource;
            _table.Update();
        }
    }
}