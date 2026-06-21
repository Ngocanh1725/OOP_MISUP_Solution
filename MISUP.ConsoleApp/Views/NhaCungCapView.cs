using MISUP.BLL.Services;
using MISUP.ConsoleApp.Dialogs;
using System;
using System.Data;
using System.Linq;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Views
{
    public class NhaCungCapView : View
    {
        private NhaCungCapBLL _db = new NhaCungCapBLL();
        private DataTable _dtSource;
        private TableView _table;

        public NhaCungCapView()
        {
            // Hiệu ứng màu cho các nút thao tác
            var actionBtnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 25, ColorScheme = ThemeManager.InputScheme };
            var btnTim = new Button("Tìm kiếm") { X = Pos.Right(txtSearch) + 1, Y = 0, ColorScheme = actionBtnScheme };

            var btnThem = new Button("➕ Thêm Đối tác") { X = Pos.Right(btnTim) + 2, Y = 0, ColorScheme = actionBtnScheme };
            var btnSua = new Button("✏️ Sửa") { X = Pos.Right(btnThem) + 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnXoa = new Button("🗑️ Xóa") { X = Pos.Right(btnSua) + 1, Y = 0, ColorScheme = actionBtnScheme };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            // Gọi Data từ Database thực tế thay vì MockData
            LoadData();

            // 1. SỰ KIỆN TÌM KIẾM
            btnTim.Clicked += () => {
                LoadData(txtSearch.Text.ToString());
            };

            // 2. SỰ KIỆN THÊM MỚI
            btnThem.Clicked += () => {
                var dialog = new AddSupplierDialog(null);
                Application.Run(dialog);

                if (dialog.IsSaved)
                {
                    try
                    {
                        _db.ThemNCC(dialog.ResultNCC);
                        LoadData(txtSearch.Text.ToString()); // Tải lại bảng sau khi lưu
                        MessageBox.Query("Thành công", "Đã thêm nhà cung cấp mới vào CSDL!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 3. SỰ KIỆN SỬA
            btnSua.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 nhà cung cấp để sửa!", "OK");
                    return;
                }

                // Lấy mã NCC ở dòng được chọn
                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã NCC"].ToString();
                var ncc = _db.LayDanhSach().FirstOrDefault(x => x.MaNCC == ma);
                if (ncc == null) return;

                var dialog = new AddSupplierDialog(ncc);
                Application.Run(dialog);

                if (dialog.IsSaved)
                {
                    try
                    {
                        _db.SuaNCC(dialog.ResultNCC);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Cập nhật thông tin thành công!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 4. SỰ KIỆN XÓA
            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 nhà cung cấp để xóa!", "OK");
                    return;
                }

                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã NCC"].ToString();
                string ten = _dtSource.DefaultView[_table.SelectedRow]["Tên Công Ty"].ToString();

                if (MessageBox.Query("Xác nhận", $"Bạn có chắc chắn muốn xóa đối tác '{ten}'?", "Có", "Không") == 0)
                {
                    try
                    {
                        _db.XoaNCC(ma);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã xóa nhà cung cấp!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            Add(txtSearch, btnTim, btnThem, btnSua, btnXoa, _table);
        }

        private void LoadData(string keyword = "")
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã NCC");
            _dtSource.Columns.Add("Tên Công Ty");
            _dtSource.Columns.Add("Điện Thoại");
            _dtSource.Columns.Add("Trạng Thái");

            // Tự động truy vấn từ DB thông qua BLL
            var list = string.IsNullOrEmpty(keyword) ? _db.LayDanhSach() : _db.TimKiem(keyword);

            foreach (var ncc in list)
            {
                _dtSource.Rows.Add(ncc.MaNCC, ncc.TenNCC, ncc.DienThoai, ncc.TrangThai);
            }

            _table.Table = _dtSource;
            _table.Update();
        }
    }
}