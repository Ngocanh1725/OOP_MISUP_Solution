using Microsoft.Data.SqlClient;
using MISUP.Models; // Lấy định nghĩa Hàng hóa và Interface từ Models
using System;
using System.Collections.Generic;

namespace MISUP.DAL.Repositories
{
    // Ký hợp đồng IQuanLyHangHoa (Tính Trừu tượng)
    public class HangHoaDAL : IQuanLyHangHoa
    {
        public List<HangHoa> LayDanhSach(string query = "SELECT * FROM HangHoa")
        {
            List<HangHoa> list = new List<HangHoa>();
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string ma = r["MaHang"].ToString(), ten = r["TenHang"].ToString(), nsx = r["NhaSanXuat"].ToString(), loai = r["LoaiHang"].ToString();
                    int sl = Convert.ToInt32(r["SoLuongNhap"]);
                    decimal gia = Convert.ToDecimal(r["DonGia"]);

                    // [TÍNH ĐA HÌNH & KẾ THỪA] Khởi tạo đối tượng lớp con tùy vào DB
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
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string q = "INSERT INTO HangHoa VALUES (@ma, @ten, @nsx, @sl, @gia, @loai)";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", h.MaHang);
                cmd.Parameters.AddWithValue("@ten", h.TenHang);
                cmd.Parameters.AddWithValue("@nsx", h.NhaSanXuat);
                cmd.Parameters.AddWithValue("@sl", h.SoLuongNhap);
                cmd.Parameters.AddWithValue("@gia", h.DonGia);
                cmd.Parameters.AddWithValue("@loai", loai);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SuaHang(HangHoa h)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string q = "UPDATE HangHoa SET TenHang=@ten, NhaSanXuat=@nsx, SoLuongNhap=@sl, DonGia=@gia WHERE MaHang=@ma";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", h.MaHang);
                cmd.Parameters.AddWithValue("@ten", h.TenHang);
                cmd.Parameters.AddWithValue("@nsx", h.NhaSanXuat);
                cmd.Parameters.AddWithValue("@sl", h.SoLuongNhap);
                cmd.Parameters.AddWithValue("@gia", h.DonGia);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void XoaHang(string maHang)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string q = "DELETE FROM HangHoa WHERE MaHang=@ma";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", maHang);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<HangHoa> TimKiem(string tk) => LayDanhSach($"SELECT * FROM HangHoa WHERE TenHang LIKE N'%{tk}%' OR MaHang LIKE '%{tk}%'");
        public List<HangHoa> TimKiemTheoNSX(string nsx) => LayDanhSach($"SELECT * FROM HangHoa WHERE NhaSanXuat LIKE N'%{nsx}%'");
        public List<HangHoa> SapXepTangDanTheoSoLuong() => LayDanhSach("SELECT * FROM HangHoa ORDER BY SoLuongNhap ASC");
        public List<HangHoa> LocTheoLoai(string loai) => LayDanhSach($"SELECT * FROM HangHoa WHERE LoaiHang = '{loai}'");

        public decimal TinhTongGiaTriKho()
        {
            decimal tong = 0;
            foreach (var sp in LayDanhSach())
            {
                // Gọi hàm TinhTongGiaTriSauThue() (Sử dụng tính Đa hình của OOP)
                tong += sp.TinhTongGiaTriSauThue();
            }
            return tong;
        }
    }
}