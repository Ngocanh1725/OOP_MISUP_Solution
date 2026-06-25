using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MISUP.BLL.Services;
using MISUP.Models;
using System.ComponentModel; // Thêm thư viện này để dùng BindingList

namespace MISUP.WinForms.Forms
{
    public partial class PhanQuyenForm : Form
    {
        private TaiKhoanBLL _bll = new TaiKhoanBLL();
        private BindingList<UserDisplayItem> _displayUsers; // Dùng BindingList để tự động Refresh UI

        public PhanQuyenForm()
        {
            InitializeComponent();
            LoadUsers();
            lstUsers.SelectedIndexChanged += LstUsers_SelectedIndexChanged;
        }

        // Hàm tự suy luận chức danh viết tắt
        private string GetRoleName(string quyen)
        {
            if (string.IsNullOrEmpty(quyen)) return "Nhân viên mới";
            if (quyen.Contains("ThanhToan") || quyen.Contains("DatHang") || quyen.Contains("BaoCao")) return "Kế toán";
            if (quyen.Contains("NhapHang") || quyen.Contains("KiemKe")) return "NV Kho";
            return "Nhân viên";
        }

        private void LoadUsers()
        {
            // Chỉ hiển thị các nhân viên, không hiển thị Admin để tránh tự khóa chính mình
            var rawUsers = _bll.LayDanhSach().Where(u => u.Quyen != "Admin").ToList();

            // Khởi tạo danh sách đối tượng hiển thị (Bao gồm Tên + Vai Trò)
            _displayUsers = new BindingList<UserDisplayItem>(rawUsers.Select(u => new UserDisplayItem
            {
                User = u,
                DisplayName = $"{u.HoTen} - ({GetRoleName(u.Quyen)})"
            }).ToList());

            lstUsers.DataSource = _displayUsers;
            lstUsers.DisplayMember = "DisplayName"; // Thuộc tính sẽ in ra giao diện
        }

        private void LstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem is UserDisplayItem item)
            {
                TaiKhoan selectedUser = item.User;

                // Mở khóa panel phân quyền
                clbModules.Enabled = true;
                btnSave.Enabled = true;

                string q = selectedUser.Quyen ?? "";

                // Duyệt qua CheckedListBox và đánh dấu nếu tài khoản có chứa Tag của Module đó
                for (int i = 0; i < clbModules.Items.Count; i++)
                {
                    var mod = (ModuleItem)clbModules.Items[i];
                    clbModules.SetItemChecked(i, q.Contains(mod.MaModule));
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem is UserDisplayItem item)
            {
                TaiKhoan selectedUser = item.User;

                // Gom các mã Module đã được Check thành chuỗi phân cách bởi dấu phẩy
                List<string> selectedModules = new List<string>();
                foreach (ModuleItem mod in clbModules.CheckedItems)
                {
                    selectedModules.Add(mod.MaModule);
                }

                string newQuyen = string.Join(",", selectedModules);

                try
                {
                    _bll.CapNhatQuyen(selectedUser.TenDangNhap, newQuyen);

                    // Cập nhật lại Object đang lưu tạm ở RAM
                    selectedUser.Quyen = newQuyen;

                    // Cập nhật lại Tên hiển thị (Vì Vai trò có thể đã thay đổi theo các checkbox quyền mới)
                    item.DisplayName = $"{selectedUser.HoTen} - ({GetRoleName(newQuyen)})";
                    _displayUsers.ResetItem(_displayUsers.IndexOf(item)); // Báo cho ListBox vẽ lại dòng này

                    MessageBox.Show($"Cập nhật quyền truy cập cho nhân viên '{selectedUser.HoTen}' thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    // Class phụ giúp hiển thị dữ liệu lên ListBox đẹp mắt
    public class UserDisplayItem
    {
        public TaiKhoan User { get; set; }
        public string DisplayName { get; set; }
        public override string ToString() => DisplayName;
    }

    // Class phụ để bọc dữ liệu cho CheckedListBox
    public class ModuleItem
    {
        public string TenModule { get; set; }
        public string MaModule { get; set; }
        public override string ToString() => TenModule;
    }
}