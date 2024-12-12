CREATE TABLE NguoiDung (
    ID_ND INT,
    Email VARCHAR(50) NOT NULL,
    Matkhau VARCHAR(20) NOT NULL,
    VerifyCode VARCHAR(10) DEFAULT NULL,
    Trangthai VARCHAR(10) DEFAULT '',
    Vaitro NVARCHAR(20),
    CONSTRAINT ND_Email_NNULL CHECK (Email IS NOT NULL),
    CONSTRAINT ND_Matkhau_NNULL CHECK (Matkhau IS NOT NULL),
    CONSTRAINT ND_Vaitro_Ten CHECK (Vaitro IN (N'Khách Hàng', N'Nhân Viên', N'Nhân Viên Kho', N'Quản Lý')),
    CONSTRAINT NguoiDung_PK PRIMARY KEY (ID_ND)
);

-- Create NhanVien table
CREATE TABLE NhanVien (
    ID_NV INT,
    TenNV VARCHAR(50) NOT NULL,
    NgayVL DATE NOT NULL,
    SDT VARCHAR(50) NOT NULL,
    Chucvu NVARCHAR(50),
    ID_ND INT DEFAULT NULL,
    ID_NQL INT,
    Tinhtrang NVARCHAR(20),
    CONSTRAINT NV_TenNV_NNULL CHECK (TenNV IS NOT NULL),
    CONSTRAINT NV_SDT_NNULL CHECK (SDT IS NOT NULL),
    CONSTRAINT NV_NgayVL_NNULL CHECK (NgayVL IS NOT NULL),
    CONSTRAINT NV_Chucvu_Thuoc CHECK (Chucvu IN (N'Phục vụ', N'Tiếp tân', N'Thu ngân', N'Bếp', N'Kho', N'Quản lý')),
    CONSTRAINT NV_Tinhtrang_Thuoc CHECK (Tinhtrang IN (N'Đang làm việc', N'Đã nghỉ việc')),
    CONSTRAINT NV_PK PRIMARY KEY (ID_NV),
    CONSTRAINT NV_fk_idND FOREIGN KEY (ID_ND) REFERENCES NguoiDung(ID_ND),
    CONSTRAINT NV_fk_idNQL FOREIGN KEY (ID_NQL) REFERENCES NhanVien(ID_NV)
);


-- Create KhachHang table
CREATE TABLE KhachHang (
    ID_KH INT,
    TenKH VARCHAR(50) NOT NULL,
    Ngaythamgia DATE NOT NULL,
    Doanhso DECIMAL(10, 0) DEFAULT 0,
    Diemtichluy DECIMAL(5, 0) DEFAULT 0,
    ID_ND INT NOT NULL,
    CONSTRAINT KH_TenKH_NNULL CHECK (TenKH IS NOT NULL),
    CONSTRAINT KH_Ngaythamgia_NNULL CHECK (Ngaythamgia IS NOT NULL),
    CONSTRAINT KH_Doanhso_NNULL CHECK (Doanhso IS NOT NULL),
    CONSTRAINT KH_Diemtichluy_NNULL CHECK (Diemtichluy IS NOT NULL),
    CONSTRAINT KH_IDND_NNULL CHECK (ID_ND IS NOT NULL),
    CONSTRAINT KH_PK PRIMARY KEY (ID_KH),
    CONSTRAINT KH_fk_idND FOREIGN KEY (ID_ND) REFERENCES NguoiDung(ID_ND)
);


