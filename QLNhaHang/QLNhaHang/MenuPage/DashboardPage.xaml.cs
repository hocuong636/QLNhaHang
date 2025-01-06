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
            DayTotalRevenue_DataContextChanged();
            MonthTotalRevenue_DataContextChanged();
        }

        private void LookupRevenue_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDatePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = SelectedDatePicker.SelectedDate.Value;

                decimal revenue = GetRevenueForDate(selectedDate);

                LookedUpRevenueTextBlock.Text = $"{revenue:N0} VNĐ"; // Format as currency
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ngày để tra cứu doanh thu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private decimal GetRevenueForDate(DateTime date)
        {
            decimal revenue = 0;

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

        private void DayTotalRevenue_DataContextChanged()
        {
            decimal revenue = 0;
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT SUM(TongTien) FROM HoaDon WHERE CAST(NgayLap AS DATE) = CAST(GETDATE() AS DATE)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        revenue = Convert.ToDecimal(result);
                        DayTotalRevenueTextBlock.Text = $"{revenue:N0} VNĐ";
                    }
                }
            }
        }

        private void MonthTotalRevenue_DataContextChanged()
        {
            decimal revenue = 0;
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT SUM(TongTien) 
                                FROM HoaDon 
                                WHERE MONTH(NgayLap) = MONTH(GETDATE()) 
                                AND YEAR(NgayLap) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        revenue = Convert.ToDecimal(result);
                        MonthTotalRevenueTextBlock.Text = $"{revenue:N0} VNĐ";
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
