using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLNhaHang
{
    /// <summary>
    /// Interaction logic for FoodManagementPage.xaml
    /// </summary>
    public partial class FoodManagementPage : UserControl
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

        public FoodManagementPage()
        {
            InitializeComponent();
            LoadMonAnData();
        }

        private void LoadMonAnData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM MonAn";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgMonAn.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Thêm phương thức để làm mới dữ liệu
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadMonAnData();
            searchTextBox.Text = string.Empty;
        }

        // Thêm phương thức tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = searchTextBox.Text.Trim();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT * FROM MonAn 
                                   WHERE TenMonAn LIKE @SearchText 
                                   OR MoTa LIKE @SearchText";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgMonAn.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void searchTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsLetter);
        }

        private void dgMonAn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgMonAn.SelectedItem == null) return;
                DataRowView selectedRow = (DataRowView)dgMonAn.SelectedItem;
                txtTenMonAn.Text = selectedRow["TenMonAn"].ToString();
                txtMoTa.Text = selectedRow["MoTa"].ToString();
                txtGia.Text = selectedRow["Gia"].ToString();
                txtSoLuong.Text = selectedRow["SoLuong"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu từ DataGrid: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonHuy(object sender, RoutedEventArgs e)
        {
            txtTenMonAn.Text = string.Empty;
            txtMoTa.Text = string.Empty;
            txtGia.Text = string.Empty;
            txtSoLuong.Text = string.Empty;
            txtTenMonAn.Focus();
            LoadMonAnData();
        }

        private void ButtonThem(object sender, RoutedEventArgs e)
        {
            try
            {
                string tenMonAn = txtTenMonAn.Text.Trim();
                string moTa = txtMoTa.Text.Trim();
                string gia = txtGia.Text.Trim();
                string soLuong = txtSoLuong.Text.Trim();

                if (string.IsNullOrEmpty(tenMonAn) || string.IsNullOrEmpty(moTa) || string.IsNullOrEmpty(gia) || string.IsNullOrEmpty(soLuong))
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

                // Kiểm tra món ăn đã có trong cơ sở dữ liệu chưa
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

                // Kiểm tra và chuyển đổi giá trị
                if (!decimal.TryParse(gia, out decimal giaValue) || giaValue <= 0)
                {
                    MessageBox.Show("Giá không hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Nếu món ăn chưa tồn tại, thực hiện thêm vào cơ sở dữ liệu
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO MonAn (TenMonAn, MoTa, Gia, SoLuong) 
                             VALUES (@TenMonAn, @MoTa, @Gia, @SoLuong)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenMonAn", tenMonAn);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@Gia", giaValue);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongValue);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm món ăn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadMonAnData(); // Làm mới dữ liệu trong DataGrid
                ButtonHuy(null, null); // Hủy bỏ thông tin đã nhập
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm món ăn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSua(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgMonAn.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn món ăn cần sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string tenMonAn = txtTenMonAn.Text.Trim();
                string moTa = txtMoTa.Text.Trim();
                string gia = txtGia.Text.Trim();
                string soLuong = txtSoLuong.Text.Trim();

                if (string.IsNullOrEmpty(tenMonAn) || string.IsNullOrEmpty(moTa) || 
                    string.IsNullOrEmpty(gia) || string.IsNullOrEmpty(soLuong))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin món ăn.", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(soLuong, out int soLuongValue) || soLuongValue < 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ.", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(gia, out decimal giaValue) || giaValue <= 0)
                {
                    MessageBox.Show("Giá không hợp lệ.", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DataRowView selectedRow = (DataRowView)dgMonAn.SelectedItem;
                int maMonAn = Convert.ToInt32(selectedRow["MaMonAn"]);

                // Kiểm tra xem tên món ăn mới có bị trùng không (trừ chính nó)
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = @"SELECT COUNT(*) FROM MonAn 
                                        WHERE TenMonAn = @TenMonAn AND MaMonAn != @MaMonAn";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenMonAn", tenMonAn);
                        checkCmd.Parameters.AddWithValue("@MaMonAn", maMonAn);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Tên món ăn này đã tồn tại.", "Thông báo", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    // Cập nhật thông tin món ăn
                    string updateQuery = @"UPDATE MonAn 
                                         SET TenMonAn = @TenMonAn, 
                                             MoTa = @MoTa, 
                                             Gia = @Gia, 
                                             SoLuong = @SoLuong
                                         WHERE MaMonAn = @MaMonAn";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaMonAn", maMonAn);
                        cmd.Parameters.AddWithValue("@TenMonAn", tenMonAn);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@Gia", giaValue);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongValue);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Sửa món ăn thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadMonAnData();
                ButtonHuy(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa món ăn: {ex.Message}", "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonXoa(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgMonAn.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn món ăn cần xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DataRowView selectedRow = (DataRowView)dgMonAn.SelectedItem;
                int idMonAn = Convert.ToInt32(selectedRow["MaMonAn"]);

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?", "Xác nhận",
                                             MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"DELETE FROM MonAn WHERE MaMonAn = @MaMonAn";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaMonAn", idMonAn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadMonAnData(); 
                    ButtonHuy(null,null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa món ăn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void searchTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (pastedText.All(char.IsDigit))
                {
                    e.CancelCommand();  
                }
            }
            else
            {
                e.CancelCommand(); 
            }
        }

        private void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
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
    }
}
