using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();
            this.DataContext = new DashboardViewModel(); // Gán ViewModel làm DataContext
        }
    }
}
