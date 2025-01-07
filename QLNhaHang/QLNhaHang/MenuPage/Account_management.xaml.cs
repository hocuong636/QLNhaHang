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
    /// Interaction logic for Account_management.xaml
    /// </summary>
    public partial class Account_management : UserControl
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

        public Account_management()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void Account_management_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load dữ liệu khi form được khởi tạo
                LoadUserData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Nếu TextBox chứa placeholder, xóa nội dung và đổi màu chữ
                if (textBox.Text == "Email hoặc Tên Đăng Nhập" || textBox.Text == "Mã OTP (6 chữ số)")
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = Brushes.Black;
                }
            }
        }

        // Phương thức xử lý khi TextBox mất focus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Nếu TextBox để trống, hiển thị lại placeholder và đổi màu chữ
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox.Name == "txtRecovery")
                    {
                        textBox.Text = "Email hoặc Tên Đăng Nhập";
                    }
                    else if (textBox.Name == "txtOtp")
                    {
                        textBox.Text = "Mã OTP (6 chữ số)";
                    }
                    textBox.Foreground = Brushes.Gray;
                }
            }
        }
        private Dictionary<string, string> userDatabase = new Dictionary<string, string>
        {
            { "admin@example.com", "123456" },
            { "user@example.com", "UserPassword456" }
        };


        private void SearchUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userInput = txtRecovery.Text.Trim();
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT MaNguoiDung, TenDangNhap, Email 
                                   FROM NguoiDung 
                                   WHERE TenDangNhap = @Input OR Email = @Input";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Input", userInput);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNotification.Text = "Tìm thấy tài khoản. Bạn có thể cập nhật mật khẩu.";
                                txtNotification.Foreground = Brushes.Green;
                                txtNotification.Visibility = Visibility.Visible;
                                // Lưu MaNguoiDung vào Tag của txtRecovery để sử dụng sau
                                txtRecovery.Tag = reader["MaNguoiDung"];
                            }
                            else
                            {
                                txtNotification.Text = "Không tìm thấy tài khoản. Vui lòng kiểm tra lại.";
                                txtNotification.Foreground = Brushes.Red;
                                txtNotification.Visibility = Visibility.Visible;
                                txtRecovery.Tag = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtRecovery.Tag == null)
                {
                    MessageBox.Show("Vui lòng tìm tài khoản trước khi cập nhật mật khẩu!", "Thông báo");
                    return;
                }

                if (txtNewPassword.Password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có từ 6 đến 8 ký tự.", "Thông báo");
                    txtNewPassword.Password = string.Empty;
                    return;
                }

                if (txtConfirmPassword.Password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có từ 6 đến 8 ký tự.", "Thông báo");
                    txtConfirmPassword.Password = string.Empty;
                    return;
                }

                string newPassword = txtNewPassword.Password;
                string confirmPassword = txtConfirmPassword.Password;

                // Kiểm tra mật khẩu
                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu mới không khớp!", "Thông báo");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE NguoiDung SET MatKhau = @MatKhau WHERE MaNguoiDung = @MaNguoiDung";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MatKhau", newPassword);
                        cmd.Parameters.AddWithValue("@MaNguoiDung", txtRecovery.Tag);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo");
                            // Reset các trường
                            txtNewPassword.Password = "";
                            txtConfirmPassword.Password = "";
                            txtRecovery.Text = "";
                            txtNotification.Visibility = Visibility.Collapsed;
                            txtRecovery.Tag = null;
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật mật khẩu!", "Lỗi");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật mật khẩu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                    string.IsNullOrWhiteSpace(txtMatKhau.Password) ||
                    string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtDienThoai.Text) ||
                    cmbQuyen.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo");
                    return;
                }

                if (txtMatKhau.Password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có từ 6 đến 8 ký tự.", "Thông báo");
                    txtMatKhau.Password = string.Empty;
                    return;
                }

                // Kiểm tra độ dài số điện thoại
                if (txtDienThoai.Text.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải có đúng 10 ký tự!", "Thông báo");
                    return;
                }

                // Lấy giá trị quyền từ ComboBox
                string quyen = ((ComboBoxItem)cmbQuyen.SelectedItem).Content.ToString();
                int maQuyen = (quyen == "Admin") ? 1 : 2;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Kiểm tra tên đăng nhập đã tồn tại chưa
                            string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                                int count = (int)checkCmd.ExecuteScalar();
                                if (count > 0)
                                {
                                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo");
                                    return;
                                }
                            }

                            // Thêm người dùng mới với IDENTITY
                            string insertQuery = @"
                                INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, DienThoai, MaQuyen)
                                VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @DienThoai, @MaQuyen);
                                SELECT SCOPE_IDENTITY();"; // Lấy ID vừa được tạo

                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                                cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Password);
                                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                                cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);
                                cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);

                                // Lấy ID vừa được tạo
                                decimal newId = (decimal)cmd.ExecuteScalar();
                                
                                transaction.Commit();
                                MessageBox.Show("Tạo tài khoản thành công!","Thông báo");

                                // Reset các trường nhập liệu
                                txtTenDangNhap.Text = "";
                                txtMatKhau.Password = "";
                                txtHoTen.Text = "";
                                txtEmail.Text = "";
                                txtDienThoai.Text = "";
                                cmbQuyen.SelectedIndex = -1;

                                // Cập nhật lại DataGrid
                                LoadUserData();
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Lỗi khi tạo tài khoản: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtDienThoai_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void TxtDienThoai_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                string clipboardText = Clipboard.GetText();

                if (clipboardText.All(char.IsDigit))
                {
                    // Chỉ cho phép dán nếu toàn bộ nội dung clipboard là số
                    (sender as TextBox).Text += clipboardText;
                }
            }
        }

        // Thêm phương thức để load dữ liệu người dùng
        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            nd.MaNguoiDung, 
                            nd.TenDangNhap, 
                            nd.HoTen, 
                            nd.Email, 
                            nd.DienThoai, 
                            CASE 
                                WHEN nd.MaQuyen = 1 THEN N'Admin'
                                WHEN nd.MaQuyen = 2 THEN N'Nhân Viên'
                                ELSE N'Không xác định'
                            END as TenQuyen
                        FROM NguoiDung nd";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgNguoiDung.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu người dùng.", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgNguoiDung.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Thông báo");
                    return;
                }

                // Lấy thông tin tài khoản được chọn
                DataRowView row = (DataRowView)dgNguoiDung.SelectedItem;
                int maNguoiDung = Convert.ToInt32(row["MaNguoiDung"]);
                string tenDangNhap = row["TenDangNhap"].ToString();

                // Hiển thị hộp thoại xác nhận
                MessageBoxResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa tài khoản '{tenDangNhap}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // Kiểm tra xem có phải tài khoản admin cuối cùng không
                                string checkAdminQuery = @"
                                    SELECT COUNT(*) 
                                    FROM NguoiDung 
                                    WHERE MaQuyen = 1 AND MaNguoiDung != @MaNguoiDung";

                                using (SqlCommand checkCmd = new SqlCommand(checkAdminQuery, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                                    int adminCount = (int)checkCmd.ExecuteScalar();

                                    // Kiểm tra nếu đây là admin cuối cùng
                                    string checkIsAdminQuery = "SELECT MaQuyen FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                                    using (SqlCommand checkIsAdminCmd = new SqlCommand(checkIsAdminQuery, conn, transaction))
                                    {
                                        checkIsAdminCmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                                        int maQuyen = Convert.ToInt32(checkIsAdminCmd.ExecuteScalar());

                                        if (maQuyen == 1 && adminCount == 0)
                                        {
                                            MessageBox.Show("Không thể xóa tài khoản Admin cuối cùng!", "Thông báo");
                                            return;
                                        }
                                    }
                                }

                                // Thực hiện xóa tài khoản
                                string deleteQuery = "DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                                    cmd.ExecuteNonQuery();
                                }

                                // Reset lại identity sau khi xóa
                                string reseedQuery = @"
                                    DECLARE @MaxID int
                                    SELECT @MaxID = ISNULL(MAX(MaNguoiDung), 0) FROM NguoiDung
                                    DBCC CHECKIDENT ('NguoiDung', RESEED, @MaxID)";

                                using (SqlCommand reseedCmd = new SqlCommand(reseedQuery, conn, transaction))
                                {
                                    reseedCmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo");

                                // Cập nhật lại DataGrid
                                LoadUserData();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw new Exception("Lỗi khi xóa tài khoản: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                // Kiểm tra nếu tab được chọn là "Thông Tin Tài Khoản"
                if (tabControl.SelectedItem is TabItem selectedTab && 
                    selectedTab.Header.ToString() == "Thông Tin Tài Khoản")
                {
                    LoadUserData(); // Load lại dữ liệu người dùng
                }
            }
        }   

    }
}
