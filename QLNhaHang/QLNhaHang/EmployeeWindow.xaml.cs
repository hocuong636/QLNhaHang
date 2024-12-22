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
            ContentArea.Content = new Employee.MenuUserControl();
        }

        private void TableStatusemployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.TableStatusUserControl();
        }

        private void KitchenButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EmployeeControl.KitchenUserControl();
        }
    }
}
