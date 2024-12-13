using System;
using System.Windows;

namespace QLNhaHang
{
    /// <summary>
    /// Interaction logic for Form_login.xaml
    /// </summary>
    public partial class Form_login : Window
    {
        public Form_login()
        {
            InitializeComponent();
        }

        // Event handler for Sign Up button
        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Sign Up form
            Form_SignUp signUpForm = new Form_SignUp();
            signUpForm.Show();
            this.Close(); // Close the login form
        }

        // Event handler for Forgot Password button
        private void ForgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Forgot Password form
            Form_ForgotPassword forgotPasswordForm = new Form_ForgotPassword();
            forgotPasswordForm.Show();
            this.Close(); // Close the login form
        }
    }
}
