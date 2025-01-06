using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Configurations;

namespace QLNhaHang
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private decimal _dayTotalRevenue;
        private decimal _monthTotalRevenue;
        public List<decimal> RevenueValues { get; set; }
        public List<string> RevenueLabels { get; set; }

        public decimal DayTotalRevenue
        {
            get => _dayTotalRevenue;
            set
            {
                _dayTotalRevenue = value;
                OnPropertyChanged();
            }
        }

        public decimal MonthTotalRevenue
        {
            get => _monthTotalRevenue;
            set
            {
                _monthTotalRevenue = value;
                OnPropertyChanged();
            }
        }

        public DashboardViewModel()
        {
            LoadDailyRevenue();
            LoadMonthlyRevenue();
            LoadWeeklyRevenue();
        }

        private void LoadDailyRevenue()
        {
            DayTotalRevenue = GetRevenueForDate(DateTime.Now);
        }

        private void LoadMonthlyRevenue()
        {
            MonthTotalRevenue = GetMonthlyRevenue();
        }

        private void LoadWeeklyRevenue()
        {
            RevenueValues = new List<decimal>();
            RevenueLabels = new List<string> { "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy", "Chủ Nhật" };

            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1); // Thứ Hai
            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startOfWeek.AddDays(i);
                decimal revenue = GetRevenueForDate(currentDate);
                RevenueValues.Add(revenue);
            }
        }

        public decimal GetRevenueForDate(DateTime date)
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

        private decimal GetMonthlyRevenue()
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
                    }
                }
            }
            return revenue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}