-- Create MonAn table
CREATE TABLE MonAn (
    ID_MonAn INT,
    TenMon VARCHAR(50),
    DonGia DECIMAL(8,0),
    Loai NVARCHAR(50),
    TrangThai NVARCHAR(30),
    CONSTRAINT MA_TenMon_NNULL CHECK (TenMon IS NOT NULL),
    CONSTRAINT MA_DonGia_NNULL CHECK (DonGia IS NOT NULL),
    CONSTRAINT MA_Loai_Ten CHECK (Loai IN (N'Thịt cừu',N'Thịt bò',N'Combo', N'Hải sản - Cua',N'Ẩm thực Hàn Quốc',N'Tráng miệng',N'Salad',N'Ẩm thực Nhật',N'Thịt ngựa',N'Thịt dê',N'Đồ uống',N'Hải sản - Cá')),
    CONSTRAINT MA_TrangThai_Thuoc CHECK (TrangThai IN (N'Đang kinh doanh',N'Ngừng kinh doanh')),
    CONSTRAINT MonAn_PK PRIMARY KEY (ID_MonAn)
);

-- Create Ban table
CREATE TABLE Ban (
    ID_Ban INT,
    TenBan VARCHAR(50),
    Vitri VARCHAR(50),
    Trangthai NVARCHAR(50),
    CONSTRAINT Ban_TenBan_NNULL CHECK (TenBan IS NOT NULL),
    CONSTRAINT Ban_Vitri_NNULL CHECK (Vitri IS NOT NULL),
    CONSTRAINT Ban_Trangthai_Ten CHECK (Trangthai IN (N'Còn trống',N'Đang dùng bữa',N'Đã đặt trước')),
    CONSTRAINT Ban_PK PRIMARY KEY (ID_Ban)
);

-- Create Voucher table
CREATE TABLE Voucher (
    Code_Voucher VARCHAR(10),
    Mota VARCHAR(50),
    Phantram INT CHECK (Phantram > 0 AND Phantram <= 100),
    LoaiMA NVARCHAR(50),
    SoLuong INT,
    Diem DECIMAL(8,0),
    CONSTRAINT V_Code_NNULL CHECK (Code_Voucher IS NOT NULL),
    CONSTRAINT V_Mota_NNULL CHECK (Mota IS NOT NULL),
    CONSTRAINT V_LoaiMA_Thuoc CHECK (LoaiMA IN ('All', N'Thịt cừu',N'Thịt bò',N'Combo', N'Hải sản - Cua',N'Ẩm thực Hàn Quốc',N'Tráng miệng',N'Salad',N'Ẩm thực Nhật',N'Thịt ngựa',N'Thịt dê',N'Đồ uống',N'Hải sản - Cá')),
    CONSTRAINT Voucher_PK PRIMARY KEY (Code_Voucher)
);

-- Create HoaDon table
CREATE TABLE HoaDon (
    ID_HoaDon INT,
    ID_KH INT,
    ID_Ban INT,
    NgayHD DATE,
    TienMonAn DECIMAL(8,0),
    Code_Voucher VARCHAR(10),
    TienGiam DECIMAL(8,0),
    Tongtien DECIMAL(10,0),
    Trangthai NVARCHAR(50),
    CONSTRAINT HD_NgayHD_NNULL CHECK (NgayHD IS NOT NULL),
    CONSTRAINT HD_TrangThai CHECK (Trangthai IN (N'Chưa thanh toán', N'Đã thanh toán')),
    CONSTRAINT HD_PK PRIMARY KEY (ID_HoaDon),
    CONSTRAINT HD_fk_idKH FOREIGN KEY (ID_KH) REFERENCES KhachHang(ID_KH),
    CONSTRAINT HD_fk_idBan FOREIGN KEY (ID_Ban) REFERENCES Ban(ID_Ban)
);


-- Create CTHD table
CREATE TABLE CTHD (
    ID_HoaDon INT,
    ID_MonAn INT,
    SoLuong INT,
    Thanhtien DECIMAL(10,0),
    CONSTRAINT CTHD_SoLuong_NNULL CHECK (SoLuong IS NOT NULL),
    CONSTRAINT CTHD_PK PRIMARY KEY (ID_HoaDon, ID_MonAn),
    CONSTRAINT CTHD_fk_idHD FOREIGN KEY (ID_HoaDon) REFERENCES HoaDon(ID_HoaDon),
    CONSTRAINT CTHD_fk_idMonAn FOREIGN KEY (ID_MonAn) REFERENCES MonAn(ID_MonAn)
);

