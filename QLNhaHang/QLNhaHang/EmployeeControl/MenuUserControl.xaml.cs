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

namespace QLNhaHang.EmployeeControl
{
    /// <summary>
    /// Interaction logic for MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {
            InitializeComponent();
        }
        private void OnProcessButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO: Implement the logic for processing the order
            MessageBox.Show("Order is being processed!");
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO: Implement the logic for deleting the selected item(s) from the order
            MessageBox.Show("Selected item(s) deleted from the order!");
        }
    }
}
