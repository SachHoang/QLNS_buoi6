Use QLNS
go
Create table PhongBan(
	MaPB char(6) PRIMARY KEY,
	TenPB nvarchar(30) not null ,
);
create table NhanVien (
	MaNV char(6) primary key,
	TenNV nvarchar(30) not null,
	NgaySinh DateTime,
	MaPB char(6),
	FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB)
);

--Nhập dữu liệu 
INSERT INTO PhongBan (MaPB, TenPB)  
VALUES
  ('PB1', N'Phòng Nhân sự'),
  ('PB2', N'Phòng Kế toán');
  
INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, MaPB)
VALUES
  ('NV01', N'Nguyễn Văn A', '1990-03-25', 'PB1'),
  ('NV02', N'Trần Thị B', '1995-08-12', 'PB2'),
  ('NV03', 'Lê Văn C', '1988-02-06', 'PB1');
   
