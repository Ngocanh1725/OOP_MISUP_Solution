using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISUP.DAL.Repositories;
using MISUP.Models;

namespace MISUP.BLL.Services
{
    public class NhaCungCapBLL
    {
        private NhaCungCapDAL _dal = new NhaCungCapDAL();

        public List<NhaCungCap> LayDanhSach() => _dal.LayDanhSach();

        public List<NhaCungCap> TimKiem(string keyword)
        {
            keyword = keyword.ToLower();
            return _dal.LayDanhSach().Where(x =>
                x.TenNCC.ToLower().Contains(keyword) ||
                x.MaNCC.ToLower().Contains(keyword) ||
                x.DienThoai.Contains(keyword)).ToList();
        }

        public void ThemNCC(NhaCungCap ncc)
        {
            if (string.IsNullOrWhiteSpace(ncc.MaNCC) || string.IsNullOrWhiteSpace(ncc.TenNCC))
                throw new Exception("Lỗi: Mã và Tên nhà cung cấp không được để trống!");

            if (_dal.LayDanhSach().Any(x => x.MaNCC.ToLower() == ncc.MaNCC.ToLower()))
                throw new Exception("Lỗi: Mã nhà cung cấp đã tồn tại trong hệ thống!");

            _dal.Them(ncc);
        }

        public void SuaNCC(NhaCungCap ncc)
        {
            if (string.IsNullOrWhiteSpace(ncc.TenNCC))
                throw new Exception("Lỗi: Tên nhà cung cấp không được để trống!");
            _dal.Sua(ncc);
        }

        public void XoaNCC(string maNCC)
        {
            _dal.Xoa(maNCC);
        }
    }
}