using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MISUP.WinForms.Forms;

namespace MISUP.WinForms
{
    public partial class ucKiemKeKho : UserControl
    {
        private DataTable dtData;

        public ucKiemKeKho()
        {
            InitializeComponent();
            LoadMockData();
            AttachEvents();
            SetRoundedRegion(pnlCard, 15);
        }

        private void AttachEvents()
        {
            btnTim.Click += BtnTim_Click;
            btnThem.Click += BtnLapPhieu_Click;

            // Tìm nút Sửa, Xóa, In
            foreach (Control c in pnlHeader.Controls)
            {
                if (c.Name == "btnSua") c.Click += BtnSua_Click;
                if (c.Name == "btnXoa") c.Click += BtnXoa_Click;
                if (c.Name == "btnIn") c.Click += BtnIn_Click;
            }
        }

        private void LoadMockData()
        {
            dtData = new DataTable();
            dtData.Columns.Add("MaKiemKe"); dtData.Columns.Add("NgayKiem"); dtData.Columns.Add("NguoiKiem"); dtData.Columns.Add("Kho"); dtData.Columns.Add("ChenhLech"); dtData.Columns.Add("TrangThai");
            dtData.Rows.Add("PKK240615", "15/06/2026 14:30", "Nguyễn Văn A", "Kho Tổng HN", "-5", "Đã cân bằng");
            dtData.Rows.Add("PKK240510", "10/05/2026 09:15", "Trần Thị B", "Kho Miền Nam", "+2", "Đã cân bằng");
            dgvData.DataSource = dtData;
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (keyword.Contains("Tìm")) keyword = "";
            dtData.DefaultView.RowFilter = string.IsNullOrEmpty(keyword) ? "1=1" : $"Kho LIKE '%{keyword}%' OR MaKiemKe LIKE '%{keyword}%'";
        }

        private void BtnLapPhieu_Click(object sender, EventArgs e)
        {
            using (var f = new PhieuKiemDialog { MaKiemKe = "PKK" + DateTime.Now.ToString("yyMMdd") + (dtData.Rows.Count + 1).ToString("D2") })
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    dtData.Rows.InsertAt(dtData.NewRow(), 0);
                    dtData.Rows[0].ItemArray = new object[] { f.MaKiemKe, f.NgayKiem, f.NguoiKiem, f.Kho, f.ChenhLech, f.TrangThai };
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Chọn phiếu để sửa!"); return; }
            var row = dgvData.SelectedRows[0];
            using (var f = new PhieuKiemDialog
            {
                MaKiemKe = row.Cells[0].Value.ToString(),
                NgayKiem = row.Cells[1].Value.ToString(),
                NguoiKiem = row.Cells[2].Value.ToString(),
                Kho = row.Cells[3].Value.ToString(),
                ChenhLech = row.Cells[4].Value.ToString(),
                TrangThai = row.Cells[5].Value.ToString()
            })
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] dr = dtData.Select($"MaKiemKe = '{f.MaKiemKe}'");
                    if (dr.Length > 0) dr[0].ItemArray = new object[] { f.MaKiemKe, f.NgayKiem, f.NguoiKiem, f.Kho, f.ChenhLech, f.TrangThai };
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Chọn phiếu để xóa!"); return; }
            string id = dgvData.SelectedRows[0].Cells[0].Value.ToString();
            if (MessageBox.Show($"Xóa phiếu {id}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataRow[] dr = dtData.Select($"MaKiemKe = '{id}'");
                if (dr.Length > 0) dtData.Rows.Remove(dr[0]);
            }
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Chọn phiếu để in / xuất chi tiết!"); return; }
            MessageBox.Show($"Đang kết nối máy in để in chi tiết phiếu kiểm kê {dgvData.SelectedRows[0].Cells[0].Value}...", "In ấn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetRoundedRegion(Control control, int radius) { /* ... Giữ nguyên ... */ }
    }
}