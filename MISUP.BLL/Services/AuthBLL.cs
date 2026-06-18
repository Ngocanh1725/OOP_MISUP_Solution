using MISUP.DAL.Repositories; // Gọi DAL
using MISUP.Models;           // Gọi Models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.BLL.Services
{
    public class AuthBLL
    {
        // Tạo một "đường dây nóng" gọi xuống anh Thủ kho AuthDAL
        private AuthDAL _authDAL = new AuthDAL();

        public TaiKhoan Login(string username, string password)
        {
            // BLL làm nhiệm vụ KIỂM TRA LOGIC (Nghiệp vụ)
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                // Ném ra lỗi ngay lập tức, không thèm gọi xuống DB làm gì cho tốn thời gian
                throw new Exception("Lỗi Nghiệp vụ: Tài khoản và mật khẩu không được để trống!");
            }

            // Nếu dữ liệu hợp lệ, mới gọi DAL để chọc xuống SQL kiểm tra
            return _authDAL.KiemTraDangNhap(username, password);
        }
    }
}