using MISUP.DAL.Repositories;
using MISUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.BLL.Services
{
    public class HangHoaBLL
    {
        // "Đường dây nóng" gọi xuống anh Thủ kho HangHoaDAL
        private HangHoaDAL _hangHoaDAL = new HangHoaDAL();

        // Với lệnh Đọc, Tìm kiếm, Sắp xếp: Quản lý không cần kiểm tra nhiều, cứ nhờ Thủ kho lấy lên là được
        public List<HangHoa> LayDanhSach() => _hangHoaDAL.LayDanhSach();
        public List<HangHoa> TimKiem(string tk) => _hangHoaDAL.TimKiem(tk);
        public List<HangHoa> LocTheoLoai(string loai) => _hangHoaDAL.LocTheoLoai(loai);
        public List<HangHoa> SapXepTangDanTheoSoLuong() => _hangHoaDAL.SapXepTangDanTheoSoLuong();
        public decimal TinhTongGiaTriKho() => _hangHoaDAL.TinhTongGiaTriKho();

        // -------------------------------------------------------------
        // VỚI LỆNH THÊM / SỬA / XÓA: Bắt buộc phải có Logic kiểm tra
        // -------------------------------------------------------------

        public void NhapHang(HangHoa h, string loai)
        {
            // Kiểm tra nghiệp vụ (Business Logic Validation)
            if (string.IsNullOrWhiteSpace(h.MaHang))
                throw new Exception("Nghiệp vụ từ chối: Mã hàng không được để trống!");

            if (string.IsNullOrWhiteSpace(h.TenHang))
                throw new Exception("Nghiệp vụ từ chối: Tên hàng không được để trống!");

            // Nếu mọi thứ ổn, sai anh Thủ kho lưu vào DB
            _hangHoaDAL.NhapHang(h, loai);
        }

        public void SuaHang(HangHoa h)
        {
            if (string.IsNullOrWhiteSpace(h.TenHang))
                throw new Exception("Nghiệp vụ từ chối: Tên hàng không được để trống!");

            _hangHoaDAL.SuaHang(h);
        }

        public void XoaHang(string maHang)
        {
            if (string.IsNullOrWhiteSpace(maHang))
                throw new Exception("Nghiệp vụ từ chối: Không xác định được mã hàng cần xóa!");

            // Có thể thêm logic: Bắt lưu lại lịch sử ai vừa xóa hàng ở đây...

            _hangHoaDAL.XoaHang(maHang);
        }
    }
}