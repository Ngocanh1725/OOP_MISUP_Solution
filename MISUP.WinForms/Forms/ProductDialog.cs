using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;

namespace MISUP.WinForms.Forms
{
    // Tách riêng Form này để ucSanPham không bị phình to
    public class ProductDialog : Form
    {
        private HangHoaBLL _db;
        private bool _isEdit;
        private HangHoa _sp;

        private TextBox txtMa, txtTen, txtNsx, txtSl, txtGia;
        private ComboBox cmbLoai;
        private Button btnSave, btnCancel;

        public ProductDialog(HangHoaBLL db, HangHoa sp = null)
        {
            _db = db;
            _sp = sp;
            _isEdit = sp != null;
            BuildUI();
            BindData();
        }

        private void BuildUI()
        {
            this.Text = _isEdit ? "Chỉnh sửa thông tin" : "Thêm mới sản phẩm";
            this.Size = new Size(420, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            Label lblTitle = new Label() { Text = _isEdit ? "CẬP NHẬT SẢN PHẨM" : "THÊM SẢN PHẨM MỚI", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(41, 128, 185), Left = 20, Top = 15, Width = 300 };

            txtMa = new TextBox() { Left = 120, Top = 60, Width = 250, Font = new Font("Segoe UI", 11), Enabled = !_isEdit };
            txtTen = new TextBox() { Left = 120, Top = 100, Width = 250, Font = new Font("Segoe UI", 11) };
            txtNsx = new TextBox() { Left = 120, Top = 140, Width = 250, Font = new Font("Segoe UI", 11) };
            txtSl = new TextBox() { Left = 120, Top = 180, Width = 250, Font = new Font("Segoe UI", 11) };
            txtGia = new TextBox() { Left = 120, Top = 220, Width = 250, Font = new Font("Segoe UI", 11) };

            cmbLoai = new ComboBox() { Left = 120, Top = 260, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11), Enabled = !_isEdit };
            cmbLoai.Items.AddRange(new string[] { "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });

            btnSave = new Button() { Text = "💾 Lưu", Left = 150, Top = 320, Width = 100, Height = 40, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnCancel = new Button() { Text = "❌ Hủy", Left = 270, Top = 320, Width = 100, Height = 40, BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };

            btnCancel.Click += (s, e) => this.Close();
            btnSave.Click += BtnSave_Click;

            this.Controls.AddRange(new Control[] {
                lblTitle,
                new Label() { Text = "Mã hàng:", Left = 20, Top = 63, Font = new Font("Segoe UI", 10) }, txtMa,
                new Label() { Text = "Tên SP:", Left = 20, Top = 103, Font = new Font("Segoe UI", 10) }, txtTen,
                new Label() { Text = "Nhà SX:", Left = 20, Top = 143, Font = new Font("Segoe UI", 10) }, txtNsx,
                new Label() { Text = "Số lượng:", Left = 20, Top = 183, Font = new Font("Segoe UI", 10) }, txtSl,
                new Label() { Text = "Đơn giá:", Left = 20, Top = 223, Font = new Font("Segoe UI", 10) }, txtGia,
                new Label() { Text = "Loại hàng:", Left = 20, Top = 263, Font = new Font("Segoe UI", 10) }, cmbLoai,
                btnSave, btnCancel
            });
        }

        private void BindData()
        {
            if (_isEdit && _sp != null)
            {
                txtMa.Text = _sp.MaHang;
                txtTen.Text = _sp.TenHang;
                txtNsx.Text = _sp.NhaSanXuat;
                txtSl.Text = _sp.SoLuongNhap.ToString();
                txtGia.Text = _sp.DonGia.ToString();

                string loaiView = _sp switch { HangThucPham _ => "Thực Phẩm", HangDienTu _ => "Điện Tử", HangMyPham _ => "Mỹ Phẩm", HangGiaDung _ => "Gia Dụng", HangThoiTrang _ => "Thời Trang", _ => "Thực Phẩm" };
                cmbLoai.SelectedIndex = cmbLoai.Items.IndexOf(loaiView);
            }
            else cmbLoai.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int sl = int.Parse(txtSl.Text);
                decimal gia = decimal.Parse(txtGia.Text);
                string l = cmbLoai.SelectedItem.ToString() switch { "Thực Phẩm" => "ThucPham", "Điện Tử" => "DienTu", "Mỹ Phẩm" => "MyPham", "Gia Dụng" => "GiaDung", "Thời Trang" => "ThoiTrang", _ => "ThucPham" };

                HangHoa h = l switch
                {
                    "ThucPham" => new HangThucPham(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                    "DienTu" => new HangDienTu(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                    "MyPham" => new HangMyPham(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                    "GiaDung" => new HangGiaDung(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                    "ThoiTrang" => new HangThoiTrang(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                    _ => null
                };

                if (_isEdit) _db.SuaHang(h); else _db.NhapHang(h, l);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nhập liệu: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}