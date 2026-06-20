using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;

namespace MISUP.WinForms
{
    public partial class ucTongQuan : UserControl
    {
        private HangHoaBLL _db = new HangHoaBLL();

        public ucTongQuan()
        {
            InitializeComponent();
            LoadDashboardData();

            // Bo tròn các Card
            SetRoundedRegion(cardSKU, 15);
            SetRoundedRegion(cardGiaTri, 15);
            SetRoundedRegion(cardCanhBaoTon, 15);
            SetRoundedRegion(cardCanhBaoHSD, 15);
            SetRoundedRegion(pnlCanhBao, 15);
        }

        // Hàm tiện ích cắt góc bo tròn cho Panel
        private void SetRoundedRegion(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);

            // Xử lý lại khi control thay đổi kích thước
            control.Resize += (s, e) =>
            {
                GraphicsPath newPath = new GraphicsPath();
                newPath.AddArc(0, 0, radius, radius, 180, 90);
                newPath.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                newPath.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                newPath.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                newPath.CloseFigure();
                control.Region = new Region(newPath);
            };
        }

        private void LoadDashboardData()
        {
            try
            {
                int tongSKU = _db.DemTongSoMatHang();
                decimal tongGiaTri = _db.TinhTongGiaTriKho();

                List<HangHoa> hangSapHetTon = _db.LayHangSapHetTonKho();
                List<HangHoa> hangHetHan = _db.LayHangHetHan();
                List<HangHoa> hangCanDate = _db.LayHangCanDate(30);

                lblTongSKUValue.Text = tongSKU.ToString("N0");
                lblTongGiaTriValue.Text = tongGiaTri.ToString("N0") + " đ";
                lblCanhBaoTonValue.Text = hangSapHetTon.Count.ToString();
                lblCanhBaoHSDValue.Text = (hangHetHan.Count + hangCanDate.Count).ToString();

                var danhSachCanhBaoTongHop = new List<HangHoa>();
                danhSachCanhBaoTongHop.AddRange(hangSapHetTon);
                danhSachCanhBaoTongHop.AddRange(hangHetHan);
                danhSachCanhBaoTongHop.AddRange(hangCanDate);

                var danhSachHienThi = danhSachCanhBaoTongHop.Distinct().Select(h => new
                {
                    MaHang = h.MaHang,
                    TenHang = h.TenHang,
                    Loai = GetLoaiViewString(h),
                    Kho = h.SoLuongNhap,
                    TrangThaiTon = h.SoLuongNhap == 0 ? "Hết hàng" : (h.SoLuongNhap <= h.TonKhoToiThieu ? "Cần nhập gấp" : "Ổn định"),
                    HanSuDung = h.HanSuDung.HasValue ? h.HanSuDung.Value.ToString("dd/MM/yyyy") : "-",
                    TrangThaiHSD = h.IsHetHan() ? "Đã hết hạn" : (h.IsSapHetHan(30) ? "Cận Date" : "-")
                }).ToList();

                dgvCanhBao.DataSource = danhSachHienThi;

                if (dgvCanhBao.Columns.Count > 0)
                {
                    dgvCanhBao.Columns["MaHang"].HeaderText = "Mã SKU";
                    dgvCanhBao.Columns["TenHang"].HeaderText = "Tên Sản phẩm";
                    dgvCanhBao.Columns["Loai"].HeaderText = "Nhóm hàng";
                    dgvCanhBao.Columns["Kho"].HeaderText = "Tồn kho";
                    dgvCanhBao.Columns["TrangThaiTon"].HeaderText = "Vấn đề Tồn kho";
                    dgvCanhBao.Columns["HanSuDung"].HeaderText = "HSD";
                    dgvCanhBao.Columns["TrangThaiHSD"].HeaderText = "Vấn đề HSD";
                    dgvCanhBao.Columns["TenHang"].FillWeight = 200;
                }

                dgvCanhBao.CellFormatting += DgvCanhBao_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Dashboard: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCanhBao_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null)
            {
                string columnName = dgvCanhBao.Columns[e.ColumnIndex].Name;
                string valueStr = e.Value.ToString();

                if (columnName == "TrangThaiTon")
                {
                    if (valueStr == "Hết hàng") e.CellStyle.ForeColor = Color.Red;
                    else if (valueStr == "Cần nhập gấp") e.CellStyle.ForeColor = Color.DarkOrange;
                }
                else if (columnName == "TrangThaiHSD")
                {
                    if (valueStr == "Đã hết hạn") { e.CellStyle.ForeColor = Color.Red; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }
                    else if (valueStr == "Cận Date") e.CellStyle.ForeColor = Color.DarkOrange;
                }
            }
        }

        private string GetLoaiViewString(HangHoa h) => h switch { HangThucPham _ => "Thực Phẩm", HangDienTu _ => "Điện Tử", HangMyPham _ => "Mỹ Phẩm", HangGiaDung _ => "Gia Dụng", HangThoiTrang _ => "Thời Trang", _ => "Khác" };
    }
}