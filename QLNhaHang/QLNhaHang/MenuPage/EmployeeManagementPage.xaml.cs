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

        // Tải dữ liệu nhân viên từ cơ sở dữ liệu
        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MaNhanVien, HoTen, NTNS, ChucVu, GioiTinh, DiaChi, DienThoai, CCCD, MaQuyen FROM NhanVien";
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
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO NhanVien (MaNhanVien, HoTen, NTNS, ChucVu, GioiTinh, DiaChi, DienThoai, CCCD, MaQuyen) " +
                                   "VALUES (@MaNhanVien, @HoTen, @NTNS, @ChucVu, @GioiTinh, @DiaChi, @DienThoai, @CCCD, @MaQuyen)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaNhanVien", txtMaNV.Text);
                    command.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    command.Parameters.AddWithValue("@NTNS", dpNgaySinh.SelectedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ChucVu", "Nhân viên");
                    command.Parameters.AddWithValue("@GioiTinh", "Nam"); // Bạn có thể thêm combobox chọn giới tính
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                    command.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                    command.Parameters.AddWithValue("@MaQuyen", txtMaQuyen.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadEmployeeData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện sửa nhân viên
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItem is Employee selectedEmployee)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE NhanVien SET HoTen = @HoTen, NTNS = @NTNS, GioiTinh = @GioiTinh, DiaChi = @DiaChi, " +
                                       "DienThoai = @DienThoai, CCCD = @CCCD, MaQuyen = @MaQuyen WHERE MaNhanVien = @MaNhanVien";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaNhanVien", txtMaNV.Text);
                        command.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                        command.Parameters.AddWithValue("@NTNS", dpNgaySinh.SelectedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@GioiTinh", "Nam");
                        command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                        command.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                        command.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                        command.Parameters.AddWithValue("@MaQuyen", txtMaQuyen.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadEmployeeData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Sự kiện xóa nhân viên
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
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

        private void EmployeeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNhanVien.SelectedItem is Employee selectedEmployee)
            {
                // Gán dữ liệu của nhân viên được chọn vào các trường chi tiết
                txtMaNV.Text = selectedEmployee.MaNhanVien;
                txtHoTen.Text = selectedEmployee.HoTen;
                txtDiaChi.Text = selectedEmployee.DiaChi;
                txtDienThoai.Text = selectedEmployee.DienThoai;
                txtCCCD.Text = selectedEmployee.CCCD;
                txtMaQuyen.Text = selectedEmployee.MaQuyen;
                dpNgaySinh.SelectedDate = selectedEmployee.NTNS;
            }
            else
            {
                // Nếu không có nhân viên nào được chọn, xóa dữ liệu trong các trường chi tiết
                ClearEmployeeDetails();
            }
        }

        // Hàm xóa dữ liệu trong các trường chi tiết
        private void ClearEmployeeDetails()
        {
            txtMaNV.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtMaQuyen.Text = string.Empty;
            dpNgaySinh.SelectedDate = null;
        }

    }

    // Lớp đại diện cho thông tin nhân viên
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
        public string MaQuyen { get; set; }
    }
}
