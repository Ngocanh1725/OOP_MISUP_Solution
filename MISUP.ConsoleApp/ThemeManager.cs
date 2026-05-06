using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MISUP.ConsoleApp
{
    public static class ThemeManager
    {
        public static ColorScheme HackerScheme { get; private set; }
        public static ColorScheme InputScheme { get; private set; }

        public static void Initialize()
        {
            HackerScheme = new ColorScheme() { Normal = Application.Driver.MakeAttribute(Color.White, Color.Black), Focus = Application.Driver.MakeAttribute(Color.White, Color.Black), HotNormal = Application.Driver.MakeAttribute(Color.Cyan, Color.Black), HotFocus = Application.Driver.MakeAttribute(Color.Cyan, Color.Black) };
            InputScheme = new ColorScheme() { Normal = Application.Driver.MakeAttribute(Color.Black, Color.Cyan), Focus = Application.Driver.MakeAttribute(Color.White, Color.DarkGray) };
        }
    }
}


