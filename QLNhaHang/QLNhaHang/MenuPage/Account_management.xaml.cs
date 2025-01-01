using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLNhaHang.MenuPage
{
    /// <summary>
    /// Interaction logic for Account_management.xaml
    /// </summary>
    public partial class Account_management : UserControl
    {
        public Account_management()
        {
            InitializeComponent();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Nếu TextBox chứa placeholder, xóa nội dung và đổi màu chữ
                if (textBox.Text == "Email hoặc Tên Đăng Nhập" || textBox.Text == "Mã OTP (6 chữ số)")
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = Brushes.Black;
                }
            }
        }

        // Phương thức xử lý khi TextBox mất focus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Nếu TextBox để trống, hiển thị lại placeholder và đổi màu chữ
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox.Name == "txtRecovery")
                    {
                        textBox.Text = "Email hoặc Tên Đăng Nhập";
                    }
                    else if (textBox.Name == "txtOtp")
                    {
                        textBox.Text = "Mã OTP (6 chữ số)";
                    }
                    textBox.Foreground = Brushes.Gray;
                }
            }
        }
        private Dictionary<string, string> userDatabase = new Dictionary<string, string>
{
    { "admin@example.com", "123456" },
    { "user@example.com", "UserPassword456" }
};


        private void SearchUser_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtRecovery.Text.Trim();

            if (userDatabase.TryGetValue(userInput, out string currentPassword))
            {
                txtCurrentPassword.Text = currentPassword;
                txtNotification.Text = "Thông tin tài khoản đã được tìm thấy.";
                txtNotification.Foreground = Brushes.Green;
                txtNotification.Visibility = Visibility.Visible;
            }
            else
            {
                txtCurrentPassword.Text = string.Empty;
                txtNotification.Text = "Không tìm thấy tài khoản. Vui lòng kiểm tra lại.";
                txtNotification.Foreground = Brushes.Red;
                txtNotification.Visibility = Visibility.Visible;
            }
        }
        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtRecovery.Text.Trim();
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            if (!userDatabase.ContainsKey(userInput))
            {
                txtNotification.Text = "Vui lòng tìm tài khoản trước khi cập nhật.";
                txtNotification.Foreground = Brushes.Red;
                txtNotification.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                txtNotification.Text = "Mật khẩu mới không được để trống.";
                txtNotification.Foreground = Brushes.Red;
                txtNotification.Visibility = Visibility.Visible;
                return;
            }

            if (newPassword != confirmPassword)
            {
                txtNotification.Text = "Mật khẩu mới không khớp. Vui lòng kiểm tra lại.";
                txtNotification.Foreground = Brushes.Red;
                txtNotification.Visibility = Visibility.Visible;
                return;
            }

            // Cập nhật mật khẩu
            userDatabase[userInput] = newPassword;
            txtNotification.Text = "Mật khẩu đã được cập nhật thành công.";
            txtNotification.Foreground = Brushes.Green;
            txtNotification.Visibility = Visibility.Visible;
        }


    }
}
