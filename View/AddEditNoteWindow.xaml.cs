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
                Result = toDoItem;
                txtContent.Text = toDoItem.Content;
                txtDueDate.SelectedDate = DateTime.TryParse(toDoItem.DueDate, out DateTime parsedDate) ? parsedDate : (DateTime?)null;
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContent.Text) && txtContent.Text.Length > 0)
            {
                tbContent.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbContent.Visibility = Visibility.Visible;
            }

        }

        //private void txtDueDate_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtDueDate.Text)&&txtDueDate.Text.Length>0) 
        //    {
        //        tbDueDate.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        tbDueDate.Visibility =Visibility.Visible;
        //    }
            
        //}

        private void tbContent_MouseDown(object sender, MouseEventArgs e)
        {
            txtContent.Focus();
        }

        private void tbDueDate_MouseDown(object sender, MouseEventArgs e)
        {
            txtDueDate.Focus();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(Result == null)
            {
                Result = new ToDoItem
                {
                    Content = txtContent.Text,
                    DueDate = txtDueDate.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty
                };
            }
            else
            {
                Result.Content = txtContent.Text;
                Result.DueDate = txtDueDate.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
