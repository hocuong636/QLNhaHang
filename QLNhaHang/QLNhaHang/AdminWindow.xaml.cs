﻿using System.Windows;

namespace QLNhaHang
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
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

        private void KitchenManagementButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new KitchenManagementPage();
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
    }
}
