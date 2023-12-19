using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTasks.Model
{
    public class ToDoItem : INotifyPropertyChanged
    {
        private string _Key;
        public string Key { get { return _Key; } set { _Key = value; } }

        private string _Content;
        private string _DueDate;
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                OnPropertyChanged("Content");
            }
        }
        public string DueDate
        {
            get
            {
                return _DueDate;
            }
            set
            {
                _DueDate = value;
                OnPropertyChanged("DueDate");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
