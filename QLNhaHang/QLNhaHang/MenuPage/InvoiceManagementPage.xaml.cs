using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace QLNhaHang
{
    /// <summary>
    /// Interaction logic for InvoiceManagementPage.xaml
    /// </summary>
    public partial class InvoiceManagementPage : UserControl
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";

        public InvoiceManagementPage()
        {
            InitializeComponent();
            this.Loaded += InvoiceManagementPage_Loaded;
        }

        private void InvoiceManagementPage_Loaded(object sender, RoutedEventArgs e)
        {
            InvoiceListView.SelectionChanged += InvoiceListView_SelectionChanged;
            LoadInvoices();
        }

        private void LoadInvoices()
        {
            try
            {
                if (ComboBox != null && SearchTextBox != null)
                {
                    ComboBox.SelectedIndex = 0; // Reset về "Tất cả trạng thái"
                    SearchTextBox.Text = ""; // Xóa text tìm kiếm
                    ComboBox_SelectionChanged(null, null); // Gọi lại hàm lọc
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvoiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InvoiceListView.SelectedItem != null)
            {
                DataRowView row = (DataRowView)InvoiceListView.SelectedItem;
                int maHoaDon = Convert.ToInt32(row["MaHoaDon"]);
                decimal tongTien = Convert.ToDecimal(row["TongTien"]);

                // Cập nhật hiển thị tổng tiền
                TotalAmountText.Text = $"Tổng tiền: {tongTien:N0} VNĐ";

                LoadInvoiceDetails(maHoaDon);
            }
            else
            {
                // Reset tổng tiền khi không có hóa đơn nào được chọn
                TotalAmountText.Text = "Tổng tiền: 0 VNĐ";
            }
        }

        private void LoadInvoiceDetails(int maHoaDon)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                    cthd.MaMonAn,
                                    ma.TenMonAn,
                                    cthd.SoLuong,
                                    cthd.DonGia,
                                    cthd.SoLuong * cthd.DonGia as ThanhTien
                                FROM ChiTietHoaDon cthd
                                JOIN MonAn ma ON cthd.MaMonAn = ma.MaMonAn
                                WHERE cthd.MaHoaDon = @MaHoaDon";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        InvoiceDetailsListView.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceListView.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in!", "Thông báo");
                return;
            }

            try
            {
                DataRowView row = (DataRowView)InvoiceListView.SelectedItem;
                int maHoaDon = Convert.ToInt32(row["MaHoaDon"]);
                int soBan = Convert.ToInt32(row["SoBan"]);
                DateTime ngayLap = Convert.ToDateTime(row["NgayLap"]);
                decimal tongTien = Convert.ToDecimal(row["TongTien"]);

                // Tạo thư mục HoaDon nếu chưa tồn tại
                string folderPath = @"C:\QLNhaHang\QLNhaHang\QLNhaHang\bin\Debug\HoaDon";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Tạo tên file PDF
                string fileName = $"HoaDon_{maHoaDon}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string pdfPath = Path.Combine(folderPath, fileName);

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

                    // Tiêu đề
                    Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Thông tin hóa đơn
                    document.Add(new Paragraph($"Mã hóa đơn: {maHoaDon}", font));
                    document.Add(new Paragraph($"Bàn số: {soBan}", font));
                    document.Add(new Paragraph($"Ngày lập: {ngayLap:dd/MM/yyyy}", font));
                    document.Add(new Paragraph("----------------------------------------", font));

                    // Tạo bảng chi tiết
                    PdfPTable table = new PdfPTable(4);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 2, 1, 1, 1 });
                    table.SpacingBefore = 20f;

                    // Header của bảng
                    table.AddCell(new PdfPCell(new Phrase("Tên món", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Số lượng", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Đơn giá", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Thành tiền", font)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    // Thêm dữ liệu từ InvoiceDetailsListView
                    foreach (DataRowView item in InvoiceDetailsListView.ItemsSource)
                    {
                        table.AddCell(new PdfPCell(new Phrase(item["TenMonAn"].ToString(), font)));
                        table.AddCell(new PdfPCell(new Phrase(item["SoLuong"].ToString(), font))
                        { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(item["DonGia"]).ToString("N0") + " VNĐ", font))
                        { HorizontalAlignment = Element.ALIGN_RIGHT });
                        table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(item["ThanhTien"]).ToString("N0") + " VNĐ", font))
                        { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }

                    document.Add(table);

                    // Tổng tiền
                    document.Add(new Paragraph("\n"));
                    Paragraph total = new Paragraph($"Tổng tiền: {tongTien:N0} VNĐ",
                        new Font(baseFont, 12, Font.BOLD));
                    total.Alignment = Element.ALIGN_RIGHT;
                    document.Add(total);

                    document.Close();
                }

                // Mở file PDF sau khi tạo
                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (InvoiceListView == null || ComboBox == null || SearchTextBox == null)
                    return;

                string selectedStatus = ((ComboBoxItem)ComboBox.SelectedItem).Content.ToString();
                string searchText = SearchTextBox.Text.Trim();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                    hd.MaHoaDon, 
                                    hd.MaNguoiDung,
                                    hd.NgayLap,
                                    hd.TongTien,
                                    hd.SoBan,
                                    hd.TrangThai
                                FROM HoaDon hd
                                WHERE 1=1 ";

                    if (selectedStatus != "Tất cả trạng thái")
                    {
                        query += " AND hd.TrangThai = @TrangThai";
                    }

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += " AND (hd.MaHoaDon LIKE @SearchText OR CAST(hd.SoBan as NVARCHAR) LIKE @SearchText)";
                    }

                    query += " ORDER BY hd.NgayLap DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (selectedStatus != "Tất cả trạng thái")
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", selectedStatus);
                        }

                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        InvoiceListView.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox_SelectionChanged(null, null);
        }

        private void SearchTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Chỉ cho phép nhập số
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return int.TryParse(text, out _);
        }

        private void SearchTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox_SelectionChanged(null, null);
        }

        private void DeleteInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceListView.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo");
                return;
            }

            DataRowView row = (DataRowView)InvoiceListView.SelectedItem;
            int maHoaDon = Convert.ToInt32(row["MaHoaDon"]);
            string trangThai = row["TrangThai"].ToString();

            if (trangThai != "Đã thanh toán")
            {
                MessageBox.Show("Chỉ có thể xóa những hóa đơn đã thanh toán!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Xóa bản ghi liên quan trong LichSuHoaDon
                    string deleteHistoryQuery = "DELETE FROM LichSuHoaDon WHERE MaHoaDon = @MaHoaDon";
                    using (SqlCommand cmd = new SqlCommand(deleteHistoryQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmd.ExecuteNonQuery();
                    }

                    // Xóa chi tiết hóa đơn
                    string deleteDetailsQuery = "DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                    using (SqlCommand cmd = new SqlCommand(deleteDetailsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmd.ExecuteNonQuery();
                    }

                    // Xóa hóa đơn
                    string deleteInvoiceQuery = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    using (SqlCommand cmd = new SqlCommand(deleteInvoiceQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadInvoices(); // Tải lại danh sách hóa đơn
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
