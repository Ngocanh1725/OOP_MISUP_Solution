using MISUP.WinForms.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucThanhToanNCC : UserControl
    {
        private DataTable dtData;

        public ucThanhToanNCC()
        {
            InitializeComponent();
            LoadMockData();
            AttachEvents();
            SetRoundedRegion(pnlCard, 15);
        }

        private void AttachEvents()
        {
            // Gắn sự kiện cho 2 nút bấm cứng trên giao diện
            if (btnTim != null) btnTim.Click += BtnTim_Click;
            if (btnLapPhieu != null) btnLapPhieu.Click += BtnLapPhieu_Click;

            // Tìm nút Sửa, Xóa động (Vì Designer cũ có thể chưa có, ta add sự kiện nếu tồn tại)
            // Thay pnlHeader thành this.Controls để fix lỗi CS0103
            foreach (Control c in this.Controls)
            {
                if (c.Name == "btnSua") c.Click += BtnSua_Click;
                if (c.Name == "btnXoa") c.Click += BtnXoa_Click;
            }
        }

        private void LoadMockData()
        {
            dtData = new DataTable();
            dtData.Columns.Add("MaPhieu");
            dtData.Columns.Add("NgayThanhToan");
            dtData.Columns.Add("NhaCungCap");
            dtData.Columns.Add("SoTienChi");
            dtData.Columns.Add("PhuongThuc");
            dtData.Columns.Add("TrangThai");

            dtData.Rows.Add("PC0001", "20/06/2026", "Công ty CP Sữa Việt Nam", "125,500,000", "Chuyển khoản", "Đã thanh toán");
            dtData.Rows.Add("PC0002", "19/06/2026", "Samsung Electronics", "20,000,000", "Tiền mặt", "Đã thanh toán");

            dgvData.DataSource = dtData;

            if (dgvData.Columns.Contains("NhaCungCap"))
            {
                dgvData.Columns["NhaCungCap"].FillWeight = 200;
            }
        }

        private void SetRoundedRegion(Control control, int radius)
        {
            if (control == null) return;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            // Logic tìm kiếm cơ bản
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                dtData.DefaultView.RowFilter = "";
            }
            else
            {
                dtData.DefaultView.RowFilter = string.Format("NhaCungCap LIKE '%{0}%'", txtTimKiem.Text);
            }
        }

        private void BtnLapPhieu_Click(object sender, EventArgs e)
        {
            using (var f = new PhieuChiDialog())
            {
                // Tự động sinh mã phiếu
                f.MaPhieu = "PC" + (dtData.Rows.Count + 1).ToString("D4");

                if (f.ShowDialog() == DialogResult.OK)
                {
                    dtData.Rows.InsertAt(dtData.NewRow(), 0);
                    // Lấy dữ liệu từ các public properties của form PhieuChiDialog để lưu vào bảng
                    dtData.Rows[0].ItemArray = new object[] { f.MaPhieu, f.NgayChi, f.NhaCungCap, f.SoTien, f.PhuongThuc, f.TrangThai };
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu chi để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvData.SelectedRows[0];
            using (var f = new PhieuChiDialog())
            {
                // Gán dữ liệu từ Grid vào các properties để form Dialog hiển thị lên
                f.MaPhieu = row.Cells[0].Value?.ToString();
                f.NgayChi = row.Cells[1].Value?.ToString();
                f.NhaCungCap = row.Cells[2].Value?.ToString();
                f.SoTien = row.Cells[3].Value?.ToString();
                f.PhuongThuc = row.Cells[4].Value?.ToString();
                f.TrangThai = row.Cells[5].Value?.ToString();

                if (f.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] dr = dtData.Select($"MaPhieu = '{f.MaPhieu}'");
                    if (dr.Length > 0)
                    {
                        // Cập nhật lại dữ liệu sau khi sửa xong
                        dr[0].ItemArray = new object[] { f.MaPhieu, f.NgayChi, f.NhaCungCap, f.SoTien, f.PhuongThuc, f.TrangThai };
                    }
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu chi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dgvData.Rows.RemoveAt(dgvData.SelectedRows[0].Index);
            }
        }
    }
}