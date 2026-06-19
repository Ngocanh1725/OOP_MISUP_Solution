using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NStack;
using Terminal.Gui;
using Application = Terminal.Gui.Application;

namespace MISUP.ConsoleApp.Dialogs
{
    public class SearchDialog : Dialog
    {
        public string Keyword { get; private set; } = "";
        public bool IsConfirmed { get; private set; } = false;

        public SearchDialog() : base("🔍 Tim Kiem San Pham", 50, 10)
        {
            this.ColorScheme = ThemeManager.HackerScheme;

            var txtTuKhoa = new TextField("") { X = 15, Y = 2, Width = 30, ColorScheme = ThemeManager.InputScheme };

            var btnTim = new Button("Tim Kiem") { X = Pos.Center() - 10, Y = 6, IsDefault = true };
            var btnBack = new Button("Huy") { X = Pos.Center() + 4, Y = 6 };

            btnTim.Clicked += () => {
                Keyword = txtTuKhoa.Text.ToString();
                IsConfirmed = true;
                Application.RequestStop();
            };

            btnBack.Clicked += () => Application.RequestStop();

            this.Add(new Label("Tu khoa:") { X = 2, Y = 2 }, txtTuKhoa, btnTim, btnBack);
        }
    }
}