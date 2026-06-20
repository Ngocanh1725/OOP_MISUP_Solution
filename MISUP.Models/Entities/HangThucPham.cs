using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models
{
    public class HangThucPham : HangHoa
    {
        public HangThucPham(string m, string mv, string t, string n, int s, decimal d, DateTime? hsd, int tmin)
            : base(m, mv, t, n, s, d, hsd, tmin) { }

        // [TÍNH ĐA HÌNH]: Thực phẩm chịu thuế VAT 5%
        public override decimal TinhThueVAT()
        {
            return (SoLuongNhap * DonGia) * 0.05m;
        }
    }
}