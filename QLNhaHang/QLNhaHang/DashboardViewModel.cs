using System.ComponentModel;

namespace QLNhaHang
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private string _revenueThisMonth;

        public string RevenueThisMonth
        {
            get => _revenueThisMonth;
            set
            {
                _revenueThisMonth = value;
                OnPropertyChanged(nameof(RevenueThisMonth));
            }
        }

        public DashboardViewModel()
        {
            LoadRevenue();
        }

        private void LoadRevenue()
        {
            var dataAccess = new RevenueDataAccess();
            RevenueThisMonth = $"{dataAccess.GetTotalRevenueThisMonth():N0} VND";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
