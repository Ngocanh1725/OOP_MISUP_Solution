using MISUP.DAL.Repositories;
using MISUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MISUP.BLL.Services
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL _dal = new TaiKhoanDAL();

        public List<TaiKhoan> LayDanhSach() => _dal.LayDanhSach();

        public void ThemTaiKhoan(TaiKhoan tk)
        {
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap)) throw new Exception("Tên đăng nhập không được trống!");
            if (_dal.LayDanhSach().Any(x => x.TenDangNhap.ToLower() == tk.TenDangNhap.ToLower()))
                throw new Exception("Tên đăng nhập đã tồn tại!");
            _dal.Them(tk);
        }

        public void DoiMatKhau(string username, string oldPass, string newPass, string confirmPass)
        {
            var user = _dal.LayDanhSach().FirstOrDefault(x => x.TenDangNhap == username);
            if (user == null) throw new Exception("Tài khoản không tồn tại!");
            if (user.MatKhau != oldPass) throw new Exception("Mật khẩu cũ không chính xác!");
            if (newPass != confirmPass) throw new Exception("Mật khẩu xác nhận không khớp!");
            if (newPass.Length < 6) throw new Exception("Mật khẩu mới phải từ 6 ký tự trở lên!");

            _dal.DoiMatKhau(username, newPass);
        }

        public void SuaTaiKhoan(TaiKhoan tk)
        {
            if (string.IsNullOrWhiteSpace(tk.HoTen)) throw new Exception("Họ tên không được trống!");
            if (tk.TenDangNhap.ToLower() == "admin" && tk.Quyen != "Admin") throw new Exception("Không thể thay đổi quyền của Admin gốc tại đây!");
            _dal.Sua(tk);
        }

        public void XoaTaiKhoan(string username)
        {
            if (username.ToLower() == "admin") throw new Exception("Không thể xóa tài khoản Admin gốc!");
            _dal.Xoa(username);
        }

        public void CapNhatQuyen(string username, string quyen)
        {
            if (username.ToLower() == "admin") throw new Exception("Không thể thay đổi quyền của Admin gốc!");
            _dal.CapNhatQuyen(username, quyen);
        }
    }
}