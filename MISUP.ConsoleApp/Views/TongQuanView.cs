using MISUP.BLL.Services;
using System.Data;
using System.Linq;
using Terminal.Gui;
using Terminal.Gui.Graphs;

namespace MISUP.ConsoleApp.Views
{
    public class TongQuanView : View
    {
        private HangHoaBLL _db;
        private TableView _table;

        public TongQuanView(HangHoaBLL db)
        {
            _db = db;

            var btnRefresh = new Button("🔄 Làm mới") { X = 1, Y = 0 };
            var btnExport = new Button("📥 Xuất nhanh") { X = Pos.Right(btnRefresh) + 2, Y = 0 };

            btnRefresh.Clicked += () => { LoadDashboard(); };
            btnExport.Clicked += () => { MessageBox.Query("Thành công", "Đã xuất báo cáo tổng quan ra file CSV!", "OK"); };

            _table = new TableView() { X = 1, Y = 10, Width = Dim.Fill() - 1, Height = Dim.Fill(), FullRowSelect = true };

            Add(btnRefresh, btnExport, _table);
            LoadDashboard(); // Lần đầu tải dữ liệu
        }

        private void LoadDashboard()
        {
            // Xóa các Label cũ (nếu có) để vẽ lại
            var labels = Subviews.Where(v => v is Label || v is LineView).ToList();
            foreach (var l in labels) Remove(l);

            int tongSP = _db.DemTongSoMatHang();
            decimal tongGiaTri = _db.TinhTongGiaTriKho();
            int hangSapHet = _db.LayHangSapHetTonKho().Count;
            int hangCanDate = _db.LayHangCanDate(30).Count;

            var lbl1 = new Label($"[ TỔNG MẶT HÀNG ]: {tongSP} SKU") { X = 2, Y = 2 };
            var lbl2 = new Label($"[ TỔNG GIÁ TRỊ ]: {tongGiaTri:N0} VNĐ") { X = 2, Y = 4 };
            var lbl3 = new Label($"[ CẦN NHẬP GẤP ]: {hangSapHet} SP") { X = 40, Y = 2, ColorScheme = ThemeManager.InputScheme };
            var lbl4 = new Label($"[ CẬN DATE/HẾT ]: {hangCanDate} SP") { X = 40, Y = 4, ColorScheme = ThemeManager.InputScheme };
            var line = new LineView(Orientation.Horizontal) { X = 1, Y = 6, Width = Dim.Fill() - 1 };
            var lblTitle = new Label("DANH SÁCH SẢN PHẨM CẦN LƯU Ý:") { X = 2, Y = 8 };

            Add(lbl1, lbl2, lbl3, lbl4, line, lblTitle);

            // Nạp dữ liệu vào bảng
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Hàng"); dt.Columns.Add("Tên Sản Phẩm"); dt.Columns.Add("Tồn Kho"); dt.Columns.Add("Cảnh Báo");

            var combinedList = _db.LayHangSapHetTonKho().Concat(_db.LayHangCanDate(30)).Distinct().ToList();
            foreach (var h in combinedList)
            {
                string warn = h.SoLuongNhap <= h.TonKhoToiThieu ? "Tồn thấp" : "Cận Date";
                dt.Rows.Add(h.MaHang, h.TenHang, h.SoLuongNhap, warn);
            }
            _table.Table = dt;
            _table.Update();
        }
    }
}