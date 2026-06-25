using Microsoft.Data.SqlClient;// Thư viện để kết nối SQL Server
using MISUP.Models; // Lấy định nghĩa TaiKhoan từ tầng Models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISUP.DAL.Repositories // Tầng Data Access Layer (DAL) để truy xuất dữ liệu từ cơ sở dữ liệu
{
    public class AuthDAL // Lớp AuthDAL để xử lý các thao tác liên quan đến xác thực người dùng
    {
        // Phương thức này chọc xuống SQL để kiểm tra User/Pass
        public TaiKhoan KiemTraDangNhap(string username, string password) //khai báo hàm KiemTraDangNhap nhận vào 2 chuỗi là tài khoản và mật khẩu
           //=>  , trả về 1 đối tượng TaiKhoan nếu đúng, null nếu sai
        {
            // Dùng using để dùng xong tự động đóng cửa kho (Connection) lại
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            // Tạo một đối tượng SqlConnection để kết nối đến cơ sở dữ liệu SQL Server, sử dụng chuỗi kết nối từ lớp DatabaseConnection
            // using giúp tự đóng kết nối và giải phóng tài nguyên ngay khi kết thúc khối lệnh
            {
                string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p"; // Khai báo câu lệnh SQL để tìm kiếm. Nhờ SQL tìm xem có dòng nào trong bảng TaiKhoan khớp cả Tên đăng nhập và Mật khẩu hay không. 
             
                SqlCommand cmd = new SqlCommand(query, conn);
                // Tạo một đối tượng SqlCommand để thực thi câu lệnh SQL trên kết nối conn
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
            
                conn.Open();
                // Mở kết nối đến cơ sở dữ liệu
                SqlDataReader r = cmd.ExecuteReader();
                // Thực thi câu lệnh SQL và trả về một SqlDataReader để đọc dữ liệu kết quả
                if (r.Read())// Nếu có dòng dữ liệu nào được trả về (tức là username và password đúng)
                {
                    return new TaiKhoan(r["TenDangNhap"].ToString(), r["MatKhau"].ToString(), r["HoTen"].ToString(), r["Quyen"].ToString());
                    // Tạo một đối tượng TaiKhoan từ dữ liệu đọc được và trả về cho BLL
                }
                return null;
            }
        }
    }
}
