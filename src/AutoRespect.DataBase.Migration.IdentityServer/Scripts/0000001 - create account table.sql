drop table if exists [Account]

create table [Account] ( 
	Id int not null identity (1,1) primary key, 
	Login varchar (64) not null,
	Password VARCHAR (32) not null
) 