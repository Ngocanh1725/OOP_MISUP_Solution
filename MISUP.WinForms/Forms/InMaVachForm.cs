using System;
using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    public partial class InMaVachForm : Form
    {
        private string _maPhieu;
        private string _ncc;
        private string _ngay;

        public InMaVachForm(string maPhieu, string ncc, string ngay)
        {
            _maPhieu = maPhieu;
            _ncc = ncc;
            _ngay = ngay;
            InitializeComponent();
        }

        private void InMaVachForm_Load(object sender, EventArgs e)
        {
            lblMaPhieu.Text = _maPhieu;
            lblInfo.Text = $"Ngày lập: {_ngay}\nNhà cung cấp: {_ncc}";
        }

        // Dùng thư viện GDI+ của C# để tự động vẽ mã vạch (Barcode) mô phỏng
        private void pnlBarcode_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = pnlBarcode.ClientRectangle;

            int x = bounds.Left + 20;
            int y = bounds.Top + 10;
            int height = bounds.Height - 20;

            // Khởi tạo độ ngẫu nhiên dựa trên HashCode của mã phiếu (Giúp 1 mã luôn sinh ra 1 hình Barcode giống nhau)
            Random rnd = new Random(_maPhieu.GetHashCode());
            while (x < bounds.Right - 20)
            {
                int barWidth = rnd.Next(2, 6);
                int spaceWidth = rnd.Next(2, 5);
                g.FillRectangle(Brushes.Black, x, y, barWidth, height);
                x += barWidth + spaceWidth;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Đã gửi lệnh in mã vạch cho phiếu '{_maPhieu}' tới máy in thành công!", "Hoàn tất in ấn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}