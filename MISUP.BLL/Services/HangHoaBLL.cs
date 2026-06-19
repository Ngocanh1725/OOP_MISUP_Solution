using MISUP.DAL.Repositories;
using MISUP.Models;
using System;
using System.Collections.Generic;
using System.Linq; // Thêm Linq để xử lý dữ liệu nhanh

namespace MISUP.BLL.Services
{
    public class HangHoaBLL
    {
        private HangHoaDAL _hangHoaDAL = new HangHoaDAL();

        public List<HangHoa> LayDanhSach() => _hangHoaDAL.LayDanhSach();
        public List<HangHoa> TimKiem(string tk) => _hangHoaDAL.TimKiem(tk);
        public List<HangHoa> LocTheoLoai(string loai) => _hangHoaDAL.LocTheoLoai(loai);
        public List<HangHoa> SapXepTangDanTheoSoLuong() => _hangHoaDAL.SapXepTangDanTheoSoLuong();
        public decimal TinhTongGiaTriKho() => _hangHoaDAL.TinhTongGiaTriKho();

        // 🔥 TÍNH NĂNG MỚI: Thống kê nâng cao
        public int DemTongSoMatHang() => _hangHoaDAL.LayDanhSach().Count;

        // 🔥 TÍNH NĂNG MỚI: Cảnh báo tồn kho (Lọc hàng có số lượng dưới 10)
        public List<HangHoa> LayHangSapHetTonKho()
        {
            return _hangHoaDAL.LayDanhSach().Where(h => h.SoLuongNhap < 10).ToList();
        }

        public void NhapHang(HangHoa h, string loai)
        {
            if (string.IsNullOrWhiteSpace(h.MaHang)) throw new Exception("Mã hàng không được để trống!");
            if (string.IsNullOrWhiteSpace(h.TenHang)) throw new Exception("Tên hàng không được để trống!");
            _hangHoaDAL.NhapHang(h, loai);
        }

        public void SuaHang(HangHoa h)
        {
            if (string.IsNullOrWhiteSpace(h.TenHang)) throw new Exception("Tên hàng không được để trống!");
            _hangHoaDAL.SuaHang(h);
        }

        public void XoaHang(string maHang)
        {
            if (string.IsNullOrWhiteSpace(maHang)) throw new Exception("Không xác định được mã hàng cần xóa!");
            _hangHoaDAL.XoaHang(maHang);
        }
    }
}