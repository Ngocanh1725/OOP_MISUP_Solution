using System;
using System.Drawing;
using System.Windows.Forms;
using MISUP.BLL.Services;

namespace MISUP.WinForms.Forms
{
    public partial class PhieuDatHangDialog : Form
    {
        private NhaCungCapBLL _nccBLL = new NhaCungCapBLL();
        private ToolTip _toolTip = new ToolTip();

        public string MaDon { get; set; }
        public string NgayTao { get; set; }
        public string NhaCungCap { get; set; }
        public string TongTien { get; set; }
        public string TrangThai { get; set; }
        public string NguoiTao { get; set; }

        public PhieuDatHangDialog()
        {
            InitializeComponent();
        }

        private void LoadNhaCungCapComboBox()
        {
            var items = new System.Collections.Generic.List<NhaCungCapItem>();
            try
            {
                var dsNCC = _nccBLL.LayDanhSach();
                int maxWidth = cmbNhaCungCap.Width;

                using (Graphics g = cmbNhaCungCap.CreateGraphics())
                {
                    foreach (var ncc in dsNCC)
                    {
                        // Thêm Mã NCC vào phía trước
                        string displayText = $"[{ncc.MaNCC}] {ncc.TenNCC}";
                        items.Add(new NhaCungCapItem { Text = displayText, Value = ncc.TenNCC });

                        // Tự động tính toán chiều rộng để mở rộng danh sách
                        int textWidth = (int)g.MeasureString(displayText, cmbNhaCungCap.Font).Width + SystemInformation.VerticalScrollBarWidth;
                        if (textWidth > maxWidth) maxWidth = textWidth;
                    }
                }

                cmbNhaCungCap.DataSource = items;
                cmbNhaCungCap.DisplayMember = "Text";
                cmbNhaCungCap.ValueMember = "Value";
                cmbNhaCungCap.DropDownWidth = maxWidth; // Mở rộng danh sách thả xuống

                // Gắn tooltip khi di chuột vào mục đã chọn
                cmbNhaCungCap.SelectedIndexChanged += (s, e) => {
                    if (cmbNhaCungCap.SelectedItem is NhaCungCapItem item)
                    {
                        _toolTip.SetToolTip(cmbNhaCungCap, item.Text);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách NCC: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PhieuDatHangDialog_Load(object sender, EventArgs e)
        {
            LoadNhaCungCapComboBox();

            cmbTrangThai.SelectedIndex = 0;
            txtMaDon.Text = MaDon;
            txtNguoiTao.Text = "Admin";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTongTien.Text))
            {
                MessageBox.Show("Vui lòng nhập Tổng tiền dự kiến!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return;
            }

            MaDon = txtMaDon.Text.Trim();
            NgayTao = dtpNgayTao.Value.ToString("dd/MM/yyyy");

            // Lấy Value (Tên NCC gốc) thay vì Text (Mã + Tên) để lưu xuống bảng chính xác
            NhaCungCap = (cmbNhaCungCap.SelectedItem as NhaCungCapItem)?.Value ?? cmbNhaCungCap.Text;

            NguoiTao = txtNguoiTao.Text.Trim();
            TrangThai = cmbTrangThai.Text;

            if (decimal.TryParse(txtTongTien.Text, out decimal tien))
                TongTien = tien.ToString("N0");
            else
                TongTien = txtTongTien.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}