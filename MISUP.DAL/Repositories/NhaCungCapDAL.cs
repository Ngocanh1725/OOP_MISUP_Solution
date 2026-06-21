using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MISUP.Models;

namespace MISUP.DAL.Repositories
{
    public class NhaCungCapDAL
    {
        public List<NhaCungCap> LayDanhSach()
        {
            var list = new List<NhaCungCap>();
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM NhaCungCap", conn);
                conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new NhaCungCap(
                        r["MaNCC"].ToString(),
                        r["TenNCC"].ToString(),
                        r["DienThoai"].ToString(),
                        Convert.ToDecimal(r["TongMua"]),
                        Convert.ToDecimal(r["CongNo"]),
                        r["TrangThai"].ToString()
                    ));
                }
            }
            return list;
        }

        public void Them(NhaCungCap ncc)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("INSERT INTO NhaCungCap (MaNCC, TenNCC, DienThoai, TongMua, CongNo, TrangThai) VALUES (@ma, @ten, @dt, @mua, @no, @tt)", conn);
                cmd.Parameters.AddWithValue("@ma", ncc.MaNCC);
                cmd.Parameters.AddWithValue("@ten", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@dt", ncc.DienThoai);
                cmd.Parameters.AddWithValue("@mua", ncc.TongMua);
                cmd.Parameters.AddWithValue("@no", ncc.CongNo);
                cmd.Parameters.AddWithValue("@tt", ncc.TrangThai);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Sua(NhaCungCap ncc)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("UPDATE NhaCungCap SET TenNCC=@ten, DienThoai=@dt, TrangThai=@tt WHERE MaNCC=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", ncc.MaNCC);
                cmd.Parameters.AddWithValue("@ten", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@dt", ncc.DienThoai);
                cmd.Parameters.AddWithValue("@tt", ncc.TrangThai);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Xoa(string maNCC)
        {
            using (var conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                var cmd = new SqlCommand("DELETE FROM NhaCungCap WHERE MaNCC=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", maNCC);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}