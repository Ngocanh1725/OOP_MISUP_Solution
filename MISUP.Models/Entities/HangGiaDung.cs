using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models
{   //Lớp con kế thừa Lớp cha bằng dấu hai chấm (:)
    // Kế thừa cái thuộc tính
    public class HangGiaDung : HangHoa
    {   // Dùng từ khóa 'base' để tái sử dụng Constructor của lớp cha
        public HangGiaDung(string m, string t, string n, int s, decimal d) : base(m, t, n, s, d) { }
    }
}
