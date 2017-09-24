CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
CategoryName VARCHAR(50) NOT NULL,
DailyRate INT,
WeeklyRate INT,
MonthlyRate INT,
WeekendRate INT
)

CREATE TABLE Cars(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
PlateNumber VARCHAR(50) UNIQUE NOT NULL,
Manufacturer VARCHAR(50) NOT NULL,
Model VARCHAR(50) NOT NULL,
CarYear INT NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
Doors INT NOT NULL,
Picture IMAGE,
Condition VARCHAR(50),
Available BIT NOT NULL
)

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
FirstName VARCHAR(20) NOT NULL,
LastName VARCHAR(20) NOT NULL,
Title NVARCHAR (50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Customers(
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DriverLicenceNumber INT UNIQUE NOT NULL,
FullName VARCHAR(50) NOT NULL,
[Address] NVARCHAR(50),
City VARCHAR (30),
ZipCode NVARCHAR(255),
Notes NVARCHAR(100)
)

CREATE TABLE RentalOrders (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
TankLevel INT,
KilometrageStart INT NOT NULL,
KilometrageEnd INT NOT NULL,
TotalKilometrage INT NOT NULL,
StartDate DATE,
EndDate DATE,
TotalDays INT,
RateApplied NVARCHAR(50),
TaxRate INT,
OrderStatus BIT NOT NULL,
Notes NVARCHAR(50)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES ('Balimuu',NULL,NULL,NULL,NULL),
('Balimuu',NULL,NULL,NULL,NULL),
('Balimuu',NULL,NULL,NULL,NULL)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES ('AHG23K','KoSss','Corsa',1999,1,4,NULL,NULL,1),
('AHGS23K','KoSss','Corsa',1999,1,4,NULL,NULL,2),
('AAHG23K','KoSss','Corsa',1999,1,4,NULL,NULL,3)

INSERT INTO Employees (FirstName,LastName,Title,Notes)
VALUES ('Gosho','Ivanov','Welcome',NULL),
('Gosho','Ivanov','Welcome',NULL),
('Gosho','Ivanov','Welcome',NULL)

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZipCode, Notes)
VALUES (4879,'Ivan',NULL,NULL,NULL,NULL),
(42879,'Ivan',NULL,NULL,NULL,NULL),
(1879,'Ivan',NULL,NULL,NULL,NULL)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES (1,1,1,NULL,100,200,20000,NULL,NULL,NULL,NULL,NULL,1,NULL),
(2,2,2,NULL,100,200,20000,NULL,NULL,NULL,NULL,NULL,1,NULL),
(3,3,3,NULL,100,200,20000,NULL,NULL,NULL,NULL,NULL,1,NULL)