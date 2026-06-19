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

        public MainWindow(TaiKhoan user) { _user = user; }

        public void Run()
        {
            var top = new Toplevel() { ColorScheme = ThemeManager.HackerScheme };

            // 1. MENU
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_He Thong", new MenuItem [] { new MenuItem ("_Dang Xuat", "", () => Application.RequestStop()) }),
                new MenuBarItem ("_Chuc Nang", new MenuItem [] {
                    new MenuItem ("_Them Moi (Ctrl+N)", "", () => ShowAddDialog(), null, null, Key.CtrlMask | Key.N),
                    new MenuItem ("_Tim Kiem (Ctrl+F)", "", () => ShowSearchDialog(), null, null, Key.CtrlMask | Key.F)
                })
            });

            // 2. LAYOUT CHÍNH
            var win = new Window($"[ MISUP ERP ] - Hien hanh: {_user.HoTen}") { X = 0, Y = 1, Width = Dim.Fill(), Height = Dim.Fill() - 1 };
            var leftPane = new FrameView("Menu") { X = 0, Y = 0, Width = Dim.Percent(20), Height = Dim.Fill() };
            var rightPane = new FrameView("Bang Du Lieu") { X = Pos.Right(leftPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            var menuList = new ListView(new ustring[] { "1. Tai Lai (F5)", "2. Them Moi", "3. Sua", "4. Xoa", "5. Tim Kiem", "6. Dashboard", "0. Dang Xuat" }) { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };
            menuList.OpenSelectedItem += (e) => HandleMenuSelection(e.Item);

            // ĐÃ FIX LỖI Ở ĐÂY: Xóa AlwaysHighlightSelection
            _tableView = new TableView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                FullRowSelect = true,
                Style = new TableStyle { ShowVerticalCellLines = true }
            };

            // 3. STATUS BAR
            _statusBar = new StatusBar(new StatusItem[] {
                new StatusItem(Key.CtrlMask | Key.Q, "~CTRL-Q~ Thoat", () => Application.RequestStop()),
                new StatusItem(Key.F5, "~F5~ Lam Moi", () => RefreshData()),
                new StatusItem(Key.Null, "", null)
            });

            leftPane.Add(menuList); rightPane.Add(_tableView); win.Add(leftPane, rightPane);
            top.Add(menu, win, _statusBar);

            RefreshData();
            Application.Run(top);
        }

        // ĐIỀU HƯỚNG TỪ MENU
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
                case 6: if (MessageBox.Query("Thoat", "Ban chac chu?", "Co", "Khong") == 0) Application.RequestStop(); break;
            }
        }

        private void UpdateStatusBar() => _statusBar.Items[2].Title = (ustring)$"Tong SP: {_db.DemTongSoMatHang()} | Tong Tien: {_db.TinhTongGiaTriKho():N0} VND";

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
            if (_tableView.SelectedRow < 0) { MessageBox.Query("Loi", "Chon 1 dong de sua!", "OK"); return; }
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
            if (_tableView.SelectedRow < 0) { MessageBox.Query("Loi", "Chon 1 dong de xoa!", "OK"); return; }
            string ma = _tableView.Table.Rows[_tableView.SelectedRow][0].ToString();
            if (MessageBox.Query("Xoa", $"Xoa san pham {ma}?", "Co", "Khong") == 0)
            {
                try { _db.XoaHang(ma); RefreshData(); } catch (Exception ex) { MessageBox.ErrorQuery("Loi", ex.Message, "OK"); }
            }
        }

        private void LoadDataToTable(List<HangHoa> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ma"); dt.Columns.Add("Ten SP"); dt.Columns.Add("Nha SX"); dt.Columns.Add("So Luong"); dt.Columns.Add("Don Gia"); dt.Columns.Add("Loai");
            foreach (var item in list) dt.Rows.Add(item.MaHang, item.TenHang, item.NhaSanXuat, item.SoLuongNhap, item.DonGia.ToString("N0"), item.GetType().Name.Replace("Hang", ""));
            _tableView.Table = dt; _tableView.Update();
        }
    }
}