using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;
using MISUP.WinForms.Forms;
using MISUP.WinForms.Utils;

namespace MISUP.WinForms
{
    public partial class ucSanPham : UserControl
    {
        private HangHoaBLL db = new HangHoaBLL();
        private string currentTab = "";
        private string currentSort = ""; // Cờ lưu trạng thái sắp xếp: "asc", "desc" hoặc ""
        private Button[] tabButtons;

        public ucSanPham()
        {
            InitializeComponent();
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AllowUserToOrderColumns = false;
            tabButtons = new Button[] { btnTabTatCa, btnTabThucPham, btnTabDienTu, btnTabMyPham, btnTabGiaDung, btnTabThoiTrang };
            AttachEvents();
            LoadData();
        }

        private void AttachEvents()
        {
            // Các nút chức năng
            btnThem.Click += (s, e) => ShowProductDialog(null);
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnXuatExcel.Click += (s, e) => ExportHelper.ExportToCSV(db.LayDanhSach());

            // Tìm kiếm
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.Contains("Tìm kiếm")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm kiếm mã, tên"; txtTimKiem.ForeColor = Color.Gray; } };

            btnTim.Click += (s, e) => ReloadCurrentState();
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) ReloadCurrentState(); };

            // Bắt sự kiện Sắp xếp Số lượng
            btnSortAsc.Click += (s, e) => { currentSort = "asc"; ReloadCurrentState(); };
            btnSortDesc.Click += (s, e) => { currentSort = "desc"; ReloadCurrentState(); };

            // Gắn sự kiện Click cho từng Tab phân loại
            foreach (var btn in tabButtons)
            {
                btn.Click += Tab_Click;
            }

            dgvData.CellDoubleClick += DgvData_CellDoubleClick;
            dgvData.CellPainting += DgvData_CellPainting;

