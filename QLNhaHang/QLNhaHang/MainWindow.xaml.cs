using System;
using System.Data.SqlClient;
using System.Windows;

namespace QLNhaHang
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MaQuyen FROM NguoiDung WHERE TenDangNhap=@Username AND MatKhau=@Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int role = Convert.ToInt32(result);
                        if (role == 1) // Admin
                        {
                            MessageBox.Show("Đăng nhập thành công! Bạn là Quản trị viên.");
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            this.Hide();
                        }
                        else if (role == 2) // Employee
                        {
                            MessageBox.Show("Đăng nhập thành công! Bạn là Nhân viên.");
                            EmployeeWindow employeeWindow = new EmployeeWindow();
                            employeeWindow.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }

        }
    }
}
