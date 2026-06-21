using System.Data;
using Terminal.Gui;

namespace MISUP.ConsoleApp.Views
{
    public class KiemKeView : View
    {
        private DataTable _dtSource;
        private TableView _table;

        public KiemKeView()
        {
            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 20 };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0 };
            var btnThem = new Button("📋 Tạo Phiếu Kiểm") { X = Pos.Right(btnTim) + 2, Y = 0 };
            var btnDuyet = new Button("✓ Duyệt Cân Bằng") { X = Pos.Right(btnThem) + 1, Y = 0 };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };
            LoadMockData();

            btnTim.Clicked += () => {
                if (_dtSource != null)
                {
                    _dtSource.DefaultView.RowFilter = $"[Mã KK] LIKE '%{txtSearch.Text}%'";
                    _table.Table = _dtSource.DefaultView.ToTable();
                }
            };

            btnThem.Clicked += () => { MessageBox.Query("Kiểm Kê", "Mở form tạo phiếu kiểm kê...", "OK"); };
            btnDuyet.Clicked += () => {
                if (_table.SelectedRow >= 0)
                {
                    MessageBox.Query("Thành công", "Đã duyệt cân bằng kho cho phiếu này!", "OK");
                    _dtSource.Rows[_table.SelectedRow]["Trạng Thái"] = "Đã cân bằng";
                    _table.Update();
                }
            };

            Add(txtSearch, btnTim, btnThem, btnDuyet, _table);
        }

        private void LoadMockData()
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã KK"); _dtSource.Columns.Add("Ngày"); _dtSource.Columns.Add("Nhân Viên"); _dtSource.Columns.Add("Chênh Lệch"); _dtSource.Columns.Add("Trạng Thái");

            _dtSource.Rows.Add("PKK01", "15/06/2026", "Admin", "-5", "Đã cân bằng");
            _dtSource.Rows.Add("PKK02", "01/04/2026", "Nhân Viên A", "-10", "Đang xử lý");

            _table.Table = _dtSource;
        }
    }
}