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
using System.Collections.Generic;
using QLNhaHang.EmployeeControl;

namespace QLNhaHang
{
    /// <summary>
    /// Interaction logic for TableStatusPage.xaml
    /// </summary>
    public partial class TableStatusPage : UserControl
    {
        private ObservableCollection<TableInfo> _tables;
        public ObservableCollection<TableInfo> Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(); }
        }
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\QLNhaHang\QLNhaHang\QLNhaHang\DatabaseQLnhahang.mdf;Integrated Security=True";
        public TableStatusPage()
        {
            InitializeComponent();
            SetupTableButtons();
            UpdateTableStatus();
            CurrentTableText.Text = "Bàn: Chưa chọn";
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

                    // Cập nhật TextBlock hiển thị số bàn
                    CurrentTableText.Text = $"Bàn: {maBan}";

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
        public void PaymentButton_Click(object sender, EventArgs e)
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

                // Tạo thư mục HoaDon nếu chưa tồn tại
                string folderPath = @"C:\QLNhaHang\QLNhaHang\QLNhaHang\bin\Debug\HoaDon";


                // Tạo tên file và đường dẫn đầy đủ
                string fileName = $"HoaDon_Ban{maBan}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string pdfPath = Path.Combine(folderPath, fileName);

                decimal tongTienTatCa = 0;
                HashSet<int> danhSachHoaDon = new HashSet<int>();
                DataTable dataForPDF = new DataTable();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Lấy dữ liệu cho hóa đơn
                            string queryHoaDon = @"
                                SELECT 
                                    ma.TenMonAn,
                                    SUM(cthd.SoLuong) as TongSoLuong,
                                    cthd.DonGia,
                                    SUM(cthd.DonGia * cthd.SoLuong) as ThanhTien,
                                    STRING_AGG(hd.MaHoaDon, ',') as DanhSachHoaDon
                                FROM HoaDon hd
                                JOIN ChiTietHoaDon cthd ON hd.MaHoaDon = cthd.MaHoaDon
                                JOIN MonAn ma ON cthd.MaMonAn = ma.MaMonAn
                                WHERE hd.SoBan = @MaBan 
                                AND hd.TrangThai = N'Chưa thanh toán'
                                GROUP BY ma.TenMonAn, cthd.DonGia
                                ORDER BY ma.TenMonAn";

                            using (SqlCommand cmdHoaDon = new SqlCommand(queryHoaDon, conn, transaction))
                            {
                                cmdHoaDon.Parameters.AddWithValue("@MaBan", maBan);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmdHoaDon))
                                {
                                    adapter.Fill(dataForPDF);
                                }

                                // Xử lý dữ liệu từ dataForPDF
                                foreach (DataRow row in dataForPDF.Rows)
                                {
                                    string[] maHoaDons = row["DanhSachHoaDon"].ToString().Split(',');
                                    foreach (string maHD in maHoaDons)
                                    {
                                        if (int.TryParse(maHD, out int maHoaDon))
                                        {
                                            danhSachHoaDon.Add(maHoaDon);
                                        }
                                    }
                                    tongTienTatCa += Convert.ToDecimal(row["ThanhTien"]);
                                }
                            }

                            if (danhSachHoaDon.Count == 0)
                            {
                                MessageBox.Show("Không có hóa đơn nào cần thanh toán!", "Thông báo");
                                return;
                            }

                            // 2. Xử lý thanh toán trong database
                            foreach (int maHoaDon in danhSachHoaDon)
                            {
                                // 1. Lưu vào LichSuHoaDon trước
                                string insertLichSu = @"
                                    INSERT INTO LichSuHoaDon (MaHoaDon, MaNhanVien, NgayLap, TongTien)
                                    SELECT 
                                        @MaHoaDon,
                                        @MaNhanVien,
                                        @NgayLap,
                                        SUM(cthd.DonGia * cthd.SoLuong)
                                    FROM ChiTietHoaDon cthd
                                    WHERE cthd.MaHoaDon = @MaHoaDon";

                                using (SqlCommand cmd = new SqlCommand(insertLichSu, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmd.Parameters.AddWithValue("@MaNhanVien", 1);
                                    cmd.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                }

                                // 2. Cập nhật trạng thái HoaDon thành 'Đã thanh toán' thay vì xóa
                                string updateHoaDon = @"
                                    UPDATE HoaDon 
                                    SET TrangThai = N'Đã thanh toán'
                                    WHERE MaHoaDon = @MaHoaDon";

                                using (SqlCommand cmd = new SqlCommand(updateHoaDon, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmd.ExecuteNonQuery();
                                }
                                //3. 
                                string xoaCTHD = @"
                                    DELETE FROM ChiTietHoaDon
                                    WHERE MaHoaDon = @MaHoaDon;
                                    ";

                                using (SqlCommand cmd = new SqlCommand(xoaCTHD, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // 4. Cập nhật trạng thái bàn
                            using (SqlCommand cmd = new SqlCommand("UPDATE Ban SET TrangThai = N'Trống' WHERE SoBan = @SoBan", conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SoBan", maBan);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            // 3. Tạo file PDF sau khi đã commit transaction thành công
                            CreatePDFFile(pdfPath, maBan, dataForPDF, tongTienTatCa);

                            // 4. Cập nhật UI và hiển thị thông báo
                            OrderDetailsGrid.ItemsSource = null;
                            TotalAmount.Text = "0 VNĐ";
                            CurrentTableText.Text = "Bàn: Chưa chọn";  // Reset text về trạng thái ban đầu
                            UpdateTableStatus();

                            // Tìm và làm mới MenuUserControl nếu đang mở
                            var mainWindow = Window.GetWindow(this) as EmployeeWindow;
                            if (mainWindow != null)
                            {
                                var menuControl = mainWindow.FindName("MenuControl") as MenuUserControl;
                            }

                            MessageBox.Show("Thanh toán thành công!", "Thông báo");

                            // Mở file PDF
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
                            transaction.Rollback();
                            throw new Exception("Lỗi khi thanh toán: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CreatePDFFile(string pdfPath, int maBan, DataTable data, decimal tongTienTatCa)
        {
            try
            {
                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(baseFont, 12);
                    Font titleFont = new Font(baseFont, 18, Font.BOLD);

                    Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Paragraph($"Bàn số: {maBan}", font));
                    document.Add(new Paragraph($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", font));
                    document.Add(new Paragraph("-----------------------------------", font));

                    PdfPTable table = new PdfPTable(4);
                    table.WidthPercentage = 100;
                    table.SpacingBefore = 20f;

                    // Header của bảng
                    table.AddCell(new PdfPCell(new Phrase("Tên món", font)));
                    table.AddCell(new PdfPCell(new Phrase("Số lượng", font)));
                    table.AddCell(new PdfPCell(new Phrase("Đơn giá", font)));
                    table.AddCell(new PdfPCell(new Phrase("Thành tiền", font)));

                    foreach (DataRow row in data.Rows)
                    {
                        table.AddCell(new PdfPCell(new Phrase(row["TenMonAn"].ToString(), font)));
                        table.AddCell(new PdfPCell(new Phrase(row["TongSoLuong"].ToString(), font))
                        { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["DonGia"]).ToString("N0"), font))
                        { HorizontalAlignment = Element.ALIGN_RIGHT });
                        table.AddCell(new PdfPCell(new Phrase(Convert.ToDecimal(row["ThanhTien"]).ToString("N0"), font))
                        { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));
                    Paragraph total = new Paragraph($"Tổng tiền: {tongTienTatCa.ToString("N0")} VNĐ",
                        new Font(baseFont, 12, Font.BOLD));
                    total.Alignment = Element.ALIGN_RIGHT;
                    document.Add(total);

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo file PDF: {ex.Message}");
            }
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