-- Create NguyenLieu table
CREATE TABLE NguyenLieu (
    ID_NL INT,
    TenNL VARCHAR(50),
    Dongia DECIMAL(8,0),
    Donvitinh VARCHAR(50),
    CONSTRAINT NL_TenNL_NNULL CHECK (TenNL IS NOT NULL),
    CONSTRAINT NL_Dongia_NNULL CHECK (Dongia IS NOT NULL),
    CONSTRAINT NL_DVT_Thuoc CHECK (Donvitinh IN ('g', 'kg', 'ml', 'l')),
    CONSTRAINT NL_PK PRIMARY KEY (ID_NL)
);

-- Create Kho table
CREATE TABLE Kho (
    ID_NL INT,
    SLTon INT DEFAULT 0,
    CONSTRAINT Kho_pk PRIMARY KEY (ID_NL),
    CONSTRAINT Kho_fk_idNL FOREIGN KEY (ID_NL) REFERENCES NguyenLieu(ID_NL)
);

-- Create PhieuNK table
CREATE TABLE PhieuNK (
    ID_NK INT,
    ID_NV INT,
    NgayNK DATE,
    Tongtien DECIMAL(10,0) DEFAULT 0,
    CONSTRAINT PNK_NgayNK_NNULL CHECK (NgayNK IS NOT NULL),
    CONSTRAINT PNK_PK PRIMARY KEY (ID_NK),
    CONSTRAINT PNK_fk_idNV FOREIGN KEY (ID_NV) REFERENCES NhanVien(ID_NV)
);

-- Create CTNK table
CREATE TABLE CTNK (
    ID_NK INT,
    ID_NL INT,
    SoLuong INT,
    Thanhtien DECIMAL(10,0),
    CONSTRAINT CTNK_SL_NNULL CHECK (SoLuong IS NOT NULL),
    CONSTRAINT CTNK_PK PRIMARY KEY (ID_NK, ID_NL),
    CONSTRAINT CTNK_fk_idNK FOREIGN KEY (ID_NK) REFERENCES PhieuNK(ID_NK),
    CONSTRAINT CTNK_fk_idNL FOREIGN KEY (ID_NL) REFERENCES NguyenLieu(ID_NL)
);

-- Create PhieuXK table
CREATE TABLE PhieuXK (
    ID_XK INT,
    ID_NV INT,
    NgayXK DATE,
    CONSTRAINT PXK_NgayXK_NNULL CHECK (NgayXK IS NOT NULL),
    CONSTRAINT PXK_PK PRIMARY KEY (ID_XK),
    CONSTRAINT PXK_fk_idNV FOREIGN KEY (ID_NV) REFERENCES NhanVien(ID_NV)
);

-- Create CTXK table
CREATE TABLE CTXK (
    ID_XK INT,
    ID_NL INT,
    SoLuong INT,
    CONSTRAINT CTXK_SL_NNULL CHECK (SoLuong IS NOT NULL),
    CONSTRAINT CTXK_PK PRIMARY KEY (ID_XK, ID_NL),
    CONSTRAINT CTNK_fk_idXK FOREIGN KEY (ID_XK) REFERENCES PhieuXK(ID_XK),
    CONSTRAINT CTXK_fk_idNL FOREIGN KEY (ID_NL) REFERENCES NguyenLieu(ID_NL)
);
SELECT CONVERT(VARCHAR, GETDATE(), 103) AS FormattedDate;  -- Displays date in dd/MM/yyyy format

--Them data cho Bang NguoiDung
-- Nhân viên
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (100, 'NVHoangViet@gmail.com', '123', 'Verified', N'Quản Lý');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (101, 'NVHoangPhuc@gmail.com', '123', 'Verified', N'Nhân Viên');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (102, 'NVAnhHong@gmail.com', '123', 'Verified', N'Nhân Viên Kho');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (103, 'NVQuangDinh@gmail.com', '123', 'Verified', N'Nhân Viên');

