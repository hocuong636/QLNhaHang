using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class EmployeeWindow : Window
    {
        private string _maNguoiDung;

        public EmployeeWindow(string maNguoiDung)
        {
            InitializeComponent();
            _maNguoiDung = maNguoiDung;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.MenuUserControl(_maNguoiDung);
        }

        private void TableStatusemployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.TableStatusUserControl();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                Application.Current.MainWindow.Show();
            }
        }
    }
}
