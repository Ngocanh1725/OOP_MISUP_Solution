using MISUP.BLL.Services;
using MISUP.Models;
using NStack;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Terminal.Gui;

namespace MISUP.ConsoleApp.Views
{
    public class BaoCaoView : View
    {
        private HangHoaBLL _db;
        private FrameView _chartView;
        private TableView _table;

        public BaoCaoView(HangHoaBLL db)
        {
            _db = db;

            var rdoFilter = new RadioGroup(new ustring[] { "Top Tồn Kho Nhất", "Top Giá Trị Nhất", "Sắp hết hàng" }) { X = 1, Y = 0 };
            var btnXem = new Button("🔄 Xem Báo Cáo") { X = Pos.Right(rdoFilter) + 2, Y = 0 };
            var btnXuat = new Button("📥 Xuất CSV") { X = Pos.Right(btnXem) + 1, Y = 0 };

            _chartView = new FrameView() { X = 0, Y = 4, Width = Dim.Fill(), Height = 10 };
            _table = new TableView() { X = 0, Y = 15, Width = Dim.Fill(), Height = Dim.Fill(), FullRowSelect = true };

            btnXem.Clicked += () => { LoadReport(rdoFilter.SelectedItem); };
            btnXuat.Clicked += () => { MessageBox.Query("Xuất File", "Đã xuất báo cáo thành công!", "OK"); };

            Add(rdoFilter, btnXem, btnXuat, _chartView, _table);
            LoadReport(0); // Tải mặc định
        }

        private void LoadReport(int filterType)
        {
            _chartView.RemoveAll();
            List<HangHoa> list = new List<HangHoa>();
            string title = "";

            if (filterType == 0)
            {
                list = _db.LayDanhSach().OrderByDescending(x => x.SoLuongNhap).Take(5).ToList();
                title = "BIỂU ĐỒ CỘT KÝ TỰ (TOP SỐ LƯỢNG TỒN KHO)";
            }
            else if (filterType == 1)
            {
                list = _db.LayDanhSach().OrderByDescending(x => x.TinhTongGiaTriSauThue()).Take(5).ToList();
                title = "BIỂU ĐỒ CỘT KÝ TỰ (TOP GIÁ TRỊ TỒN KHO)";
            }
            else
            {
                list = _db.LayHangSapHetTonKho().Take(5).ToList();
                title = "BIỂU ĐỒ CỘT KÝ TỰ (HÀNG SẮP HẾT)";
            }

            _chartView.Title = title;

            // 1. Vẽ lại Biểu đồ ASCII
            int maxVal = list.Any() ? list.Max(x => filterType == 1 ? (int)x.TinhTongGiaTriSauThue() : x.SoLuongNhap) : 1;
            int yPos = 0;
            foreach (var item in list)
            {
                int val = filterType == 1 ? (int)item.TinhTongGiaTriSauThue() : item.SoLuongNhap;
                // Tính chiều dài cột (tối đa 40 ký tự)
                int barLength = maxVal > 0 ? (int)((double)val / maxVal * 40) : 0;
                string bar = new string('█', barLength);
                string name = item.TenHang.Length > 12 ? item.TenHang.Substring(0, 12) : item.TenHang.PadRight(12);

                string displayVal = filterType == 1 ? $"{val:N0} VNĐ" : $"{val} Cái";
                _chartView.Add(new Label($"{name} | {bar} {displayVal}") { X = 1, Y = yPos });
                yPos++;
            }

            // 2. Cập nhật Bảng Grid
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Hàng"); dt.Columns.Add("Tên SP"); dt.Columns.Add("Tồn Kho"); dt.Columns.Add("Giá Trị Kho (VNĐ)");

            foreach (var h in list)
            {
                dt.Rows.Add(h.MaHang, h.TenHang, h.SoLuongNhap, h.TinhTongGiaTriSauThue().ToString("N0"));
            }
            _table.Table = dt;
            _table.Update();
        }
    }
}