using StickyTasks.Model;
using StickyTasks.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace StickyTasks.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem> Items { get; set; }

        private MainWindow view;
        private ToDoItemRepository repository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel(
            MainWindow mainWindow,
            ToDoItemRepository toDoItemRepository
        )
        {
            view = mainWindow;
            repository = toDoItemRepository;
            Items = new ObservableCollection<ToDoItem>();
        }
        
        public void AddNote()
        {
            AddEditNoteWindow inputWindow = new AddEditNoteWindow();
            if (inputWindow.ShowDialog() == true)
            {
                Items.Add(inputWindow.Result);
            }
        }

        public void EditNote(ToDoItem toDoItem)
        {
            AddEditNoteWindow inputWindow = new AddEditNoteWindow(toDoItem);
            if (inputWindow.ShowDialog() == true)
            {
                toDoItem.DueDate = inputWindow.Result.DueDate;
                toDoItem.Content = inputWindow.Result.Content;                
            }
        }

        public void DeleteNote(ToDoItem toDoItem)
        {
            if(MessageBox.Show("Are you sure to delete the item?", "Alert", MessageBoxButton.OKCancel, MessageBoxImage.Warning) 
                == MessageBoxResult.OK)
            {
                Items.Remove(toDoItem);
            }
        }

    }
}
