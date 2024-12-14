CREATE TABLE NguoiDung (
    MaNguoiDung INT PRIMARY KEY IDENTITY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(50) NOT NULL,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    DienThoai NVARCHAR(15),
    MaQuyen INT,
    FOREIGN KEY (MaQuyen) REFERENCES Quyen(MaQuyen)
);

CREATE TABLE Quyen (
    MaQuyen INT PRIMARY KEY IDENTITY,
    TenQuyen NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Thêm dữ liệu mẫu vào bảng Quyen
INSERT INTO Quyen (TenQuyen) VALUES ('Admin'), ('NhanVien');
GO

CREATE TABLE MonAn (
    MaMonAn INT PRIMARY KEY IDENTITY,
    TenMonAn NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255),
    Gia DECIMAL(10, 2) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong >= 0)
);
GO
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,      -- Mã hóa đơn, tự động tăng
    MaNguoiDung INT,                             -- Mã người dùng
    NgayLap DATE,                                -- Ngày lập hóa đơn
    TongTien DECIMAL(10, 2),                     -- Tổng tiền
    SoBan INT,                                   -- Số bàn
    TrangThai NVARCHAR(50),                      -- Trạng thái hóa đơn (ví dụ: Hoàn thành, Đang chờ)
    CONSTRAINT FK_HoaDon_MaNguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung) -- Khóa ngoại đến bảng NguoiDung
);


CREATE TABLE ChiTietHoaDon (
    MaChiTietHoaDon INT IDENTITY(1,1) PRIMARY KEY,  -- Mã chi tiết hóa đơn, tự động tăng
    MaHoaDon INT,                                  -- Mã hóa đơn
    MaMonAn INT,                                   -- Mã món ăn
    SoLuong INT,                                   -- Số lượng món ăn
    DonGia DECIMAL(10, 2),                          -- Đơn giá món ăn
    GiamGia DECIMAL(10, 2),                         -- Giảm giá
    CONSTRAINT FK_ChiTietHoaDon_MaHoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon), -- Khóa ngoại đến bảng HoaDon
    CONSTRAINT FK_ChiTietHoaDon_MaMonAn FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn)  -- Khóa ngoại đến bảng MonAn
);

GO

CREATE TABLE NguyenLieu (
    MaNguyenLieu INT PRIMARY KEY IDENTITY,
    TenNguyenLieu NVARCHAR(100) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong >= 0),
    DonVi NVARCHAR(20)
);

CREATE TABLE ChiTietNguyenLieu (
    MaChiTietNguyenLieu INT PRIMARY KEY IDENTITY,
    MaMonAn INT,
    MaNguyenLieu INT,
    SoLuongCan INT NOT NULL CHECK (SoLuongCan > 0),
    FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn),
    FOREIGN KEY (MaNguyenLieu) REFERENCES NguyenLieu(MaNguyenLieu)
);
GO
CREATE TABLE NhanVien (
    MaNhanVien INT PRIMARY KEY IDENTITY,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    DienThoai NVARCHAR(15),
    DiaChi NVARCHAR(255),
    MaQuyen INT,
    FOREIGN KEY (MaQuyen) REFERENCES Quyen(MaQuyen)
);
GO
CREATE TABLE ChamCong (
    MaChamCong INT PRIMARY KEY IDENTITY,
    MaNhanVien INT,
    Ngay DATE,
    ThoiGianVao DATETIME,
    ThoiGianRa DATETIME,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);
GO
CREATE TABLE LichSuHoaDon (
    MaLichSuHoaDon INT PRIMARY KEY IDENTITY,
    MaHoaDon INT,
    MaNhanVien INT,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(10, 2),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);
GO
CREATE TABLE KhachHangVIP (
    MaVIP INT PRIMARY KEY IDENTITY,
    MaNguoiDung INT,
    TongTienTieu DECIMAL(10, 2),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
GO

INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, DienThoai, MaQuyen) VALUES 
('admin', 'admin123', 'Người Quản Trị', 'admin@example.com', '0123456789', 1),
('nhanvien1', 'matkhau1', 'Nhân Viên Một', 'nhanvien1@example.com', '0987654321', 2),
('nhanvien2', 'matkhau2', 'Nhân Viên Hai', 'nhanvien2@example.com', '0987654322', 2),
('nhanvien3', 'matkhau3', 'Nhân Viên Ba', 'nhanvien3@example.com', '0987654323', 2);
GO

