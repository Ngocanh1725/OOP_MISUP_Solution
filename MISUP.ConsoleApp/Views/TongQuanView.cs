using MISUP.BLL.Services;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Terminal.Gui;
using Terminal.Gui.Graphs;
using Attribute = Terminal.Gui.Attribute; // Định danh rõ Attribute của Terminal.Gui

namespace MISUP.ConsoleApp.Views
{
    public class TongQuanView : View
    {
        private HangHoaBLL _db;
        private TableView _table;

        // Khai báo cố định các Label để Update Text mượt mà hơn (không cần xóa đi vẽ lại)
        private Label _lblTongSP;
        private Label _lblTongGiaTri;
        private Label _lblCanhBaoTon;
        private Label _lblCanhBaoHSD;

        public TongQuanView(HangHoaBLL db)
        {
            _db = db;

            // ========================================================
            // 1. TẠO HIỆU ỨNG MÀU CHO NÚT (COLOR SCHEME)
            // ========================================================
            var actionBtnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),          // Bình thường: Chữ xanh, nền đen
                Focus = new Attribute(Color.Black, Color.Cyan),           // Khi trỏ vào: Chữ đen, nền xanh (Nổi bật)
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            var btnRefresh = new Button("🔄 Làm mới") { X = 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnExport = new Button("📥 Xuất nhanh") { X = Pos.Right(btnRefresh) + 2, Y = 0, ColorScheme = actionBtnScheme };

            // Khởi tạo các Label trống (sẽ nạp text trong LoadDashboard)
            _lblTongSP = new Label() { X = 2, Y = 2 };
            _lblTongGiaTri = new Label() { X = 2, Y = 4 };
            _lblCanhBaoTon = new Label() { X = 40, Y = 2, ColorScheme = ThemeManager.InputScheme };
            _lblCanhBaoHSD = new Label() { X = 40, Y = 4, ColorScheme = ThemeManager.InputScheme };

            var line = new LineView(Orientation.Horizontal) { X = 1, Y = 6, Width = Dim.Fill() - 1 };
            var lblTitle = new Label("DANH SÁCH SẢN PHẨM CẦN LƯU Ý:") { X = 2, Y = 8 };

            _table = new TableView() { X = 1, Y = 10, Width = Dim.Fill() - 1, Height = Dim.Fill(), FullRowSelect = true };

            Add(btnRefresh, btnExport, _lblTongSP, _lblTongGiaTri, _lblCanhBaoTon, _lblCanhBaoHSD, line, lblTitle, _table);

            // ========================================================
            // 2. GẮN SỰ KIỆN CHO CÁC NÚT (EVENTS)
            // ========================================================

            // Xử lý nút LÀM MỚI
            btnRefresh.Clicked += () => {
                LoadDashboard();
                MessageBox.Query("Thành công", "Dữ liệu kho hàng đã được làm mới!", "OK");
            };

            // Xử lý nút XUẤT NHANH (Hỗ trợ nhiều định dạng và lưu ra Desktop)
            btnExport.Clicked += () => {
                try
                {
                    // Tạo menu lựa chọn định dạng file
                    int choice = MessageBox.Query("Tùy chọn Xuất File", "Bạn muốn xuất báo cáo ra định dạng nào?", "Tệp CSV (Dùng cho Excel)", "Tệp HTML (Dùng in ra PDF)", "Hủy bỏ");

                    if (choice == 0) // Chọn CSV
                    {
                        ExportDataToCsv();
                    }
                    else if (choice == 1) // Chọn HTML
                    {
                        ExportDataToHtml();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Lỗi Xuất File", "Không thể xuất file: " + ex.Message, "OK");
                }
            };

            LoadDashboard(); // Lần đầu tải dữ liệu
        }

        private void LoadDashboard()
        {
            // Lấy dữ liệu từ tầng BLL
            int tongSP = _db.DemTongSoMatHang();
            decimal tongGiaTri = _db.TinhTongGiaTriKho();
            int hangSapHet = _db.LayHangSapHetTonKho().Count;
            int hangCanDate = _db.LayHangCanDate(30).Count;

            // Cập nhật Text cho các Label (Đổi SKU thành Mặt hàng)
            _lblTongSP.Text = $"[ TỔNG MẶT HÀNG ]: {tongSP} Mặt hàng";
            _lblTongGiaTri.Text = $"[ TỔNG GIÁ TRỊ ]: {tongGiaTri:N0} VNĐ";
            _lblCanhBaoTon.Text = $"[ CẦN NHẬP GẤP ]: {hangSapHet} SP";
            _lblCanhBaoHSD.Text = $"[ CẬN DATE/HẾT ]: {hangCanDate} SP";

            // Nạp dữ liệu vào bảng TableView
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Hàng");
            dt.Columns.Add("Tên Sản Phẩm");
            dt.Columns.Add("Tồn Kho");
            dt.Columns.Add("Cảnh Báo");

            var combinedList = _db.LayHangSapHetTonKho().Concat(_db.LayHangCanDate(30)).Distinct().ToList();
            foreach (var h in combinedList)
            {
                string warn = h.SoLuongNhap <= h.TonKhoToiThieu ? "Tồn thấp" : "Cận Date";
                dt.Rows.Add(h.MaHang, h.TenHang, h.SoLuongNhap, warn);
            }
            _table.Table = dt;
            _table.Update();
        }

        private void ExportDataToCsv()
        {
            // Lấy dữ liệu cảnh báo
            var combinedList = _db.LayHangSapHetTonKho().Concat(_db.LayHangCanDate(30)).Distinct().ToList();

            StringBuilder sb = new StringBuilder();
            // Thêm Header
            sb.AppendLine("Mã Hàng,Tên Sản Phẩm,Tồn Kho,Tình Trạng Cảnh Báo");

            // Đổ dữ liệu
            foreach (var h in combinedList)
            {
                string warn = h.SoLuongNhap <= h.TonKhoToiThieu ? "Tồn thấp" : "Cận Date";
                // Nếu Tên sản phẩm có dấu phẩy, bọc trong dấu ngoặc kép theo chuẩn CSV
                string safeName = h.TenHang.Contains(",") ? $"\"{h.TenHang}\"" : h.TenHang;
                sb.AppendLine($"{h.MaHang},{safeName},{h.SoLuongNhap},{warn}");
            }

            // Lấy đường dẫn Desktop của máy tính
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "BaoCao_CanhBao.csv");

            // Ghi file với chuẩn UTF8 có BOM để Excel không lỗi font Tiếng Việt
            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));

