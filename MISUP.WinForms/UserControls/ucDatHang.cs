using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MISUP.WinForms.Forms;
using MISUP.BLL.Services;

namespace MISUP.WinForms
{
    public partial class ucDatHang : UserControl
    {
        private DataTable dtDonHang;
        private NhaCungCapBLL _nccBLL = new NhaCungCapBLL();
        private ToolTip _toolTip = new ToolTip(); // Khởi tạo ToolTip để hiện tên khi trỏ chuột

        public ucDatHang()
        {
            InitializeComponent();
            LoadNhaCungCapComboBox(); // Tự động load Database và định dạng lại giao diện
            cmbTrangThai.SelectedIndex = 0;
            AttachEvents();
            LoadMockData();

            // Bo tròn thẻ pnlCard
            SetRoundedRegion(pnlCard, 15);
        }

        private void LoadNhaCungCapComboBox()
        {
            var items = new System.Collections.Generic.List<NhaCungCapItem>();
            items.Add(new NhaCungCapItem { Text = "Tất cả NCC", Value = "" });

            try
            {
                var dsNCC = _nccBLL.LayDanhSach();
                int maxWidth = cmbNhaCungCap.Width;

                // Tạo đối tượng Graphics để đo chiều dài thực tế của từng chữ
                using (Graphics g = cmbNhaCungCap.CreateGraphics())
                {
                    foreach (var ncc in dsNCC)
                    {
                        // 1. Thêm Mã NCC lên đầu theo định dạng [Mã] Tên NCC
                        string displayText = $"[{ncc.MaNCC}] {ncc.TenNCC}";
                        items.Add(new NhaCungCapItem { Text = displayText, Value = ncc.TenNCC });

                        // 2. Tự động tính toán chiều rộng chữ để nới rộng phần Dropdown
                        int textWidth = (int)g.MeasureString(displayText, cmbNhaCungCap.Font).Width + SystemInformation.VerticalScrollBarWidth;
                        if (textWidth > maxWidth) maxWidth = textWidth;
                    }
                }

                // Gắn dữ liệu vào ComboBox bằng cơ chế Data Binding
                cmbNhaCungCap.DataSource = items;
                cmbNhaCungCap.DisplayMember = "Text"; // Cái người dùng nhìn thấy
                cmbNhaCungCap.ValueMember = "Value";  // Cái lưu ngầm ở dưới để dùng khi tìm kiếm

                // Mở rộng phần thả xuống để không bị khuyết chữ
                cmbNhaCungCap.DropDownWidth = maxWidth;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách NCC: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm mã đơn, tên NCC..."; txtTimKiem.ForeColor = Color.Gray; } };

            btnTim.Click += BtnTim_Click;
            btnTaoDonHang.Click += BtnTaoDonHang_Click;
            btnXuatFile.Click += BtnXuatFile_Click;

            dgvDonHang.CellPainting += DgvDonHang_CellPainting;

            // 3. Hiện ToolTip chứa tên đầy đủ khi người dùng chọn xong và trỏ chuột vào ComboBox
            cmbNhaCungCap.SelectedIndexChanged += (s, e) => {
                if (cmbNhaCungCap.SelectedItem is NhaCungCapItem item)
                {
                    _toolTip.SetToolTip(cmbNhaCungCap, item.Text);
                }
            };
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

            dtDonHang.Rows.Add("PON001", "20/06/2026", "Công ty CP Sữa Việt Nam", "15,500,000", "Đang giao dịch", "Admin");
            dtDonHang.Rows.Add("PON002", "19/06/2026", "Samsung Electronics", "125,000,000", "Hoàn thành", "Nhân viên Kho");
            dtDonHang.Rows.Add("PON003", "18/06/2026", "Nhà Phân Phối Hà Nội", "8,200,000", "Phiếu tạm", "Admin");
            dtDonHang.Rows.Add("PON004", "15/06/2026", "Công ty Nhựa Chợ Lớn", "4,350,000", "Đã hủy", "Nhân viên Kho");

            dgvDonHang.DataSource = dtDonHang;

            dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn nhập";
            dgvDonHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvDonHang.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
            dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDonHang.Columns["NguoiTao"].HeaderText = "Người tạo";

            dgvDonHang.Columns["NhaCungCap"].FillWeight = 200;
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            if (dtDonHang == null) return;
            string keyword = txtTimKiem.Text.Contains("Tìm") ? "" : txtTimKiem.Text.Trim();

            // Lấy tên thật của Nhà cung cấp từ ValueMember thay vì Text hiển thị
            string nccValue = "";
            if (cmbNhaCungCap.SelectedItem is NhaCungCapItem item)
            {
                nccValue = item.Value;
            }

            string trangThai = cmbTrangThai.SelectedIndex == 0 ? "" : cmbTrangThai.Text;

            string filter = "1=1";
            if (!string.IsNullOrEmpty(keyword)) filter += $" AND (MaDon LIKE '%{keyword}%' OR NhaCungCap LIKE '%{keyword}%')";
            if (!string.IsNullOrEmpty(nccValue)) filter += $" AND NhaCungCap = '{nccValue}'";
            if (!string.IsNullOrEmpty(trangThai)) filter += $" AND TrangThai = '{trangThai}'";

            dtDonHang.DefaultView.RowFilter = filter;
        }

        private void BtnTaoDonHang_Click(object sender, EventArgs e)
        {
            using (var dialog = new PhieuDatHangDialog())
            {
                dialog.MaDon = "PON" + (dtDonHang.Rows.Count + 1).ToString("D3");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    dtDonHang.Rows.InsertAt(dtDonHang.NewRow(), 0);
                    dtDonHang.Rows[0]["MaDon"] = dialog.MaDon;
                    dtDonHang.Rows[0]["NgayTao"] = dialog.NgayTao;
                    dtDonHang.Rows[0]["NhaCungCap"] = dialog.NhaCungCap;
                    dtDonHang.Rows[0]["TongTien"] = dialog.TongTien;
                    dtDonHang.Rows[0]["TrangThai"] = dialog.TrangThai;
                    dtDonHang.Rows[0]["NguoiTao"] = dialog.NguoiTao;

                    MessageBox.Show($"Đã tạo thành công Đơn đặt hàng mới: {dialog.MaDon}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

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

                Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
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

    // Class phụ trợ để quản lý hiển thị Text (Mã + Tên) và Value (Tên thật) cho ComboBox
    public class NhaCungCapItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public override string ToString() => Text;
    }
}