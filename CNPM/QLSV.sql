Create database QLSV
go
use QLSV
go

create table Sinhvien(
	Id int identity(1,1) primary key,
	Hoten nvarchar(max),
	Khoa nvarchar(max),
	Lop nvarchar(max),
	Giotinh nvarchar(10),
	Ngaysinh Date,
	Sodienthoai nvarchar(20),
	Thongtinkhac nvarchar(max)
)

insert into Sinhvien(Hoten, Khoa, Lop, Giotinh, Ngaysinh, Sodienthoai, Thongtinkhac) values (N'Nguyễn Văn A', N'CNTT', N'112', N'Nam', '1/1/1998', N'05888888', N'Không')