using StickyTasks.Model;
using StickyTasks.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StickyTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainViewModel(this, new ToDoItemRepository());
            this.DataContext = viewModel;
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
