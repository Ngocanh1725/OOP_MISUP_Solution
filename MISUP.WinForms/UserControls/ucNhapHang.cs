using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MISUP.WinForms.Forms; // Thêm thư viện để gọi tới PhieuNhapDialog

namespace MISUP.WinForms
{
    public partial class ucNhapHang : UserControl
    {
        private DataTable dtPhieuNhap;

        public ucNhapHang()
        {
            InitializeComponent();
            cmbTrangThai.SelectedIndex = 0;
            AttachEvents();
            LoadMockData();

            // Cắt góc bo tròn cho Panel chính
            SetRoundedRegion(pnlCard, 15);
        }

        private void SetRoundedRegion(Control control, int radius)
        {
            control.Resize += (s, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            };
        }

        private void AttachEvents()
        {
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.Contains("Tìm")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm theo mã phiếu..."; txtTimKiem.ForeColor = Color.Gray; } };

            btnTim.Click += BtnTim_Click;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnInPhieu.Click += BtnInPhieu_Click;

            dgvData.CellPainting += DgvData_CellPainting;
        }

        private void LoadMockData()
        {
            dtPhieuNhap = new DataTable();
            dtPhieuNhap.Columns.Add("MaPhieu");
            dtPhieuNhap.Columns.Add("ThoiGian");
            dtPhieuNhap.Columns.Add("NhaCungCap");
            dtPhieuNhap.Columns.Add("ChiNhanh");
            dtPhieuNhap.Columns.Add("TongTien");
            dtPhieuNhap.Columns.Add("TrangThai");

            dtPhieuNhap.Rows.Add("PON0001", "20/06/2026 14:30", "Công ty CP Vinamilk", "Kho Tổng HN", "125,500,000", "Đã nhập kho");
            dtPhieuNhap.Rows.Add("PON0002", "19/06/2026 09:15", "Samsung Electronics", "Kho Miền Nam", "450,000,000", "Đã nhập kho");
            dtPhieuNhap.Rows.Add("PON0003", "21/06/2026 10:00", "Tập đoàn Sunhouse", "Kho Tổng HN", "85,200,000", "Đang vận chuyển");
            dtPhieuNhap.Rows.Add("PON0004", "18/06/2026 16:45", "Nhà PP Hàng Tiêu Dùng", "Kho Miền Trung", "12,000,000", "Đã hủy");
            dtPhieuNhap.Rows.Add("PON0005", "15/06/2026 11:20", "Công ty Thời Trang Yody", "Kho Tổng HN", "54,600,000", "Đã nhập kho");

            dgvData.DataSource = dtPhieuNhap;

            dgvData.Columns["MaPhieu"].HeaderText = "Mã Phiếu";
            dgvData.Columns["ThoiGian"].HeaderText = "Thời Gian";
            dgvData.Columns["NhaCungCap"].HeaderText = "Nhà Cung Cấp";
            dgvData.Columns["ChiNhanh"].HeaderText = "Chi Nhánh Nhận";
            dgvData.Columns["TongTien"].HeaderText = "Tổng Tiền Nhập";
            dgvData.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvData.Columns["NhaCungCap"].FillWeight = 200;
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            if (dtPhieuNhap == null) return;
            string keyword = txtTimKiem.Text.Contains("Tìm") ? "" : txtTimKiem.Text.Trim();
            string trangThai = cmbTrangThai.SelectedIndex == 0 ? "" : cmbTrangThai.Text;

            string filter = "1=1";
            if (!string.IsNullOrEmpty(keyword)) filter += $" AND MaPhieu LIKE '%{keyword}%'";
            if (!string.IsNullOrEmpty(trangThai)) filter += $" AND TrangThai = '{trangThai}'";

            dtPhieuNhap.DefaultView.RowFilter = filter;
        }

        // =========================================================================
        // NGHIỆP VỤ: THÊM / SỬA / XÓA BẰNG DIALOG
        // =========================================================================

