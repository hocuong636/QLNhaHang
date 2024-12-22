using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

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
                MessageBox.Show("Error loading ingredients: " + ex.Message);
            }
        }


        private void NhapKho_Click(object sender, RoutedEventArgs e)
        {
            string tenSanPham = ProductNameTextBox.Text;
            int soLuong;
            decimal giaNhap;
            string donVi = (UnitComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string nguoiNhap = PersonTextBox.Text;

            if (string.IsNullOrWhiteSpace(tenSanPham) ||
                !int.TryParse(QuantityTextBox.Text, out soLuong) ||
                !decimal.TryParse(PriceTextBox.Text, out giaNhap) ||
                string.IsNullOrWhiteSpace(donVi) || string.IsNullOrWhiteSpace(nguoiNhap))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra xem nguyên liệu đã tồn tại chưa
                    string checkQuery = "SELECT MaNguyenLieu FROM NguyenLieu WHERE TenNguyenLieu = @TenNguyenLieu";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenNguyenLieu", tenSanPham);
                        var result = checkCmd.ExecuteScalar();

                        if (result != null)
                        {
                            int maNguyenLieu = (int)result;

                            // Cập nhật số lượng trong bảng NguyenLieu
                            string updateQuery = @"UPDATE NguyenLieu 
                                           SET SoLuong = SoLuong + @SoLuong, 
                                               DonGia = @DonGia, 
                                               DonVi = @DonVi
                                           WHERE MaNguyenLieu = @MaNguyenLieu";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                                updateCmd.Parameters.AddWithValue("@DonGia", giaNhap);
                                updateCmd.Parameters.AddWithValue("@DonVi", donVi);
                                updateCmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Thêm vào bảng NhapKho
                            string insertNhapKhoQuery = @"INSERT INTO NhapKho (MaNguyenLieu, TenNguyenLieu, SoLuong, DonGia, DonVi, NguoiNhap) 
                                                 VALUES (@MaNguyenLieu, @TenNguyenLieu, @SoLuong, @DonGia, @DonVi, @NguoiNhap)";
                            using (SqlCommand insertCmd = new SqlCommand(insertNhapKhoQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                                insertCmd.Parameters.AddWithValue("@TenNguyenLieu", tenSanPham);
                                insertCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                                insertCmd.Parameters.AddWithValue("@DonGia", giaNhap);
                                insertCmd.Parameters.AddWithValue("@DonVi", donVi);
                                insertCmd.Parameters.AddWithValue("@NguoiNhap", nguoiNhap);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nguyên liệu không tồn tại trong kho.");
                            return;
                        }
                    }
                }

                MessageBox.Show("Nhập kho thành công!");
                LoadIngredients(); // Cập nhật danh sách hiển thị
                ClearInputForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nhập kho: " + ex.Message);
            }
        }



        private void XoaKho_Click(object sender, RoutedEventArgs e)
        {
            var selectedIngredient = InventoryDataGrid.SelectedItem as NguyenLieu;
            if (selectedIngredient == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
            }
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
