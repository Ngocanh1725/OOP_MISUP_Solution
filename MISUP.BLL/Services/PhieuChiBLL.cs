using MISUP.DAL.Repositories;
using MISUP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUP.BLL.Services
{
    public class PhieuChiBLL
    {
        private PhieuChiDAL _dal = new PhieuChiDAL();

        public List<PhieuChi> LayDanhSach() => _dal.LayDanhSach();

        public List<PhieuChi> TimKiem(string keyword)
        {
            keyword = keyword.ToLower();
            return _dal.LayDanhSach().Where(x =>
                x.MaPhieu.ToLower().Contains(keyword) ||
                x.TenNCC.ToLower().Contains(keyword)).ToList();
        }

        public void ThemPhieu(PhieuChi pc)
        {
            if (string.IsNullOrWhiteSpace(pc.MaPhieu)) throw new Exception("Lỗi: Mã phiếu không được trống!");
            if (pc.SoTien < 0) throw new Exception("Lỗi: Số tiền chi không được âm!");

            if (_dal.LayDanhSach().Any(x => x.MaPhieu.ToLower() == pc.MaPhieu.ToLower()))
                throw new Exception("Lỗi: Mã phiếu chi đã tồn tại!");

            _dal.Them(pc);
        }

        public void SuaPhieu(PhieuChi pc)
        {
            if (pc.SoTien < 0) throw new Exception("Lỗi: Số tiền chi không được âm!");
            _dal.Sua(pc);
        }

        public void XoaPhieu(string maPhieu)
        {
            _dal.Xoa(maPhieu);
        }
    }
}
