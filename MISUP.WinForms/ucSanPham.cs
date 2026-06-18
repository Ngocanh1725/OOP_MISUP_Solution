using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MISUP.Models.Entities;
using MISUP.Models.Interfaces;

namespace MISUP.WinForms
{
    public partial class ucSanPham : UserControl
    {
        // Khởi tạo DatabaseHelper thông qua Interface để đảm bảo Tính Trừu Tượng
        IQuanLyHangHoa db = new DatabaseHelper();
        ComboBox cmbLocLoai;
        Label lblThongKe;

        public ucSanPham()
        {
            InitializeComponent();

            // 1. GIAO DIỆN CÁC NÚT BẤM (Dựng bằng code)
            Button btnThem = new Button() { Text = "Thêm", Left = 20, Top = 10, Width = 80, Height = 35, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            Button btnSua = new Button() { Text = "Sửa", Left = 110, Top = 10, Width = 80, Height = 35, BackColor = Color.FromArgb(243, 156, 18), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            Button btnXoa = new Button() { Text = "Xóa", Left = 200, Top = 10, Width = 80, Height = 35, BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            Button btnRefresh = new Button() { Text = "Mới", Left = 290, Top = 10, Width = 70, Height = 35, BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };

            // 2. GIAO DIỆN BỘ LỌC
            cmbLocLoai = new ComboBox() { Left = 380, Top = 15, Width = 150, Font = new Font("Segoe UI", 11), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLocLoai.Items.AddRange(new string[] { "Tất cả", "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });
            cmbLocLoai.SelectedIndex = 0;
            cmbLocLoai.SelectedIndexChanged += (s, e) => LoadData("", cmbLocLoai.SelectedIndex == 0 ? "" : GetLoaiDbString(cmbLocLoai.SelectedItem.ToString()));

            // Gắn sự kiện
            btnThem.Click += (s, e) => ShowProductDialog(null);
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnRefresh.Click += (s, e) => { txtTimKiem.Clear(); cmbLocLoai.SelectedIndex = 0; LoadData(); };

            pnlTop.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnRefresh, cmbLocLoai });

            // 3. GIAO DIỆN THỐNG KÊ (Dưới đáy)
            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.WhiteSmoke };
            lblThongKe = new Label() { Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(41, 128, 185), AutoSize = true, Left = 20, Top = 12 };
            pnlBottom.Controls.Add(lblThongKe);
            this.Controls.Add(pnlBottom);
        }

        private void ucSanPham_Load(object sender, EventArgs e) => LoadData();

        // =================================================================================
        // HÀM LOAD DATA (ĐIỂM NHẤN TÍNH ĐA HÌNH OOP)
        // =================================================================================
        private void LoadData(string keyword = "", string loai = "")
        {
            var list = string.IsNullOrEmpty(loai) ? db.LayDanhSach() : db.LocTheoLoai(loai);
            if (!string.IsNullOrEmpty(keyword)) list = db.TimKiem(keyword);

            dgvData.DataSource = list.Select(h => new {
                MaHang = h.MaHang,
                TenHang = h.TenHang,
                NhaSanXuat = h.NhaSanXuat,
                Kho = h.SoLuongNhap,
                Gia = h.DonGia.ToString("N0") + " đ",

                // [TÍNH ĐA HÌNH]: Gọi hàm TinhThueVAT() và TinhTongGiaTriSauThue(), trình biên dịch tự động 
                // biết đối tượng là Điện Tử hay Mỹ Phẩm để áp dụng đúng mức thuế tương ứng.
                VAT = h.TinhThueVAT().ToString("N0") + " đ",
                TongSauThue = h.TinhTongGiaTriSauThue().ToString("N0") + " đ",
                Loai = GetLoaiViewString(h)
            }).ToList();

            // Tổng giá trị kho cũng được tính toán bằng Đa hình bên trong hàm TinhTongGiaTriKho()
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

            // Xử lý chuỗi giá trị (Xóa ký tự " đ", dấu phẩy, dấu chấm) để đưa về kiểu decimal an toàn
            string giaRaw = dgvData.SelectedRows[0].Cells["Gia"].Value.ToString().Replace(" đ", "").Replace(",", "").Replace(".", "");
            decimal gia = Convert.ToDecimal(giaRaw);

            // [TÍNH KẾ THỪA]: Khởi tạo đối tượng lớp con tương ứng trước khi truyền vào Popup
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
        // HỘP THOẠI THÊM / SỬA (CÓ NÚT QUAY LẠI)
        // =================================================================================
        private void ShowProductDialog(HangHoa sp = null)
        {
            bool isEdit = sp != null;
            using (Form form = new Form() { Text = isEdit ? "Sửa Sản Phẩm" : "Thêm Mới Sản Phẩm", Size = new Size(420, 450), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false })
            {
                TextBox txtMa = new TextBox() { Left = 120, Top = 40, Width = 250, Text = isEdit ? sp.MaHang : "", Enabled = !isEdit };
                TextBox txtTen = new TextBox() { Left = 120, Top = 80, Width = 250, Text = isEdit ? sp.TenHang : "" };
                TextBox txtNsx = new TextBox() { Left = 120, Top = 120, Width = 250, Text = isEdit ? sp.NhaSanXuat : "" };
                TextBox txtSl = new TextBox() { Left = 120, Top = 160, Width = 250, Text = isEdit ? sp.SoLuongNhap.ToString() : "" };
                TextBox txtGia = new TextBox() { Left = 120, Top = 200, Width = 250, Text = isEdit ? sp.DonGia.ToString() : "" };

                ComboBox cmbLoai = new ComboBox() { Left = 120, Top = 240, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList, Enabled = !isEdit };
                cmbLoai.Items.AddRange(new string[] { "Thực Phẩm", "Điện Tử", "Mỹ Phẩm", "Gia Dụng", "Thời Trang" });
                cmbLoai.SelectedIndex = isEdit ? cmbLoai.Items.IndexOf(GetLoaiViewString(sp)) : 0;

                form.Controls.AddRange(new Control[] {
                    new Label() { Text = "Mã hàng:", Left = 20, Top = 40 }, txtMa, new Label() { Text = "Tên SP:", Left = 20, Top = 80 }, txtTen,
                    new Label() { Text = "Nhà SX:", Left = 20, Top = 120 }, txtNsx, new Label() { Text = "Số lượng:", Left = 20, Top = 160 }, txtSl,
                    new Label() { Text = "Đơn giá:", Left = 20, Top = 200 }, txtGia, new Label() { Text = "Loại hàng:", Left = 20, Top = 240 }, cmbLoai
                });

                // TẠO NÚT LƯU VÀ QUAY LẠI
                Button btnSave = new Button() { Text = "Lưu Dữ Liệu", Left = 90, Top = 300, Width = 110, Height = 40, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Cursor = Cursors.Hand };
                Button btnBack = new Button() { Text = "Quay lại", Left = 210, Top = 300, Width = 110, Height = 40, BackColor = Color.Gray, ForeColor = Color.White, Cursor = Cursors.Hand };

                btnBack.Click += (s, ev) => form.Close(); // Đóng popup khi nhấn Quay Lại

                btnSave.Click += (s, ev) => {
                    try
                    {
                        if (string.IsNullOrEmpty(txtMa.Text) || string.IsNullOrEmpty(txtTen.Text)) throw new Exception("Vui lòng điền đủ Mã và Tên!");
                        int sl = int.Parse(txtSl.Text);
                        decimal gia = decimal.Parse(txtGia.Text);
                        string l = GetLoaiDbString(cmbLoai.SelectedItem.ToString());

                        // [TÍNH KẾ THỪA & ĐA HÌNH]: Khởi tạo đối tượng
                        HangHoa h = l switch
                        {
                            "ThucPham" => new HangThucPham(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                            "DienTu" => new HangDienTu(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                            "MyPham" => new HangMyPham(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                            "GiaDung" => new HangGiaDung(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
                            "ThoiTrang" => new HangThoiTrang(txtMa.Text, txtTen.Text, txtNsx.Text, sl, gia),
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

                if (form.ShowDialog() == DialogResult.OK) LoadData(); // Refresh DataGridView
            }
        }
    }
}