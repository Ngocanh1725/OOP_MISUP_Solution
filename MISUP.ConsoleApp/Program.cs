using MISUP.ConsoleApp.Dialogs;
using MISUP.Models;
using System;
using Terminal.Gui;
// DÒNG NÀY ĐỂ FIX LỖI AMBIGUOUS (XUNG ĐỘT TÊN)
using Application = Terminal.Gui.Application;

namespace MISUP.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            ThemeManager.Initialize();

            while (true)
            {
                var loginDialog = new LoginDialog();
                Application.Run(loginDialog);

                if (loginDialog.AuthenticatedUser == null)
                    break;

                var mainWindow = new MainWindow(loginDialog.AuthenticatedUser);
                mainWindow.Run();
            }

            Application.Shutdown();
        }
    }
}