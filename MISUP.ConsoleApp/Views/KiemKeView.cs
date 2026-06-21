using MISUP.BLL.Services;
using MISUP.ConsoleApp.Dialogs;
using System;
using System.Data;
using System.Linq;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Views
{
    public class KiemKeView : View
    {
        private PhieuKiemBLL _db = new PhieuKiemBLL();
        private DataTable _dtSource;
        private TableView _table;

        public KiemKeView()
        {
            var actionBtnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 15, ColorScheme = ThemeManager.InputScheme };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0, ColorScheme = actionBtnScheme };

            var btnThem = new Button("📋 Tạo Phiếu") { X = Pos.Right(btnTim) + 2, Y = 0, ColorScheme = actionBtnScheme };
            var btnSua = new Button("✏️ Sửa") { X = Pos.Right(btnThem) + 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnXoa = new Button("🗑️ Xóa") { X = Pos.Right(btnSua) + 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnDuyet = new Button("✓ Duyệt") { X = Pos.Right(btnXoa) + 1, Y = 0, ColorScheme = actionBtnScheme };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            // Tải dữ liệu thật từ DB
            LoadData();

            // 1. TÌM KIẾM
            btnTim.Clicked += () => {
                LoadData(txtSearch.Text.ToString());
            };

            // 2. TẠO PHIẾU
            btnThem.Clicked += () => {
                var dialog = new AddPhieuKiemDialog();
                Application.Run(dialog);

                if (dialog.IsSaved)
                {
                    try
                    {
                        _db.ThemPhieu(dialog.ResultPhieu);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã tạo phiếu kiểm kê mới!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 3. SỬA
            btnSua.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 phiếu kiểm kê để sửa!", "OK");
                    return;
                }

                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã KK"].ToString();
                var phieu = _db.LayDanhSach().FirstOrDefault(x => x.MaKK == ma);
                if (phieu == null) return;

                var dialog = new AddPhieuKiemDialog(phieu);
                Application.Run(dialog);

                if (dialog.IsSaved)
                {
                    try
                    {
                        _db.SuaPhieu(dialog.ResultPhieu);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã cập nhật phiếu kiểm kê!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 4. XÓA
            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 phiếu kiểm kê để xóa!", "OK");
                    return;
                }

                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã KK"].ToString();
                if (MessageBox.Query("Xác nhận", $"Bạn có chắc chắn muốn xóa phiếu kiểm kê '{ma}'?", "Có", "Không") == 0)
                {
                    try
                    {
                        _db.XoaPhieu(ma);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Đã xóa phiếu kiểm kê!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            // 5. DUYỆT CÂN BẰNG
            btnDuyet.Clicked += () => {
                if (_table.SelectedRow < 0)
                {
                    MessageBox.ErrorQuery("Lỗi", "Vui lòng chọn 1 phiếu kiểm kê để duyệt!", "OK");
                    return;
                }

                string ma = _dtSource.DefaultView[_table.SelectedRow]["Mã KK"].ToString();
                string trangThaiHienTai = _dtSource.DefaultView[_table.SelectedRow]["Trạng Thái"].ToString();

                if (trangThaiHienTai == "Đã cân bằng")
                {
                    MessageBox.Query("Thông báo", "Phiếu này đã được cân bằng rồi!", "OK");
                    return;
                }

                if (MessageBox.Query("Duyệt phiếu", $"Duyệt cân bằng kho cho phiếu '{ma}'?", "Duyệt", "Hủy") == 0)
                {
                    try
                    {
                        _db.DuyetPhieu(ma);
                        LoadData(txtSearch.Text.ToString());
                        MessageBox.Query("Thành công", "Kho đã được cân bằng!", "OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Lỗi CSDL", ex.Message, "OK");
                    }
                }
            };

            Add(txtSearch, btnTim, btnThem, btnSua, btnXoa, btnDuyet, _table);
        }

        private void LoadData(string keyword = "")
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã KK");
            _dtSource.Columns.Add("Ngày Kiểm");
            _dtSource.Columns.Add("Nhân Viên");
            _dtSource.Columns.Add("Kho Kiểm");
            _dtSource.Columns.Add("Chênh Lệch");
            _dtSource.Columns.Add("Trạng Thái");

            var list = string.IsNullOrEmpty(keyword) ? _db.LayDanhSach() : _db.TimKiem(keyword);

            foreach (var pk in list)
            {
                // Thêm dấu + phía trước nếu số chênh lệch dương để nhìn trực quan
                string clStr = pk.SLChenhLech > 0 ? $"+{pk.SLChenhLech}" : pk.SLChenhLech.ToString();

                _dtSource.Rows.Add(
                    pk.MaKK,
                    pk.NgayKiem.ToString("dd/MM/yyyy"),
                    pk.NhanVien,
                    pk.KhoKiem,
                    clStr,
                    pk.TrangThai
                );
            }

            _table.Table = _dtSource;
            _table.Update();
        }
    }
}