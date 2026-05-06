using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace MISUP.Models
{
    public class DatabaseHelper : IQuanLyHangHoa
    {
        private string connectionString = @"Server=ADMIN-PC;Database=QuanLyMISUP;Trusted_Connection=True;TrustServerCertificate=True;";

        public TaiKhoan KiemTraDangNhap(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", username); cmd.Parameters.AddWithValue("@p", password);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read()) return new TaiKhoan(r["TenDangNhap"].ToString(), r["MatKhau"].ToString(), r["HoTen"].ToString(), r["Quyen"].ToString());
                return null;
            }
        }

        public List<HangHoa> LayDanhSach(string query = "SELECT * FROM HangHoa")
        {
            List<HangHoa> list = new List<HangHoa>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string ma = r["MaHang"].ToString(), ten = r["TenHang"].ToString(), nsx = r["NhaSanXuat"].ToString(), loai = r["LoaiHang"].ToString();
                    int sl = Convert.ToInt32(r["SoLuongNhap"]); decimal gia = Convert.ToDecimal(r["DonGia"]);

                    switch (loai)
                    {
                        case "ThucPham": list.Add(new HangThucPham(ma, ten, nsx, sl, gia)); break;
                        case "DienTu": list.Add(new HangDienTu(ma, ten, nsx, sl, gia)); break;
                        case "MyPham": list.Add(new HangMyPham(ma, ten, nsx, sl, gia)); break;
                        case "GiaDung": list.Add(new HangGiaDung(ma, ten, nsx, sl, gia)); break;
                        case "ThoiTrang": list.Add(new HangThoiTrang(ma, ten, nsx, sl, gia)); break;
                    }
                }
            }
            return list;
        }

        public void NhapHang(HangHoa h, string loai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string q = "INSERT INTO HangHoa VALUES (@ma, @ten, @nsx, @sl, @gia, @loai)";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", h.MaHang); cmd.Parameters.AddWithValue("@ten", h.TenHang);
                cmd.Parameters.AddWithValue("@nsx", h.NhaSanXuat); cmd.Parameters.AddWithValue("@sl", h.SoLuongNhap);
                cmd.Parameters.AddWithValue("@gia", h.DonGia); cmd.Parameters.AddWithValue("@loai", loai);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public void SuaHang(HangHoa h)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string q = "UPDATE HangHoa SET TenHang=@ten, NhaSanXuat=@nsx, SoLuongNhap=@sl, DonGia=@gia WHERE MaHang=@ma";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", h.MaHang); cmd.Parameters.AddWithValue("@ten", h.TenHang);
                cmd.Parameters.AddWithValue("@nsx", h.NhaSanXuat); cmd.Parameters.AddWithValue("@sl", h.SoLuongNhap);
                cmd.Parameters.AddWithValue("@gia", h.DonGia);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public void XoaHang(string maHang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string q = "DELETE FROM HangHoa WHERE MaHang=@ma";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", maHang);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public List<HangHoa> TimKiem(string tk) => LayDanhSach($"SELECT * FROM HangHoa WHERE TenHang LIKE N'%{tk}%' OR MaHang LIKE '%{tk}%'");
        public List<HangHoa> TimKiemTheoNSX(string nsx) => LayDanhSach($"SELECT * FROM HangHoa WHERE NhaSanXuat LIKE N'%{nsx}%'");
        public List<HangHoa> SapXepTangDanTheoSoLuong() => LayDanhSach("SELECT * FROM HangHoa ORDER BY SoLuongNhap ASC");
        public List<HangHoa> LocTheoLoai(string loai) => LayDanhSach($"SELECT * FROM HangHoa WHERE LoaiHang = '{loai}'");
        public decimal TinhTongGiaTriKho() { decimal tong = 0; foreach (var sp in LayDanhSach()) tong += sp.TinhTongGiaTriSauThue(); return tong; }
    }
}
