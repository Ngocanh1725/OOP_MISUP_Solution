using Microsoft.Data.SqlClient;
using MISUP.Models;
using System;
using System.Collections.Generic;

namespace MISUP.DAL.Repositories
{
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
                    string ma = r["MaHang"].ToString();
                    string maVach = r["MaVach"] != DBNull.Value ? r["MaVach"].ToString() : "";
                    string ten = r["TenHang"].ToString();
                    string nsx = r["NhaSanXuat"].ToString();
                    string loai = r["LoaiHang"].ToString();
                    int sl = Convert.ToInt32(r["SoLuongNhap"]);
                    decimal gia = Convert.ToDecimal(r["DonGia"]);
                    int tonMin = r["TonKhoToiThieu"] != DBNull.Value ? Convert.ToInt32(r["TonKhoToiThieu"]) : 10;
                    DateTime? hsd = r["HanSuDung"] != DBNull.Value ? Convert.ToDateTime(r["HanSuDung"]) : (DateTime?)null;

                    // Đọc Đơn vị tính (Bảo vệ lỗi nếu DB chưa có cột này)
                    string dvt = "Cái";
                    try { dvt = r["DonViTinh"] != DBNull.Value ? r["DonViTinh"].ToString() : "Cái"; } catch { }

                    HangHoa sp = null;
                    switch (loai)
                    {
                        case "ThucPham": sp = new HangThucPham(ma, maVach, ten, nsx, sl, gia, hsd, tonMin); break;
                        case "DienTu": sp = new HangDienTu(ma, maVach, ten, nsx, sl, gia, hsd, tonMin); break;
                        case "MyPham": sp = new HangMyPham(ma, maVach, ten, nsx, sl, gia, hsd, tonMin); break;
                        case "GiaDung": sp = new HangGiaDung(ma, maVach, ten, nsx, sl, gia, hsd, tonMin); break;
                        case "ThoiTrang": sp = new HangThoiTrang(ma, maVach, ten, nsx, sl, gia, hsd, tonMin); break;
                    }

                    if (sp != null)
                    {
                        sp.DonViTinh = dvt; // Gán Đơn vị tính độc lập
                        list.Add(sp);
                    }
                }
            }
            return list;
        }

        public void NhapHang(HangHoa h, string loaiHang)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                // Thêm trường DonViTinh vào lệnh INSERT
                string q = "INSERT INTO HangHoa (MaHang, MaVach, TenHang, NhaSanXuat, SoLuongNhap, DonGia, HanSuDung, TonKhoToiThieu, LoaiHang, DonViTinh) " +
                           "VALUES (@ma, @mv, @ten, @nsx, @sl, @gia, @hsd, @tmin, @loai, @dvt)";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", h.MaHang);
                cmd.Parameters.AddWithValue("@mv", string.IsNullOrEmpty(h.MaVach) ? (object)DBNull.Value : h.MaVach);
                cmd.Parameters.AddWithValue("@ten", h.TenHang);
                cmd.Parameters.AddWithValue("@nsx", h.NhaSanXuat);
                cmd.Parameters.AddWithValue("@sl", h.SoLuongNhap);
                cmd.Parameters.AddWithValue("@gia", h.DonGia);
                cmd.Parameters.AddWithValue("@hsd", h.HanSuDung.HasValue ? (object)h.HanSuDung.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@tmin", h.TonKhoToiThieu);
                cmd.Parameters.AddWithValue("@loai", loaiHang);
                cmd.Parameters.AddWithValue("@dvt", string.IsNullOrEmpty(h.DonViTinh) ? "Cái" : h.DonViTinh);

                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public void SuaHang(HangHoa h)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                string q = "UPDATE HangHoa SET MaVach=@mv, TenHang=@ten, NhaSanXuat=@nsx, SoLuongNhap=@sl, DonGia=@gia, HanSuDung=@hsd, TonKhoToiThieu=@tmin, DonViTinh=@dvt WHERE MaHang=@ma";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ma", h.MaHang);
                cmd.Parameters.AddWithValue("@mv", string.IsNullOrEmpty(h.MaVach) ? (object)DBNull.Value : h.MaVach);
                cmd.Parameters.AddWithValue("@ten", h.TenHang);
                cmd.Parameters.AddWithValue("@nsx", h.NhaSanXuat);
                cmd.Parameters.AddWithValue("@sl", h.SoLuongNhap);
                cmd.Parameters.AddWithValue("@gia", h.DonGia);
                cmd.Parameters.AddWithValue("@hsd", h.HanSuDung.HasValue ? (object)h.HanSuDung.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@tmin", h.TonKhoToiThieu);
                cmd.Parameters.AddWithValue("@dvt", string.IsNullOrEmpty(h.DonViTinh) ? "Cái" : h.DonViTinh);

                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public void XoaHang(string maHang)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM HangHoa WHERE MaHang=@ma", conn);
                cmd.Parameters.AddWithValue("@ma", maHang);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public List<HangHoa> TimKiem(string tk) => LayDanhSach($"SELECT * FROM HangHoa WHERE TenHang LIKE N'%{tk}%' OR MaHang LIKE '%{tk}%' OR MaVach LIKE '%{tk}%'");
        public List<HangHoa> TimKiemTheoNSX(string nsx) => LayDanhSach($"SELECT * FROM HangHoa WHERE NhaSanXuat LIKE N'%{nsx}%'");
        public List<HangHoa> SapXepTangDanTheoSoLuong() => LayDanhSach("SELECT * FROM HangHoa ORDER BY SoLuongNhap ASC");
        public List<HangHoa> LocTheoLoai(string loaiHang) => LayDanhSach($"SELECT * FROM HangHoa WHERE LoaiHang = '{loaiHang}'");
        public decimal TinhTongGiaTriKho() { decimal tong = 0; foreach (var sp in LayDanhSach()) tong += sp.TinhTongGiaTriSauThue(); return tong; }
    }
}