        private void BtnThem_Click(object sender, EventArgs e)
        {
            using (var dialog = new PhieuNhapDialog())
            {
                dialog.IsEditMode = false;

                // Mở cửa sổ chi tiết nhập liệu
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Tự động sinh ID mới (D4 tức là chuỗi độ dài 4 kí tự, VD: 0006)
                    string newID = "PON" + (dtPhieuNhap.Rows.Count + 1).ToString("D4");

                    // Thêm dữ liệu từ Dialog vào bảng
                    dtPhieuNhap.Rows.InsertAt(dtPhieuNhap.NewRow(), 0);
                    dtPhieuNhap.Rows[0]["MaPhieu"] = newID;
                    dtPhieuNhap.Rows[0]["ThoiGian"] = dialog.ThoiGian;
                    dtPhieuNhap.Rows[0]["NhaCungCap"] = dialog.NhaCungCap;
                    dtPhieuNhap.Rows[0]["ChiNhanh"] = dialog.ChiNhanh;
                    dtPhieuNhap.Rows[0]["TongTien"] = dialog.TongTien;
                    dtPhieuNhap.Rows[0]["TrangThai"] = dialog.TrangThai;

                    MessageBox.Show($"Đã tạo Phiếu nhập mới: {newID}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu nhập ở bảng bên dưới để Sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dòng dữ liệu đang được chọn
            var row = dgvData.SelectedRows[0];
            string maPhieu = row.Cells["MaPhieu"].Value.ToString();

            using (var dialog = new PhieuNhapDialog())
            {
                // Bật chế độ Sửa, truyền dữ liệu cũ sang Dialog
                dialog.IsEditMode = true;
                dialog.MaPhieu = maPhieu;
                dialog.ThoiGian = row.Cells["ThoiGian"].Value.ToString();
                dialog.NhaCungCap = row.Cells["NhaCungCap"].Value.ToString();
                dialog.ChiNhanh = row.Cells["ChiNhanh"].Value.ToString();
                dialog.TongTien = row.Cells["TongTien"].Value.ToString();
                dialog.TrangThai = row.Cells["TrangThai"].Value.ToString();

                // Mở cửa sổ
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Sau khi lưu xong, cập nhật lại vào DataTable trên giao diện
                    DataRow[] dr = dtPhieuNhap.Select($"MaPhieu = '{maPhieu}'");
                    if (dr.Length > 0)
                    {
                        dr[0]["ThoiGian"] = dialog.ThoiGian;
                        dr[0]["NhaCungCap"] = dialog.NhaCungCap;
                        dr[0]["ChiNhanh"] = dialog.ChiNhanh;
                        dr[0]["TongTien"] = dialog.TongTien;
                        dr[0]["TrangThai"] = dialog.TrangThai;
                    }
                    MessageBox.Show($"Đã cập nhật thay đổi cho Phiếu: {maPhieu}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu nhập ở bảng bên dưới để Xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = dgvData.SelectedRows[0].Cells["MaPhieu"].Value.ToString();

            // Hiển thị Popup xác nhận
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa vĩnh viễn phiếu nhập '{id}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow[] dr = dtPhieuNhap.Select($"MaPhieu = '{id}'");
                if (dr.Length > 0)
                {
                    dtPhieuNhap.Rows.Remove(dr[0]);
                }
                MessageBox.Show("Đã xóa phiếu thành công khỏi hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // =========================================================================

        private void BtnInPhieu_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                string id = dgvData.SelectedRows[0].Cells["MaPhieu"].Value.ToString();
                MessageBox.Show($"Đang kết nối máy in để in mã vạch cho phiếu {id}...", "In ấn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu nhập để in mã vạch!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["TrangThai"].Index && e.Value != null)
            {
                e.PaintBackground(e.CellBounds, true);
                string text = e.Value.ToString();
                Color bgColor = Color.White, textColor = Color.Black, borderColor = Color.Gray;

                if (text == "Đã nhập kho") { bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201); }
                else if (text == "Đang vận chuyển") { bgColor = Color.FromArgb(232, 244, 253); textColor = Color.FromArgb(2, 136, 209); borderColor = Color.FromArgb(179, 229, 252); }
                else if (text == "Đã hủy") { bgColor = Color.FromArgb(255, 235, 238); textColor = Color.FromArgb(211, 47, 47); borderColor = Color.FromArgb(255, 205, 210); }

                Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                SizeF textSize = g.MeasureString(text, e.CellStyle.Font);
                int badgeWidth = (int)textSize.Width + 20;
                int badgeHeight = 26;
                int x = e.CellBounds.Left + 15;
                int y = e.CellBounds.Top + (e.CellBounds.Height - badgeHeight) / 2;

                Rectangle badgeRect = new Rectangle(x, y, badgeWidth, badgeHeight);

                using (GraphicsPath path = new GraphicsPath())
                {
                    int r = 12, d = r * 2;
                    path.AddArc(badgeRect.X, badgeRect.Y, d, d, 180, 90); path.AddArc(badgeRect.Right - d, badgeRect.Y, d, d, 270, 90);
                    path.AddArc(badgeRect.Right - d, badgeRect.Bottom - d, d, d, 0, 90); path.AddArc(badgeRect.X, badgeRect.Bottom - d, d, d, 90, 90); path.CloseFigure();
                    g.FillPath(new SolidBrush(bgColor), path); g.DrawPath(new Pen(borderColor), path);
                }
                TextRenderer.DrawText(g, text, new Font("Segoe UI", 9, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
    }
}