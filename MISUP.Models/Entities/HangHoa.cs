using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TÍNH ĐÓNG GÓI (ENCAPSULATION): Che giấu dữ liệu của đối tượng và chỉ cho phép giao tiếp,
// thay đổi dữ liệu thông qua các phương thức công khai (Properties/Methods)
// để kiểm soát tính hợp lệ của dữ liệu.
namespace MISUP.Models
{
    // LỚP CHA (Trừu tượng) - Đã được dọn dẹp sạch sẽ, không chứa các lớp con
    public abstract class HangHoa
    {
        private int _soLuongNhap;
        private decimal _donGia;
        private int _tonKhoToiThieu;

        public string MaHang { get; set; }
        public string MaVach { get; set; }
        public string TenHang { get; set; }
        public string NhaSanXuat { get; set; }
        public DateTime? HanSuDung { get; set; }

        public string DonViTinh { get; set; } = "Cái";

        public int SoLuongNhap
        {
            get => _soLuongNhap;
            set { if (value < 0) throw new ArgumentException("Số lượng không được âm!"); _soLuongNhap = value; }
        }

        public decimal DonGia
        {
            get => _donGia;
            set { if (value < 0) throw new ArgumentException("Đơn giá không được âm!"); _donGia = value; }
        }

        public int TonKhoToiThieu
        {
            get => _tonKhoToiThieu;
            set { if (value < 0) throw new ArgumentException("Tồn tối thiểu không được âm!"); _tonKhoToiThieu = value; }
        }

        public HangHoa(string maHang, string maVach, string tenHang, string nhaSanXuat, int soLuongNhap, decimal donGia, DateTime? hanSuDung, int tonKhoToiThieu)
        {
            MaHang = maHang; MaVach = maVach; TenHang = tenHang; NhaSanXuat = nhaSanXuat;
            SoLuongNhap = soLuongNhap; DonGia = donGia; HanSuDung = hanSuDung; TonKhoToiThieu = tonKhoToiThieu;
        }

        // ====================================================================
        // TÍNH ĐA HÌNH (POLYMORPHISM) - Phương thức ảo cho phép ghi đè
        // ====================================================================
        public virtual decimal TinhThueVAT()
        {
            return 0; // Mặc định không thuế
        }

        public decimal TinhTongGiaTriSauThue()
        {
            return (SoLuongNhap * DonGia) + TinhThueVAT();
        }

        // ====================================================================
        // TÍNH ĐÓNG GÓI (ENCAPSULATION) - Xử lý nghiệp vụ nội tại
        // ====================================================================
        public bool IsHetHan() => HanSuDung.HasValue && HanSuDung.Value.Date < DateTime.Now.Date;

        public bool IsSapHetHan(int soNgayCanhBao = 30)
            => HanSuDung.HasValue && HanSuDung.Value.Date >= DateTime.Now.Date && HanSuDung.Value.Date <= DateTime.Now.AddDays(soNgayCanhBao).Date;
    }
}