using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;

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
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";
        public TableStatusUserControl()
        {
            InitializeComponent();
            SetupTableButtons();
            UpdateTableStatus();
        }

        private void UpdateTableStatus()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SoBan, TrangThai FROM Ban";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maBan = reader.GetInt32(0);
                            string trangThai = reader.GetString(1);
                            // Tìm button tương ứng
                            string buttonName = $"btn{maBan}";
                            Button btn = this.FindName(buttonName) as Button;
                            if (btn != null)
                            {
                                // Đổi màu button dựa trên trạng thái
                                if (trangThai == "Đang phục vụ")
                                {
                                    btn.Background = Brushes.Red;
                                }
                                else
                                {
                                    btn.Background = Brushes.Green;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load trạng thái bàn: {ex.Message}");
            }
        }
        private void RefreshTableStatus(object sender, RoutedEventArgs e)
        {
            UpdateTableStatus();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SetupTableButtons()
       {
            // Giả sử các button được đặt trong một panel tên là panelTables
            // Nếu các button nằm trong một ScrollViewer
            if (scrollViewer != null)
            {
                // Lấy panel chứa các button (thường là Grid hoặc StackPanel trong XAML)
                var panel = scrollViewer.Content as Panel;
                if (panel != null)
                {
                    foreach (var element in panel.Children)
                    {
                        if (element is Button btn && btn.Name.StartsWith("btn"))
                        {
                            string numberStr = btn.Name.Replace("btn", "");
                            if (int.TryParse(numberStr, out int tableNumber))
                            {
                                btn.Tag = tableNumber;
                                btn.Click -= Table_Click;
                                btn.Click += Table_Click;
                            }
                        }
                    }
                }
            }
        }
        public void Table_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn && btn.Tag != null)
                {
                    int maBan = Convert.ToInt32(btn.Tag);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"SELECT 
                                  hd.MaHoaDon,
                                  ma.TenMonAn,
                                  cthd.SoLuong,
                                  cthd.SoLuong*cthd.SoLuong as ThanhTien
                              FROM HoaDon hd
                              INNER JOIN ChiTietHoaDon cthd ON hd.MaHoaDon = cthd.MaHoaDon
                              INNER JOIN MonAn ma ON cthd.MaMonAn = ma.MaMonAn
                              WHERE hd.SoBan = @MaBan
                              ORDER BY hd.MaHoaDon";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaBan", maBan);
                            DataTable dt = new DataTable();
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                            OrderDetailsGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                    // Cập nhật lại trạng thái các bàn sau khi click
                    UpdateTableStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void PaymentButton_Click(object sender, EventArgs e)
        {
            // Logic xử lý sự kiện PaymentButton_Click
        }
        public void TransferButton_Click(object sender, EventArgs e)
        {
            // Logic xử lý sự kiện khi nhấn nút Transfer
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
