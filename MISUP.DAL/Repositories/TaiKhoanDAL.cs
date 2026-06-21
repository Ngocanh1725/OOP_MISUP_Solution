using Microsoft.Data.SqlClient;
using MISUP.Models;
using System.Collections.Generic;

namespace MISUP.DAL.Repositories
{
    public class TaiKhoanDAL
    {
        public List<TaiKhoan> LayDanhSach()
        {
            var list = new List<TaiKhoan>();
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM TaiKhoan", conn);
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new TaiKhoan(r["TenDangNhap"].ToString(), r["MatKhau"].ToString(), r["HoTen"].ToString(), r["Quyen"].ToString()));
                }
            }
            return list;
        }

        public void Them(TaiKhoan tk)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, Quyen) VALUES (@user, @pass, @ten, @quyen)", conn);
                cmd.Parameters.AddWithValue("@user", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@pass", tk.MatKhau);
                cmd.Parameters.AddWithValue("@ten", tk.HoTen);
                cmd.Parameters.AddWithValue("@quyen", tk.Quyen);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public void DoiMatKhau(string username, string newPass)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("UPDATE TaiKhoan SET MatKhau=@pass WHERE TenDangNhap=@user", conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", newPass);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public void Xoa(string username)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE TenDangNhap=@user", conn);
                cmd.Parameters.AddWithValue("@user", username);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
    }
}