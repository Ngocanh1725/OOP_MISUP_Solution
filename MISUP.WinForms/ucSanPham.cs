using System;
using System.Linq;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;
using MISUP.WinForms.Forms;   // Import thư mục Forms
using MISUP.WinForms.Utils;   // Import thư mục Utils

namespace MISUP.WinForms
{
    public partial class ucSanPham : UserControl
    {
        private HangHoaBLL db = new HangHoaBLL(); // Tầng Nghiệp vụ

        public ucSanPham()
        {
            InitializeComponent();
            AttachEvents();
        }

        private void AttachEvents()
        {
            this.Load += (s, e) => LoadData();
            btnThem.Click += (s, e) => ShowProductDialog(null);
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnXuatExcel.Click += (s, e) => ExportHelper.ExportToCSV(db.LayDanhSach()); // Gọi class tiện ích
            btnTim.Click += (s, e) => LoadData(txtTimKiem.Text);
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadData(txtTimKiem.Text); };
            cmbLocLoai.SelectedIndex = 0;
            cmbLocLoai.SelectedIndexChanged += (s, e) => LoadData("", cmbLocLoai.SelectedIndex == 0 ? "" : GetLoaiDbString(cmbLocLoai.SelectedItem.ToString()));
        }

        private void LoadData(string keyword = "", string loai = "")
        {
            var list = string.IsNullOrEmpty(loai) ? db.LayDanhSach() : db.LocTheoLoai(loai);
            if (!string.IsNullOrEmpty(keyword)) list = db.TimKiem(keyword);

            dgvData.DataSource = list.Select(h => new {
                MaHang = h.MaHang,
                TenHang = h.TenHang,
                NhaSanXuat = h.NhaSanXuat,
                Kho = h.SoLuongNhap,
                Gia = h.DonGia.ToString("N0") + " đ",
                TongGT = h.TinhTongGiaTriSauThue().ToString("N0") + " đ",
                Loai = GetLoaiViewString(h)
            }).ToList();

            // Cập nhật thẻ Dashboard
            lblTongSP.Text = db.DemTongSoMatHang().ToString("N0");
            lblTongGiaTri.Text = db.TinhTongGiaTriKho().ToString("N0") + "đ";
            lblCanhBaoTon.Text = db.LayHangSapHetTonKho().Count.ToString() + " SP";
        }

        private string GetLoaiDbString(string viewStr) => viewStr switch { "Thực Phẩm" => "ThucPham", "Điện Tử" => "DienTu", "Mỹ Phẩm" => "MyPham", "Gia Dụng" => "GiaDung", "Thời Trang" => "ThoiTrang", _ => "ThucPham" };
        private string GetLoaiViewString(HangHoa h) => h switch { HangThucPham _ => "Thực Phẩm", HangDienTu _ => "Điện Tử", HangMyPham _ => "Mỹ Phẩm", HangGiaDung _ => "Gia Dụng", HangThoiTrang _ => "Thời Trang", _ => "Khác" };

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn 1 dòng!"); return; }
            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try { db.XoaHang(dgvData.SelectedRows[0].Cells["MaHang"].Value.ToString()); LoadData(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) return;
            string ma = dgvData.SelectedRows[0].Cells["MaHang"].Value.ToString(), ten = dgvData.SelectedRows[0].Cells["TenHang"].Value.ToString();
            string nsx = dgvData.SelectedRows[0].Cells["NhaSanXuat"].Value.ToString(), loai = dgvData.SelectedRows[0].Cells["Loai"].Value.ToString();
            int sl = Convert.ToInt32(dgvData.SelectedRows[0].Cells["Kho"].Value);
            decimal gia = Convert.ToDecimal(dgvData.SelectedRows[0].Cells["Gia"].Value.ToString().Replace(" đ", "").Replace(",", ""));

            HangHoa sp = GetLoaiDbString(loai) switch { "ThucPham" => new HangThucPham(ma, ten, nsx, sl, gia), "DienTu" => new HangDienTu(ma, ten, nsx, sl, gia), "MyPham" => new HangMyPham(ma, ten, nsx, sl, gia), "GiaDung" => new HangGiaDung(ma, ten, nsx, sl, gia), "ThoiTrang" => new HangThoiTrang(ma, ten, nsx, sl, gia), _ => null };
            ShowProductDialog(sp);
        }

        private void ShowProductDialog(HangHoa sp = null)
        {
            // Gọi form đã được tách riêng rẽ từ thư mục Forms
            using (ProductDialog dialog = new ProductDialog(db, sp))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }
    }
}