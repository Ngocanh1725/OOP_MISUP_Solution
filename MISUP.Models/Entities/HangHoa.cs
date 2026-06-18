using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TÍNH ĐÓNG GÓI (ENCAPSULATION): Che giấu dữ liệu của đối tượng và chỉ cho phép giao tiếp,
// thay đổi dữ liệu thông qua các phương thức công khai (Properties/Methods)
// để kiểm soát tính hợp lệ của dữ liệu.
namespace MISUP.Models
{   //Lớp cha
    public abstract class HangHoa
    {
        private int _soLuongNhap;// Giấu đi để biến chứa dữ liệu là private để không ai có thể sửa trực tiếp.
        private decimal _donGia;

        public string MaHang { get; set; }
        public string TenHang { get; set; }
        public string NhaSanXuat { get; set; }

        public int SoLuongNhap// Cổng giao tiếp public.
                             
        {
            get => _soLuongNhap;// Muốn xem thì get sẽ trả về
            set { if (value < 0) throw new ArgumentException("Số lượng không âm!"); _soLuongNhap = value; }
            // Set kiểm tra, sau đó cài đặt lại giá trị
        }

        public decimal DonGia
        {
            get => _donGia;
            set { if (value < 0) throw new ArgumentException("Đơn giá không âm!"); _donGia = value; }
        }
        // Cho phép một lớp mới (Lớp con) sử dụng lại các thuộc tính
        // và phương thức của một lớp đã có (Lớp cha) mà không cần phải viết lại code.
        //Constructor lớp cha
        public HangHoa(string maHang, string tenHang, string nhaSanXuat, int soLuongNhap, decimal donGia)
        {
            MaHang = maHang;
            TenHang = tenHang;
            NhaSanXuat = nhaSanXuat;
            SoLuongNhap = soLuongNhap;
            DonGia = donGia;
        }

        // ====================================================================
        // 3. CHUẨN BỊ CHO TÍNH ĐA HÌNH (POLYMORPHISM)
        // Dùng từ khóa 'virtual' để cho phép các lớp con ghi đè lại cách tính thuế.
        // Mặc định lớp cha không tính thuế (0%).
        // ====================================================================
        public virtual decimal TinhThueVAT()
        {
            return 0;
        }

        // Hàm chung: Tổng giá trị = (Số lượng * Đơn giá) + Thuế VAT
        public decimal TinhTongGiaTriSauThue()
        {
            return SoLuongNhap * DonGia + TinhThueVAT();
        }
    }

}

