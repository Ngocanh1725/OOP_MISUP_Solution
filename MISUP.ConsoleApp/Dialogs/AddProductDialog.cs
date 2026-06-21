using MISUP.BLL.Services;
using MISUP.Models;
using NStack;
using System;
using Terminal.Gui;
using Application = Terminal.Gui.Application;
using Attribute = Terminal.Gui.Attribute; // Thêm dòng này để xử lý màu sắc

namespace MISUP.ConsoleApp.Dialogs
{
    public class AddProductDialog : Dialog
    {
        private HangHoaBLL _db;
        private bool _isEdit;
        public bool IsSaved { get; private set; } = false;

        public AddProductDialog(HangHoaBLL db, HangHoa sp = null)
            : base((ustring)(sp == null ? "Thêm Sản Phẩm" : "Sửa Sản Phẩm"), 65, 26) // TĂNG CHIỀU CAO TỪ 22 LÊN 26 ĐỂ KHÔNG BỊ CHE NÚT BẤM
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

            var radioLoai = new ComboBox() { X = 18, Y = 18, Width = 35, Height = 6 }; // Tăng độ cao cho menu thả xuống

            radioLoai.SetSource(new string[] { "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });

            if (_isEdit)
            {
                string loaiStr = sp.GetType().Name;
                radioLoai.SelectedItem = loaiStr switch { "HangThucPham" => 0, "HangDienTu" => 1, "HangMyPham" => 2, "HangGiaDung" => 3, "HangThoiTrang" => 4, _ => 0 };
            }
            else
            {
                radioLoai.SelectedItem = 0;
            }

            // Tạo bộ màu (hiệu ứng) riêng cho các nút bấm
            var btnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            // Đẩy vị trí Y xuống 22 để nút nằm gọn gàng bên trong hộp thoại
            var btnSave = new Button("Lưu lại") { X = Pos.Center() - 10, Y = 22, IsDefault = true, ColorScheme = btnScheme };
            var btnBack = new Button("Hủy bỏ") { X = Pos.Center() + 4, Y = 22, ColorScheme = btnScheme };

            btnBack.Clicked += () => Application.RequestStop();
            btnSave.Clicked += () => SaveProduct(
                txtMa.Text.ToString(), txtMaVach.Text.ToString(), txtTen.Text.ToString(), txtNSX.Text.ToString(),
                txtSL.Text.ToString(), txtGia.Text.ToString(), txtTonMin.Text.ToString(), txtHSD.Text.ToString(),
                radioLoai.SelectedItem);

            Add(new Label("Mã hàng:") { X = 2, Y = 2 }, txtMa,
                new Label("Mã vạch:") { X = 2, Y = 4 }, txtMaVach,
                new Label("Tên SP:") { X = 2, Y = 6 }, txtTen,
                new Label("Nhà SX:") { X = 2, Y = 8 }, txtNSX,
                new Label("Số lượng:") { X = 2, Y = 10 }, txtSL,
                new Label("Đơn giá:") { X = 2, Y = 12 }, txtGia,
                new Label("Tồn tối thiểu:") { X = 2, Y = 14 }, txtTonMin,
                new Label("HSD (dd/MM/yyyy):") { X = 2, Y = 16 }, txtHSD,
                new Label("Loại hàng:") { X = 2, Y = 18 }, radioLoai,
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

                // Khai báo kiểu nullable (HangHoa?) để C# biết biến này có thể nhận giá trị null
                HangHoa? h = l switch
                {
                    "ThucPham" => new HangThucPham(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "DienTu" => new HangDienTu(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "MyPham" => new HangMyPham(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "GiaDung" => new HangGiaDung(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "ThoiTrang" => new HangThoiTrang(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    _ => null
                };

                // Kiểm tra null an toàn trước khi gọi Database
                if (h != null)
                {
                    if (_isEdit) _db.SuaHang(h); else _db.NhapHang(h, l);

                    IsSaved = true;
                    Application.RequestStop();
                }
                else
                {
                    MessageBox.ErrorQuery("Lỗi", "Hệ thống không nhận dạng được loại sản phẩm này!", "OK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Lỗi Nhập Liệu", ex.Message, "OK");
            }
        }
    }
}