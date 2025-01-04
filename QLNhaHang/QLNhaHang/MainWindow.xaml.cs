using QLNhaHang.EmployeeControl;
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
                    string query = "SELECT MaQuyen, MaNguoiDung FROM NguoiDung WHERE TenDangNhap=@Username AND MatKhau=@Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int role = Convert.ToInt32(reader["MaQuyen"]);
                            string maNguoiDung = reader["MaNguoiDung"].ToString();

                            if (role == 1) // Admin
                            {
                                AdminWindow adminWindow = new AdminWindow();
                                adminWindow.Show();
                                this.Hide();
                                SetNull();
                            }
                            else if (role == 2) // Employee
                            {
                                EmployeeWindow employeeWindow = new EmployeeWindow(maNguoiDung);
                                employeeWindow.Show();
                                this.Hide();
                                SetNull();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                            SetNull();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                SetNull();
            }
        }

        private void SetNull()
        {
            UsernameTextBox.Text = string.Empty;    
            PasswordBox.Password = string.Empty; 
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
