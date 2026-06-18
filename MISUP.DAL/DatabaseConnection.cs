using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.DAL
{
    // Lớp tĩnh (static) để mọi nơi trong DAL đều gọi được mà không cần dùng từ khóa 'new'
    public static class DatabaseConnection
    {
        // LƯU Ý: Đổi 'ADMIN-PC' thành tên Server SQL của máy bạn nếu cần.
        public static string ConnectionString { get; } = @"Server=ADMIN-PC;Database=QuanLyMISUP;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
