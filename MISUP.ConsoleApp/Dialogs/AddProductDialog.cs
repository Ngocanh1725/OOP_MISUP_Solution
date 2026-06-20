using MISUP.BLL.Services;
using MISUP.Models;
using NStack;
using System;
using Terminal.Gui;
using Application = Terminal.Gui.Application;

namespace MISUP.ConsoleApp.Dialogs
{
    public class AddProductDialog : Dialog
    {
        private HangHoaBLL _db;
        private bool _isEdit;
        public bool IsSaved { get; private set; } = false;

        public AddProductDialog(HangHoaBLL db, HangHoa sp = null)
            : base((ustring)(sp == null ? "Them San Pham" : "Sua San Pham"), 65, 22)
        {
            _db = db;
            _isEdit = sp != null;
            ColorScheme = ThemeManager.HackerScheme;

            var txtMa = new TextField(_isEdit ? sp.MaHang : "") { X = 18, Y = 2, Width = 35, ReadOnly = _isEdit, ColorScheme = ThemeManager.InputScheme };
            var txtMaVach = new TextField(_isEdit ? sp.MaVach : "") { X = 18, Y = 4, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtTen = new TextField(_isEdit ? sp.TenHang : "") { X = 18, Y = 6, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtNSX = new TextField(_isEdit ? sp.NhaSanXuat : "") { X = 18, Y = 8, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtSL = new TextField(_isEdit ? sp.SoLuongNhap.ToString() : "0") { X = 18, Y = 10, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtGia = new TextField(_isEdit ? sp.DonGia.ToString() : "0") { X = 18, Y = 12, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtTonMin = new TextField(_isEdit ? sp.TonKhoToiThieu.ToString() : "10") { X = 18, Y = 14, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtHSD = new TextField(_isEdit && sp.HanSuDung.HasValue ? sp.HanSuDung.Value.ToString("dd/MM/yyyy") : "") { X = 18, Y = 16, Width = 35, ColorScheme = ThemeManager.InputScheme };

            var radioLoai = new ComboBox() { X = 18, Y = 18, Width = 35, Height = 5 };

            // ĐÃ FIX LỖI CS0029 Ở DÒNG DƯỚI ĐÂY: Sử dụng new string[]
            radioLoai.SetSource(new string[] { "Thuc Pham", "Dien Tu", "My Pham", "Gia Dung", "Thoi Trang" });

            if (_isEdit)
            {
                string loaiStr = sp.GetType().Name;
                radioLoai.SelectedItem = loaiStr switch { "HangThucPham" => 0, "HangDienTu" => 1, "HangMyPham" => 2, "HangGiaDung" => 3, "HangThoiTrang" => 4, _ => 0 };
            }
            else
            {
                radioLoai.SelectedItem = 0;
            }

            var btnSave = new Button("Luu Lai") { X = Pos.Center() - 10, Y = 20, IsDefault = true };
            var btnBack = new Button("Quay lai") { X = Pos.Center() + 4, Y = 20 };

            btnBack.Clicked += () => Application.RequestStop();
            btnSave.Clicked += () => SaveProduct(
                txtMa.Text.ToString(), txtMaVach.Text.ToString(), txtTen.Text.ToString(), txtNSX.Text.ToString(),
                txtSL.Text.ToString(), txtGia.Text.ToString(), txtTonMin.Text.ToString(), txtHSD.Text.ToString(),
                radioLoai.SelectedItem);

            Add(new Label("Ma hang:") { X = 2, Y = 2 }, txtMa,
                new Label("Ma vach:") { X = 2, Y = 4 }, txtMaVach,
                new Label("Ten SP:") { X = 2, Y = 6 }, txtTen,
                new Label("Nha SX:") { X = 2, Y = 8 }, txtNSX,
                new Label("So luong:") { X = 2, Y = 10 }, txtSL,
                new Label("Don gia:") { X = 2, Y = 12 }, txtGia,
                new Label("Ton Toi Thieu:") { X = 2, Y = 14 }, txtTonMin,
                new Label("HSD (dd/MM/yyyy):") { X = 2, Y = 16 }, txtHSD,
                new Label("Loai hang:") { X = 2, Y = 18 }, radioLoai,
                btnSave, btnBack);
        }

        private void SaveProduct(string ma, string maVach, string ten, string nsx, string slText, string giaText, string minText, string hsdText, int loaiIndex)
        {
            try
            {
                int sl = int.Parse(slText);
                decimal gia = decimal.Parse(giaText);
                int min = int.Parse(minText);

                DateTime? hsd = null;
                if (!string.IsNullOrWhiteSpace(hsdText))
                    hsd = DateTime.ParseExact(hsdText, "dd/MM/yyyy", null);

                string l = loaiIndex switch { 0 => "ThucPham", 1 => "DienTu", 2 => "MyPham", 3 => "GiaDung", 4 => "ThoiTrang", _ => "ThucPham" };

                HangHoa h = l switch
                {
                    "ThucPham" => new HangThucPham(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "DienTu" => new HangDienTu(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "MyPham" => new HangMyPham(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "GiaDung" => new HangGiaDung(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "ThoiTrang" => new HangThoiTrang(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    _ => null
                };

                if (_isEdit) _db.SuaHang(h); else _db.NhapHang(h, l);

                IsSaved = true;
                Application.RequestStop();
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Loi Nhap Lieu", ex.Message, "OK");
            }
        }
    }
}