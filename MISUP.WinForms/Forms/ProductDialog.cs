using System;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;

namespace MISUP.WinForms.Forms
{
    public partial class ProductDialog : Form
    {
        private HangHoaBLL _db;
        private bool _isEdit;
        private HangHoa _sp;

        // Constructor nhận vào BLL và đối tượng Sản phẩm (nếu null là Thêm mới, nếu có data là Sửa)
        public ProductDialog(HangHoaBLL db, HangHoa sp = null)
        {
            InitializeComponent(); // Dòng này sẽ gọi hàm vẽ giao diện ở file Designer

            _db = db;
            _sp = sp;
            _isEdit = (sp != null);

            AttachEvents();
            BindData();
        }

        private void AttachEvents()
        {
            // Bắt sự kiện Click nút
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnDelete.Click += BtnDelete_Click;

            // Bắt sự kiện Checkbox Hạn sử dụng
            chkCoHSD.CheckedChanged += (s, e) => dtpHsd.Enabled = chkCoHSD.Checked;
        }

        private void BindData()
        {
            // Khởi tạo ComboBox
            cmbLoai.Items.AddRange(new string[] { "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });
            cmbDvt.Items.AddRange(new string[] { "Cái", "Hộp", "Thùng", "Lốc", "Kg", "Lít", "Gói" });

            if (_isEdit && _sp != null)
            {
                this.Text = "Cập nhật sản phẩm";
                lblTitle.Text = "Chi tiết sản phẩm";
                btnSave.Text = "Lưu cập nhật";
                btnDelete.Visible = true; // Hiện nút Xóa

                // Đổ dữ liệu cũ lên giao diện
                txtMa.Text = _sp.MaHang;
                txtMa.Enabled = false; // Khóa mã không cho sửa

                txtMaVach.Text = _sp.MaVach;
                txtTen.Text = _sp.TenHang;
                txtNsx.Text = _sp.NhaSanXuat;
                txtSl.Text = _sp.SoLuongNhap.ToString();
                txtGia.Text = _sp.DonGia.ToString("G0");
                txtTonMin.Text = _sp.TonKhoToiThieu.ToString();

                // Đổ Hạn Sử Dụng
                if (_sp.HanSuDung.HasValue)
                {
                    chkCoHSD.Checked = true;
                    dtpHsd.Enabled = true;
                    dtpHsd.Value = _sp.HanSuDung.Value;
                }

                // Chọn đúng Loại hàng
                string loaiView = _sp switch
                {
                    HangThucPham _ => "Thực Phẩm",
                    HangDienTu _ => "Điện Tử",
                    HangMyPham _ => "Mỹ Phẩm",
                    HangGiaDung _ => "Gia Dụng",
                    HangThoiTrang _ => "Thời Trang",
                    _ => "Thực Phẩm"
                };
                cmbLoai.SelectedIndex = cmbLoai.Items.IndexOf(loaiView);

                // Chọn Đơn vị tính
                cmbDvt.Text = string.IsNullOrEmpty(_sp.DonViTinh) ? "Cái" : _sp.DonViTinh;
            }
            else
            {
                // Chế độ Thêm Mới
                this.Text = "Nhập thông tin sản phẩm";
                lblTitle.Text = "Thêm mới hàng hóa";
                btnSave.Text = "Lưu & Khởi tạo";
                btnDelete.Visible = false; // Ẩn nút Xóa

                cmbLoai.SelectedIndex = 0;
                cmbDvt.SelectedIndex = 0;
                txtSl.Text = "0";
                txtTonMin.Text = "10";
                txtGia.Text = "0";
                chkCoHSD.Checked = false;
                dtpHsd.Enabled = false;
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // Thu thập thông tin từ giao diện
                string ma = txtMa.Text.Trim();
                string maVach = txtMaVach.Text.Trim();
                string ten = txtTen.Text.Trim();
                string nsx = txtNsx.Text.Trim();
                int sl = int.Parse(txtSl.Text.Trim());
                decimal gia = decimal.Parse(txtGia.Text.Trim());
                int min = int.Parse(txtTonMin.Text.Trim());
                DateTime? hsd = chkCoHSD.Checked ? dtpHsd.Value : (DateTime?)null;

                string l = cmbLoai.SelectedItem?.ToString() switch
                {
                    "Thực Phẩm" => "ThucPham",
                    "Điện Tử" => "DienTu",
                    "Mỹ Phẩm" => "MyPham",
                    "Gia Dụng" => "GiaDung",
                    "Thời Trang" => "ThoiTrang",
                    _ => "ThucPham"
                };

                // Tính Đa Hình & Kế Thừa: Khởi tạo đối tượng lớp con
                HangHoa? h = l switch
                {
                    "ThucPham" => new HangThucPham(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "DienTu" => new HangDienTu(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "MyPham" => new HangMyPham(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "GiaDung" => new HangGiaDung(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    "ThoiTrang" => new HangThoiTrang(ma, maVach, ten, nsx, sl, gia, hsd, min),
                    _ => null
                };

                if (h != null)
                {
                    // Gán thuộc tính Đơn vị tính
                    h.DonViTinh = string.IsNullOrWhiteSpace(cmbDvt.Text) ? "Cái" : cmbDvt.Text;

                    // Gọi BLL lưu Database
                    if (_isEdit)
                    {
                        _db.SuaHang(h);
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _db.NhapHang(h, l);
                        MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_sp == null) return;
            if (MessageBox.Show($"Xóa hoàn toàn sản phẩm '{_sp.TenHang}' khỏi hệ thống?\nHành động này không thể hoàn tác!", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _db.XoaHang(_sp.MaHang);
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text)) { MessageBox.Show("Vui lòng nhập Mã sản phẩm."); txtMa.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtTen.Text)) { MessageBox.Show("Vui lòng nhập Tên sản phẩm."); txtTen.Focus(); return false; }
            if (!int.TryParse(txtSl.Text, out int sl) || sl < 0) { MessageBox.Show("Số lượng nhập không hợp lệ."); txtSl.Focus(); return false; }
            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia < 0) { MessageBox.Show("Giá nhập không hợp lệ."); txtGia.Focus(); return false; }
            if (!int.TryParse(txtTonMin.Text, out int min) || min < 0) { MessageBox.Show("Định mức tồn không hợp lệ."); txtTonMin.Focus(); return false; }
            return true;
        }
    }
}