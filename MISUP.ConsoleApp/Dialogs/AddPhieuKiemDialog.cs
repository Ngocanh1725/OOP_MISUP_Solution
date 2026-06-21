using MISUP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp.Dialogs
{
    public class AddPhieuKiemDialog : Dialog
    {
        public bool IsSaved { get; private set; } = false;
        public PhieuKiem ResultPhieu { get; private set; }

        // CHIỀU CAO 22 ĐỂ NÚT LƯU/HỦY KHÔNG BỊ CHÈN KHUẤT
        public AddPhieuKiemDialog(PhieuKiem pk = null)
            : base(pk == null ? "Lập Phiếu Kiểm Kê" : "Sửa Phiếu Kiểm Kê", 65, 22)
        {
            ColorScheme = ThemeManager.HackerScheme;
            bool isEdit = (pk != null);

            var txtMa = new TextField(isEdit ? pk.MaKK : "") { X = 20, Y = 2, Width = 35, ReadOnly = isEdit, ColorScheme = ThemeManager.InputScheme };
            var txtNgay = new TextField(isEdit ? pk.NgayKiem.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy")) { X = 20, Y = 4, Width = 35, ColorScheme = ThemeManager.InputScheme };
            var txtNhanVien = new TextField(isEdit ? pk.NhanVien : "Admin") { X = 20, Y = 6, Width = 35, ColorScheme = ThemeManager.InputScheme };

            var cmbKho = new ComboBox() { X = 20, Y = 8, Width = 35, Height = 4 };
            cmbKho.SetSource(new string[] { "Kho Tổng HN", "Kho Miền Trung", "Kho Miền Nam" });
            if (isEdit)
            {
                if (pk.KhoKiem == "Kho Miền Trung") cmbKho.SelectedItem = 1;
                else if (pk.KhoKiem == "Kho Miền Nam") cmbKho.SelectedItem = 2;
                else cmbKho.SelectedItem = 0;
            }
            else
            {
                cmbKho.SelectedItem = 0;
            }

            var txtChenhLech = new TextField(isEdit ? pk.SLChenhLech.ToString() : "0") { X = 20, Y = 10, Width = 35, ColorScheme = ThemeManager.InputScheme };

            var cmbTrangThai = new ComboBox() { X = 20, Y = 12, Width = 35, Height = 4 };
            cmbTrangThai.SetSource(new string[] { "Đang xử lý", "Đã cân bằng", "Đã hủy" });

            if (isEdit)
            {
                if (pk.TrangThai == "Đã cân bằng") cmbTrangThai.SelectedItem = 1;
                else if (pk.TrangThai == "Đã hủy") cmbTrangThai.SelectedItem = 2;
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
                    DateTime ngay = DateTime.ParseExact(txtNgay.Text.ToString().Trim(), "dd/MM/yyyy", null);
                    string nv = txtNhanVien.Text.ToString().Trim();

                    string kho = "Kho Tổng HN";
                    if (cmbKho.SelectedItem == 1) kho = "Kho Miền Trung";
                    else if (cmbKho.SelectedItem == 2) kho = "Kho Miền Nam";

                    int cl = int.Parse(txtChenhLech.Text.ToString().Trim());

                    string tt = "Đang xử lý";
                    if (cmbTrangThai.SelectedItem == 1) tt = "Đã cân bằng";
                    else if (cmbTrangThai.SelectedItem == 2) tt = "Đã hủy";

                    ResultPhieu = new PhieuKiem(ma, ngay, nv, kho, cl, tt);
                    IsSaved = true;
                    Application.RequestStop();
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Lỗi nhập liệu", "Vui lòng kiểm tra lại định dạng Ngày (dd/MM/yyyy) hoặc Số chênh lệch!", "OK");
                }
            };

            Add(new Label("Mã Kiểm Kê:") { X = 2, Y = 2 }, txtMa,
                new Label("Ngày Kiểm:") { X = 2, Y = 4 }, txtNgay,
                new Label("Nhân Viên:") { X = 2, Y = 6 }, txtNhanVien,
                new Label("Kho Kiểm:") { X = 2, Y = 8 }, cmbKho,
                new Label("Số Chênh Lệch:") { X = 2, Y = 10 }, txtChenhLech,
                new Label("Trạng Thái:") { X = 2, Y = 12 }, cmbTrangThai,
                btnSave, btnBack);
        }
    }
}