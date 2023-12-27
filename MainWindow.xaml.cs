using StickyTasks.Model;
using StickyTasks.ViewModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StickyTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Threading.Timer checkTimer;
        public MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel(this, new ToDoItemRepository());
            this.DataContext = viewModel;
            checkTimer = new System.Threading.Timer(CheckDueTasks, null, 0, 60000); // 1분마다 체크
        }

        private void CheckDueTasks(object state)
        {
            viewModel.CheckAndNotifyDueTasks();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddNote();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            viewModel.EditNote(editButton.DataContext as ToDoItem);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            viewModel.DeleteNote(deleteButton.DataContext as ToDoItem);
        }
    }
}
