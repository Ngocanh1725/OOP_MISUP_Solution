using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NStack;
using Terminal.Gui;
using MISUP.Models;
namespace MISUP.ConsoleApp
{// Lớp là một khuôn mẫu (bản thiết kế) định nghĩa các đặc điểm
 // và hành vi chung cho một nhóm các thực thể.
    public class AddProductDialog : Dialog
    {
        private IQuanLyHangHoa _db;
        private bool _isEdit;
        public bool IsSaved { get; private set; } = false;

        public AddProductDialog(IQuanLyHangHoa db, HangHoa sp = null) : base(sp == null ? "Thêm Sản Phẩm" : "Sửa Sản Phẩm", 60, 20)
        {
            _db = db; _isEdit = sp != null; this.ColorScheme = ThemeManager.HackerScheme;

            var txtMa = new TextField(_isEdit ? sp.MaHang : "") { X = 15, Y = 2, Width = 35, ReadOnly = _isEdit, ColorScheme = ThemeManager.InputScheme };
            var txtTen = new TextField(_isEdit ? sp.TenHang : "") { X = 15, Y = 4, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtNSX = new TextField(_isEdit ? sp.NhaSanXuat : "") { X = 15, Y = 6, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtSL = new TextField(_isEdit ? sp.SoLuongNhap.ToString() : "") { X = 15, Y = 8, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtGia = new TextField(_isEdit ? sp.DonGia.ToString() : "") { X = 15, Y = 10, Width = 35, ColorScheme = ThemeManager.InputScheme };

            var radioLoai = new RadioGroup(new ustring[] { "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" }) { X = 15, Y = 12 };
            if (_isEdit) radioLoai.SelectedItem = sp switch { HangThucPham _ => 0, HangDienTu _ => 1, HangMyPham _ => 2, HangGiaDung _ => 3, HangThoiTrang _ => 4, _ => 0 };

            var btnSave = new Button("Lưu Lại", is_default: true) { X = Pos.Center() - 12, Y = 18 };
            var btnBack = new Button("Quay lại") { X = Pos.Center() + 4, Y = 18 };

            btnBack.Clicked += () => Application.RequestStop();
            btnSave.Clicked += () => SaveProduct(txtMa.Text.ToString(), txtTen.Text.ToString(), txtNSX.Text.ToString(), txtSL.Text.ToString(), txtGia.Text.ToString(), radioLoai.SelectedItem);

            this.Add(new Label("Mã hàng:") { X = 2, Y = 2 }, txtMa, new Label("Tên SP:") { X = 2, Y = 4 }, txtTen,
                     new Label("Nhà SX:") { X = 2, Y = 6 }, txtNSX, new Label("Số lượng:") { X = 2, Y = 8 }, txtSL,
                     new Label("Đơn giá:") { X = 2, Y = 10 }, txtGia, new Label("Loại hàng:") { X = 2, Y = 12 }, radioLoai, btnSave, btnBack);
        }

        private void SaveProduct(string ma, string ten, string nsx, string slText, string giaText, int loaiIndex)
        {
            try
            {
                int sl = int.Parse(slText); decimal gia = decimal.Parse(giaText);
                HangHoa h = loaiIndex switch { 0 => new HangThucPham(ma, ten, nsx, sl, gia), 1 => new HangDienTu(ma, ten, nsx, sl, gia), 2 => new HangMyPham(ma, ten, nsx, sl, gia), 3 => new HangGiaDung(ma, ten, nsx, sl, gia), 4 => new HangThoiTrang(ma, ten, nsx, sl, gia), _ => null };

                if (_isEdit) _db.SuaHang(h); else _db.NhapHang(h, loaiIndex switch { 0 => "ThucPham", 1 => "DienTu", 2 => "MyPham", 3 => "GiaDung", 4 => "ThoiTrang", _ => "ThucPham" });

                IsSaved = true; Application.RequestStop();
            }
            catch (Exception ex) { MessageBox.ErrorQuery("Lỗi", ex.Message, "OK"); }
        }
    }
}