using Microsoft.Data.SqlClient;
using MISUP.Models; // Lấy định nghĩa TaiKhoan từ tầng Models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISUP.DAL.Repositories
{
    public class AuthDAL
    {
        // Phương thức này chọc xuống SQL để kiểm tra User/Pass
        public TaiKhoan KiemTraDangNhap(string username, string password)
        {
            // Dùng using để dùng xong tự động đóng cửa kho (Connection) lại
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    return new TaiKhoan(r["TenDangNhap"].ToString(), r["MatKhau"].ToString(), r["HoTen"].ToString(), r["Quyen"].ToString());
                }
                return null;
            }
        }
    }
}
