using System.Data;
using Terminal.Gui;

namespace MISUP.ConsoleApp.Views
{
    public class NhaCungCapView : View
    {
        private DataTable _dtSource;
        private TableView _table;

        public NhaCungCapView()
        {
            var txtSearch = new TextField("") { X = 1, Y = 0, Width = 25 };
            var btnTim = new Button("Tìm") { X = Pos.Right(txtSearch) + 1, Y = 0 };

            var btnThem = new Button("➕ Thêm Đối tác") { X = Pos.Right(btnTim) + 2, Y = 0 };
            var btnSua = new Button("✏️ Sửa") { X = Pos.Right(btnThem) + 1, Y = 0 };
            var btnXoa = new Button("🗑️ Xóa") { X = Pos.Right(btnSua) + 1, Y = 0 };

            _table = new TableView() { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };
            LoadMockData();

            btnTim.Clicked += () => {
                if (_dtSource != null)
                {
                    _dtSource.DefaultView.RowFilter = $"[Tên Công Ty] LIKE '%{txtSearch.Text}%'";
                    _table.Table = _dtSource.DefaultView.ToTable();
                }
            };

            btnThem.Clicked += () => { MessageBox.Query("Thêm", "Mở form thêm Đối tác...", "OK"); };
            btnSua.Clicked += () => {
                if (_table.SelectedRow < 0) return;
                MessageBox.Query("Sửa", "Mở form sửa thông tin Đối tác...", "OK");
            };
            btnXoa.Clicked += () => {
                if (_table.SelectedRow < 0) return;
                if (MessageBox.Query("Xác nhận", "Xóa đối tác này?", "Có", "Không") == 0)
                {
                    _dtSource.Rows.RemoveAt(_table.SelectedRow);
                    _table.Update();
                }
            };

            Add(txtSearch, btnTim, btnThem, btnSua, btnXoa, _table);
        }

        private void LoadMockData()
        {
            _dtSource = new DataTable();
            _dtSource.Columns.Add("Mã NCC"); _dtSource.Columns.Add("Tên Công Ty"); _dtSource.Columns.Add("Điện Thoại"); _dtSource.Columns.Add("Trạng Thái");

            _dtSource.Rows.Add("NCC001", "Công ty CP Sữa VN", "1900 1568", "Đang giao dịch");
            _dtSource.Rows.Add("NCC002", "Samsung Electronics", "028 3821 111", "Đang giao dịch");
            _dtSource.Rows.Add("NCC003", "Nhựa Chợ Lớn", "0988 123 456", "Ngừng GD");

            _table.Table = _dtSource;
        }
    }
}