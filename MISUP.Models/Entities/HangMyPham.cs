using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models
{
    public class HangMyPham : HangHoa //cú pháp của tính kế thừa     
    {
        public HangMyPham(string m, string mv, string t, string n, int s, decimal d, DateTime? hsd, int tmin)
            : base(m, mv, t, n, s, d, hsd, tmin) { }

        // [TÍNH ĐA HÌNH]: Mỹ phẩm chịu thuế VAT đặc biệt 8%
        public override decimal TinhThueVAT()
        {
            return (SoLuongNhap * DonGia) * 0.08m;
        }
    }
}