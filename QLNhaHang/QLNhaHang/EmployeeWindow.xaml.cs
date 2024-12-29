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
            MessageBox.Show("Bạn đã đăng xuất thành công!");
            this.Close();
            Application.Current.MainWindow.Show();
        }
    }
}
