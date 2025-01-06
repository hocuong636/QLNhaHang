using LiveCharts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class DashboardPage : UserControl
    {
        public DashboardViewModel ViewModel { get; set; }
        public List<decimal> RevenueValues { get; set; }
        public List<string> RevenueLabels { get; set; }
        public Func<decimal, string> Formatter { get; set; }
        public DashboardPage()
        {
            InitializeComponent();
            DayTotalRevenue_DataContextChanged();
            MonthTotalRevenue_DataContextChanged();
            ViewModel = new DashboardViewModel();
            DataContext = ViewModel;

            // Cập nhật biểu đồ
            UpdateChart();
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
        private void UpdateChart()
        {
            // Cập nhật giá trị cho biểu đồ
            RevenueChart.Series[0].Values = new ChartValues<decimal>(ViewModel.RevenueValues);
            RevenueChart.AxisX[0].Labels = ViewModel.RevenueLabels;
        }

    }
}
