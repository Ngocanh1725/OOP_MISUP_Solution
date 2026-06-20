using MISUP.DAL.Repositories;
using MISUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MISUP.BLL.Services
{
    public class HangHoaBLL
    {
        private HangHoaDAL _hangHoaDAL = new HangHoaDAL();

        public List<HangHoa> LayDanhSach() => _hangHoaDAL.LayDanhSach();
        public List<HangHoa> TimKiem(string tk) => _hangHoaDAL.TimKiem(tk);
        public List<HangHoa> LocTheoLoai(string loai) => _hangHoaDAL.LocTheoLoai(loai);
        public decimal TinhTongGiaTriKho() => _hangHoaDAL.TinhTongGiaTriKho();

        public int DemTongSoMatHang() => _hangHoaDAL.LayDanhSach().Count;
        public int TinhTongTonKhoThucTe() => _hangHoaDAL.LayDanhSach().Sum(h => h.SoLuongNhap);

        // =========================================================================
        // 🔥 PHÂN HỆ CẢNH BÁO TỰ ĐỘNG (SMART ALERTS - THEO FSD)
        // =========================================================================

        // 1. Cảnh báo Tồn kho (Dưới mức TonKhoToiThieu do Quản lý thiết lập)
        public List<HangHoa> LayHangSapHetTonKho()
        {
            return _hangHoaDAL.LayDanhSach()
                              .Where(h => h.SoLuongNhap > 0 && h.SoLuongNhap <= h.TonKhoToiThieu)
                              .OrderBy(h => h.SoLuongNhap)
                              .ToList();
        }

        public List<HangHoa> LayHangDaHetKho() => _hangHoaDAL.LayDanhSach().Where(h => h.SoLuongNhap == 0).ToList();

        // 2. Cảnh báo Hạn sử dụng (Hết hạn hoặc Cận Date)
        public List<HangHoa> LayHangHetHan() => _hangHoaDAL.LayDanhSach().Where(h => h.IsHetHan()).ToList();
        public List<HangHoa> LayHangCanDate(int soNgay = 30) => _hangHoaDAL.LayDanhSach().Where(h => h.IsSapHetHan(soNgay)).ToList();

        // =========================================================================
        // NGHIỆP VỤ NHẬP / SỬA / XÓA
        // =========================================================================

        public void NhapHang(HangHoa h, string loai)
        {
            if (string.IsNullOrWhiteSpace(h.MaHang)) throw new Exception("Nghiệp vụ: Mã hàng không được để trống!");
            if (string.IsNullOrWhiteSpace(h.TenHang)) throw new Exception("Nghiệp vụ: Tên hàng không được để trống!");

            // Validate Mã vạch không được trùng (nếu có nhập)
            if (!string.IsNullOrEmpty(h.MaVach))
            {
                bool isExist = _hangHoaDAL.LayDanhSach().Any(x => x.MaVach == h.MaVach);
                if (isExist) throw new Exception($"Nghiệp vụ: Mã vạch {h.MaVach} đã tồn tại trong hệ thống!");
            }

            _hangHoaDAL.NhapHang(h, loai);
        }

        public void SuaHang(HangHoa h)
        {
            if (string.IsNullOrWhiteSpace(h.TenHang)) throw new Exception("Nghiệp vụ: Tên hàng không được để trống!");
            _hangHoaDAL.SuaHang(h);
        }

        public void XoaHang(string maHang)
        {
            if (string.IsNullOrWhiteSpace(maHang)) throw new Exception("Lỗi hệ thống: Mã hàng trống!");
            _hangHoaDAL.XoaHang(maHang);
        }
    }
}