            MessageBox.Query("Thành công", $"Đã xuất báo cáo ra file 'BaoCao_CanhBao.csv'!\nĐường dẫn: {filePath}", "OK");
        }

        private void ExportDataToHtml()
        {
            var combinedList = _db.LayHangSapHetTonKho().Concat(_db.LayHangCanDate(30)).Distinct().ToList();
            StringBuilder sb = new StringBuilder();

            // Xây dựng mã HTML và CSS nội tuyến để bảng biểu đẹp mắt
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='vi'>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            sb.AppendLine("<title>Báo Cáo Cảnh Báo Siêu Thị</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; padding: 20px; color: #333; }");
            sb.AppendLine("h2 { text-align: center; color: #2c3e50; }");
            sb.AppendLine("p { text-align: right; font-style: italic; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            sb.AppendLine("th, td { border: 1px solid #bdc3c7; padding: 12px; text-align: left; }");
            sb.AppendLine("th { background-color: #34495e; color: white; }");
            sb.AppendLine(".warn { color: #e74c3c; font-weight: bold; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            // Nội dung chính
            sb.AppendLine("<h2>DANH SÁCH SẢN PHẨM CẦN LƯU Ý KHO HÀNG</h2>");
            sb.AppendLine($"<p>Thời gian xuất báo cáo: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}</p>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead><tr><th>Mã Hàng</th><th>Tên Sản Phẩm</th><th>Tồn Kho</th><th>Tình Trạng</th></tr></thead>");
            sb.AppendLine("<tbody>");

            // Đổ dữ liệu vào bảng HTML
            foreach (var h in combinedList)
            {
                string warn = h.SoLuongNhap <= h.TonKhoToiThieu ? "Tồn thấp" : "Cận Date";
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{h.MaHang}</td>");
                sb.AppendLine($"<td>{h.TenHang}</td>");
                sb.AppendLine($"<td>{h.SoLuongNhap}</td>");
                sb.AppendLine($"<td class='warn'>{warn}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            // Lấy đường dẫn Desktop của máy tính
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "BaoCao_CanhBao.html");

            // Ghi file HTML ra màn hình Desktop
            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(false));

            MessageBox.Query("Thành công", $"Đã xuất báo cáo ra file HTML!\n\nĐường dẫn: {filePath}\n\nHướng dẫn tạo PDF:\n1. Mở file HTML vừa tải trên trình duyệt\n2. Nhấn Ctrl + P\n3. Chọn 'Lưu dưới dạng PDF'", "OK");
        }
    }
}