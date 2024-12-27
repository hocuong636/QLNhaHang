using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace QLNhaHang.EmployeeControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TableStatusUserControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<TableInfo> _tables;
        public ObservableCollection<TableInfo> Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(); }
        }
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";
        public TableStatusUserControl()
        {
            InitializeComponent();
            SetupTableButtons();
            UpdateTableStatus();
        }

        private void UpdateTableStatus()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SoBan, TrangThai FROM Ban";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maBan = reader.GetInt32(0);
                            string trangThai = reader.GetString(1);
                            // Tìm button tương ứng
                            string buttonName = $"btn{maBan}";
                            Button btn = this.FindName(buttonName) as Button;
                            if (btn != null)
                            {
                                // Đổi màu button dựa trên trạng thái
                                if (trangThai == "Đang phục vụ")
                                {
                                    btn.Background = Brushes.Red;
                                }
                                else
                                {
                                    btn.Background = Brushes.Green;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load trạng thái bàn: {ex.Message}");
            }
        }
        private void RefreshTableStatus(object sender, RoutedEventArgs e)
        {
            UpdateTableStatus();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SetupTableButtons()
       {
            // Giả sử các button được đặt trong một panel tên là panelTables
            // Nếu các button nằm trong một ScrollViewer
            if (scrollViewer != null)
            {
                // Lấy panel chứa các button (thường là Grid hoặc StackPanel trong XAML)
                var panel = scrollViewer.Content as Panel;
                if (panel != null)
                {
                    foreach (var element in panel.Children)
                    {
                        if (element is Button btn && btn.Name.StartsWith("btn"))
                        {
                            string numberStr = btn.Name.Replace("btn", "");
                            if (int.TryParse(numberStr, out int tableNumber))
                            {
                                btn.Tag = tableNumber;
                                btn.Click -= Table_Click;
                                btn.Click += Table_Click;
                            }
                        }
                    }
                }
            }
        }
        public void Table_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn && btn.Tag != null)
                {
                    int maBan = Convert.ToInt32(btn.Tag);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        // Cập nhật câu query để tính tổng tiền chính xác
                        string query = @"SELECT 
                              hd.MaHoaDon,
                              ma.TenMonAn,
                              cthd.SoLuong,
                              cthd.DonGia * cthd.SoLuong as ThanhTien
                          FROM HoaDon hd
                          INNER JOIN ChiTietHoaDon cthd ON hd.MaHoaDon = cthd.MaHoaDon
                          INNER JOIN MonAn ma ON cthd.MaMonAn = ma.MaMonAn
                          WHERE hd.SoBan = @MaBan
                          ORDER BY hd.MaHoaDon";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaBan", maBan);
                            DataTable dt = new DataTable();
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                            OrderDetailsGrid.ItemsSource = dt.DefaultView;

                            // Tính tổng tiền
                            decimal totalAmount = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                totalAmount += Convert.ToDecimal(row["ThanhTien"]);
                            }

                            // Hiển thị tổng tiền lên TextBox
                            TotalAmount.Text = totalAmount.ToString("#,##0 VNĐ");
                        }
                    }
                    UpdateTableStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy số bàn từ button được chọn
                Button selectedButton = null;
                if (scrollViewer != null)
                {
                    var panel = scrollViewer.Content as Panel;
                    if (panel != null)
                    {
                        selectedButton = panel.Children.OfType<Button>()
                            .FirstOrDefault(b => b.Background == Brushes.Red);
                    }
                }

                if (selectedButton == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn cần thanh toán!", "Thông báo");
                    return;
                }

                int maBan = Convert.ToInt32(selectedButton.Tag);

                // Tạo file PDF
                string pdfPath = $"HoaDon_Ban{maBan}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // Thêm font chữ Unicode
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(baseFont, 12);
                    Font titleFont = new Font(baseFont, 18, Font.BOLD);

                    // Thêm tiêu đề
                    Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Paragraph($"Bàn số: {maBan}", font));
                    document.Add(new Paragraph($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", font));
                    document.Add(new Paragraph("-----------------------------------", font));

                    // Tạo bảng
                    PdfPTable table = new PdfPTable(4);
                    table.WidthPercentage = 100;
                    table.SpacingBefore = 20f;

                    // Header của bảng
                    table.AddCell(new PdfPCell(new Phrase("Tên món", font)));
                    table.AddCell(new PdfPCell(new Phrase("Số lượng", font)));
                    table.AddCell(new PdfPCell(new Phrase("Đơn giá", font)));
                    table.AddCell(new PdfPCell(new Phrase("Thành tiền", font)));
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // 1. Lấy thông tin hóa đơn hiện tại
                                string queryHoaDon = @"
                                    SELECT hd.MaHoaDon, 
                                           SUM(cthd.DonGia * cthd.SoLuong) as TongTien
                                    FROM HoaDon hd
                                    JOIN ChiTietHoaDon cthd ON hd.MaHoaDon = cthd.MaHoaDon
                                    JOIN MonAn ma ON cthd.MaMonAn = ma.MaMonAn
                                    WHERE hd.SoBan = @MaBan
                                    GROUP BY hd.MaHoaDon";

                                int maHoaDon = 0;
                                decimal tongTien = 0;

                                using (SqlCommand cmdHoaDon = new SqlCommand(queryHoaDon, conn, transaction))
                                {
                                    cmdHoaDon.Parameters.AddWithValue("@MaBan", maBan);
                                    using (SqlDataReader reader = cmdHoaDon.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            maHoaDon = reader.GetInt32(0);
                                            tongTien = reader.GetDecimal(1);
                                        }
                                    }
                                }

                                // 2. Cập nhật trạng thái HoaDon thành "Hoàn thành"
                                string updateHoaDon = @"
                                    UPDATE HoaDon 
                                    SET TrangThai = N'Hoàn thành',
                                        TongTien = @TongTien,
                                        NgayLap = @NgayLap
                                    WHERE MaHoaDon = @MaHoaDon";

                                using (SqlCommand cmdUpdateHoaDon = new SqlCommand(updateHoaDon, conn, transaction))
                                {
                                    cmdUpdateHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmdUpdateHoaDon.Parameters.AddWithValue("@TongTien", tongTien);
                                    cmdUpdateHoaDon.ExecuteNonQuery();
                                }
                                string deletequery = "DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                                using (SqlCommand cmdDelete = new SqlCommand(deletequery, conn, transaction))
                                {
                                    cmdDelete.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmdDelete.ExecuteNonQuery();
                                }

                                // 3. Thêm vào LichSuHoaDon
                                string insertLichSu = @"
                                    INSERT INTO LichSuHoaDon (MaHoaDon, MaNhanVien, NgayLap, TongTien)
                                    VALUES (@MaHoaDon, @MaNhanVien, @NgayLap, @TongTien)";

                                using (SqlCommand cmdInsertLichSu = new SqlCommand(insertLichSu, conn, transaction))
                                {
                                    cmdInsertLichSu.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmdInsertLichSu.Parameters.AddWithValue("@MaNhanVien", 1); // Thay bằng MaNhanVien thực tế
                                    cmdInsertLichSu.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                                    cmdInsertLichSu.Parameters.AddWithValue("@TongTien", tongTien);
                                    cmdInsertLichSu.ExecuteNonQuery();
                                }

                                // 4. Cập nhật trạng thái bàn
                                string updateBan = "UPDATE Ban SET TrangThai = N'Trống' WHERE SoBan = @MaBan";
                                using (SqlCommand cmdUpdateBan = new SqlCommand(updateBan, conn, transaction))
                                {
                                    cmdUpdateBan.Parameters.AddWithValue("@MaBan", maBan);
                                    cmdUpdateBan.ExecuteNonQuery();
                                }

                                // Commit transaction nếu tất cả thành công
                                transaction.Commit();

                                // Cập nhật UI
                                UpdateTableStatus();
                                OrderDetailsGrid.ItemsSource = null;
                                TotalAmount.Text = "0 VNĐ";

                                MessageBox.Show("Thanh toán thành công!", "Thông báo");
                            }
                            catch (Exception ex)
                            {
                                // Rollback nếu có lỗi
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }

                    // Mở file PDF
                    Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });

                    // Cập nhật trạng thái hiển thị
                    UpdateTableStatus();
                    OrderDetailsGrid.ItemsSource = null;
                    TotalAmount.Text = "0 VNĐ";

                    MessageBox.Show("Thanh toán thành công!", "Thông báo");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class TableInfo : INotifyPropertyChanged
    {
        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(); }
        }

        private bool _isOccupied;
        public bool IsOccupied
        {
            get { return _isOccupied; }
            set { _isOccupied = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}