using System;
using System.Drawing;
using System.Windows.Forms;
using MISUP.BLL.Services;

namespace MISUP.WinForms.Forms
{
    public partial class PhieuNhapDialog : Form
    {
        private NhaCungCapBLL _nccBLL = new NhaCungCapBLL();
        private ToolTip _toolTip = new ToolTip(); // Khởi tạo ToolTip

        public bool IsEditMode { get; set; } = false;
        public string MaPhieu { get; set; }

        public string ThoiGian { get; set; }
        public string NhaCungCap { get; set; }
        public string ChiNhanh { get; set; }
        public string TongTien { get; set; }
        public string TrangThai { get; set; }

        public PhieuNhapDialog()
        {
            InitializeComponent();
        }

        private void LoadNhaCungCapComboBox()
        {
            var items = new System.Collections.Generic.List<NhaCungCapItemPhieuNhap>();
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
                        items.Add(new NhaCungCapItemPhieuNhap { Text = displayText, Value = ncc.TenNCC });

                        // Tự động tính toán chiều rộng để mở rộng danh sách thả xuống
                        int textWidth = (int)g.MeasureString(displayText, cmbNhaCungCap.Font).Width + SystemInformation.VerticalScrollBarWidth;
                        if (textWidth > maxWidth) maxWidth = textWidth;
                    }
                }

                cmbNhaCungCap.DataSource = items;
                cmbNhaCungCap.DisplayMember = "Text";
                cmbNhaCungCap.ValueMember = "Value";
                cmbNhaCungCap.DropDownWidth = maxWidth; // Áp dụng độ rộng mới

                // Gắn tooltip khi di chuột vào mục đã chọn
                cmbNhaCungCap.SelectedIndexChanged += (s, e) => {
                    if (cmbNhaCungCap.SelectedItem is NhaCungCapItemPhieuNhap item)
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

        private void PhieuNhapDialog_Load(object sender, EventArgs e)
        {
            LoadNhaCungCapComboBox();

            cmbChiNhanh.SelectedIndex = 0;
            cmbTrangThai.SelectedIndex = 0;

            if (IsEditMode)
            {
                this.Text = "Cập nhật Phiếu Nhập Kho";
                lblTitle.Text = $"Cập nhật Phiếu: {MaPhieu}";

                dtpThoiGian.Value = DateTime.ParseExact(ThoiGian, "dd/MM/yyyy HH:mm", null);

                // Tìm và chọn đúng Nhà cung cấp dựa vào Value
                foreach (NhaCungCapItemPhieuNhap item in cmbNhaCungCap.Items)
                {
                    if (item.Value == NhaCungCap)
                    {
                        cmbNhaCungCap.SelectedItem = item;
                        break;
                    }
                }

                cmbChiNhanh.Text = ChiNhanh;
                txtTongTien.Text = TongTien.Replace(",", "");
                cmbTrangThai.Text = TrangThai;
            }
            else
            {
                this.Text = "Tạo Phiếu Nhập Kho Mới";
                lblTitle.Text = "Tạo Mới Phiếu Nhập";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTongTien.Text))
            {
                MessageBox.Show("Vui lòng nhập Tổng tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ThoiGian = dtpThoiGian.Value.ToString("dd/MM/yyyy HH:mm");

            // Lấy Value (Tên thật) thay vì Text (có chứa mã)
            NhaCungCap = (cmbNhaCungCap.SelectedItem as NhaCungCapItemPhieuNhap)?.Value ?? cmbNhaCungCap.Text;

            ChiNhanh = cmbChiNhanh.Text;

            if (decimal.TryParse(txtTongTien.Text, out decimal tien))
                TongTien = tien.ToString("N0");
            else
                TongTien = txtTongTien.Text;

            TrangThai = cmbTrangThai.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Lớp phụ trợ cho Binding dữ liệu ComboBox
    public class NhaCungCapItemPhieuNhap
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public override string ToString() => Text;
    }
}