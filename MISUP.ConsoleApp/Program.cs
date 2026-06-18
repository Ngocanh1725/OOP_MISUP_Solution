using System;
using Terminal.Gui;
using MISUP.Models;

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
                // ĐÃ SỬA: Không truyền (db) vào nữa vì LoginDialog đã dùng AuthBLL bên trong
                var loginDialog = new LoginDialog();
                Application.Run(loginDialog);

                if (loginDialog.AuthenticatedUser == null)
                    break;

                // ĐÃ SỬA: MainWindow giờ cũng chỉ cần truyền 'user', không truyền (db) nữa
                var mainWindow = new MainWindow(loginDialog.AuthenticatedUser);
                mainWindow.Run();
            }

            Application.Shutdown();
        }
    }
}