            // Vẽ viền cho Card
            pnlCard.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, pnlCard.ClientRectangle, Color.FromArgb(226, 232, 240), ButtonBorderStyle.Solid); };
        }

        private void Tab_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;

            foreach (var btn in tabButtons)
            {
                btn.ForeColor = Color.Gray;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            }

            clickedBtn.ForeColor = Color.FromArgb(0, 136, 255);
            clickedBtn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            string t = clickedBtn.Text;
            currentTab = t == "Tất cả" ? "" : GetLoaiDbString(t);
            ReloadCurrentState();
        }

        private void ReloadCurrentState()
        {
            string keyword = txtTimKiem.Text.Replace("🔍 Tìm kiếm mã, tên", "").Trim();
            LoadData(keyword, currentTab, currentSort);
        }

        private void LoadData(string keyword = "", string loai = "", string sort = "")
        {
            // 1. Lấy dữ liệu cơ bản (Có lọc Tab luôn)
            var list = string.IsNullOrEmpty(loai) ? db.LayDanhSach() : db.LocTheoLoai(loai);

            // 2. Lọc theo từ khóa (Tìm trong danh sách hiện tại bằng LINQ để không mất Tab)
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(h => h.MaHang.ToLower().Contains(keyword.ToLower()) ||
                                       h.TenHang.ToLower().Contains(keyword.ToLower()) ||
                                       (!string.IsNullOrEmpty(h.MaVach) && h.MaVach.Contains(keyword))).ToList();
            }

            // 3. Xử lý Sắp xếp Tăng/Giảm theo Số lượng
            if (sort == "asc") list = list.OrderBy(h => h.SoLuongNhap).ToList();
            else if (sort == "desc") list = list.OrderByDescending(h => h.SoLuongNhap).ToList();

            // 4. Đổ dữ liệu lên Grid
            dgvData.DataSource = list.Select(h => new {
                MaHang = h.MaHang,
                TenHang = h.TenHang,
                DVT = h.DonViTinh,
                NhaCungCap = h.NhaSanXuat,
                Kho = h.SoLuongNhap,
                GiaNhap = h.DonGia.ToString("N0") + "đ",
                TrangThaiTon = h.SoLuongNhap == 0 ? "Hết hàng" : (h.SoLuongNhap <= h.TonKhoToiThieu ? "Cần nhập" : "Ổn định"),
                TrangThaiHSD = h.IsHetHan() ? "Đã hết hạn" : (h.IsSapHetHan(30) ? "Cận Date" : (h.HanSuDung.HasValue ? "Tốt" : "-"))
            }).ToList();

            if (dgvData.Columns.Count > 0)
            {
                dgvData.Columns["MaHang"].HeaderText = "Mã hàng";
                dgvData.Columns["TenHang"].HeaderText = "Tên sản phẩm";
                dgvData.Columns["DVT"].HeaderText = "ĐVT";
                dgvData.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
                dgvData.Columns["Kho"].HeaderText = "Tồn kho";
                dgvData.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvData.Columns["TrangThaiTon"].HeaderText = "Trạng thái";
                dgvData.Columns["TrangThaiHSD"].HeaderText = "Hạn sử dụng";

                dgvData.Columns["TenHang"].FillWeight = 200;
                dgvData.Columns["DVT"].FillWeight = 60;
                dgvData.Columns["TrangThaiTon"].FillWeight = 110;
                dgvData.Columns["TrangThaiHSD"].FillWeight = 110;
            }

            // 5. Cập nhật Dashboard Thống Kê
            lblTongSP.Text = db.DemTongSoMatHang().ToString("N0");
            lblTongGiaTri.Text = db.TinhTongGiaTriKho().ToString("N0") + " đ";
            lblCanhBaoTon.Text = db.LayHangSapHetTonKho().Count.ToString() + " SP";
        }

        // Vẽ Huy hiệu (Badge) Bo tròn
        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null)
            {
                string colName = dgvData.Columns[e.ColumnIndex].Name;
                if (colName == "TrangThaiTon" || colName == "TrangThaiHSD")
                {
                    e.PaintBackground(e.CellBounds, true);

                    string text = e.Value.ToString();
                    if (text == "-") { e.PaintContent(e.CellBounds); return; }

                    Color bgColor, textColor, borderColor;

                    if (text.Contains("Hết hàng") || text.Contains("Đã hết hạn"))
                    {
                        bgColor = Color.FromArgb(255, 235, 238); textColor = Color.FromArgb(211, 47, 47); borderColor = Color.FromArgb(255, 205, 210);
                    }
                    else if (text.Contains("Cần nhập") || text.Contains("Cận Date"))
                    {
                        bgColor = Color.FromArgb(255, 244, 229); textColor = Color.FromArgb(255, 152, 0); borderColor = Color.FromArgb(255, 224, 178);
                    }
                    else
                    {
                        bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201);
                    }

                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    SizeF textSize = g.MeasureString(text, e.CellStyle.Font);
                    int badgeWidth = (int)textSize.Width + 20;
                    int badgeHeight = 26;
                    int x = e.CellBounds.Left + 15;
                    int y = e.CellBounds.Top + (e.CellBounds.Height - badgeHeight) / 2;

                    Rectangle badgeRect = new Rectangle(x, y, badgeWidth, badgeHeight);
                    using (GraphicsPath path = GetRoundedRect(badgeRect, 13))
                    {
                        using (SolidBrush brush = new SolidBrush(bgColor)) g.FillPath(brush, path);
                        using (Pen pen = new Pen(borderColor, 1)) g.DrawPath(pen, path);
                    }

                    TextRenderer.DrawText(g, text, new Font("Segoe UI", 9, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2; Size size = new Size(diameter, diameter); Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();
            if (radius == 0) { path.AddRectangle(bounds); return path; }
            path.AddArc(arc, 180, 90); arc.X = bounds.Right - diameter; path.AddArc(arc, 270, 90); arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90); arc.X = bounds.Left; path.AddArc(arc, 90, 90); path.CloseFigure();
            return path;
        }

        private string GetLoaiDbString(string viewStr) => viewStr switch { "Thực Phẩm" => "ThucPham", "Điện Tử" => "DienTu", "Mỹ Phẩm" => "MyPham", "Gia Dụng" => "GiaDung", "Thời Trang" => "ThoiTrang", _ => "ThucPham" };

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn 1 dòng!"); return; }
            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try { db.XoaHang(dgvData.SelectedRows[0].Cells["MaHang"].Value.ToString()); ReloadCurrentState(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn 1 dòng để sửa!"); return; }
            string ma = dgvData.SelectedRows[0].Cells["MaHang"].Value.ToString();
            HangHoa sp = db.LayDanhSach().FirstOrDefault(x => x.MaHang == ma);
            if (sp != null) ShowProductDialog(sp);
        }

        private void DgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ma = dgvData.Rows[e.RowIndex].Cells["MaHang"].Value.ToString();
                HangHoa sp = db.LayDanhSach().FirstOrDefault(x => x.MaHang == ma);
                if (sp != null) ShowProductDialog(sp);
            }
        }

        private void ShowProductDialog(HangHoa sp = null)
        {
            using (ProductDialog dialog = new ProductDialog(db, sp))
            {
                if (dialog.ShowDialog() == DialogResult.OK) ReloadCurrentState();
            }
        }
    }
}