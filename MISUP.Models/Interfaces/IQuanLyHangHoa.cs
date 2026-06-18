using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MISUP.Models;

namespace MISUP.Models
{
    // [TÍNH TRỪU TƯỢNG] (Abstraction): 
    // Interface chỉ định nghĩa "LÀM GÌ" (các chức năng cần có) chứ không định nghĩa "LÀM NHƯ THẾ NÀO".
    // Giúp ẩn đi chi tiết kết nối Database phức tạp ở tầng dưới.
    public interface IQuanLyHangHoa
    {
        // 1. Nhóm chức năng Đọc (Read)
        List<HangHoa> LayDanhSach(string query = "SELECT * FROM HangHoa");

        // 2. Nhóm chức năng Thêm (Create)
        void NhapHang(HangHoa h, string loaiHang);

        // 3. Nhóm chức năng Sửa (Update)
        void SuaHang(HangHoa h);

        // 4. Nhóm chức năng Xóa (Delete)
        void XoaHang(string maHang);

        // 5. Nhóm chức năng Tìm kiếm (Search)
        List<HangHoa> TimKiem(string tuKhoa);
        List<HangHoa> TimKiemTheoNSX(string nsx);

        // 6. Nhóm chức năng Sắp xếp (Sort)
        List<HangHoa> SapXepTangDanTheoSoLuong();

        // CHỨC NĂNG MỚI NÂNG CAO
        List<HangHoa> LocTheoLoai(string loaiHang);
        decimal TinhTongGiaTriKho();
    }
}