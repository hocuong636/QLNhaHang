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
using System.Data.SqlClient;
using System.Data;

namespace QLNhaHang.MenuPage
{
    /// <summary>
    /// Interaction logic for ChamCong.xaml
    /// </summary>
    public partial class ChamCong : UserControl
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

        public ChamCong()
        {
            InitializeComponent();
            LoadChamCongData();
            txtMaNhanVien.TextChanged += TxtMaNhanVien_TextChanged;
            dgChamCong.SelectionChanged += DgChamCong_SelectionChanged;
        }

        private void LoadChamCongData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT cc.MaNhanVien, nv.HoTen, cc.Ngay, cc.ThoiGianVao, cc.ThoiGianRa 
                                   FROM ChamCong cc 
                                   INNER JOIN NhanVien nv ON cc.MaNhanVien = nv.MaNhanVien";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgChamCong.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        // Nút Làm mới
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadChamCongData();
        }

        // Nút Tìm kiếm
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string searchText = searchTextBox.Text.Trim();
                    string query = @"SELECT cc.MaNhanVien, nv.HoTen, cc.Ngay, cc.ThoiGianVao, cc.ThoiGianRa 
                                   FROM ChamCong cc 
                                   INNER JOIN NhanVien nv ON cc.MaNhanVien = nv.MaNhanVien
                                   WHERE nv.MaNhanVien LIKE @searchText OR nv.HoTen LIKE @searchText";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgChamCong.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
                }
            }
        }

        // Nút Vào Ca
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    
                    // Kiểm tra nhân viên tồn tại
                    string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Mã nhân viên không tồn tại!");
                            return;
                        }
                    }

                    // Kiểm tra đã chấm công chưa
                    string checkAttendance = @"SELECT COUNT(*) FROM ChamCong 
                                             WHERE MaNhanVien = @MaNhanVien 
                                             AND Ngay = CAST(GETDATE() AS DATE)";
                    using (SqlCommand cmd = new SqlCommand(checkAttendance, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Nhân viên đã chấm công vào ca hôm nay!");
                            return;
                        }
                    }

                    // Thêm bản ghi chấm công
                    string insertQuery = @"INSERT INTO ChamCong (MaNhanVien, Ngay, ThoiGianVao) 
                                         VALUES (@MaNhanVien, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME))";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Chấm công vào ca thành công!");
                        LoadChamCongData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // Nút Kết Thúc Ca
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    
                    // Kiểm tra có bản ghi chấm công vào chưa
                    string checkQuery = @"SELECT COUNT(*) FROM ChamCong 
                                        WHERE MaNhanVien = @MaNhanVien 
                                        AND Ngay = CAST(GETDATE() AS DATE) 
                                        AND ThoiGianRa IS NULL";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Không tìm thấy bản ghi chấm công vào ca của nhân viên này!");
                            return;
                        }
                    }

                    // Cập nhật giờ ra
                    string updateQuery = @"UPDATE ChamCong 
                                         SET ThoiGianRa = CAST(GETDATE() AS TIME) 
                                         WHERE MaNhanVien = @MaNhanVien 
                                         AND Ngay = CAST(GETDATE() AS DATE) 
                                         AND ThoiGianRa IS NULL";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Chấm công kết thúc ca thành công!");
                        LoadChamCongData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void TxtMaNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                txtHoTen.Text = "";
                txtChucVu.Text = "";
                txtTGL.Text = "";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    
                    // Lấy thông tin nhân viên
                    string query = @"SELECT HoTen, ChucVu FROM NhanVien 
                                   WHERE MaNhanVien = @MaNhanVien";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtHoTen.Text = reader["HoTen"].ToString();
                                txtChucVu.Text = reader["ChucVu"].ToString();
                            }
                            else
                            {
                                txtHoTen.Text = "";
                                txtChucVu.Text = "";
                                txtTGL.Text = "";
                            }
                        }
                    }

                    // Sửa lại câu query tính tổng giờ làm
                    string queryGioLam = @"SELECT 
                            SUM(
                                DATEDIFF(HOUR, 
                                    CAST(CONVERT(datetime, CONVERT(varchar(10), Ngay, 120) + ' ' + CONVERT(varchar(8), ThoiGianVao, 108)) AS datetime), 
                                    CAST(CONVERT(datetime, CONVERT(varchar(10), Ngay, 120) + ' ' + CONVERT(varchar(8), ThoiGianRa, 108)) AS datetime))
                            ) as TongGio
                            FROM ChamCong 
                            WHERE MaNhanVien = @MaNhanVien 
                            AND MONTH(Ngay) = MONTH(GETDATE()) 
                            AND YEAR(Ngay) = YEAR(GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(queryGioLam, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                        object result = cmd.ExecuteScalar();
                        
                        if (result != DBNull.Value && result != null)
                        {
                            int tongGio = Convert.ToInt32(result);
                            txtTGL.Text = tongGio.ToString() + " giờ";
                        }
                        else
                        {
                            txtTGL.Text = "0 giờ";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thông tin nhân viên: " + ex.Message);
                }
            }
        }

        private void DgChamCong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgChamCong.SelectedItem != null)
            {
                try
                {
                    DataRowView row = (DataRowView)dgChamCong.SelectedItem;
                    
                    // Lấy mã nhân viên từ dòng được chọn
                    string maNV = row["MaNhanVien"].ToString();
                    
                    // Gán mã nhân viên vào TextBox
                    txtMaNhanVien.Text = maNV;
                    
                    // Các TextBox khác sẽ tự động cập nhật thông qua sự kiện TextChanged
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message);
                }
            }
        }
    }
}
