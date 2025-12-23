create database staffs_db;
use staffs_db;

create table staffs(
id int identity(1,1) primary key,
name nvarchar(100),
role nvarchar(50)
);