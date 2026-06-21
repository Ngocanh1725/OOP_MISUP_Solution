using System;

namespace MISUP.Models
{
    public class NhaCungCap
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DienThoai { get; set; }
        public decimal TongMua { get; set; }
        public decimal CongNo { get; set; }
        public string TrangThai { get; set; }

        public NhaCungCap(string maNCC, string tenNCC, string dienThoai, decimal tongMua, decimal congNo, string trangThai)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            DienThoai = dienThoai;
            TongMua = tongMua;
            CongNo = congNo;
            TrangThai = trangThai;
        }
    }
}