create database BookStoreApp

use BookStoreApp
select*from Users
delete from Users where UserId=3

create table Users(
UserId int primary key identity(1,1),
FullName varchar(100),   
Email varchar(100),  
Password varchar(100),  
Mobile varchar (50)  
)

create procedure spAddUser(  
@FullName varchar(100),   
@Email varchar(100),  
@Password varchar(100),  
@Mobile varchar (50)  
)  
As  
Begin
insert into Users(FullName,Email,Password,Mobile) values(@FullName,@Email,@Password,@Mobile)  
end


create procedure spLoginUser(   
@Email varchar(100)  
)  
As  
Begin
select * from Users where Email=@Email  
end


Create procedure spForgetPasswordUser(  
@Email varchar(100)  
)  
As  
Begin
select * from Users where Email=@Email   
end

Create procedure spResetPassword(  
@Email varchar(100),  
@Password varchar(100)  
)  
As  
Begin
update Users set Password=@Password where Email=@Email   
end