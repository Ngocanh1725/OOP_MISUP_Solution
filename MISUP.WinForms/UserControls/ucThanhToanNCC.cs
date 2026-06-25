using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MISUP.WinForms.Forms;

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

        // Bổ sung nút bấm động vào Header
        private void AttachEvents()
        {
            btnTim.Click += BtnTim_Click;
            btnLapPhieu.Click += BtnLapPhieu_Click;

            // Tìm nút Sửa, Xóa (Vì Designer cũ có thể chưa có, ta add sự kiện nếu tồn tại)
            foreach (Control c in pnlHeader.Controls)
            {
                if (c.Name == "btnSua") c.Click += BtnSua_Click;
                if (c.Name == "btnXoa") c.Click += BtnXoa_Click;
            }
        }

        private void LoadMockData()
        {
            dtData = new DataTable();
            dtData.Columns.Add("MaPhieu"); dtData.Columns.Add("NgayThanhToan"); dtData.Columns.Add("NhaCungCap"); dtData.Columns.Add("SoTienChi"); dtData.Columns.Add("PhuongThuc"); dtData.Columns.Add("TrangThai");
            dtData.Rows.Add("PC0001", "20/06/2026", "Công ty CP Sữa Việt Nam", "125,500,000", "Chuyển khoản", "Đã thanh toán");
            dtData.Rows.Add("PC0002", "19/06/2026", "Samsung Electronics", "20,000,000", "Tiền mặt", "Đã thanh toán");
            dgvData.DataSource = dtData;
            dgvData.Columns["NhaCungCap"].FillWeight = 200;
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (keyword.Contains("Tìm")) keyword = "";
            dtData.DefaultView.RowFilter = string.IsNullOrEmpty(keyword) ? "1=1" : $"NhaCungCap LIKE '%{keyword}%' OR MaPhieu LIKE '%{keyword}%'";
        }

        private void BtnLapPhieu_Click(object sender, EventArgs e)
        {
            using (var f = new PhieuChiDialog { MaPhieu = "PC" + (dtData.Rows.Count + 1).ToString("D4") })
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    dtData.Rows.InsertAt(dtData.NewRow(), 0);
                    dtData.Rows[0].ItemArray = new object[] { f.MaPhieu, f.NgayChi, f.NhaCungCap, f.SoTien, f.PhuongThuc, f.TrangThai };
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Chọn phiếu để sửa!"); return; }
            var row = dgvData.SelectedRows[0];
            using (var f = new PhieuChiDialog
            {
                MaPhieu = row.Cells[0].Value.ToString(),
                NgayChi = row.Cells[1].Value.ToString(),
                NhaCungCap = row.Cells[2].Value.ToString(),
                SoTien = row.Cells[3].Value.ToString(),
                PhuongThuc = row.Cells[4].Value.ToString(),
                TrangThai = row.Cells[5].Value.ToString()
            })
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] dr = dtData.Select($"MaPhieu = '{f.MaPhieu}'");
                    if (dr.Length > 0) dr[0].ItemArray = new object[] { f.MaPhieu, f.NgayChi, f.NhaCungCap, f.SoTien, f.PhuongThuc, f.TrangThai };
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Chọn phiếu để xóa!"); return; }
            string id = dgvData.SelectedRows[0].Cells[0].Value.ToString();
            if (MessageBox.Show($"Xóa phiếu {id}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataRow[] dr = dtData.Select($"MaPhieu = '{id}'");
                if (dr.Length > 0) dtData.Rows.Remove(dr[0]);
            }
        }

        private void SetRoundedRegion(Control control, int radius) { /* ... Giữ nguyên ... */ }
    }
}