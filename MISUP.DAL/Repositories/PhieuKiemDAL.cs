using Microsoft.Data.SqlClient;
using MISUP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.DAL.Repositories
{
    public class PhieuKiemDAL
    {
        public List<PhieuKiem> LayDanhSach()
        {
            var list = new List<PhieuKiem>();
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM PhieuKiem ORDER BY NgayKiem DESC", conn);
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new PhieuKiem(
                        r["MaKK"].ToString(),
                        Convert.ToDateTime(r["NgayKiem"]),
                        r["NhanVien"].ToString(),
                        r["KhoKiem"].ToString(),
                        Convert.ToInt32(r["SLChenhLech"]),
                        r["TrangThai"].ToString()
                    ));
                }
            }
            return list;
        }

        public void Them(PhieuKiem pk)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("INSERT INTO PhieuKiem (MaKK, NgayKiem, NhanVien, KhoKiem, SLChenhLech, TrangThai) VALUES (@ma, @ngay, @nv, @kho, @sl, @tt)", conn);
                cmd.Parameters.AddWithValue("@ma", pk.MaKK);
                cmd.Parameters.AddWithValue("@ngay", pk.NgayKiem);
                cmd.Parameters.AddWithValue("@nv", pk.NhanVien);
                cmd.Parameters.AddWithValue("@kho", pk.KhoKiem);
                cmd.Parameters.AddWithValue("@sl", pk.SLChenhLech);
                cmd.Parameters.AddWithValue("@tt", pk.TrangThai);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Sua(PhieuKiem pk)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("UPDATE PhieuKiem SET NgayKiem=@ngay, NhanVien=@nv, KhoKiem=@kho, SLChenhLech=@sl, TrangThai=@tt WHERE MaKK=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", pk.MaKK);
                cmd.Parameters.AddWithValue("@ngay", pk.NgayKiem);
                cmd.Parameters.AddWithValue("@nv", pk.NhanVien);
                cmd.Parameters.AddWithValue("@kho", pk.KhoKiem);
                cmd.Parameters.AddWithValue("@sl", pk.SLChenhLech);
                cmd.Parameters.AddWithValue("@tt", pk.TrangThai);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Xoa(string maKK)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("DELETE FROM PhieuKiem WHERE MaKK=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", maKK);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DuyetCanBang(string maKK)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("UPDATE PhieuKiem SET TrangThai=N'Đã cân bằng' WHERE MaKK=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", maKK);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}