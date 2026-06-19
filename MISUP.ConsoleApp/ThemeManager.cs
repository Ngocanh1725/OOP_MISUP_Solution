using System;
using Terminal.Gui;

// DÒNG NÀY ĐỂ FIX LỖI XUNG ĐỘT TÊN ATTRIBUTE
using Attribute = Terminal.Gui.Attribute;

namespace MISUP.ConsoleApp
{
    public static class ThemeManager
    {
        public static ColorScheme HackerScheme { get; private set; }
        public static ColorScheme InputScheme { get; private set; }

        public static void Initialize()
        {
            HackerScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.White, Color.Black),
                Focus = new Attribute(Color.White, Color.Black),
                HotNormal = new Attribute(Color.Cyan, Color.Black),
                HotFocus = new Attribute(Color.Cyan, Color.Black)
            };

            InputScheme = new ColorScheme()
            {
                Normal = new Attribute(Color.Black, Color.Cyan),
                Focus = new Attribute(Color.White, Color.DarkGray)
            };
        }
    }
}