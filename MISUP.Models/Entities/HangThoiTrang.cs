using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models
{
    public class HangThoiTrang : HangHoa
    {
        public HangThoiTrang(string m, string mv, string t, string n, int s, decimal d, DateTime? hsd, int tmin)
            : base(m, mv, t, n, s, d, hsd, tmin) { }

        // [TÍNH ĐA HÌNH]: Thời trang chịu thuế VAT 10%
        public override decimal TinhThueVAT()
        {
            return (SoLuongNhap * DonGia) * 0.1m;
        }
    }
}