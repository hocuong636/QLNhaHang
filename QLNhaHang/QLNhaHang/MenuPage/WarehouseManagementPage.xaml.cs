using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class WarehouseManagementPage : UserControl

    {
        public ObservableCollection<NguyenLieu> Ingredients { get; set; }
        public ObservableCollection<MonAn> Dishes { get; set; }

        public WarehouseManagementPage()
        {
            InitializeComponent();
            Ingredients = new ObservableCollection<NguyenLieu>();
            Dishes = new ObservableCollection<MonAn>();
            LoadIngredients();
            IngredientsDataGrid.ItemsSource = Ingredients;
        }

        private void LoadIngredients()
        {
            Ingredients.Clear();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
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
                                MaNguyenLieu = reader.GetInt32(0),
                                TenNguyenLieu = reader.GetString(1),
                                SoLuong = reader.GetInt32(2),
                                DonVi = reader.GetString(3)
                            });
                        }
                    }
                }
            }
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            NguyenLieu newIngredient = new NguyenLieu
            {
                TenNguyenLieu = IngredientNameTextBox.Text,
                SoLuong = int.Parse(IngredientQuantityTextBox.Text),
                DonVi = IngredientUnitTextBox.Text
            };

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO NguyenLieu (TenNguyenLieu, SoLuong, DonVi) VALUES (@TenNguyenLieu, @SoLuong, @DonVi)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenNguyenLieu", newIngredient.TenNguyenLieu);
                    cmd.Parameters.AddWithValue("@SoLuong", newIngredient.SoLuong);
                    cmd.Parameters.AddWithValue("@DonVi", newIngredient.DonVi);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadIngredients();
            ClearIngredientForm();
        }

        private void EditIngredient_Click(object sender, RoutedEventArgs e)
        {
            NguyenLieu selectedIngredient = IngredientsDataGrid.SelectedItem as NguyenLieu;
            if (selectedIngredient != null)
            {
                IngredientNameTextBox.Text = selectedIngredient.TenNguyenLieu;
                IngredientQuantityTextBox.Text = selectedIngredient.SoLuong.ToString();
                IngredientUnitTextBox.Text = selectedIngredient.DonVi;
            }
        }

        private void DeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            NguyenLieu selectedIngredient = IngredientsDataGrid.SelectedItem as NguyenLieu;
            if (selectedIngredient != null)
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM NguyenLieu WHERE MaNguyenLieu = @MaNguyenLieu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNguyenLieu", selectedIngredient.MaNguyenLieu);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadIngredients();
                ClearIngredientForm();
            }
        }

        private void SaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            NguyenLieu selectedIngredient = IngredientsDataGrid.SelectedItem as NguyenLieu;
            if (selectedIngredient != null)
            {
                selectedIngredient.TenNguyenLieu = IngredientNameTextBox.Text;
                selectedIngredient.SoLuong = int.Parse(IngredientQuantityTextBox.Text);
                selectedIngredient.DonVi = IngredientUnitTextBox.Text;

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE NguyenLieu SET TenNguyenLieu = @TenNguyenLieu, SoLuong = @SoLuong, DonVi = @DonVi WHERE MaNguyenLieu = @MaNguyenLieu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNguyenLieu", selectedIngredient.TenNguyenLieu);
                        cmd.Parameters.AddWithValue("@SoLuong", selectedIngredient.SoLuong);
                        cmd.Parameters.AddWithValue("@DonVi", selectedIngredient.DonVi);
                        cmd.Parameters.AddWithValue("@MaNguyenLieu", selectedIngredient.MaNguyenLieu);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadIngredients();
                ClearIngredientForm();
            }
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e)
        {
            ClearIngredientForm();
        }

        private void ClearIngredientForm()
        {
            IngredientNameTextBox.Text = string.Empty;
            IngredientQuantityTextBox.Text = string.Empty;
            IngredientUnitTextBox.Text = string.Empty;
            IngredientsDataGrid.SelectedItem = null;
        }

        private void IngredientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NguyenLieu selectedIngredient = IngredientsDataGrid.SelectedItem as NguyenLieu;
            if (selectedIngredient != null)
            {
                LoadRelatedDishes(selectedIngredient.MaNguyenLieu);
            }
        }

        private void LoadRelatedDishes(int maNguyenLieu)
        {
            Dishes.Clear();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT m.MaMonAn, m.TenMonAn, ctnl.SoLuongCan
                    FROM MonAn m
                    INNER JOIN ChiTietNguyenLieu ctnl ON m.MaMonAn = ctnl.MaMonAn
                    WHERE ctnl.MaNguyenLieu = @MaNguyenLieu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dishes.Add(new MonAn
                            {
                                MaMonAn = reader.GetInt32(0),
                                TenMonAn = reader.GetString(1),
                                SoLuongCan = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            DishesDataGrid.ItemsSource = Dishes;
        }
    }

    public class NguyenLieu
    {
        public int MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
    }

    public class MonAn
    {
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public int SoLuongCan { get; set; }
    }
}