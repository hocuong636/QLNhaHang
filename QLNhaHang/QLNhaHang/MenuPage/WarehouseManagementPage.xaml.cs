using iTextSharp.text;
using QLNhaHang.EmployeeControl;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLNhaHang
{
    public partial class WarehouseManagementPage : UserControl
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLNhaHang.mdf;Integrated Security=True";

        public ObservableCollection<NguyenLieu> Ingredients { get; set; }

        public WarehouseManagementPage()
        {
            InitializeComponent();
            Ingredients = new ObservableCollection<NguyenLieu>();
            LoadIngredients();
            InventoryDataGrid.ItemsSource = Ingredients;
        }

        private void LoadIngredients()
        {
            Ingredients.Clear(); // Xóa danh sách cũ
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM NguyenLieu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Ingredients.Add(new NguyenLieu
                                {
                                    MaNguyenlieu = reader.GetInt32(0),
                                    TenNguyenLieu = reader.GetString(1),
                                    SoLuong = reader.GetInt32(2),
                                    DonVi = !reader.IsDBNull(3) ? reader.GetString(3) : string.Empty,
                                    DonGia = !reader.IsDBNull(4) ? (decimal)reader.GetDouble(4) : 0m
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nguyên liệu: " + ex.Message);
            }
        }

        private void ClearInputForm()
        {
            ProductNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            UnitTextBox.Text = string.Empty;
        }

        private void ButtonNhap(object sender, RoutedEventArgs e)
        {
            try
            {
                string tenMonAn = ProductNameTextBox.Text.Trim();
                string soLuong = QuantityTextBox.Text.Trim();
                string gia = PriceTextBox.Text.Trim();
                string donvi = UnitTextBox.Text.Trim();

                if (string.IsNullOrEmpty(tenMonAn) || string.IsNullOrEmpty(soLuong) || string.IsNullOrEmpty(gia) || string.IsNullOrEmpty(donvi))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin món ăn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(soLuong, out int soLuongValue) || soLuongValue <= 0)
                {
                    MessageBox.Show("Số lượng phải nhiều 0 mới có thể thêm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (gia.Length < 3)
                {
                    gia += "000";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM MonAn WHERE TenMonAn = @TenMonAn";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenMonAn", tenMonAn);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Món ăn này đã tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO NguyenLieu (TenNguyenLieu, SoLuong, DonVi, DonGia) VALUES (@TenNguyenLieu, @SoLuong, @DonVi, @DonGia)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@TenNguyenLieu", tenMonAn);
                        cmd.Parameters.AddWithValue("@SoLuong", int.Parse(soLuong));
                        cmd.Parameters.AddWithValue("@DonVi", donvi);
                        cmd.Parameters.AddWithValue("@DonGia", decimal.Parse(gia));
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadIngredients();
                ClearInputForm();
                MessageBox.Show("Thêm nguyên liệu thành công.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nguyên liệu: " + ex.Message);
            }
        }

        private void ButtonHuy(object sender, RoutedEventArgs e)
        {
            ClearInputForm();
            ProductNameTextBox.Focus();
        }

        private void ButtonSua(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem is NguyenLieu selectedIngredient)
            {
                try
                {
                    string tenMonAn = ProductNameTextBox.Text.Trim();
                    string soLuong = QuantityTextBox.Text.Trim();
                    string gia = PriceTextBox.Text.Trim();
                    string donvi = UnitTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(tenMonAn) || string.IsNullOrEmpty(soLuong) || string.IsNullOrEmpty(gia) || string.IsNullOrEmpty(donvi))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin món ăn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!int.TryParse(soLuong, out int soLuongValue) || soLuongValue <= 0)
                    {
                        MessageBox.Show("Số lượng phải nhiều 0 mới có thể thêm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (gia.Length < 3)
                    {
                        gia += "000";
                    }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE NguyenLieu SET TenNguyenLieu = @TenNguyenLieu, SoLuong = @SoLuong, DonVi = @DonVi, DonGia = @DonGia WHERE MaNguyenlieu = @MaNguyenlieu";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNguyenlieu", selectedIngredient.MaNguyenlieu);
                            cmd.Parameters.AddWithValue("@TenNguyenLieu", tenMonAn);
                            cmd.Parameters.AddWithValue("@SoLuong", int.Parse(soLuong));
                            cmd.Parameters.AddWithValue("@DonVi", donvi);
                            cmd.Parameters.AddWithValue("@DonGia", decimal.Parse(gia));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadIngredients();
                    ClearInputForm();
                    MessageBox.Show("Sửa nguyên liệu thành công.", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa nguyên liệu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần sửa.");
            }
        }

        private void ButtonXoa(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem is NguyenLieu selectedIngredient)
            {
                try
                {
                    if (InventoryDataGrid.SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn món ăn cần xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?", "Xác nhận",
                                            MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM NguyenLieu WHERE MaNguyenlieu = @MaNguyenlieu";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaNguyenlieu", selectedIngredient.MaNguyenlieu);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        LoadIngredients();
                        ClearInputForm();
                        MessageBox.Show("Xóa nguyên liệu thành công.", "Thông báo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nguyên liệu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần xóa.");
            }
        }

        private void InventoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem is NguyenLieu selectedIngredient)
            {
                ProductNameTextBox.Text = selectedIngredient.TenNguyenLieu;
                QuantityTextBox.Text = selectedIngredient.SoLuong.ToString();
                PriceTextBox.Text = selectedIngredient.DonGia.ToString();
                UnitTextBox.Text = selectedIngredient.DonVi;
            }
        }

        private void ButtonSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                Ingredients.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM NguyenLieu WHERE TenNguyenLieu LIKE @SearchTerm";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + SearchTextBox.Text + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Ingredients.Add(new NguyenLieu
                                {
                                    MaNguyenlieu = reader.GetInt32(0),
                                    TenNguyenLieu = reader.GetString(1),
                                    SoLuong = reader.GetInt32(2),
                                    DonVi = !reader.IsDBNull(3) ? reader.GetString(3) : string.Empty,
                                    DonGia = !reader.IsDBNull(4) ? (decimal)reader.GetDouble(4) : 0m
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm nguyên liệu: " + ex.Message);
            }
        }

        private void ButtonRefresh(object sender, RoutedEventArgs e)
        {
            LoadIngredients();
            SearchTextBox.Text = null;
        }

        private void SearchTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = char.IsDigit(e.Text, 0);
        }

        private void UnitTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = char.IsDigit(e.Text, 0);
        }

        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void SearchTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (int.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void UnitTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (int.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void PriceTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!int.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void QuantityTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!int.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        public class NguyenLieu
        {
            public int MaNguyenlieu { get; set; }
            public string TenNguyenLieu { get; set; }
            public int SoLuong { get; set; }
            public string DonVi { get; set; }
            public decimal DonGia { get; set; }
        }
    }
}

