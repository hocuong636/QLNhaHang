using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace QLNhaHang.EmployeeControl
{
    /// <summary>
    /// Interaction logic for MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";
        private List<MonAn> danhSachMonAn;
        private ObservableCollection<OrderItem> orderItems;
        private string currentUserId;

        public MenuUserControl(string maNguoiDung)
        {
            InitializeComponent();
            currentUserId = maNguoiDung;
            danhSachMonAn = new List<MonAn>();
            orderItems = new ObservableCollection<OrderItem>();
            OrderListView.ItemsSource = orderItems;
            LoadData();
            LoadTables();
        }

        private void LoadData()
        {
            danhSachMonAn.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM MonAn";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSachMonAn.Add(new MonAn
                            {
                                MaMonAn = reader["MaMonAn"].ToString(),
                                TenMonAn = reader["TenMonAn"].ToString(),
                                Gia = Convert.ToDecimal(reader["Gia"])
                            });
                        }
                    }
                }
            }
            MenuItemsControl.ItemsSource = danhSachMonAn;
        }

        private void LoadTables()
        {
            for (int i = 1; i <= 21; i++)
            {
                TableComboBox.Items.Add(i);
            }
            TableComboBox.SelectedIndex = 0;
        }

        private void OnOrderButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is MonAn monAn)
            {
                var existingItem = orderItems.FirstOrDefault(x => x.MaMonAn == monAn.MaMonAn);
                if (existingItem != null)
                {
                    existingItem.SoLuong++;
                    existingItem.UpdateThanhTien();
                }
                else
                {
                    orderItems.Add(new OrderItem(monAn));
                }
                UpdateTotal();
            }
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = OrderListView.SelectedItem as OrderItem;
            if (selectedItem != null)
            {
                orderItems.Remove(selectedItem);
                UpdateTotal();
            }
        }

        private void OnProcessButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn món ăn trước khi đặt món!", "Thông báo");
                    return;
                }

                if (TableComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn!", "Thông báo");
                    return;
                }

                int soBan = (int)TableComboBox.SelectedItem;
                decimal tongTien = orderItems.Sum(x => x.ThanhTien);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int maHoaDon;

                            // Kiểm tra xem bàn đã có hóa đơn chưa thanh toán chưa
                            string checkHoaDonQuery = @"
                                SELECT MaHoaDon 
                                FROM HoaDon 
                                WHERE SoBan = @SoBan 
                                AND TrangThai = N'Chưa thanh toán'";

                            using (SqlCommand cmd = new SqlCommand(checkHoaDonQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SoBan", soBan);
                                var result = cmd.ExecuteScalar();
                                
                                if (result != null)
                                {
                                    // Nếu đã có hóa đơn, sử dụng MaHoaDon hiện có
                                    maHoaDon = Convert.ToInt32(result);
                                }
                                else
                                {
                                    // Nếu chưa có, tạo hóa đơn mới
                                    string insertHoaDonQuery = @"
                                        INSERT INTO HoaDon (MaNguoiDung, NgayLap, TongTien, SoBan, TrangThai)
                                        VALUES (@MaNguoiDung, @NgayLap, @TongTien, @SoBan, @TrangThai);
                                        SELECT SCOPE_IDENTITY();";

                                    using (SqlCommand cmdInsert = new SqlCommand(insertHoaDonQuery, conn, transaction))
                                    {
                                        cmdInsert.Parameters.AddWithValue("@MaNguoiDung", currentUserId);
                                        cmdInsert.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                                        cmdInsert.Parameters.AddWithValue("@TongTien", tongTien);
                                        cmdInsert.Parameters.AddWithValue("@SoBan", soBan);
                                        cmdInsert.Parameters.AddWithValue("@TrangThai", "Chưa thanh toán");
                                        maHoaDon = Convert.ToInt32(cmdInsert.ExecuteScalar());
                                    }
                                }
                            }

                            // Thêm chi tiết hóa đơn
                            foreach (var item in orderItems)
                            {
                                // Kiểm tra xem món này đã có trong hóa đơn chưa
                                string checkExistingItem = @"
                                    SELECT SoLuong 
                                    FROM ChiTietHoaDon 
                                    WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn";

                                using (SqlCommand cmdCheck = new SqlCommand(checkExistingItem, conn, transaction))
                                {
                                    cmdCheck.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmdCheck.Parameters.AddWithValue("@MaMonAn", item.MaMonAn);
                                    var existingQuantity = cmdCheck.ExecuteScalar();

                                    if (existingQuantity != null)
                                    {
                                        // Nếu món đã tồn tại, cập nhật số lượng
                                        string updateQuery = @"
                                            UPDATE ChiTietHoaDon 
                                            SET SoLuong = SoLuong + @SoLuong 
                                            WHERE MaHoaDon = @MaHoaDon 
                                            AND MaMonAn = @MaMonAn";

                                        using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn, transaction))
                                        {
                                            cmdUpdate.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                            cmdUpdate.Parameters.AddWithValue("@MaMonAn", item.MaMonAn);
                                            cmdUpdate.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                                            cmdUpdate.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        // Nếu món chưa tồn tại, thêm mới
                                        string insertQuery = @"
                                            INSERT INTO ChiTietHoaDon (MaHoaDon, MaMonAn, SoLuong, DonGia)
                                            VALUES (@MaHoaDon, @MaMonAn, @SoLuong, @DonGia)";

                                        using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn, transaction))
                                        {
                                            cmdInsert.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                            cmdInsert.Parameters.AddWithValue("@MaMonAn", item.MaMonAn);
                                            cmdInsert.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                                            cmdInsert.Parameters.AddWithValue("@DonGia", item.Gia);
                                            cmdInsert.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            // Cập nhật tổng tiền của hóa đơn
                            string updateTongTien = @"
                                UPDATE HoaDon 
                                SET TongTien = (
                                    SELECT SUM(SoLuong * DonGia) 
                                    FROM ChiTietHoaDon 
                                    WHERE MaHoaDon = @MaHoaDon
                                )
                                WHERE MaHoaDon = @MaHoaDon";

                            using (SqlCommand cmd = new SqlCommand(updateTongTien, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                cmd.ExecuteNonQuery();
                            }

                            // Cập nhật trạng thái bàn
                            string updateBanQuery = @"
                                UPDATE Ban 
                                SET TrangThai = N'Đang phục vụ'
                                WHERE SoBan = @SoBan";

                            using (SqlCommand cmd = new SqlCommand(updateBanQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SoBan", soBan);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Đã gửi yêu cầu chế biến thành công!", "Thông báo");
                            // Tạo phiếu order PDF
                            CreateOrderPDF(maHoaDon, soBan, orderItems.ToList());

                            // Xóa dữ liệu trong OrderListView và làm mới giao diện
                            orderItems.Clear();
                            OrderListView.ItemsSource = null;
                            OrderListView.ItemsSource = orderItems;
                            UpdateTotal();
                            TableComboBox.SelectedItem = null;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredList = danhSachMonAn.Where(x => 
                x.TenMonAn.ToLower().Contains(searchText));
            MenuItemsControl.ItemsSource = filteredList;
        }

        private void UpdateTotal()
        {
            decimal total = orderItems.Sum(x => x.ThanhTien);
            TotalTextBlock.Text = $"Tổng tiền: {total:N0} VNĐ";
        }

        public void ClearOrderList()
        {
            orderItems.Clear();
            OrderListView.ItemsSource = null;
            OrderListView.ItemsSource = orderItems;
            UpdateTotal();
            TableComboBox.SelectedItem = null;
        }

        private void CreateOrderPDF(int maHoaDon, int soBan, List<OrderItem> items)
        {
            try
            {
                string folderPath = @"C:\QLNhaHang\QLNhaHang\QLNhaHang\bin\Debug\PhieuOrder";
                string fileName = $"PhieuOrder_Ban{soBan}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string pdfPath = Path.Combine(folderPath, fileName);

                using (System.IO.FileStream fs = new System.IO.FileStream(pdfPath, System.IO.FileMode.Create))
                {
                    Document document = new Document(PageSize.A5, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // Thêm font chữ Unicode
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(baseFont, 12);
                    Font titleFont = new Font(baseFont, 16, Font.BOLD);

                    // Tiêu đề
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("PHIẾU ORDER", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Thông tin cơ bản
                    document.Add(new iTextSharp.text.Paragraph($"Mã hóa đơn: {maHoaDon}", font));
                    document.Add(new iTextSharp.text.Paragraph($"Bàn số: {soBan}", font));
                    document.Add(new iTextSharp.text.Paragraph($"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm}", font));
                    document.Add(new iTextSharp.text.Paragraph("----------------------------------------", font));

                    // Tạo bảng
                    PdfPTable table = new PdfPTable(3);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 2, 1, 1 });
                    table.SpacingBefore = 20f;

                    // Header của bảng
                    table.AddCell(new PdfPCell(new Phrase("Tên món", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Số lượng", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Ghi chú", font)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    // Thêm dữ liệu vào bảng
                    foreach (var item in items)
                    {
                        table.AddCell(new PdfPCell(new Phrase(item.TenMonAn, font)));
                        table.AddCell(new PdfPCell(new Phrase(item.SoLuong.ToString(), font))
                        { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase("", font))); // Cột ghi chú để trống
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph("Ghi chú: ", font));
                    document.Add(new Paragraph("----------------------------------------", font));

                    document.Close();
                }

                // Mở file PDF sau khi tạo
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = pdfPath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể mở file PDF: {ex.Message}\nFile được lưu tại: {pdfPath}", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo file PDF: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class MonAn
    {
        public string MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public decimal Gia { get; set; }

    }

    public class OrderItem : INotifyPropertyChanged
    {
        public string MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public decimal Gia { get; set; }
        public string MoTa { get; set; }

        private int soLuong;
        public int SoLuong
        {
            get => soLuong;
            set
            {
                soLuong = value;
                OnPropertyChanged(nameof(SoLuong));
                UpdateThanhTien();
            }
        }
        public decimal ThanhTien { get; private set; }

        public OrderItem(MonAn monAn)
        {
            MaMonAn = monAn.MaMonAn;
            TenMonAn = monAn.TenMonAn;
            Gia = monAn.Gia;
            SoLuong = 1;
            UpdateThanhTien();
        }

        public void UpdateThanhTien()
        {
            ThanhTien = Gia * SoLuong;
            OnPropertyChanged(nameof(ThanhTien));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
