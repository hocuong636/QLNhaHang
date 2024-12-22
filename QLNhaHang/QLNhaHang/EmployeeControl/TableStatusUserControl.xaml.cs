using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang.EmployeeControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TableStatusUserControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<TableInfo> _tables;
        public ObservableCollection<TableInfo> Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(); }
        }

        public TableStatusUserControl()
        {
            InitializeComponent();
            InitializeTables();
            DataContext = this;
        }

        private void InitializeTables()
        {
            Tables = new ObservableCollection<TableInfo>();
            for (int i = 1; i <= 20; i++)
            {
                Tables.Add(new TableInfo
                {
                    TableName = $"Bàn {i}",
                    IsOccupied = i % 3 == 0 // Đánh dấu mỗi bàn thứ 3 là "Đang sử dụng"
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TableInfo : INotifyPropertyChanged
    {
        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(); }
        }

        private bool _isOccupied;
        public bool IsOccupied
        {
            get { return _isOccupied; }
            set { _isOccupied = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
