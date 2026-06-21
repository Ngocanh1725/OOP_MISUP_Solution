using MISUP.Models;
using System;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class AddSupplierDialog : Dialog
    {
        public bool IsSaved { get; private set; } = false;
        public NhaCungCap ResultNCC { get; private set; }

        // TĂNG CHIỀU CAO LÊN 22 ĐỂ NÚT LƯU VÀ HỦY KHÔNG BỊ ĐÈ MẤT CHỮ
        public AddSupplierDialog(NhaCungCap ncc = null)
            : base(ncc == null ? "Thêm Đối Tác / Nhà Cung Cấp" : "Sửa Đối Tác / Nhà Cung Cấp", 60, 22)
        {
            ColorScheme = ThemeManager.HackerScheme;
            bool isEdit = (ncc != null);

            var txtMa = new TextField(isEdit ? ncc.MaNCC : "") { X = 18, Y = 2, Width = 35, ReadOnly = isEdit, ColorScheme = ThemeManager.InputScheme };
            var txtTen = new TextField(isEdit ? ncc.TenNCC : "") { X = 18, Y = 4, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtSdt = new TextField(isEdit ? ncc.DienThoai : "") { X = 18, Y = 6, Width = 35, ColorScheme = ThemeManager.InputScheme };

            var cmbTrangThai = new ComboBox() { X = 18, Y = 8, Width = 35, Height = 4 };
            cmbTrangThai.SetSource(new string[] { "Đang giao dịch", "Ngừng GD" });
            cmbTrangThai.SelectedItem = (isEdit && ncc.TrangThai == "Ngừng GD") ? 1 : 0;

            // Hiệu ứng màu nổi bật cho 2 nút thao tác
            var btnScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Cyan, Color.Black),
                Focus = new Attribute(Color.Black, Color.Cyan),
                HotNormal = new Attribute(Color.BrightCyan, Color.Black),
                HotFocus = new Attribute(Color.Black, Color.BrightCyan)
            };

            // Đặt Y = 16 để nút có khoảng cách an toàn, tránh bị mép Dialog cắt mất
            var btnSave = new Button("Lưu lại") { X = Pos.Center() - 10, Y = 16, IsDefault = true, ColorScheme = btnScheme };
            var btnBack = new Button("Hủy bỏ") { X = Pos.Center() + 4, Y = 16, ColorScheme = btnScheme };

            btnBack.Clicked += () => Application.RequestStop();

            btnSave.Clicked += () => {
                string ma = txtMa.Text.ToString().Trim();
                string ten = txtTen.Text.ToString().Trim();
                string sdt = txtSdt.Text.ToString().Trim();
                string tt = cmbTrangThai.SelectedItem == 1 ? "Ngừng GD" : "Đang giao dịch";

                // Bảo toàn trường tổng mua/công nợ nếu là chỉnh sửa
                decimal mua = isEdit ? ncc.TongMua : 0;
                decimal no = isEdit ? ncc.CongNo : 0;

                ResultNCC = new NhaCungCap(ma, ten, sdt, mua, no, tt);
                IsSaved = true;
                Application.RequestStop();
            };

            Add(new Label("Mã NCC:") { X = 2, Y = 2 }, txtMa,
                new Label("Tên Công Ty:") { X = 2, Y = 4 }, txtTen,
                new Label("Điện Thoại:") { X = 2, Y = 6 }, txtSdt,
                new Label("Trạng Thái:") { X = 2, Y = 8 }, cmbTrangThai,
                btnSave, btnBack);
        }
    }
}