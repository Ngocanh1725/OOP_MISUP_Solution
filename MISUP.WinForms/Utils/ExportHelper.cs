using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISUP.Models;
using System.IO;
using System.Windows.Forms;

namespace MISUP.WinForms.Utils
{
    public static class ExportHelper
    {
        // Hàm này nhận vào danh sách Hàng Hóa và tự động lưu ra file CSV
        public static void ExportToCSV(List<HangHoa> dataList)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV File|*.csv", FileName = "BaoCaoKhoHang.csv" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder sb = new StringBuilder();
                        // 1. Tạo dòng tiêu đề (Header)
                        sb.AppendLine("Mã Hàng,Tên Hàng,Nhà Sản Xuất,Số Lượng,Đơn Giá,Phân Loại,Tổng Giá Trị (Sau Thuế)");

                        // 2. Đổ dữ liệu
                        foreach (var h in dataList)
                        {
                            string loai = GetLoaiViewString(h);
                            // Dùng dấu phẩy để ngăn cách các cột trong CSV
                            sb.AppendLine($"{h.MaHang},{h.TenHang},{h.NhaSanXuat},{h.SoLuongNhap},{h.DonGia},{loai},{h.TinhTongGiaTriSauThue()}");
                        }

                        // 3. Ghi ra file với chuẩn UTF-8 để không lỗi font tiếng Việt
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất báo cáo Excel (CSV) thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetLoaiViewString(HangHoa h)
        {
            return h switch
            {
                HangThucPham _ => "Thực Phẩm",
                HangDienTu _ => "Điện Tử",
                HangMyPham _ => "Mỹ Phẩm",
                HangGiaDung _ => "Gia Dụng",
                HangThoiTrang _ => "Thời Trang",
                _ => "Khác"
            };
        }
    }
}