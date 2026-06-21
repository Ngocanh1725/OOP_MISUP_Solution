using MISUP.BLL.Services;
using MISUP.ConsoleApp.Dialogs;
using MISUP.Models;
using NStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Views
{
    public class BaoCaoView : View
    {
        private HangHoaBLL _db;
        private TableView _table;
        private RadioGroup _rdoFilter;

        public BaoCaoView(HangHoaBLL db)
        {
            _db = db;

            // Hiệu ứng màu nổi bật cho nút bấm
            var actionBtnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            _rdoFilter = new RadioGroup(new ustring[] { "Top Tồn Kho Nhất", "Top Giá Trị Nhất", "Sắp hết hàng" }) { X = 1, Y = 0 };

            // Đã rút gọn tên nút để tránh bị Terminal chèn ép mất chữ
            var btnXem = new Button("👁️ Mở Báo Cáo") { X = Pos.Right(_rdoFilter) + 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnXuat = new Button("📥 Xuất File") { X = Pos.Right(btnXem) + 1, Y = 0, ColorScheme = actionBtnScheme };

            // Tính năng sắp xếp trực tiếp trên bảng
            var btnSortAsc = new Button("↑ Xếp") { X = Pos.Right(btnXuat) + 1, Y = 0, ColorScheme = actionBtnScheme };
            var btnSortDesc = new Button("↓ Xếp") { X = Pos.Right(btnSortAsc) + 1, Y = 0, ColorScheme = actionBtnScheme };

            // Kéo bảng TableView lên trên (Y=4) để giao diện rộng rãi hơn vì đã bỏ biểu đồ
            _table = new TableView() { X = 0, Y = 4, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            // 1. MỞ HỘP THOẠI BÁO CÁO RỘNG RÃI CHỨA BIỂU ĐỒ
            btnXem.Clicked += () => {
                var data = GetDataForReport(_rdoFilter.SelectedItem);
                string title = _rdoFilter.SelectedItem == 0 ? "BÁO CÁO TOP TỒN KHO" : (_rdoFilter.SelectedItem == 1 ? "BÁO CÁO TOP GIÁ TRỊ" : "BÁO CÁO CẢNH BÁO");
                var dialog = new ReportDialog(title, data, _rdoFilter.SelectedItem);
                Application.Run(dialog);
            };

            // 2. XUẤT RA FILE (Hỗ trợ HTML để in PDF)
            btnXuat.Clicked += () => ExportReport(_rdoFilter.SelectedItem);

            // 3. ĐỔI BÁO CÁO KHI CHỌN RADIO
            _rdoFilter.SelectedItemChanged += (e) => LoadInlineReport(e.SelectedItem);

            // 4. SẮP XẾP BẢNG TĂNG/GIẢM
            btnSortAsc.Clicked += () => {
                if (_table.Table != null)
                {
                    var dt = _table.Table;
                    dt.DefaultView.Sort = "Tồn Kho ASC";
                    _table.Table = dt.DefaultView.ToTable();
                }
            };
            btnSortDesc.Clicked += () => {
                if (_table.Table != null)
                {
                    var dt = _table.Table;
                    dt.DefaultView.Sort = "Tồn Kho DESC";
                    _table.Table = dt.DefaultView.ToTable();
                }
            };

            Add(_rdoFilter, btnXem, btnXuat, btnSortAsc, btnSortDesc, _table);
            LoadInlineReport(0); // Tải mặc định
        }

        private List<HangHoa> GetDataForReport(int filterType)
        {
            if (filterType == 0) return _db.LayDanhSach().OrderByDescending(x => x.SoLuongNhap).Take(10).ToList();
            if (filterType == 1) return _db.LayDanhSach().OrderByDescending(x => x.TinhTongGiaTriSauThue()).Take(10).ToList();
            return _db.LayHangSapHetTonKho().Take(10).ToList();
        }

        // Đã xóa hàm vẽ biểu đồ ở View ngoài, chỉ để lại load Data lên Bảng
        private void LoadInlineReport(int filterType)
        {
            var list = GetDataForReport(filterType);

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Hàng");
            dt.Columns.Add("Tên Sản Phẩm");
            dt.Columns.Add("Tồn Kho", typeof(int));
            dt.Columns.Add("Giá Trị Kho (VNĐ)");

            foreach (var h in list)
                dt.Rows.Add(h.MaHang, h.TenHang, h.SoLuongNhap, h.TinhTongGiaTriSauThue().ToString("N0"));

            _table.Table = dt;
            _table.Update();
        }

        private void ExportReport(int filterType)
        {
            try
            {
                int choice = MessageBox.Query("Tùy chọn Xuất File", "Bạn muốn xuất báo cáo ra định dạng nào?", "Tệp CSV (Excel)", "Tệp HTML (Để in PDF)", "Hủy bỏ");
                if (choice == 2 || choice == -1) return;

                var data = GetDataForReport(filterType);
                string title = filterType == 0 ? "TOP TỒN KHO" : (filterType == 1 ? "TOP GIÁ TRỊ" : "SẮP HẾT HÀNG");

                // Cung cấp đường dẫn chính xác tuyệt đối trên màn hình Desktop
                string currentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (choice == 0)
                {
                    string filePath = Path.Combine(currentPath, "BaoCao_ThongKe.csv");
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"BÁO CÁO: {title}");
                    sb.AppendLine("Mã Hàng,Tên Sản Phẩm,Tồn Kho,Giá Trị Kho");
                    foreach (var h in data)
                    {
                        string safeName = h.TenHang.Contains(",") ? $"\"{h.TenHang}\"" : h.TenHang;
                        sb.AppendLine($"{h.MaHang},{safeName},{h.SoLuongNhap},{h.TinhTongGiaTriSauThue()}");
                    }
                    File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Query("Thành công", $"Đã lưu tệp 'BaoCao_ThongKe.csv'.\nĐường dẫn: {filePath}", "OK");
                }
                else if (choice == 1)
                {
                    string filePath = Path.Combine(currentPath, "BaoCao_ThongKe.html");
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<!DOCTYPE html><html lang='vi'><head><meta charset='UTF-8'><title>Báo Cáo Siêu Thị</title>");
                    sb.AppendLine("<style>body{font-family:Arial; padding:20px} table{width:100%; border-collapse:collapse; margin-top:20px;} th,td{border:1px solid #ccc; padding:12px;} th{background:#2980b9; color:white;}</style></head><body>");
                    sb.AppendLine($"<h2 style='text-align:center; color:#2980b9;'>BÁO CÁO THỐNG KÊ: {title}</h2>");
                    sb.AppendLine($"<p>Ngày lập: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}</p>");
                    sb.AppendLine("<table><tr><th>Mã Hàng</th><th>Tên Sản Phẩm</th><th>Tồn Kho</th><th>Giá Trị (VNĐ)</th></tr>");
                    foreach (var h in data)
                    {
                        sb.AppendLine($"<tr><td>{h.MaHang}</td><td>{h.TenHang}</td><td>{h.SoLuongNhap}</td><td>{h.TinhTongGiaTriSauThue():N0}</td></tr>");
                    }
                    sb.AppendLine("</table></body></html>");
                    File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(false));

                    // Hiện hướng dẫn in PDF cụ thể và đường dẫn file
                    MessageBox.Query("Thành công", $"Đã lưu tệp HTML thành công!\n\nĐường dẫn: {filePath}\n\n*HƯỚNG DẪN IN PDF:\n1. Mở file HTML trên bằng trình duyệt Web\n2. Nhấn phím Ctrl + P\n3. Chọn máy in là 'Lưu dưới dạng PDF'", "OK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Lỗi Xuất File", ex.Message, "OK");
            }
        }
    }
}