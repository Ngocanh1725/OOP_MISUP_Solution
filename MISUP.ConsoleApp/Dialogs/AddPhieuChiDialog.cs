using MISUP.Models;
using MISUP.Models.Entities;
using System;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class AddPhieuChiDialog : Dialog
    {
        public bool IsSaved { get; private set; } = false;
        public PhieuChi ResultPhieu { get; private set; }

        // CHIỀU CAO 22 ĐỂ NÚT KHÔNG BỊ ĐÈ
        public AddPhieuChiDialog(PhieuChi pc = null)
            : base(pc == null ? "Lập Phiếu Chi Mới" : "Sửa Phiếu Chi", 65, 22)
        {
            ColorScheme = ThemeManager.HackerScheme;
            bool isEdit = (pc != null);

            var txtMa = new TextField(isEdit ? pc.MaPhieu : "") { X = 20, Y = 2, Width = 35, ReadOnly = isEdit, ColorScheme = ThemeManager.InputScheme };
            var txtThoiGian = new TextField(isEdit ? pc.ThoiGian.ToString("dd/MM/yyyy HH:mm") : DateTime.Now.ToString("dd/MM/yyyy HH:mm")) { X = 20, Y = 4, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtTenNCC = new TextField(isEdit ? pc.TenNCC : "") { X = 20, Y = 6, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtTien = new TextField(isEdit ? pc.SoTien.ToString("G0") : "0") { X = 20, Y = 8, Width = 35, ColorScheme = ThemeManager.InputScheme };

            var cmbPhuongThuc = new ComboBox() { X = 20, Y = 10, Width = 35, Height = 3 };
            cmbPhuongThuc.SetSource(new string[] { "Tiền mặt", "Chuyển khoản" });
            cmbPhuongThuc.SelectedItem = (isEdit && pc.PhuongThuc == "Chuyển khoản") ? 1 : 0;

            var cmbTrangThai = new ComboBox() { X = 20, Y = 12, Width = 35, Height = 4 };
            cmbTrangThai.SetSource(new string[] { "Đã thanh toán", "Kỳ hạn nợ", "Đã hủy" });

            if (isEdit)
            {
                if (pc.TrangThai == "Kỳ hạn nợ") cmbTrangThai.SelectedItem = 1;
                else if (pc.TrangThai == "Đã hủy") cmbTrangThai.SelectedItem = 2;
                else cmbTrangThai.SelectedItem = 0;
            }
            else
            {
                cmbTrangThai.SelectedItem = 0;
            }

            // Hiệu ứng màu nổi bật
            var btnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            var btnSave = new Button("Lưu lại") { X = Pos.Center() - 10, Y = 16, IsDefault = true, ColorScheme = btnScheme };
            var btnBack = new Button("Hủy bỏ") { X = Pos.Center() + 4, Y = 16, ColorScheme = btnScheme };

            btnBack.Clicked += () => Application.RequestStop();

            btnSave.Clicked += () => {
                try
                {
                    string ma = txtMa.Text.ToString().Trim();
                    DateTime tg = DateTime.ParseExact(txtThoiGian.Text.ToString().Trim(), "dd/MM/yyyy HH:mm", null);
                    string ten = txtTenNCC.Text.ToString().Trim();
                    decimal tien = decimal.Parse(txtTien.Text.ToString().Trim());
                    string pt = cmbPhuongThuc.SelectedItem == 1 ? "Chuyển khoản" : "Tiền mặt";

                    string tt = "Đã thanh toán";
                    if (cmbTrangThai.SelectedItem == 1) tt = "Kỳ hạn nợ";
                    else if (cmbTrangThai.SelectedItem == 2) tt = "Đã hủy";

                    ResultPhieu = new PhieuChi(ma, tg, ten, tien, pt, tt);
                    IsSaved = true;
                    Application.RequestStop();
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Lỗi nhập liệu", "Vui lòng kiểm tra lại định dạng Ngày (dd/MM/yyyy HH:mm) và Số tiền!", "OK");
                }
            };

            Add(new Label("Mã Phiếu:") { X = 2, Y = 2 }, txtMa,
                new Label("Thời Gian:") { X = 2, Y = 4 }, txtThoiGian,
                new Label("Tên NCC:") { X = 2, Y = 6 }, txtTenNCC,
                new Label("Số Tiền (VNĐ):") { X = 2, Y = 8 }, txtTien,
                new Label("Phương Thức:") { X = 2, Y = 10 }, cmbPhuongThuc,
                new Label("Trạng Thái:") { X = 2, Y = 12 }, cmbTrangThai,
                btnSave, btnBack);
        }
    }
}