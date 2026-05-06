using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models
{//Đối tượng là một thực thể cụ thể được tạo ra từ Lớp (Class). Quá trình tạo ra đối tượng gọi là Khởi tạo (Instantiation).
    public class HangDienTu : HangHoa
    {
        //HangDienTu tivi = new HangDienTu(...), thì HangDienTu là Lớp,
        //còn tivi là Đối tượng cụ thể nằm trong bộ nhớ máy tính.
        public HangDienTu(string m, string t, string n, int s, decimal d) : base(m, t, n, s, d) { }
    }
}
