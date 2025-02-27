CREATE DATABASE FantastyStoreDB;
GO
USE FantasyStoreDB

CREATE TABLE Inventory (    Id INT IDENTITY(1,1) PRIMARY KEY,    ItemName NVARCHAR(100) NOT NULL,    StockQuantity INT NOT NULL,    RestockThreshold INT NOT NULL);INSERT INTO Inventory (ItemName, StockQuantity, RestockThreshold) VALUES('Mana Potion', 50, 20),('Elven Bread', 30, 15),('Phoenix Feather', 10, 5);CREATE TABLE Orders (    Id INT IDENTITY(1,1) PRIMARY KEY,    ItemId INT FOREIGN KEY REFERENCES Inventory(Id),    Quantity INT NOT NULL,    OrderDate DATETIME DEFAULT GETDATE());INSERT INTO Orders (ItemId, Quantity, OrderDate)VALUES    (1, 3, GETDATE()),    (2, 1, GETDATE()),    (3, 2, GETDATE());