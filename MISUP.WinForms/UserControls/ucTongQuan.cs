using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        private void LoadDashboardData()
        {
            try
            {
                // 1. GỌI DỮ LIỆU TỪ TẦNG BLL
                int tongSKU = _db.DemTongSoMatHang();
                decimal tongGiaTri = _db.TinhTongGiaTriKho();

                // Lấy danh sách hàng cần lưu ý
                List<HangHoa> hangSapHetTon = _db.LayHangSapHetTonKho();
                List<HangHoa> hangHetHan = _db.LayHangHetHan();
                List<HangHoa> hangCanDate = _db.LayHangCanDate(30); // Cận date trong 30 ngày tới

                // 2. GÁN LÊN CÁC THẺ CARD (Hiển thị số liệu tổng quát)
                lblTongSKUValue.Text = tongSKU.ToString("N0");
                lblTongGiaTriValue.Text = tongGiaTri.ToString("N0") + " đ";

                // Tổng hợp hàng sắp hết tồn kho (Cảnh báo cam)
                lblCanhBaoTonValue.Text = hangSapHetTon.Count.ToString();

                // Tổng hợp số lượng hàng có vấn đề về HSD (Cảnh báo đỏ)
                lblCanhBaoHSDValue.Text = (hangHetHan.Count + hangCanDate.Count).ToString();

                // 3. ĐỔ DỮ LIỆU CHI TIẾT LÊN DATAGRIDVIEW
                // Gộp chung 3 danh sách cần cảnh báo lại thành 1 list duy nhất để hiển thị
                var danhSachCanhBaoTongHop = new List<HangHoa>();
                danhSachCanhBaoTongHop.AddRange(hangSapHetTon);
                danhSachCanhBaoTongHop.AddRange(hangHetHan);
                danhSachCanhBaoTongHop.AddRange(hangCanDate);

                // Loại bỏ các sản phẩm trùng lặp (ví dụ 1 SP vừa sắp hết tồn, vừa sắp hết hạn)
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

                // Tinh chỉnh hiển thị DataGridView
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

                // Gắn sự kiện để tô màu chữ đỏ/cam cho các cột cảnh báo
                dgvCanhBao.CellFormatting += DgvCanhBao_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Dashboard: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đổi màu cảnh báo tự động
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
                else if (columnName == "Kho")
                {
                    int sl = Convert.ToInt32(e.Value);
                    if (sl == 0) e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private string GetLoaiViewString(HangHoa h) => h switch { HangThucPham _ => "Thực Phẩm", HangDienTu _ => "Điện Tử", HangMyPham _ => "Mỹ Phẩm", HangGiaDung _ => "Gia Dụng", HangThoiTrang _ => "Thời Trang", _ => "Khác" };
    }
}