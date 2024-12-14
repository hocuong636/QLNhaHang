using System.Data.SqlClient;
using System;

public class RevenueDataAccess
{
    private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

    public decimal GetTotalRevenueThisMonth()
    {
        decimal totalRevenue = 0;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT SUM(TongTien) AS TotalRevenue FROM LichSuHoaDon";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        totalRevenue = Convert.ToDecimal(result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return totalRevenue;
    }
}
