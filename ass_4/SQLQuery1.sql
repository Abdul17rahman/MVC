CREATE TABLE Vehicles (
    vid INT PRIMARY KEY IDENTITY(1,1),
	price float not null,
	stock int not null,
	yom int not null,
    vname nvarchar(100) not null,
    category nvarchar(50) not null,
	brand nvarchar(50) not null,
	condition nvarchar(20) not null,
	supplier nvarchar(100) not null,
	CONSTRAINT chk_Price CHECK (price > 0),
	CONSTRAINT chk_Stock CHECK (stock > 0),
	CONSTRAINT chk_YearManufactured CHECK (yom <= YEAR(GETDATE())),
	CONSTRAINT chk_Condition CHECK (condition in ('New', 'Used'))
);