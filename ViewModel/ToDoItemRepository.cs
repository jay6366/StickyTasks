using StickyTasks.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace StickyTasks.ViewModel
{
    /*데이터베이스 연결 및 CRUD 작업을 관리하는 클래스*/
    public class ToDoItemRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ConnectionString;

        public ToDoItemRepository()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {

            // 데이터베이스 스키마 생성 및 초기화
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS T_TASKS (TS_KEY INTEGER PRIMARY KEY AUTOINCREMENT, 
                                                                        TS_Content TEXT, 
                                                                        TS_DueDate TEXT)";
                cmd.ExecuteNonQuery();
            }
        }

        public void AddToDoItem(ToDoItem item)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "INSERT INTO T_TASKS (TS_Content, TS_DueDate) VALUES (@TS_Content, @TS_DueDate)";
                cmd.Parameters.AddWithValue("@TS_Content", item.Content);
                cmd.Parameters.AddWithValue("@TS_DueDate", item.DueDate);
                cmd.ExecuteNonQuery();
            }
        }

        public List<ToDoItem> LoadToDoItems()
        {
            List<ToDoItem> items = new List<ToDoItem>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM T_TASKS", conn);
                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        ToDoItem item = new ToDoItem
                        {
                            Key = reader["TS_KEY"].ToString(),
                            Content = reader["TS_Content"].ToString(),
                            DueDate = reader["TS_DueDate"].ToString()
                        };
                        items.Add(item);
                    }                   
                } 
            }
            return items;
        }

        public void UpdateToDoItem(ToDoItem item)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "UPDATE T_TASKS SET TS_Content = @TS_Content, TS_DueDate = @TS_DueDate WHERE TS_KEY = @TS_KEY";
                cmd.Parameters.AddWithValue("@TS_Content", item.Content);
                cmd.Parameters.AddWithValue("@TS_DueDate", item.DueDate);
                cmd.Parameters.AddWithValue("@TS_KEY", item.Key);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteToDoItem(ToDoItem item)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "DELETE FROM T_TASKS WHERE TS_KEY = @TS_KEY";
                cmd.Parameters.AddWithValue("@TS_KEY", item.Key);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
