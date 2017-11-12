create table Comment (
	Id int not null identity(1,1) primary key,
	VehicleId int not null,
	Content varchar (1024),

	index IX_Comment_VehicleId (VehicleId)
)