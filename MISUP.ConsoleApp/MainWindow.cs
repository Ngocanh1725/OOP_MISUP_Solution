using MISUP.BLL.Services;
using MISUP.ConsoleApp.Dialogs;
using MISUP.Models;
using NStack;
using System;
using System.Collections.Generic;
using System.Data;
using Terminal.Gui;
using static Terminal.Gui.TableView;

namespace MISUP.ConsoleApp
{
    public class MainWindow
    {
        private HangHoaBLL _db = new HangHoaBLL();
        private TaiKhoan _user;
        private TableView _tableView;
        private StatusBar _statusBar;
        private Window _win;

        public MainWindow(TaiKhoan user) { _user = user; }

        public void Run()
        {
            var top = new Toplevel() { ColorScheme = ThemeManager.HackerScheme };

            // 1. MENU CHUẨN MỰC
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_He Thong", new MenuItem [] {
                    new MenuItem ("_Thong Tin", "", () => MessageBox.Query("Thong Tin", "MISUP ERP Console v2.0\nPhan mem quan tri kho sieu thi.", "OK")),
                    new MenuItem ("_Dang Xuat", "Dang xuat khoi phien", () => Application.RequestStop())
                }),
                new MenuBarItem ("_Giao Dich", new MenuItem [] {
                    new MenuItem ("_Nhap Hang (Ctrl+N)", "Tao phieu nhap", () => ShowAddDialog(), null, null, Key.CtrlMask | Key.N),
                    new MenuItem ("_Xuat Huy (Del)", "Huy hang loi", () => DeleteSelected(), null, null, Key.Delete),
                    new MenuItem ("_Kiem Ke Kho", "Cap nhat so luong", () => ShowEditDialog(), null, null, Key.CtrlMask | Key.E)
                }),
                new MenuBarItem ("_Bao Cao", new MenuItem [] {
                    new MenuItem ("_Bao Cao Tong Hop", "", () => Application.Run(new DashboardDialog(_db))),
                    new MenuItem ("_Hang Can Date/Sap Het", "", () => LoadDataToTable(_db.LayHangSapHetTonKho()))
                })
            });

            // 2. KHUNG LÀM VIỆC CHÍNH
            _win = new Window($"[ HỆ THỐNG QUẢN TRỊ MISUP ERP ] - Đang trực: {_user.HoTen} ({_user.Quyen})")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var leftPane = new FrameView("Phan He Nghiep Vu") { X = 0, Y = 0, Width = 25, Height = Dim.Fill() };
            var rightPane = new FrameView("Du Lieu Master Data") { X = Pos.Right(leftPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            var menuList = new ListView(new ustring[] {
                " [F5]  Tai Lai Du Lieu",
                " [ADD] Nhap Hang Moi",
                " [EDT] Kiem Ke / Sua",
                " [DEL] Xuat Huy Hang",
                " [SRC] Tim Kiem (Ctrl+F)",
                " [RPT] Xem Dashboard",
                " [OUT] Dang Xuat"
            })
            { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill(), ColorScheme = ThemeManager.InputScheme };

            menuList.OpenSelectedItem += (e) => HandleMenuSelection(e.Item);

            _tableView = new TableView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                FullRowSelect = true,
                Style = new TableStyle { ShowVerticalCellLines = true, ShowHorizontalHeaderOverline = true }
            };

            // 3. THANH TRẠNG THÁI (HIỂN THỊ ĐỒNG HỒ & TỔNG KẾT)
            _statusBar = new StatusBar(new StatusItem[] {
                new StatusItem(Key.CtrlMask | Key.Q, "~CTRL-Q~ Thoat", () => Application.RequestStop()),
                new StatusItem(Key.CtrlMask | Key.N, "~CTRL-N~ Nhap Hang", () => ShowAddDialog()),
                new StatusItem(Key.CtrlMask | Key.F, "~CTRL-F~ Tim", () => ShowSearchDialog()),
                new StatusItem(Key.Null, "", null) // Index 3: Dùng để ghi text thống kê
            });

            leftPane.Add(menuList); rightPane.Add(_tableView); _win.Add(leftPane, rightPane);
            top.Add(menu, _win, _statusBar);

            // Cập nhật đồng hồ mỗi giây (UI Thread)
            Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (loop) => { UpdateStatusBar(); return true; });

            RefreshData();
            Application.Run(top);
        }

        private void HandleMenuSelection(int index)
        {
            switch (index)
            {
                case 0: RefreshData(); break;
                case 1: ShowAddDialog(); break;
                case 2: ShowEditDialog(); break;
                case 3: DeleteSelected(); break;
                case 4: ShowSearchDialog(); break;
                case 5: Application.Run(new DashboardDialog(_db)); break;
                case 6: if (MessageBox.Query("Thoat", "Ban chac chan muon thoat?", "Co", "Khong") == 0) Application.RequestStop(); break;
            }
        }

        private void UpdateStatusBar()
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _statusBar.Items[3].Title = (ustring)$"🕒 {time} | TONG SKU: {_db.DemTongSoMatHang()} | GIA TRI: {_db.TinhTongGiaTriKho():N0} VND";
            _statusBar.SetNeedsDisplay();
        }

        private void RefreshData() { LoadDataToTable(_db.LayDanhSach()); UpdateStatusBar(); }

        private void ShowSearchDialog()
        {
            var searchDlg = new SearchDialog();
            Application.Run(searchDlg);
            if (searchDlg.IsConfirmed) LoadDataToTable(_db.TimKiem(searchDlg.Keyword));
        }

        private void ShowAddDialog()
        {
            var dlg = new AddProductDialog(_db, null);
            Application.Run(dlg);
            if (dlg.IsSaved) RefreshData();
        }

        private void ShowEditDialog()
        {
            if (_tableView.SelectedRow < 0) { MessageBox.Query("Loi", "Chon 1 dong de thao tac!", "OK"); return; }
            string ma = _tableView.Table.Rows[_tableView.SelectedRow][0].ToString();

            HangHoa sp = _db.LayDanhSach().Find(x => x.MaHang == ma);
            if (sp != null)
            {
                var dlg = new AddProductDialog(_db, sp);
                Application.Run(dlg);
                if (dlg.IsSaved) RefreshData();
            }
        }

        private void DeleteSelected()
        {
            if (_tableView.SelectedRow < 0) { MessageBox.Query("Loi", "Chon 1 dong de xuat huy!", "OK"); return; }
            string ma = _tableView.Table.Rows[_tableView.SelectedRow][0].ToString();
            string ten = _tableView.Table.Rows[_tableView.SelectedRow][1].ToString();

            if (MessageBox.Query("Xuat Huy", $"Ban dang thuc hien lenh XUAT HUY mat hang:\n[{ma}] - {ten}\n\nXac nhan?", "DONG Y", "HUY BO") == 0)
            {
                try { _db.XoaHang(ma); RefreshData(); MessageBox.Query("Thanh cong", "Da xuat huy khoi he thong", "OK"); }
                catch (Exception ex) { MessageBox.ErrorQuery("Loi He Thong", ex.Message, "OK"); }
            }
        }

        private void LoadDataToTable(List<HangHoa> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SKU"); dt.Columns.Add("Ten Hang Hoa"); dt.Columns.Add("Nha SX"); dt.Columns.Add("Ton Kho"); dt.Columns.Add("Gia Von"); dt.Columns.Add("Tinh Trang");

            foreach (var item in list)
            {
                string status = item.SoLuongNhap == 0 ? "Het Hang" : (item.SoLuongNhap < 10 ? "Sap Het" : "On Dinh");
                dt.Rows.Add(item.MaHang, item.TenHang, item.NhaSanXuat, item.SoLuongNhap, item.DonGia.ToString("N0"), status);
            }

            _tableView.Table = dt;
            _tableView.Update();
            _win.Title = (ustring)$"[ HỆ THỐNG QUẢN TRỊ MISUP ERP ] - Đang hiển thị: {list.Count} bản ghi";
        }
    }
}