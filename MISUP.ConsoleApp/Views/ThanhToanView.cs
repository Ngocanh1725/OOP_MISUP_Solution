using MISUP.BLL.Services;
using MISUP.ConsoleApp.Dialogs;
using System;
using System.Data;
using System.Linq;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Views
{
    public class ThanhToanView : View
    {
        private PhieuChiBLL _db = new PhieuChiBLL();
        private DataTable _dtSource;
        private TableView _table;

        public ThanhToanView()
        {
            var actionBtnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 25, ColorScheme = ThemeManager.InputScheme };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0, ColorScheme = actionBtnScheme };

            var btnThem = new Button("💸 Lập Phiếu Chi") { X = Pos.Right(btnTim) + 2, Y = 0, ColorScheme = actionBtnScheme };
            var btnSua = new Button("✏️ Sửa") { X = Pos.Right(btnThem) + 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnXoa = new Button("🗑️ Xóa") { X = Pos.Right(btnSua) + 1, Y = 0, ColorScheme = actionBtnScheme };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            // Tải dữ liệu thật từ SQL
            LoadData();

            // 1. TÌM KIẾM
            btnTim.Clicked += () => {
                LoadData(txtSearch.Text.ToString());
            };

            // 2. THÊM PHIẾU CHI
            btnThem.Clicked += () => {
                var dialog = new AddPhieuChiDialog();
                Application.Run(dialog);

                if (dialog.IsSaved)
                {
                    try
                    {
                        _db.ThemPhieu(dialog.ResultPhieu);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã lập phiếu chi mới thành công!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 3. SỬA PHIẾU CHI
            btnSua.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 phiếu chi để sửa!", "OK");
                    return;
                }

                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã Phiếu"].ToString();
                var phieu = _db.LayDanhSach().FirstOrDefault(x => x.MaPhieu == ma);
                if (phieu == null) return;

                var dialog = new AddPhieuChiDialog(phieu);
                Application.Run(dialog);

                if (dialog.IsSaved)
                {
                    try
                    {
                        _db.SuaPhieu(dialog.ResultPhieu);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã cập nhật phiếu chi!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 4. XÓA PHIẾU CHI
            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 phiếu chi để xóa!", "OK");
                    return;
                }

                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã Phiếu"].ToString();
                if (MessageBox.Query("Xác nhận", $"Bạn có chắc chắn muốn hủy phiếu chi '{ma}'?", "Có", "Không") == 0)
                {
                    try
                    {
                        _db.XoaPhieu(ma);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã hủy phiếu chi!", "OK");
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
            _dtSource.Columns.Add("Mã Phiếu");
            _dtSource.Columns.Add("Thời Gian");
            _dtSource.Columns.Add("Nhà Cung Cấp");
            _dtSource.Columns.Add("Số Tiền (VNĐ)");
            _dtSource.Columns.Add("Phương Thức");
            _dtSource.Columns.Add("Trạng Thái");

            var list = string.IsNullOrEmpty(keyword) ? _db.LayDanhSach() : _db.TimKiem(keyword);

            foreach (var pc in list)
            {
                _dtSource.Rows.Add(
                    pc.MaPhieu,
                    pc.ThoiGian.ToString("dd/MM/yyyy HH:mm"),
                    pc.TenNCC,
                    pc.SoTien.ToString("N0"),
                    pc.PhuongThuc,
                    pc.TrangThai
                );
            }

            _table.Table = _dtSource;
            _table.Update();
        }
    }
}