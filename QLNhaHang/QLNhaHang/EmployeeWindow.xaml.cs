using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.MenuUserControl();
        }

        private void TableStatusemployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.TableStatusUserControl();
        }

        private void KitchenButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.KitchenUserControl();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã đăng xuất thành công!");
            this.Close();
            Application.Current.MainWindow.Show();
        }
    }
}
