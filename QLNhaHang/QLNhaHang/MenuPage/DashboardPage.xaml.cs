using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();
            // Initialize other bindings or data contexts if needed
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
                string query = "SELECT SUM(TongTien) FROM HoaDon WHERE CAST(NgayTao AS DATE) = @SelectedDate";
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
    }
}
