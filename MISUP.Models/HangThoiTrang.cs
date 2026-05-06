using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.Models
{
    public class HangThoiTrang : HangHoa
    {
        public HangThoiTrang(string m, string t, string n, int s, decimal d) : base(m, t, n, s, d) { }
    }
}

