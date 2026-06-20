using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucDatHang : UserControl
    {
        private DataTable dtDonHang;

        public ucDatHang()
        {
            InitializeComponent();
            cmbTrangThai.SelectedIndex = 0;
            cmbNhaCungCap.SelectedIndex = 0;
            AttachEvents();
            LoadMockData();

            // Bo tròn thẻ pnlCard
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
            // Placeholder
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.Contains("Tìm")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm mã đơn, tên NCC..."; txtTimKiem.ForeColor = Color.Gray; } };

            // Logic các nút bấm
            btnTim.Click += BtnTim_Click;
            btnTaoDonHang.Click += BtnTaoDonHang_Click;
            btnXuatFile.Click += BtnXuatFile_Click;

            // Cell painting for status badges
            dgvDonHang.CellPainting += DgvDonHang_CellPainting;
        }

        private void LoadMockData()
        {
            dtDonHang = new DataTable();
            dtDonHang.Columns.Add("MaDon", typeof(string));
            dtDonHang.Columns.Add("NgayTao", typeof(string));
            dtDonHang.Columns.Add("NhaCungCap", typeof(string));
            dtDonHang.Columns.Add("TongTien", typeof(string));
            dtDonHang.Columns.Add("TrangThai", typeof(string));
            dtDonHang.Columns.Add("NguoiTao", typeof(string));

            dtDonHang.Rows.Add("PON001", "20/06/2026", "Công ty CP Acecook", "15,500,000", "Đang giao dịch", "Admin");
            dtDonHang.Rows.Add("PON002", "19/06/2026", "Samsung Việt Nam", "125,000,000", "Hoàn thành", "Nhân viên Kho");
            dtDonHang.Rows.Add("PON003", "18/06/2026", "Nhà phân phối Unilever", "8,200,000", "Phiếu tạm", "Admin");
            dtDonHang.Rows.Add("PON004", "15/06/2026", "Đại lý phân phối TH True Milk", "4,350,000", "Đã hủy", "Nhân viên Kho");
            dtDonHang.Rows.Add("PON005", "10/06/2026", "Công ty TNHH Panasonic", "45,000,000", "Hoàn thành", "Admin");

            dgvDonHang.DataSource = dtDonHang;

            dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn nhập";
            dgvDonHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvDonHang.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
            dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDonHang.Columns["NguoiTao"].HeaderText = "Người tạo";

            dgvDonHang.Columns["NhaCungCap"].FillWeight = 200;
        }

        // Logic Lọc (Filter) bằng DataView
        private void BtnTim_Click(object sender, EventArgs e)
        {
            if (dtDonHang == null) return;
            string keyword = txtTimKiem.Text.Contains("Tìm") ? "" : txtTimKiem.Text.Trim();
            string ncc = cmbNhaCungCap.SelectedIndex == 0 ? "" : cmbNhaCungCap.Text;
            string trangThai = cmbTrangThai.SelectedIndex == 0 ? "" : cmbTrangThai.Text;

            string filter = "1=1";
            if (!string.IsNullOrEmpty(keyword)) filter += $" AND (MaDon LIKE '%{keyword}%' OR NhaCungCap LIKE '%{keyword}%')";
            if (!string.IsNullOrEmpty(ncc)) filter += $" AND NhaCungCap = '{ncc}'";
            if (!string.IsNullOrEmpty(trangThai)) filter += $" AND TrangThai = '{trangThai}'";

            dtDonHang.DefaultView.RowFilter = filter;
        }

        // Logic Tạo đơn nhập giả lập
        private void BtnTaoDonHang_Click(object sender, EventArgs e)
        {
            string newID = "PON" + (dtDonHang.Rows.Count + 1).ToString("D3");
            dtDonHang.Rows.InsertAt(dtDonHang.NewRow(), 0);
            dtDonHang.Rows[0]["MaDon"] = newID;
            dtDonHang.Rows[0]["NgayTao"] = DateTime.Now.ToString("dd/MM/yyyy");
            dtDonHang.Rows[0]["NhaCungCap"] = "Công ty CP Acecook";
            dtDonHang.Rows[0]["TongTien"] = "0";
            dtDonHang.Rows[0]["TrangThai"] = "Phiếu tạm";
            dtDonHang.Rows[0]["NguoiTao"] = "Admin";

            MessageBox.Show($"Đã tạo thành công Phiếu tạm mới: {newID}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Logic xuất file CSV
        private void BtnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV File (*.csv)|*.csv", FileName = "DanhSachDatHang.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Mã đơn,Ngày tạo,Nhà cung cấp,Tổng tiền,Trạng thái,Người tạo");
                foreach (DataRowView rowView in dtDonHang.DefaultView)
                {
                    DataRow r = rowView.Row;
                    sb.AppendLine($"{r["MaDon"]},{r["NgayTao"]},{r["NhaCungCap"]},\"{r["TongTien"]}\",{r["TrangThai"]},{r["NguoiTao"]}");
                }
                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Vẽ huy hiệu (Badge) cho trạng thái
        private void DgvDonHang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null && dgvDonHang.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                e.PaintBackground(e.CellBounds, true);

                string text = e.Value.ToString();
                Color bgColor, textColor, borderColor;

                if (text == "Hoàn thành") { bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201); }
                else if (text == "Đang giao dịch") { bgColor = Color.FromArgb(232, 244, 253); textColor = Color.FromArgb(25, 118, 210); borderColor = Color.FromArgb(187, 222, 251); }
                else if (text == "Đã hủy") { bgColor = Color.FromArgb(255, 235, 238); textColor = Color.FromArgb(211, 47, 47); borderColor = Color.FromArgb(255, 205, 210); }
                else { bgColor = Color.FromArgb(255, 244, 229); textColor = Color.FromArgb(255, 152, 0); borderColor = Color.FromArgb(255, 224, 178); }

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

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2; Size size = new Size(diameter, diameter); Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();
            if (radius == 0) { path.AddRectangle(bounds); return path; }
            path.AddArc(arc, 180, 90); arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90); arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90); arc.X = bounds.Left;
            path.AddArc(arc, 90, 90); path.CloseFigure();
            return path;
        }
    }
}