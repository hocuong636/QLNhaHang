using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class DashboardPage : UserControl
    {
        public List<decimal> RevenueValues { get; set; }
        public List<string> RevenueLabels { get; set; }
        public Func<decimal, string> Formatter { get; set; }
        public DashboardPage()
        {
            InitializeComponent();
            TotalRevenueTextBlock_DataContextChanged();
            ActiveOrdersTextBlock_DataContextChanged();
        }

        private void LookupRevenue_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected date from the DatePicker
            if (SelectedDatePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = SelectedDatePicker.SelectedDate.Value;

                // Call a method to fetch revenue data for the selected date
                decimal revenue = GetRevenueForDate(selectedDate);

                // Update the LookedUpRevenueTextBlock to display the fetched revenue
                LookedUpRevenueTextBlock.Text = $"{revenue:N0} VNĐ"; // Format as currency
            }
            else
            {
                // Show a message if no date is selected
                MessageBox.Show("Vui lòng chọn ngày để tra cứu doanh thu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private decimal GetRevenueForDate(DateTime date)
        {
            // Replace with actual logic to fetch revenue from the database
            // This is a placeholder for demonstration purposes
            decimal revenue = 0;

            // Example logic: Query the database for the revenue of the given date
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT SUM(TongTien) FROM HoaDon WHERE CAST(NgayLap AS DATE) = @SelectedDate";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SelectedDate", date.Date);

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        revenue = Convert.ToDecimal(result);
                    }
                }
            }

            return revenue;
        }

        private void TotalRevenueTextBlock_DataContextChanged()
        {
            decimal revenue = 0;
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT SUM(TongTien) FROM LichSuHoaDon";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        revenue = Convert.ToDecimal(result);
                        TotalRevenueTextBlock.Text = $"{revenue:N0} VNĐ";
                    }
                }
            }
        }

        private void ActiveOrdersTextBlock_DataContextChanged()
        {
            int activeOrders = 0;
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Ban WHERE TrangThai LIKE N'Đang phục vụ'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        activeOrders = Convert.ToInt32(result);
                        ActiveOrdersTextBlock.Text = activeOrders.ToString();
                    }
                }
            }
        }

        private void RevenueChart_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRevenueData();
        }
        private void LoadRevenueData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";
            string query = @"
            SELECT TOP 7 
                CONVERT(date, NgayLap) AS Ngay, 
                SUM(TongTien) AS DoanhThu
            FROM HoaDon
            GROUP BY CONVERT(date, NgayLap)
            ORDER BY Ngay DESC";

            RevenueValues = new List<decimal>();
            RevenueLabels = new List<string>();

            using(SqlConnection conn = DatabaseConnection.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    decimal revenue = reader.GetDecimal(1);

                    RevenueLabels.Insert(0, date.ToString("dd/MM"));
                    RevenueValues.Insert(0, revenue);
                }
            }
        }
    }
}
