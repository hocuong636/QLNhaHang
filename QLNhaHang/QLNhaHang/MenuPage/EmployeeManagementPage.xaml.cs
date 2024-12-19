using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class EmployeeManagementPage : UserControl
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

        public EmployeeManagementPage()
        {
            InitializeComponent();
            LoadEmployeeData();
        }


        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MaNhanVien, HoTen, NTNS, ChucVu, GioiTinh, DiaChi, DienThoai, CCCD, Luong, MaQuyen FROM NhanVien";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    List<Employee> employees = new List<Employee>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        employees.Add(new Employee
                        {
                            MaNhanVien = row["MaNhanVien"].ToString(),
                            HoTen = row["HoTen"].ToString(),
                            NTNS = row["NTNS"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NTNS"]),
                            ChucVu = row["ChucVu"].ToString(),
                            GioiTinh = row["GioiTinh"].ToString(),
                            DiaChi = row["DiaChi"].ToString(),
                            DienThoai = row["DienThoai"].ToString(),
                            CCCD = row["CCCD"].ToString(),
                            Luong = row["Luong"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Luong"]),
                            MaQuyen = row["MaQuyen"].ToString()
                        });
                    }

                    lvNhanVien.ItemsSource = employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện thêm nhân viên
        private void click_ThemNV(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thêm nhân viên không được triển khai trong ví dụ này.");
        }

        // Sự kiện tìm kiếm nhân viên
        private void TimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txbTimKiem.Text.ToLower();
            var filteredEmployees = ((List<Employee>)lvNhanVien.ItemsSource)?.FindAll(emp =>
                emp.HoTen.ToLower().Contains(searchText) || emp.MaNhanVien.ToLower().Contains(searchText));
            lvNhanVien.ItemsSource = filteredEmployees;
        }

        // Sự kiện xóa nhân viên
        private void click_XoaNV(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItem is Employee selectedEmployee)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaNhanVien", selectedEmployee.MaNhanVien);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadEmployeeData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ColorZone_SuggestionChosen(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }

    // Lớp biểu diễn nhân viên
    public class Employee
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public DateTime? NTNS { get; set; }
        public string ChucVu { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string CCCD { get; set; }
        public decimal Luong { get; set; }
        public string MaQuyen { get; set; }
    }
}