INSERT INTO MonAn (TenMonAn, MoTa, Gia, SoLuong) VALUES 
('Nem', 'Nem rán Việt Nam', 5.00, 100),
('Gỏi cuốn', 'Gỏi cuốn tươi ngon', 4.00, 150),
('Phở', 'Phở bò truyền thống', 6.00, 80),
('Bò Bún', 'Bò Bún thơm ngon', 7.00, 70),
('Mì xào', 'Mì xào hải sản', 8.00, 60);
GO

INSERT INTO NguyenLieu (TenNguyenLieu, SoLuong, DonVi) VALUES 
('Thịt bò', 50, 'kg'),
('Bún', 100, 'kg'),
('Rau thơm', 30, 'kg'),
('Tôm', 40, 'kg'),
('Gà', 60, 'kg');
GO
-- Thêm dữ liệu mẫu vào bảng HoaDon
-- Dữ liệu mẫu cho bảng HoaDon
INSERT INTO HoaDon (MaNguoiDung, NgayLap, TongTien, SoBan, TrangThai) VALUES 
(1, '2023-01-01', 15.00, 1, 'Hoàn thành'),
(2, '2023-01-02', 20.00, 2, 'Hoàn thành'),
(2, '2023-01-03', 25.00, 3, 'Hoàn thành'),
(3, '2023-01-04', 30.00, 4, 'Hoàn thành'),
(3, '2023-01-05', 35.00, 5, 'Hoàn thành');
GO


-- Thêm dữ liệu mẫu vào bảng ChiTietHoaDon
-- Dữ liệu mẫu cho bảng ChiTietHoaDon
INSERT INTO ChiTietHoaDon (MaHoaDon, MaMonAn, SoLuong, DonGia, GiamGia) VALUES 
(1, 1, 3, 5.00, 0),  -- HoaDon 1, món Phở
(2, 2, 4, 4.00, 0),  -- HoaDon 2, món Bánh mì
(3, 3, 5, 6.00, 0),  -- HoaDon 3, món Cơm tấm
(4, 4, 2, 7.00, 0),  -- HoaDon 4, món Bún bò
(5, 5, 1, 8.00, 0);  -- HoaDon 5, món Gỏi cuốn
GO



INSERT INTO NhanVien (HoTen, Email, DienThoai, DiaChi, MaQuyen) VALUES 
('Nguyen Van A', 'nguyenvana@example.com', '0123456789', '123 Nguyen Trai, Quan 1, HCM', 2),
('Tran Thi B', 'tranthib@example.com', '0123456790', '456 Le Loi, Quan 3, HCM', 2),
('Le Van C', 'levanc@example.com', '0123456791', '789 Tran Hung Dao, Quan 5, HCM', 2),
('Pham Thi D', 'phamthid@example.com', '0123456792', '321 Pasteur, Quan 1, HCM', 2),
('Hoang Van E', 'hoangvane@example.com', '0123456793', '654 Nguyen Hue, Quan 1, HCM', 2);
GO
INSERT INTO ChamCong (MaNhanVien, Ngay, ThoiGianVao, ThoiGianRa) VALUES 
(1, '2023-01-01', '2023-01-01 08:00', '2023-01-01 17:00'),
(2, '2023-01-02', '2023-01-02 08:00', '2023-01-02 17:00'),
(3, '2023-01-03', '2023-01-03 08:00', '2023-01-03 17:00'),
(4, '2023-01-04', '2023-01-04 08:00', '2023-01-04 17:00'),
(5, '2023-01-05', '2023-01-05 08:00', '2023-01-05 17:00');
GO
INSERT INTO LichSuHoaDon (MaHoaDon, MaNhanVien, NgayLap, TongTien) VALUES 
(1, 1, '2023-01-01', 15.00),
(2, 2, '2023-01-02', 20.00),
(3, 3, '2023-01-03', 25.00),
(4, 4, '2023-01-04', 30.00),
(5, 5, '2023-01-05', 35.00);
GO