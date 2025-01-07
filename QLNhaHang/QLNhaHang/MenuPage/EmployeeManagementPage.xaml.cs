using iTextSharp.text;
using QLNhaHang.EmployeeControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QLNhaHang
{
    public partial class EmployeeManagementPage : UserControl
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

        public EmployeeManagementPage()
        {
            InitializeComponent();
            LoadEmployeeData();
            txtDienThoai.PreviewTextInput += TextBox_OnlyNumbers;
            txtCCCD.PreviewTextInput += TextBox_OnlyNumbers;
            txtMaQuyen.PreviewTextInput += TextBox_OnlyNumbers;

            DataObject.AddPastingHandler(txtDienThoai, TextBox_PastingHandler);
            DataObject.AddPastingHandler(txtCCCD, TextBox_PastingHandler);
            DataObject.AddPastingHandler(txtMaQuyen, TextBox_PastingHandler);
        }

        // Tải dữ liệu nhân viên từ cơ sở dữ liệu
        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MaNhanVien, HoTen, NTNS, ChucVu, GioiTinh, DiaChi, DienThoai, CCCD, MaQuyen FROM NhanVien";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    List<Employee> employees = new List<Employee>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        employees.Add(new Employee
                        {
                            MaNhanVien = row["MaNhanVien"].ToString(),
                            HoTen = row["HoTen"].ToString(),
                            NTNS = row["NTNS"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NTNS"]),
                            ChucVu = row["ChucVu"].ToString(),
                            GioiTinh = row["GioiTinh"].ToString(),
                            DiaChi = row["DiaChi"].ToString(),
                            DienThoai = row["DienThoai"].ToString(),
                            CCCD = row["CCCD"].ToString(),
                            MaQuyen = row["MaQuyen"].ToString()
                        });
                    }

                    lvNhanVien.ItemsSource = employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện thêm nhân viên
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtChuVu.Text) || string.IsNullOrEmpty(txtDienThoai.Text) ||string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtCCCD.Text) || string.IsNullOrEmpty(txtMaQuyen.Text) || ComboBox.SelectedIndex == -1 || dpNgaySinh.SelectedDate == null) 
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (txtDienThoai.Text.Length != 10 || !txtDienThoai.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số điện thoại phải có đúng 10 chữ số.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate txtCCCD for 12 digits
                if (txtCCCD.Text.Length != 12 || !txtCCCD.Text.All(char.IsDigit))
                {
                    MessageBox.Show("CCCD phải có đúng 12 chữ số.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate txtMaQuyen for either "1" or "2"
                if (txtMaQuyen.Text != "1" && txtMaQuyen.Text != "2")
                {
                    MessageBox.Show("Mã quyền chỉ được phép nhập 1 (ADMIN) hoặc 2 (NHÂN VIÊN).", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE HoTen = @HoTen";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@HoTen", txtHoTen);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Nhân viên này đã tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO NhanVien (HoTen, NTNS, ChucVu, GioiTinh, DiaChi, DienThoai, CCCD, MaQuyen) " +
                                   "VALUES (@HoTen, @NTNS, @ChucVu, @GioiTinh, @DiaChi, @DienThoai, @CCCD, @MaQuyen)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    command.Parameters.AddWithValue("@NTNS", dpNgaySinh.SelectedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ChucVu", txtChuVu.Text);

                    // Lấy giá trị từ ComboBox
                    ComboBoxItem selectedGenderItem = ComboBox.SelectedItem as ComboBoxItem;
                    string gender = selectedGenderItem != null ? selectedGenderItem.Content.ToString() : string.Empty;
                    command.Parameters.AddWithValue("@GioiTinh", gender);

                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                    command.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                    command.Parameters.AddWithValue("@MaQuyen", txtMaQuyen.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadEmployeeData();
                    CancelEmployee_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện sửa nhân viên
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItem is Employee selectedEmployee)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE NhanVien SET HoTen = @HoTen, ChucVu = @ChucVu, NTNS = @NTNS, GioiTinh = @GioiTinh, DiaChi = @DiaChi, " +
                                       "DienThoai = @DienThoai, CCCD = @CCCD, MaQuyen = @MaQuyen WHERE MaNhanVien = @MaNhanVien";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                        command.Parameters.AddWithValue("@ChucVu", txtChuVu.Text);
                        command.Parameters.AddWithValue("@NTNS", dpNgaySinh.SelectedDate ?? (object)DBNull.Value);

                        // Lấy giá trị giới tính từ ComboBox
                        ComboBoxItem selectedGenderItem = ComboBox.SelectedItem as ComboBoxItem;
                        string gender = selectedGenderItem != null ? selectedGenderItem.Content.ToString() : string.Empty;
                        command.Parameters.AddWithValue("@GioiTinh", gender);

                        command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                        command.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                        command.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                        command.Parameters.AddWithValue("@MaQuyen", txtMaQuyen.Text);

                        // Thêm tham số @MaNhanVien
                        command.Parameters.AddWithValue("@MaNhanVien", selectedEmployee.MaNhanVien);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadEmployeeData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Sự kiện xóa nhân viên
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lvNhanVien.SelectedItem is Employee selectedEmployee)
            {
                try
                {
                    if (lvNhanVien.SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }


                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận",
                                                 MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@MaNhanVien", selectedEmployee.MaNhanVien);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadEmployeeData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // Hàm chỉ cho phép nhập số
        private void TextBox_OnlyNumbers(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        // Kiểm tra nếu văn bản chỉ chứa số
        private static bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        // Hàm xử lý sự kiện dán, chỉ cho phép dán số
        private void TextBox_PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextNumeric(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void EmployeeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNhanVien.SelectedItem is Employee selectedEmployee)
            {
                // Gán dữ liệu của nhân viên được chọn vào các trường chi tiết
                txtHoTen.Text = selectedEmployee.HoTen;
                txtChuVu.Text = selectedEmployee.ChucVu;
                foreach (ComboBoxItem item in ComboBox.Items)
                {
                    if (item.Content.ToString().Trim().Equals(selectedEmployee.GioiTinh.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        ComboBox.SelectedItem = item;
                        break;
                    }
                }
                txtDiaChi.Text = selectedEmployee.DiaChi;
                txtDienThoai.Text = selectedEmployee.DienThoai;
                txtCCCD.Text = selectedEmployee.CCCD;
                txtMaQuyen.Text = selectedEmployee.MaQuyen;
                dpNgaySinh.SelectedDate = selectedEmployee.NTNS;
            }
            else
            {
                // Nếu không có nhân viên nào được chọn, xóa dữ liệu trong các trường chi tiết
                ClearEmployeeDetails();
            }
        }

        // Hàm xóa dữ liệu trong các trường chi tiết
        private void ClearEmployeeDetails()
        {
            txtHoTen.Text = string.Empty;
            txtChuVu.Text= string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtMaQuyen.Text = string.Empty;
            dpNgaySinh.SelectedDate = null;
            ComboBox.SelectedIndex = -1;
        }

        private void CancelEmployee_Click(object sender, RoutedEventArgs e)
        {
            ClearEmployeeDetails();
            txtHoTen.Focus();
        }

        private void SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txbTimKiem.Text.Trim(); // Assuming there is a TextBox named txtSearch for entering search text

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MaNhanVien, HoTen, NTNS, ChucVu, GioiTinh, DiaChi, DienThoai, CCCD, MaQuyen FROM NhanVien " +
                                   "WHERE MaNhanVien LIKE @SearchText OR HoTen LIKE @SearchText";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    List<Employee> employees = new List<Employee>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        employees.Add(new Employee
                        {
                            MaNhanVien = row["MaNhanVien"].ToString(),
                            HoTen = row["HoTen"].ToString(),
                            NTNS = row["NTNS"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NTNS"]),
                            ChucVu = row["ChucVu"].ToString(),
                            GioiTinh = row["GioiTinh"].ToString(),
                            DiaChi = row["DiaChi"].ToString(),
                            DienThoai = row["DienThoai"].ToString(),
                            CCCD = row["CCCD"].ToString(),
                            MaQuyen = row["MaQuyen"].ToString()
                        });
                    }

                    lvNhanVien.ItemsSource = employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshEmployee_Click(object sender, RoutedEventArgs e)
        {
             LoadEmployeeData();
             txbTimKiem.Clear(); 
        }
    }

    // Lớp đại diện cho thông tin nhân viên
    public class Employee
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public DateTime? NTNS { get; set; }
        public string ChucVu { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string CCCD { get; set; }
        public string MaQuyen { get; set; }
    }
}
