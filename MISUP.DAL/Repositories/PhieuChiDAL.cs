using Microsoft.Data.SqlClient;
using MISUP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.DAL.Repositories
{
    public class PhieuChiDAL
    {
        public List<PhieuChi> LayDanhSach()
        {
            var list = new List<PhieuChi>();
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM PhieuChi ORDER BY ThoiGian DESC", conn);
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new PhieuChi(
                        r["MaPhieu"].ToString(),
                        Convert.ToDateTime(r["ThoiGian"]),
                        r["TenNCC"].ToString(),
                        Convert.ToDecimal(r["SoTien"]),
                        r["PhuongThuc"].ToString(),
                        r["TrangThai"].ToString()
                    ));
                }
            }
            return list;
        }

        public void Them(PhieuChi pc)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("INSERT INTO PhieuChi (MaPhieu, ThoiGian, TenNCC, SoTien, PhuongThuc, TrangThai) VALUES (@ma, @tg, @ten, @tien, @pt, @tt)", conn);
                cmd.Parameters.AddWithValue("@ma", pc.MaPhieu);
                cmd.Parameters.AddWithValue("@tg", pc.ThoiGian);
                cmd.Parameters.AddWithValue("@ten", pc.TenNCC);
                cmd.Parameters.AddWithValue("@tien", pc.SoTien);
                cmd.Parameters.AddWithValue("@pt", pc.PhuongThuc);
                cmd.Parameters.AddWithValue("@tt", pc.TrangThai);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Sua(PhieuChi pc)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("UPDATE PhieuChi SET ThoiGian=@tg, TenNCC=@ten, SoTien=@tien, PhuongThuc=@pt, TrangThai=@tt WHERE MaPhieu=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", pc.MaPhieu);
                cmd.Parameters.AddWithValue("@tg", pc.ThoiGian);
                cmd.Parameters.AddWithValue("@ten", pc.TenNCC);
                cmd.Parameters.AddWithValue("@tien", pc.SoTien);
                cmd.Parameters.AddWithValue("@pt", pc.PhuongThuc);
                cmd.Parameters.AddWithValue("@tt", pc.TrangThai);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Xoa(string maPhieu)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("DELETE FROM PhieuChi WHERE MaPhieu=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", maPhieu);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}