-- Khách Hàng
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (104, 'KHThaoDuong@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (105, 'KHTanHieu@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (106, 'KHQuocThinh@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (107, 'KHNhuMai@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (108, 'KHBichHao@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (109, 'KHMaiQuynh@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (110, 'KHMinhQuang@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (111, 'KHThanhHang@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (112, 'KHThanhNhan@gmail.com', '123', 'Verified', N'Khách Hàng');
INSERT INTO NguoiDung(ID_ND, Email, MatKhau, Trangthai, Vaitro) VALUES (113, 'KHPhucNguyen@gmail.com', '123', 'Verified', N'Khách Hàng');


--Them data cho bang Nhan Vien
SELECT CONVERT(VARCHAR, GETDATE(), 103) AS FormattedDate;  -- Displays date in dd/MM/yyyy format
SELECT CONVERT(DATETIME, NgayVL, 103) AS Date
FROM NhanVien;
-- Có tài khoản
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_ND, ID_NQL, Tinhtrang) VALUES (100, N'Nguyễn Hoàng Việt', '2023-05-10', '0848044725', N'Quản lý', 100, 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_ND, ID_NQL, Tinhtrang) VALUES (101, N'Nguyễn Hoàng Phúc', '2023-05-20', '0838033334', N'Tiếp tân', 101, 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_ND, ID_NQL, Tinhtrang) VALUES (102, N'Lê Thị Anh Hồng', '2023-05-19', '0838033234', N'Kho', 102, 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_ND, ID_NQL, Tinhtrang) VALUES (103, N'Hồ Quang Định', '2023-05-19', '0838033234', N'Tiếp tân', 103, 100, N'Đang làm việc');

-- Không có tài khoản
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_NQL, Tinhtrang) VALUES (104, N'Hà Thảo Dương', '2023-05-10', '0838033232', N'Phục vụ', 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_NQL, Tinhtrang) VALUES (105, N'Nguyễn Quốc Thịnh', '2023-05-11', '0838033734', N'Phục vụ', 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_NQL, Tinhtrang) VALUES (106, N'Trương Tấn Hiếu', '2023-05-12', '0838033834', N'Phục vụ', 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_NQL, Tinhtrang) VALUES (107, N'Nguyễn Thái Bảo', '2023-05-10', '0838093234', N'Phục vụ', 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_NQL, Tinhtrang) VALUES (108, N'Trần Nhật Khang', '2023-05-11', '0838133234', N'Thu ngân', 100, N'Đang làm việc');
INSERT INTO NhanVien(ID_NV, TenNV, NgayVL, SDT, Chucvu, ID_NQL, Tinhtrang) VALUES (109, N'Nguyễn Ngọc Lương', '2023-05-12', '0834033234', N'Bếp', 100, N'Đang làm việc');

-- Thêm data cho bảng Khách Hàng
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (100, N'Hà Thảo Dương', '10/05/2023', 104);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (101, N'Trương Tấn Hiếu', '10/05/2023', 105);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (102, N'Nguyễn Quốc Thịnh', '10/05/2023', 106);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (103, N'Trần Như Mai', '10/05/2023', 107);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (104, N'Nguyễn Thị Bích Hào', '10/05/2023', 108);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (105, N'Nguyễn Mai Quỳnh', '11/05/2023', 109);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (106, N'Hoàng Minh Quang', '11/05/2023', 110);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (107, N'Nguyễn Thanh Hằng', '12/05/2023', 111);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (108, N'Nguyễn Ngọc Thanh Nhàn', '11/05/2023', 112);
INSERT INTO KhachHang(ID_KH, TenKH, Ngaythamgia, ID_ND) VALUES (109, N'Hoàng Thị Phúc Nguyễn', '12/05/2023', 113);


--Thịt cừu
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(1,'Đùi Cừu Nướng Xé Nhỏ', 250000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(2,'Bẹ Sườn Cừu Nướng Giấy Bạc Mông Cổ', 230000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(3,'Đùi Cừu Nướng Trung Đông', 350000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(4,'Cừu Xốc Lá Cà Ri', 129000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(5,'Cừu Kungbao', 250000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(6,'Bắp Cừu Nướng Cay', 250000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(7,'Cừu Viên Hầm Cay', 19000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(8,'Sườn Cồng Nướng Mông Cổ', 250000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(9,'Đùi Cừu Lớn Nướng Tại Bàn', 750000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(10,'Sườn Cừu Nướng Sốt Nấm', 450000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(11,'Đùi Cừu Nướng Tiêu Xanh', 285000,N'Thịt cừu',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(12,'Sườn Cừu Sốt Phô Mai', 450000,N'Thịt cừu',N'Đang kinh doanh');

--Thịt bò
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(13,'Bít Tết Bò Mỹ Khoai Tây', 179000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(14,'Bò Bít Tết Úc',169000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(15,'Bít Tết Bò Mỹ BASIC', 179000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(16,'Mỳ Ý Bò Bằm', 169000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(17,'Thịt Sườn Wagyu', 1180000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(18,'Steak Thịt Vai Wagyu', 1290000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(19,'Steak Thịt Bụng Bò', 550000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(20,'Tomahawk', 2390000,N'Thịt bò',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(21,'Salad Romaine Nướng', 180000,N'Thịt bò',N'Đang kinh doanh');

--Combo
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(22,'Combo Happy', 180000,'Combo',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(23,'Combo Fantastic', 190000,'Combo',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(24,'Combo Dreamer', 230000,'Combo',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(25,'Combo Cupid', 180000,'Combo',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(26,'Combo Poseidon', 190000,'Combo',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(27,'Combo Luang Prabang', 490000,'Combo',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(28,'Combo Vientiane', 620000,'Combo',N'Đang kinh doanh');

--Hải sản - Cua
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(29,'Cua KingCrab Đức Sốt', 3650000,N'Hải sản - Cua',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(30,'Mai Cua KingCrab Topping Phô Mai', 2650000,N'Hải sản - Cua',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(31,'Cua KingCrab Sốt Tứ Xuyên', 2300000,N'Hải sản - Cua',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(32,'Cua KingCrab Nướng Tự Nhiên', 2550000,N'Hải sản - Cua',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(33,'Cua KingCrab Nướng Bơ Tỏi', 2650000,N'Hải sản - Cua',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(34,'Cơm Mai Cua KingCrab Chiên', 1850000,N'Hải sản - Cua',N'Đang kinh doanh');

--Ẩm thực Hàn Quốc
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(35,'Bossam', 650000,N'Ẩm thực Hàn Quốc',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(36,'Kimchi Pancake', 350000,N'Ẩm thực Hàn Quốc',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(37,'Spicy Rice Cake', 250000,N'Ẩm thực Hàn Quốc',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(38,'Spicy Sausage Hotpot', 650000,N'Ẩm thực Hàn Quốc',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(39,'Spicy Pork', 350000,N'Ẩm thực Hàn Quốc',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(40,'Mushroom Spicy Silky Tofu Stew', 350000,N'Ẩm thực Hàn Quốc',N'Đang kinh doanh');

--Tráng miệng
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(41,'Pavlova', 150000,N'Tráng miệng',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(42,'Kesutera', 120000,N'Tráng miệng',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(43,'Cremeschnitte', 250000,N'Tráng miệng',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(44,'Sachertorte', 150000,N'Tráng miệng',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(45,'Schwarzwalder Kirschtorte', 250000,N'Tráng miệng',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(46,'New York-Style Cheesecake', 250000,N'Tráng miệng',N'Đang kinh doanh');

--Salad
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(47,'Cobb Salad', 150000,'Salad',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(48,'Salad Israeli', 120000,'Salad',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(49,N'Salad Dâu đen', 120000,'Salad',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(50,'Waldorf Salad', 160000,'Salad',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(51,'Salad Gado-Gado', 200000,'Salad',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(52,'Nicoise Salad', 250000,'Salad',N'Đang kinh doanh');

--Ẩm thực Nhật
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(53,'BULGOGI LUNCHBOX', 250000,N'Ẩm thực Nhật',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(54,'CHICKEN TERIYAKI LUNCHBOX', 350000,N'Ẩm thực Nhật',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(55,'SPICY PORK LUNCHBOX', 350000,N'Ẩm thực Nhật',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(56,'TOFU TERIYAKI LUNCHBOX', 250000,N'Ẩm thực Nhật',N'Đang kinh doanh');

--Thịt ngựa
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(57,'Thit ngua do tuoi', 250000,N'Thịt ngựa',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(58,'Steak Thit ngua', 350000,N'Thịt ngựa',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(59,'Thit ngua ban gang', 350000,N'Thịt ngựa',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(60,'Long ngua xao dua', 150000,N'Thịt ngựa',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(61,'Thit ngua xao sa ot', 250000,N'Thịt ngựa',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(62,'Ngua tang', 350000,N'Thịt ngựa',N'Đang kinh doanh');

--Thịt dê
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(63,'Thit de xong hoi', 229000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(64,'Thit de xao rau ngo', 199000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(65,'Thit de nuong tang', 229000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(66,'Thit de chao', 199000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(67,'Thit de nuong xien', 199000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(68,'Nam de nuong/chao', 199000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(69,'Thit de xao lan', 19000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(70,'Dui de tan thuoc bac', 199000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(71,'Canh de ham duong quy', 199000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(72,'Chao de dau xanh', 50000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(73,'Thit de nhung me', 229000,N'Thịt dê',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(74,'Lau de nhu', 499000,N'Thịt dê',N'Đang kinh doanh');


--Đồ uống
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(75,'SIGNATURE WINE', 3290000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(76,'CHILEAN WINE', 3990000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(77,'ARGENTINA WINE', 2890000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(78,'ITALIAN WINE', 5590000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(79,'AMERICAN WINE', 4990000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(80,'CLASSIC COCKTAIL', 200000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(81,'SIGNATURE COCKTAIL', 250000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(82,'MOCKTAIL', 160000,N'Đồ uống',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(83,'JAPANESE SAKE', 1490000,N'Đồ uống',N'Đang kinh doanh');

--Hải sản - Cá
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(84,N'Cá Hồi Ngâm Tương', 289000,N'Hải sản - Cá',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(85,N'Cá Ngừ Ngâm Tương', 289000,N'Hải sản - Cá',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(86,N'IKURA:Trứng Cá Hồi', 189000,N'Hải sản - Cá',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(87,N'KARIN:Sashimi Cá Ngừ', 149000,N'Hải sản - Cá',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(88,N'KEIKO:Sashimi Cá Hồi', 199000,N'Hải sản - Cá',N'Đang kinh doanh');
insert into MonAn(ID_MonAn,TenMon,Dongia,Loai,TrangThai) values(89,N'CHIYO:Sashimi Bụng Cá Hồi', 219000,N'Hải sản - Cá',N'Đang kinh doanh');  
--Them data cho bang Ban
--Tang 1
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(100,'Ban T1.1','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(101,'Ban T1.2','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(102,'Ban T1.3','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(103,'Ban T1.4','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(104,'Ban T1.5','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(105,'Ban T1.6','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(106,'Ban T1.7','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(107,'Ban T1.8','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(108,'Ban T1.9','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(109,'Ban T1.10','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(110,'Ban T1.11','Tang 1',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(111,'Ban T1.12','Tang 1',N'Còn trống');
--Tang 2
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(112,'Ban T2.1','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(113,'Ban T2.2','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(114,'Ban T2.3','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(115,'Ban T2.4','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(116,'Ban T2.5','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(117,'Ban T2.6','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(118,'Ban T2.7','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(119,'Ban T2.8','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(120,'Ban T2.9','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(121,'Ban T2.10','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(122,'Ban T2.11','Tang 2',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(123,'Ban T2.12','Tang 2',N'Còn trống');
--Tang 3
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(124,'Ban T3.1','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(125,'Ban T3.2','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(126,'Ban T3.3','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(127,'Ban T3.4','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(128,'Ban T3.5','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(129,'Ban T3.6','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(130,'Ban T3.7','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(131,'Ban T3.8','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(132,'Ban T3.9','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(133,'Ban T3.10','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(134,'Ban T3.11','Tang 3',N'Còn trống');
insert into Ban(ID_Ban,TenBan,Vitri,Trangthai) values(135,'Ban T3.12','Tang 3',N'Còn trống');

--Them data cho bang Voucher
INSERT INTO Voucher (Code_Voucher, Phantram, LoaiMA, SoLuong, Diem, Mota)
VALUES 
('loQy', 20, N'Thịt cừu', 10, 200, '20% off for Thịt Cừu Menu'),
('pCfI', 30, N'Thịt bò', 5, 300, '30% off for Thịt Bò Menu'),
('pApo', 20, 'Combo', 10, 200, '20% off for Combo Menu'),
('ugQx', 100, 'Salad', 3, 500, '100% off for Salad Menu'),
('nxVX', 20, 'All', 5, 300, '20% off for All Menu'),
('Pwyn', 20, N'Hải sản - Cua', 10, 200, '20% off for Hải sản - Cua Menu'),
('bjff', 50, N'Ẩm thực Hàn Quốc', 5, 600, '50% off for Ẩm thực Hàn Quốc Menu'),
('YPzJ', 20, N'Đồ uống', 5, 200, '20% off for Đồ uống Menu'),
('Y5g0', 30, N'Ẩm thực Nhật', 5, 300, '30% off for Ẩm thực Nhật Menu'),
('7hVO', 60, N'Thịt cừu', 0, 1000, '60% off for Thịt cừu Menu'),
('WHLm', 20, N'Tráng miệng', 0, 200, '20% off for Tráng miệng Menu'),
('GTsC', 20, N'Ẩm thực Hàn Quốc', 0, 200, '20% off for Ẩm thực Hàn Quốc Menu');


INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (101, 100, 100, '2023-01-10', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (102, 104, 102, '2023-01-15', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (103, 105, 103, '2023-01-20', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (104, 101, 101, '2023-02-13', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (105, 103, 120, '2023-02-12', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (106, 104, 100, '2023-03-16', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (107, 107, 103, '2023-03-20', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (108, 108, 101, '2023-04-10', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (109, 100, 100, '2023-04-20', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (110, 103, 101, '2023-05-05', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (111, 106, 102, '2023-05-10', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (112, 108, 103, '2023-05-15', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (113, 106, 102, '2023-05-20', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (114, 108, 103, '2023-06-05', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (115, 109, 104, '2023-06-07', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (116, 100, 105, '2023-06-07', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (117, 106, 106, '2023-06-10', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (118, 102, 106, '2023-02-10', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (119, 103, 106, '2023-02-12', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (120, 104, 106, '2023-04-10', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (121, 105, 106, '2023-04-12', 0, 0, N'Chưa thanh toán');
INSERT INTO HoaDon(ID_HoaDon,ID_KH,ID_Ban,NgayHD,TienMonAn,TienGiam,Trangthai) VALUES (122, 107, 106, '2023-05-12', 0, 0, N'Chưa thanh toán');

--Them data cho CTHD
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (101,1,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (101,3,1);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (101,10,3);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (102,1,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (102,2,1);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (102,4,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (103,12,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (104,30,3);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (104,59,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (105,28,1);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (105,88,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (106,70,3);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (106,75,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (106,78,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (107,32,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (107,12,5);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (108,12,1);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (108,40,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (109,45,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (110,34,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (110,43,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (111,65,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (111,47,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (112,49,3);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (112,80,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (112,31,5);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (113,80,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (113,80,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (114,30,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (114,32,3);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (115,80,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (116,57,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (116,34,1);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (117,67,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (117,66,3);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (118,34,10);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (118,35,5);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (119,83,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (119,78,2);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (120,38,5);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (120,39,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (121,53,5);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (121,31,4);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (122,33,5);
INSERT INTO CTHD(ID_HoaDon,ID_MonAn,SoLuong) VALUES (122,34,6);
UPDATE HOADON SET TrangThai=N'Đã thanh toán';

--Them data cho bang NguyenLieu
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(100,'Thit ga',40000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(101,'Thit heo',50000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(102,'Thit bo',80000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(103,'Tom',100000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(104,'Ca hoi',500000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(105,'Gao',40000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(106,'Sua tuoi',40000,'l');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(107,'Bot mi',20000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(108,'Dau ca hoi',1000000,'l');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(109,'Dau dau nanh',150000,'l');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(110,'Muoi',20000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(111,'Duong',20000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(112,'Hanh tay',50000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(113,'Toi',30000,'kg');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(114,'Dam',50000,'l');
INSERT INTO NguyenLieu(ID_NL,TenNL,Dongia,Donvitinh) VALUES(115,'Thit de',130000,'kg');

--Them data cho PhieuNK
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (100, 102, '2023-01-10');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (101, 102, '2023-02-11');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (102, 102, '2023-02-12');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (103, 102, '2023-03-12');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (104, 102, '2023-03-15');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (105, 102, '2023-04-12');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (106, 102, '2023-04-15');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (107, 102, '2023-05-12');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (108, 102, '2023-05-15');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (109, 102, '2023-06-05');
INSERT INTO PhieuNK (ID_NK, ID_NV, NgayNK) VALUES (110, 102, '2023-06-07');


--Them data cho CTNK
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (100,100,10);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (100,101,20);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (100,102,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,101,10);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,103,20);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,104,10);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,105,10);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,106,20);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,107,5);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (101,108,5);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (102,109,10);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (102,110,20);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (102,112,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (102,113,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (102,114,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (103,112,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (103,113,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (103,114,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (104,112,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (104,113,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (105,110,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (106,102,25);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (106,115,25);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (107,110,35);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (107,105,25);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (108,104,25);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (108,103,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (108,106,30);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (109,112,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (109,113,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (109,114,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (110,102,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (110,106,25);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (110,107,15);
INSERT INTO CTNK(ID_NK,ID_NL,SoLuong) VALUES (110,110,20);

--Them data cho PhieuXK
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (100,102,'10-01-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (101,102,'11-02-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (102,102,'12-03-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (103,102,'13-03-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (104,102,'12-04-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (105,102,'13-04-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (106,102,'12-05-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (107,102,'15-05-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (108,102,'20-05-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (109,102,'5-06-2023');
INSERT INTO PhieuXK(ID_XK,ID_NV,NgayXK) VALUES (110,102,'10-06-2023');

--Them data cho CTXK
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (100,100,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (100,101,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (100,102,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (101,101,7);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (101,103,10);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (101,104,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (101,105,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (101,106,10);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (102,109,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (102,110,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (102,112,10);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (102,113,8);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (102,114,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (103,114,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (103,104,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (104,101,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (104,112,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (105,113,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (105,102,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (106,103,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (106,114,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (107,105,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (107,106,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (108,115,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (108,110,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (109,110,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (109,112,5);
INSERT INTO CTXK(ID_XK,ID_NL,SoLuong) VALUES (110,113,5);
