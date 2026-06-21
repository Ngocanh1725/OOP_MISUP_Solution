using System.Data;
using Terminal.Gui;

namespace MISUP.ConsoleApp.Views
{
    public class ThanhToanView : View
    {
        private DataTable _dtSource;
        private TableView _table;

        public ThanhToanView()
        {
            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 20 };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0 };
            var btnThem = new Button("💸 Lập Phiếu Chi") { X = Pos.Right(btnTim) + 2, Y = 0 };
            var btnXoa = new Button("Hủy Phiếu") { X = Pos.Right(btnThem) + 1, Y = 0 };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };
            LoadMockData();

            btnTim.Clicked += () => {
                if (_dtSource != null)
                {
                    _dtSource.DefaultView.RowFilter = $"[Mã Phiếu] LIKE '%{txtSearch.Text}%' OR NCC LIKE '%{txtSearch.Text}%'";
                    _table.Table = _dtSource.DefaultView.ToTable();
                }
            };

            btnThem.Clicked += () => { MessageBox.Query("Tạo Phiếu", "Mở form lập phiếu chi tiền...", "OK"); };
            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0) return;
                if (MessageBox.Query("Hủy", "Bạn có chắc muốn hủy phiếu chi này?", "Có", "Không") == 0)
                {
                    _dtSource.Rows.RemoveAt(_table.SelectedRow);
                    _table.Update();
                }
            };

            Add(txtSearch, btnTim, btnThem, btnXoa, _table);
        }

        private void LoadMockData()
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã Phiếu"); _dtSource.Columns.Add("NCC"); _dtSource.Columns.Add("Số Tiền"); _dtSource.Columns.Add("Trạng Thái");

            _dtSource.Rows.Add("PC0001", "Công ty CP Sữa VN", "125,500,000", "Đã thanh toán");
            _dtSource.Rows.Add("PC0002", "Samsung Electronics", "150,000,000", "Kỳ hạn nợ");

            _table.Table = _dtSource;
        }
    }
}