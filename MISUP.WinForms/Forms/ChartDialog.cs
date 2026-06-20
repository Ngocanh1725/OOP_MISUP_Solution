using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MISUP.Models;

namespace MISUP.WinForms.Forms
{
    public partial class ChartDialog : Form
    {
        public ChartDialog(List<HangHoa> data, string title, string metric)
        {
            InitializeComponent();
            lblTitle.Text = title;
            DrawChart(data, metric);
        }

        private void DrawChart(List<HangHoa> data, string metric)
        {
            chartThongKe.Series.Clear();

            // Khởi tạo chuỗi dữ liệu (Series) cho biểu đồ Cột
            Series series = new Series();
            series.ChartType = SeriesChartType.Column; // Chọn biểu đồ cột
            series.IsValueShownAsLabel = true; // Hiện con số ngay trên đỉnh cột
            series.Color = Color.FromArgb(52, 152, 219);

            // Lấy Top 10 sản phẩm để biểu đồ không bị quá rễ chằng chịt
            var topData = data.Take(10).ToList();

            foreach (var item in topData)
            {
                // Cắt ngắn tên sản phẩm nếu quá dài để hiển thị trục X được đẹp
                string name = item.TenHang.Length > 15 ? item.TenHang.Substring(0, 15) + "..." : item.TenHang;

                if (metric == "GiaTri")
                {
                    series.Name = "Giá Trị Tồn (VND)";
                    // Thêm dữ liệu vào biểu đồ
                    series.Points.AddXY(name, item.TinhTongGiaTriSauThue());
                }
                else // "SoLuong"
                {
                    series.Name = "Số Lượng (Cái)";
                    series.Color = Color.FromArgb(231, 76, 60); // Đổi màu cột thành Đỏ nếu là cảnh báo tồn
                    // Thêm dữ liệu vào biểu đồ
                    series.Points.AddXY(name, item.SoLuongNhap);
                }
            }

            chartThongKe.Series.Add(series);
        }
    }
}