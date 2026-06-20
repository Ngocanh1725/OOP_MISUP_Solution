using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucNhaCungCap : UserControl
    {
        public ucNhaCungCap()
        {
            InitializeComponent();
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AllowUserToOrderColumns = false;
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text == "Tìm theo tên, SĐT đối tác...") { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "Tìm theo tên, SĐT đối tác..."; txtTimKiem.ForeColor = Color.Gray; } };
            dgvData.CellPainting += DgvData_CellPainting;
            LoadMockData();
        }

        private void LoadMockData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNCC"); dt.Columns.Add("TenNCC"); dt.Columns.Add("DienThoai"); dt.Columns.Add("TongMua"); dt.Columns.Add("CongNo"); dt.Columns.Add("TrangThai");

            dt.Rows.Add("NCC001", "Công ty CP Sữa Việt Nam (Vinamilk)", "1900 1568", "1,250,000,000", "0", "Đang giao dịch");
            dt.Rows.Add("NCC002", "Samsung Vina Electronics", "028 3821 1111", "5,450,000,000", "150,000,000", "Đang giao dịch");
            dt.Rows.Add("NCC003", "Nhà Phân Phối Đồ Gia Dụng HN", "0988 123 456", "320,000,000", "45,000,000", "Đang giao dịch");
            dt.Rows.Add("NCC004", "Công ty TNHH Nhựa Chợ Lớn", "028 3855 2222", "85,000,000", "0", "Ngừng giao dịch");
            dt.Rows.Add("NCC005", "Tập đoàn Masan Consumer", "028 3823 6666", "890,500,000", "0", "Đang giao dịch");

            dgvData.DataSource = dt;
            dgvData.Columns["MaNCC"].HeaderText = "Mã Đối Tác"; dgvData.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp"; dgvData.Columns["DienThoai"].HeaderText = "Điện Thoại"; dgvData.Columns["TongMua"].HeaderText = "Tổng Nhập Hàng"; dgvData.Columns["CongNo"].HeaderText = "Công Nợ Hiện Tại"; dgvData.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvData.Columns["TenNCC"].FillWeight = 200;
        }

        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["TrangThai"].Index && e.Value != null)
            {
                e.PaintBackground(e.CellBounds, true);
                string text = e.Value.ToString();
                Color bgColor = Color.White, textColor = Color.Black, borderColor = Color.Gray;

                if (text == "Đang giao dịch") { bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201); }
                else if (text == "Ngừng giao dịch") { bgColor = Color.FromArgb(242, 242, 242); textColor = Color.FromArgb(97, 97, 97); borderColor = Color.FromArgb(224, 224, 224); }

                Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle badgeRect = new Rectangle(e.CellBounds.Left + 10, e.CellBounds.Top + 10, (int)g.MeasureString(text, e.CellStyle.Font).Width + 20, 24);

                using (GraphicsPath path = new GraphicsPath())
                {
                    int r = 12, d = r * 2; path.AddArc(badgeRect.X, badgeRect.Y, d, d, 180, 90); path.AddArc(badgeRect.Right - d, badgeRect.Y, d, d, 270, 90); path.AddArc(badgeRect.Right - d, badgeRect.Bottom - d, d, d, 0, 90); path.AddArc(badgeRect.X, badgeRect.Bottom - d, d, d, 90, 90); path.CloseFigure();
                    g.FillPath(new SolidBrush(bgColor), path); g.DrawPath(new Pen(borderColor), path);
                }
                TextRenderer.DrawText(g, text, new Font("Segoe UI", 9, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
    }
}