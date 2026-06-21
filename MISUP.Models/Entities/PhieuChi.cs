using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models.Entities
{
    public class PhieuChi
    {
        public string MaPhieu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TenNCC { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; }
        public string TrangThai { get; set; }

        public PhieuChi(string maPhieu, DateTime thoiGian, string tenNCC, decimal soTien, string phuongThuc, string trangThai)
        {
            MaPhieu = maPhieu;
            ThoiGian = thoiGian;
            TenNCC = tenNCC;
            SoTien = soTien;
            PhuongThuc = phuongThuc;
            TrangThai = trangThai;
        }
    }
}