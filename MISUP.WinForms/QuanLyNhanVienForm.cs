using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;

namespace MISUP.WinForms.Forms
{
    public partial class QuanLyNhanVienForm : Form
    {
        private TaiKhoanBLL _bll = new TaiKhoanBLL();

        public QuanLyNhanVienForm()
        {
            InitializeComponent();
            LoadData();
        }

        // Hàm thông minh tự suy luận chức danh dựa trên quyền hạn
        private string GetRoleName(string quyen)
        {
            if (string.IsNullOrEmpty(quyen)) return "Nhân viên mới";
            if (quyen.Contains("Admin")) return "Quản trị viên (Admin)";

            if (quyen.Contains("ThanhToan") || quyen.Contains("DatHang") || quyen.Contains("BaoCao"))
                return "Nhân viên Kế toán";

            if (quyen.Contains("NhapHang") || quyen.Contains("KiemKe"))
                return "Nhân viên Kho";

            return "Nhân viên (Có giới hạn)";
        }

        private void LoadData()
        {
            var list = _bll.LayDanhSach().Select(x => new
            {
                TenDangNhap = x.TenDangNhap,
                HoTen = x.HoTen,
                VaiTro = GetRoleName(x.Quyen)
            }).ToList();

            dgvData.DataSource = list;

            if (dgvData.Columns.Count > 0)
            {
                dgvData.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập (User)";
                dgvData.Columns["HoTen"].HeaderText = "Họ và tên nhân viên";
                dgvData.Columns["VaiTro"].HeaderText = "Quyền được cấp";
            }
        }

        // Sự kiện khi bấm vào dòng bất kỳ trong Lưới, tự động đẩy dữ liệu lên TextBox để Sửa
        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string user = dgvData.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();
                var tk = _bll.LayDanhSach().FirstOrDefault(x => x.TenDangNhap == user);
                if (tk != null)
                {
                    txtUser.Text = tk.TenDangNhap;
                    txtName.Text = tk.HoTen;

                    // Phục hồi lại ComboBox (Vai trò gốc)
                    if (tk.Quyen.Contains("Admin")) cmbRole.SelectedIndex = 2;
                    else if (tk.Quyen.Contains("ThanhToan") || tk.Quyen.Contains("DatHang")) cmbRole.SelectedIndex = 1;
                    else cmbRole.SelectedIndex = 0; // Mặc định Kho
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng điền đủ Tên đăng nhập và Họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gán quyền truy cập mặc định theo Role
                string quyenStr = "";
                if (cmbRole.Text == "Admin") quyenStr = "Admin";
                else if (cmbRole.Text == "Nhân viên Kho") quyenStr = "TongQuan,NhapHang,SanPham,KiemKe";
                else if (cmbRole.Text == "Kế toán") quyenStr = "TongQuan,DatHang,NhaCungCap,ThanhToan,BaoCao";

                TaiKhoan tk = new TaiKhoan(txtUser.Text.Trim(), "123456", txtName.Text.Trim(), quyenStr);
                _bll.ThemTaiKhoan(tk);

                MessageBox.Show("Thêm nhân viên thành công!\nMật khẩu mặc định cấp phát là: 123456", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtUser.Clear(); txtName.Clear(); cmbRole.SelectedIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên từ danh sách dưới đây để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string quyenStr = "";
                if (cmbRole.Text == "Admin") quyenStr = "Admin";
                else if (cmbRole.Text == "Nhân viên Kho") quyenStr = "TongQuan,NhapHang,SanPham,KiemKe";
                else if (cmbRole.Text == "Kế toán") quyenStr = "TongQuan,DatHang,NhaCungCap,ThanhToan,BaoCao";

                TaiKhoan tk = new TaiKhoan(txtUser.Text.Trim(), "", txtName.Text.Trim(), quyenStr);
                _bll.SuaTaiKhoan(tk);

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUser.Clear(); txtName.Clear(); cmbRole.SelectedIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 tài khoản trong danh sách để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string user = dgvData.SelectedRows[0].Cells["TenDangNhap"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa quyền truy cập của tài khoản '{user}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _bll.XoaTaiKhoan(user);
                    MessageBox.Show("Xóa tài khoản thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi thao tác", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}