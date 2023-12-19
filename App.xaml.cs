using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
using System.IO;

namespace StickyTasks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ConnectDB();
        }

        private void ConnectDB()
        {
            string filePath = @"C:\users\junhy\source\StickyTasks\test.db";

            // 데이터베이스 파일이 없으면 생성
            if (!File.Exists(filePath))
            {
                SQLiteConnection.CreateFile(filePath);
            }
        }
    }
}
