using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models.Entities
{
    public class PhieuKiem
    {
        public string MaKK { get; set; }
        public DateTime NgayKiem { get; set; }
        public string NhanVien { get; set; }
        public string KhoKiem { get; set; }
        public int SLChenhLech { get; set; }
        public string TrangThai { get; set; }

        public PhieuKiem(string maKK, DateTime ngayKiem, string nhanVien, string khoKiem, int slChenhLech, string trangThai)
        {
            MaKK = maKK;
            NgayKiem = ngayKiem;
            NhanVien = nhanVien;
            KhoKiem = khoKiem;
            SLChenhLech = slChenhLech;
            TrangThai = trangThai;
        }
    }
}
