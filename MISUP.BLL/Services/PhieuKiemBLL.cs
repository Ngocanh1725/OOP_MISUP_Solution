using MISUP.DAL.Repositories;
using MISUP.Models;
using MISUP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MISUP.BLL.Services
{
    public class PhieuKiemBLL
    {
        private PhieuKiemDAL _dal = new PhieuKiemDAL();

        public List<PhieuKiem> LayDanhSach() => _dal.LayDanhSach();

        public List<PhieuKiem> TimKiem(string keyword)
        {
            keyword = keyword.ToLower();
            return _dal.LayDanhSach().Where(x =>
                x.MaKK.ToLower().Contains(keyword) ||
                x.NhanVien.ToLower().Contains(keyword) ||
                x.KhoKiem.ToLower().Contains(keyword)).ToList();
        }

        public void ThemPhieu(PhieuKiem pk)
        {
            if (string.IsNullOrWhiteSpace(pk.MaKK)) throw new Exception("Lỗi: Mã kiểm kê không được trống!");

            if (_dal.LayDanhSach().Any(x => x.MaKK.ToLower() == pk.MaKK.ToLower()))
                throw new Exception("Lỗi: Mã phiếu kiểm kê đã tồn tại!");

            _dal.Them(pk);
        }

        public void SuaPhieu(PhieuKiem pk)
        {
            _dal.Sua(pk);
        }

        public void XoaPhieu(string maKK)
        {
            _dal.Xoa(maKK);
        }

        public void DuyetPhieu(string maKK)
        {
            _dal.DuyetCanBang(maKK);
        }
    }
}