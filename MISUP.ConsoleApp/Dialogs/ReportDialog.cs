using MISUP.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class ReportDialog : Dialog
    {
        public ReportDialog(string title, List<HangHoa> data, int filterType)
            : base(title, 95, 26) // Tăng độ rộng và độ cao để tránh bị cắt/chèn chữ
        {
            ColorScheme = ThemeManager.HackerScheme;

            string chartTitle = filterType == 0 ? "BIỂU ĐỒ SỐ LƯỢNG TỒN KHO" : (filterType == 1 ? "BIỂU ĐỒ GIÁ TRỊ KHO" : "BIỂU ĐỒ SẢN PHẨM CẦN NHẬP GẤP");
            var chartFrame = new FrameView(chartTitle) { X = 0, Y = 0, Width = Dim.Fill(), Height = 12 };

            // TẠO MÀU XANH DƯƠNG (BRIGHT BLUE) CHO BIỂU ĐỒ
            var blueScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.BrightBlue, Color.Black)
            };

            int maxVal = data.Count > 0 ? data.Max(x => filterType == 1 ? (int)x.TinhTongGiaTriSauThue() : x.SoLuongNhap) : 1;
            int yPos = 0;

            foreach (var item in data)
            {
                int val = filterType == 1 ? (int)item.TinhTongGiaTriSauThue() : item.SoLuongNhap;
                // Căn chỉnh độ dài thanh biểu đồ tối đa 40 ký tự
                int barLength = maxVal > 0 ? (int)((double)val / maxVal * 40) : 0;

                // Dùng ký tự ô vuông nhỏ để biểu đồ trông gọn và thanh thoát hơn
                string bar = new string('■', barLength);

                // Cắt tên sản phẩm nếu quá dài để biểu đồ thẳng hàng
                string name = item.TenHang.Length > 20 ? item.TenHang.Substring(0, 20) : item.TenHang.PadRight(20);
                string displayVal = filterType == 1 ? $"{val:N0} VNĐ" : $"{val} Cái";

                chartFrame.Add(new Label($"{name} | ") { X = 1, Y = yPos });
                // Gắn màu xanh vào Label vẽ biểu đồ
                chartFrame.Add(new Label(bar) { X = 24, Y = yPos, ColorScheme = blueScheme });
                chartFrame.Add(new Label($" {displayVal}") { X = 24 + barLength, Y = yPos });
                yPos++;
            }

            var tableFrame = new FrameView("Bảng Dữ Liệu Chi Tiết") { X = 0, Y = Pos.Bottom(chartFrame), Width = Dim.Fill(), Height = 9 };
            var table = new TableView() { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Hàng");
            dt.Columns.Add("Tên SP");
            dt.Columns.Add("Tồn Kho", typeof(int));
            dt.Columns.Add("Giá Trị Kho (VNĐ)");

            foreach (var h in data)
                dt.Rows.Add(h.MaHang, h.TenHang, h.SoLuongNhap, h.TinhTongGiaTriSauThue().ToString("N0"));

            table.Table = dt;
            tableFrame.Add(table);

            // Nút bấm đóng có hiệu ứng
            var btnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan)
            };

            var btnClose = new Button("Đóng Báo Cáo") { X = Pos.Center(), Y = 22, ColorScheme = btnScheme, IsDefault = true };
            btnClose.Clicked += () => Application.RequestStop();

            Add(chartFrame, tableFrame, btnClose);
        }
    }
}