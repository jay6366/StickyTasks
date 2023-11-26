using Microsoft.VisualBasic;
using StickyTasks.Model;
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
using System.Windows.Shapes;

namespace StickyTasks.View
{
    /// <summary>
    /// AddEditNoteWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddEditNoteWindow : Window
    {
        public ToDoItem Result { get; set; }

        public AddEditNoteWindow(ToDoItem toDoItem = null)
        {
            InitializeComponent();
            if (toDoItem != null)
            {
                TitleTextBox.Text = toDoItem.DueDate.ToString();
                ContentTextBox.Text = toDoItem.Content;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Result = new ToDoItem
            {
                DueDate = TitleTextBox.Text,   
                Content = ContentTextBox.Text
            };
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
