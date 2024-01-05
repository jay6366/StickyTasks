using StickyTasks.Model;
using StickyTasks.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace StickyTasks.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem> Items { get; set; }

        private MainWindow view;
        private ToDoItemRepository repository;

        private NotifyIcon notifyIcon;

        public event PropertyChangedEventHandler? PropertyChanged;

        

        public MainViewModel(
            MainWindow mainWindow,
            ToDoItemRepository toDoItemRepository
        )
        {
            view = mainWindow;
            repository = toDoItemRepository;
            Items = new ObservableCollection<ToDoItem>();
            this.LoadNote();

            // NotifyIcon 초기화
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information, // 예시로 시스템 아이콘을 사용
                Visible = true
            };
        }

        public void CheckAndNotifyDueTasks()
        {
            var items = repository.LoadToDoItems();
            var dueTasks = new StringBuilder();
            foreach (var item in items)
            {
                if (IsTaskDue(item.DueDate))
                {
                    dueTasks.AppendLine($"{item.Content} - Due Date: {item.DueDate}");
                }
            }
            if (dueTasks.Length > 0) 
            {
                ShowNotification(dueTasks.ToString());
            }
        }

        private bool IsTaskDue(string dueDateStr)
        {
            DateTime.TryParse(dueDateStr, out var dueDate);
            return DateTime.Now <= dueDate;
        }

        private void ShowNotification(string content)
        {
            // NotifyIcon을 사용하여 풍선 도움말 알림을 표시합니다.
            notifyIcon.BalloonTipTitle = "기한 알림";
            notifyIcon.BalloonTipText = content;
            notifyIcon.ShowBalloonTip(3000); // 3초 동안 표시
        }

        public void Dispose()
        {
            notifyIcon.Dispose();
        }

        public void LoadNote()
        {
            Items.Clear();
            foreach (ToDoItem item in repository.LoadToDoItems())
            {
                Items.Add(item);
            }
        }
        
        public void AddNote()
        {
            AddEditNoteWindow inputWindow = new AddEditNoteWindow();
            if (inputWindow.ShowDialog() == true)
            {
                repository.AddToDoItem(inputWindow.Result);
                this.LoadNote();
            }
        }

        public void EditNote(ToDoItem toDoItem)
        {
            AddEditNoteWindow inputWindow = new AddEditNoteWindow(toDoItem);
            if (inputWindow.ShowDialog() == true)
            {
                repository.UpdateToDoItem(inputWindow.Result);
                this.LoadNote();
            }
        }

        public void DeleteNote(ToDoItem toDoItem)
        {
            if(System.Windows.MessageBox.Show("Are you sure to delete the item?", "Alert", MessageBoxButton.OKCancel, MessageBoxImage.Warning) 
                == MessageBoxResult.OK)
            {
                repository.DeleteToDoItem(toDoItem);
                this.LoadNote();
            }
        }

    }
}
