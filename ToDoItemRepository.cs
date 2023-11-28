using StickyTasks.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTasks.ViewModel
{
    /*데이터베이스 연결 및 CRUD 작업을 관리하는 클래스*/
    public class ToDoItemRepository
    {
        private string connectionString = "Data Source=StickyTasks.sqlite;Version=3;";

        public ToDoItemRepository()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            // 데이터베이스 파일이 없으면 생성
            if (!File.Exists("StickyTasks.sqlite"))
            {
                SQLiteConnection.CreateFile("StickyTasks.sqlite");
            }

            // 데이터베이스 스키마 생성 및 초기화
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS ToDoItems (Id INTEGER PRIMARY KEY, Content TEXT, DueDate TEXT)";
                cmd.ExecuteNonQuery();
            }
        }

        public void AddToDoItem(ToDoItem item)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "INSERT INTO ToDoItems (Content, DueDate) VALUES (@Content, @DueDate)";
                cmd.Parameters.AddWithValue("@Content", item.Content);
                cmd.Parameters.AddWithValue("@DueDate", item.DueDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateToDoItem(ToDoItem item)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "UPDATE ToDoItems SET Content = @Content, DueDate = @DueDate WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Content", item.Content);
                cmd.Parameters.AddWithValue("@DueDate", item.DueDate);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteToDoItem(ToDoItem item)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "DELETE FROM ToDoItems WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
