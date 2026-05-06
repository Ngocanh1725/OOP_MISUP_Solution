using System;
using System.Collections.Generic;
using System.Data;
using NStack;
using Terminal.Gui;
using MISUP.Models;

namespace MISUP.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            ThemeManager.Initialize();
            IQuanLyHangHoa db = new DatabaseHelper();

            while (true)
            {
                var loginDialog = new LoginDialog(db);
                Application.Run(loginDialog);
                if (loginDialog.AuthenticatedUser == null) break;

                var mainWindow = new MainWindow(loginDialog.AuthenticatedUser, db);
                mainWindow.Run();
            }
            Application.Shutdown();
        }
    }
}

