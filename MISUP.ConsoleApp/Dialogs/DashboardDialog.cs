using MISUP.BLL.Services;
using NStack;
using System;
using Terminal.Gui;

// ÉP KIỂU ĐỂ TRÁNH XUNG ĐỘT
using Application = Terminal.Gui.Application;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class DashboardDialog : Dialog
    {
        public DashboardDialog(HangHoaBLL db) : base("📊 THONG KE KHO HANG", 50, 12)
        {
            this.ColorScheme = ThemeManager.HackerScheme;

            int tongSP = db.DemTongSoMatHang();
            decimal tongGiaTri = db.TinhTongGiaTriKho();
            int hangSapHet = db.LayHangSapHetTonKho().Count;

            // Bảng màu đỏ cho cảnh báo
            var redScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.BrightRed, Color.Black),
                Focus = new Attribute(Color.BrightRed, Color.Black)
            };

            var lblTongSP = new Label($"Tong so mat hang  : {tongSP} SP") { X = 5, Y = 2 };
            var lblTongTien = new Label($"Tong gia tri kho : {tongGiaTri:N0} VND") { X = 5, Y = 4 };

            var lblCanhBao = new Label($"San pham sap het : {hangSapHet} (Can nhap them)")
            {
                X = 5,
                Y = 6,
                ColorScheme = redScheme
            };

            var btnClose = new Button("Dong") { X = Pos.Center(), Y = 9, IsDefault = true };
            btnClose.Clicked += () => Application.RequestStop();

            this.Add(lblTongSP, lblTongTien, lblCanhBao, btnClose);
        }
    }
}