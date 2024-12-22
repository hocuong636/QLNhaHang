using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class WarehouseManagementPage : UserControl
    {
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
            Ingredients.Clear();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MaNguyenLieu, TenNguyenLieu, Soluong, DonVi, DonGia FROM NguyenLieu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Ingredients.Add(new NguyenLieu
                                {
                                    MaNguyenlieu = reader.GetInt32(0),   // First column: MaNguyenLieu (int)
                                    TenNguyenLieu = reader.GetString(1),  // Second column: TenNguyenLieu (string)
                                    SoLuong = reader.GetInt32(2),         // Third column: SoLuong (int)
                                    DonVi = reader.GetString(3),          // Fourth column: DonVi (string)
                                    DonGia = reader.GetDecimal(4)         // Fifth column: DonGia (decimal)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ingredients: " + ex.Message);
            }
        }

       


        private void NhapKho_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ giao diện
            string maNhap = InputCodeTextBox.Text;
            string tenSanPham = ProductNameTextBox.Text;
            int soLuong;
            decimal giaNhap;
            string donVi = (UnitComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string nguoiNhap = PersonTextBox.Text;

            if (string.IsNullOrWhiteSpace(maNhap) || string.IsNullOrWhiteSpace(tenSanPham) ||
                !int.TryParse(QuantityTextBox.Text, out soLuong) ||
                !decimal.TryParse(PriceTextBox.Text, out giaNhap) ||
                string.IsNullOrWhiteSpace(donVi) || string.IsNullOrWhiteSpace(nguoiNhap))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Thực hiện lưu dữ liệu vào cơ sở dữ liệu
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO NguyenLieu (MaNguyenLieu, TenNguyenLieu, SoLuong, DonGia, DonVi, NguoiNhap) 
                                 VALUES (@MaNhap, @TenSanPham, @SoLuong, @DonGia, @DonVi, @NguoiNhap)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNhap", maNhap);
                    cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@DonGia", giaNhap);
                    cmd.Parameters.AddWithValue("@DonVi", donVi);
                    cmd.Parameters.AddWithValue("@NguoiNhap", nguoiNhap);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Nhập kho thành công!");
            LoadIngredients();
            ClearInputForm();
        }

        private void XoaKho_Click(object sender, RoutedEventArgs e)
        {
            var selectedIngredient = InventoryDataGrid.SelectedItem as NguyenLieu;
            if (selectedIngredient == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
                return;
            }

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM NguyenLieu WHERE TenNguyenLieu = @TenNguyenLieu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNguyenLieu", selectedIngredient.TenNguyenLieu);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa sản phẩm thành công!");
            LoadIngredients();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Kiểm tra nếu TextBox rỗng thì hiển thị placeholder, nếu không thì ẩn placeholder
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                SearchPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void ClearInputForm()
        {
            InputCodeTextBox.Text = string.Empty;
            ProductNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            UnitComboBox.SelectedIndex = -1;
            PersonTextBox.Text = string.Empty;
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
