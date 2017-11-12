create table Vehicle (
	Id int not null identity(1,1) primary key,
	AccountId int not null,
	LicencePlateId int not null,
	Name varchar(128),

	index IX_Vehicle_AccountId (AccountId),
	index IX_Vehicle_LicencePlateId (LicencePlateId)
)