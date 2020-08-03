IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'dbPizzeria1')
CREATE DATABASE dbPizzeria1;
GO
USE dbPizzeria1
--dropping tables
IF OBJECT_ID('vwOrders') IS NOT NULL
DROP VIEW vwOrders;

IF OBJECT_ID('tblOrders') IS NOT NULL 
DROP TABLE tblOrders;

IF OBJECT_ID('tblDishes') IS NOT NULL 
DROP TABLE tblDishes;


CREATE TABLE tblDishes(
	dishId INT PRIMARY KEY IDENTITY(1,1),
	dishName VARCHAR(30) NOT NULL,
	price INT NOT NULL
	);
CREATE TABLE tblOrders(
	orderId INT PRIMARY KEY IDENTITY(1,1),
	username CHAR(13) NOT NULL,
	dishId INT FOREIGN KEY REFERENCES  tblDishes(dishId) ON DELETE SET NULL,
	count INT NOT NULL,
	dateAndTime DATETIME not null,
	status VARCHAR(20) not null
);
GO
CREATE VIEW vwOrders
as
select o.orderId, o.username, o.dishId, o.count, o.dateAndTIme, o.count * d.price AS totalPrice
from tblOrders o
inner join tblDishes d
on o.dishId = d.dishId;

GO
INSERT INTO tblDishes(dishName, price)
VALUES ('Capricciosa', 700), ('Hawai',750),('Spinaci',790),('Vegetariana',580),('Napolitana',730),('Siciliana',770),('Vesuivo',750);