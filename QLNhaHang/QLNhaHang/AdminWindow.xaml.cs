using System.Windows;

namespace QLNhaHang
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            ContentArea.Content = new DashboardPage();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DashboardPage(); // Thay bằng trang thực tế
        }

        private void EmployeeManagementButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeManagementPage(); // Thay bằng trang thực tế
        }

        private void TableStatusButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TableStatusPage();
        }

        private void WarehouseManagementButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new WarehouseManagementPage();
        }

        private void FoodManagementButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new FoodManagementPage();
        }

        private void InvoiceManagementButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InvoiceManagementPage();
        }

        private void ChamCongButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new MenuPage.ChamCong();
        }

        private void account_managementButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new MenuPage.Account_management();
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                Application.Current.MainWindow.Show();
            }
            // Hiện lại MainWindow
            Application.Current.MainWindow.Show();
        }

    }
}
