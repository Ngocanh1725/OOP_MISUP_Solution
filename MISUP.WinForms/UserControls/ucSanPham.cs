using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MISUP.Models;
using MISUP.Models.Entities;

// Nếu bạn đã có NhaCungCapBLL, hãy using thư viện BLL ở đây
// using MISUP.BLL.Services; 

namespace MISUP.WinForms
{
    public partial class ucSanPham : UserControl
    {
        IQuanLyHangHoa db = new MISUP.DAL.Repositories.HangHoaDAL();
        ComboBox cmbLocLoai;
        Label lblThongKe;

        public ucSanPham()
        {
            InitializeComponent();

            Button btnThem = new Button() { Text = "Thêm", Left = 20, Top = 10, Width = 80, Height = 35, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            Button btnSua = new Button() { Text = "Sửa", Left = 110, Top = 10, Width = 80, Height = 35, BackColor = Color.FromArgb(243, 156, 18), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            Button btnXoa = new Button() { Text = "Xóa", Left = 200, Top = 10, Width = 80, Height = 35, BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            Button btnRefresh = new Button() { Text = "Mới", Left = 290, Top = 10, Width = 70, Height = 35, BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };

            cmbLocLoai = new ComboBox() { Left = 380, Top = 15, Width = 150, Font = new Font("Segoe UI", 11), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLocLoai.Items.AddRange(new string[] { "Tất cả", "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });
            cmbLocLoai.SelectedIndex = 0;
            cmbLocLoai.SelectedIndexChanged += (s, e) => LoadData("", cmbLocLoai.SelectedIndex == 0 ? "" : GetLoaiDbString(cmbLocLoai.SelectedItem.ToString()));

            btnThem.Click += (s, e) => ShowProductDialog(null);
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnRefresh.Click += (s, e) => { txtTimKiem.Clear(); cmbLocLoai.SelectedIndex = 0; LoadData(); };

            pnlTop.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnRefresh, cmbLocLoai });

            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.WhiteSmoke };
            lblThongKe = new Label() { Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(41, 128, 185), AutoSize = true, Left = 20, Top = 12 };
            pnlBottom.Controls.Add(lblThongKe);
            this.Controls.Add(pnlBottom);
        }

        private void ucSanPham_Load(object sender, EventArgs e) => LoadData();

        private void LoadData(string keyword = "", string loai = "")
        {
            var list = string.IsNullOrEmpty(loai) ? db.LayDanhSach() : db.LocTheoLoai(loai);

            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(x => x.TenHang.ToLower().Contains(keyword.ToLower()) ||
                                     x.MaHang.ToLower().Contains(keyword.ToLower()) ||
                                     x.NhaSanXuat.ToLower().Contains(keyword.ToLower())).ToList();
            }

            dgvData.DataSource = list.Select(h => new {
                MaHang = h.MaHang,
                TenHang = h.TenHang,
                NhaSanXuat = h.NhaSanXuat,
                Kho = h.SoLuongNhap,
                Gia = h.DonGia.ToString("N0") + " đ",
                VAT = h.TinhThueVAT().ToString("N0") + " đ",
                TongSauThue = h.TinhTongGiaTriSauThue().ToString("N0") + " đ",
                Loai = GetLoaiViewString(h)
            }).ToList();

            lblThongKe.Text = $"📊 TỔNG GIÁ TRỊ TỒN KHO: {db.TinhTongGiaTriKho():N0} VNĐ";
        }

        private string GetLoaiDbString(string viewStr) => viewStr switch { "Thực Phẩm" => "ThucPham", "Điện Tử" => "DienTu", "Mỹ Phẩm" => "MyPham", "Gia Dụng" => "GiaDung", "Thời Trang" => "ThoiTrang", _ => "ThucPham" };
        private string GetLoaiViewString(HangHoa h) => h switch { HangThucPham _ => "Thực Phẩm", HangDienTu _ => "Điện Tử", HangMyPham _ => "Mỹ Phẩm", HangGiaDung _ => "Gia Dụng", HangThoiTrang _ => "Thời Trang", _ => "Khác" };

        private void btnTim_Click(object sender, EventArgs e) => LoadData(txtTimKiem.Text);

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn một dòng để xóa!"); return; }
            if (MessageBox.Show("Xác nhận xóa sản phẩm này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.XoaHang(dgvData.SelectedRows[0].Cells["MaHang"].Value.ToString());
                LoadData();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn một dòng để sửa!"); return; }

            string ma = dgvData.SelectedRows[0].Cells["MaHang"].Value.ToString();
            string ten = dgvData.SelectedRows[0].Cells["TenHang"].Value.ToString();
            string nsx = dgvData.SelectedRows[0].Cells["NhaSanXuat"].Value.ToString();
            string loai = dgvData.SelectedRows[0].Cells["Loai"].Value.ToString();
            int sl = Convert.ToInt32(dgvData.SelectedRows[0].Cells["Kho"].Value);
            string giaRaw = dgvData.SelectedRows[0].Cells["Gia"].Value.ToString().Replace(" đ", "").Replace(",", "").Replace(".", "");
            decimal gia = Convert.ToDecimal(giaRaw);

            HangHoa sp = GetLoaiDbString(loai) switch
            {
                "ThucPham" => new HangThucPham(ma, ten, nsx, sl, gia),
                "DienTu" => new HangDienTu(ma, ten, nsx, sl, gia),
                "MyPham" => new HangMyPham(ma, ten, nsx, sl, gia),
                "GiaDung" => new HangGiaDung(ma, ten, nsx, sl, gia),
                "ThoiTrang" => new HangThoiTrang(ma, ten, nsx, sl, gia),
                _ => null
            };

            ShowProductDialog(sp);
        }

        // =================================================================================
        // ĐÃ CẬP NHẬT LOGIC COMBOBOX NHÀ CUNG CẤP TỪ HÌNH ẢNH
        // =================================================================================
        private void ShowProductDialog(HangHoa sp = null)
        {
            bool isEdit = sp != null;
            using (Form form = new Form() { Text = isEdit ? "Sửa Sản Phẩm" : "Thêm Mới Sản Phẩm", Size = new Size(420, 450), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false })
            {
                TextBox txtMa = new TextBox() { Left = 120, Top = 40, Width = 250, Text = isEdit ? sp.MaHang : "", Enabled = !isEdit };
                TextBox txtTen = new TextBox() { Left = 120, Top = 80, Width = 250, Text = isEdit ? sp.TenHang : "" };

                // THAY THẾ TEXTBOX BẰNG COMBOBOX NHÀ CUNG CẤP
                ComboBox cmbNhaCungCap = new ComboBox() { Left = 120, Top = 120, Width = 250, DropDownStyle = ComboBoxStyle.DropDown };

                // TẢI DANH SÁCH NHÀ CUNG CẤP (Tương tự logic trong hình)
                cmbNhaCungCap.Items.Clear();
                try
                {
                    // NẾU BẠN CÓ FILE NhaCungCapBLL, HÃY MỞ COMMENT DÒNG DƯỚI ĐÂY:
                    // var _nccBLL = new NhaCungCapBLL();
                    // foreach (var ncc in _nccBLL.LayDanhSach()) { cmbNhaCungCap.Items.Add(ncc.TenNCC); }

                    // Dữ liệu mẫu tạm thời để form không bị trống nếu chưa kết nối DB
                    cmbNhaCungCap.Items.AddRange(new string[] { "Unilever", "P&G", "L'Oréal", "Sony", "Vinamilk" });
                }
                catch { /* Bỏ qua lỗi kết nối tạm thời */ }

                if (isEdit) cmbNhaCungCap.Text = sp.NhaSanXuat;

                TextBox txtSl = new TextBox() { Left = 120, Top = 160, Width = 250, Text = isEdit ? sp.SoLuongNhap.ToString() : "" };
                TextBox txtGia = new TextBox() { Left = 120, Top = 200, Width = 250, Text = isEdit ? sp.DonGia.ToString() : "" };

                ComboBox cmbLoai = new ComboBox() { Left = 120, Top = 240, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Enabled = !isEdit };
                cmbLoai.Items.AddRange(new string[] { "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });
                cmbLoai.SelectedIndex = isEdit ? cmbLoai.Items.IndexOf(GetLoaiViewString(sp)) : 0;

                form.Controls.AddRange(new Control[] {
                    new Label() { Text = "Mã hàng:", Left = 20, Top = 40 }, txtMa,
                    new Label() { Text = "Tên SP:", Left = 20, Top = 80 }, txtTen,
                    new Label() { Text = "Nhà C.Cấp:", Left = 20, Top = 120 }, cmbNhaCungCap, // Đã đổi tên Label và Control
                    new Label() { Text = "Số lượng:", Left = 20, Top = 160 }, txtSl,
                    new Label() { Text = "Đơn giá:", Left = 20, Top = 200 }, txtGia,
                    new Label() { Text = "Loại hàng:", Left = 20, Top = 240 }, cmbLoai
                });

                Button btnSave = new Button() { Text = "Lưu Dữ Liệu", Left = 90, Top = 300, Width = 110, Height = 40, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Cursor = Cursors.Hand };
                Button btnBack = new Button() { Text = "Quay lại", Left = 210, Top = 300, Width = 110, Height = 40, BackColor = Color.Gray, ForeColor = Color.White, Cursor = Cursors.Hand };

                btnBack.Click += (s, ev) => form.Close();

                btnSave.Click += (s, ev) => {
                    try
                    {
                        if (string.IsNullOrEmpty(txtMa.Text) || string.IsNullOrEmpty(txtTen.Text)) throw new Exception("Vui lòng điền đủ Mã và Tên!");
                        int sl = int.Parse(txtSl.Text);
                        decimal gia = decimal.Parse(txtGia.Text);
                        string l = GetLoaiDbString(cmbLoai.SelectedItem.ToString());

                        // Lấy giá trị từ ComboBox Nhà Cung Cấp
                        string ncc = cmbNhaCungCap.Text;

                        HangHoa h = l switch
                        {
                            "ThucPham" => new HangThucPham(txtMa.Text, txtTen.Text, ncc, sl, gia),
                            "DienTu" => new HangDienTu(txtMa.Text, txtTen.Text, ncc, sl, gia),
                            "MyPham" => new HangMyPham(txtMa.Text, txtTen.Text, ncc, sl, gia),
                            "GiaDung" => new HangGiaDung(txtMa.Text, txtTen.Text, ncc, sl, gia),
                            "ThoiTrang" => new HangThoiTrang(txtMa.Text, txtTen.Text, ncc, sl, gia),
                            _ => null
                        };

                        if (isEdit) db.SuaHang(h); else db.NhapHang(h, l);

                        form.DialogResult = DialogResult.OK;
                        form.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi nhập liệu: " + ex.Message, "Báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                };

                form.Controls.Add(btnSave);
                form.Controls.Add(btnBack);

                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
        }
    }
}