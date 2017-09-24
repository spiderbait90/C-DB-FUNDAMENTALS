CREATE TABLE Employees (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
FirstName NVARCHAR(20) NOT NULL,
LastName NVARCHAR(20) NOT NULL,
Title NVARCHAR(20) NOT NULL,
Notes NVARCHAR
)

CREATE TABLE Customers (
AccountNumber INT PRIMARY KEY IDENTITY NOT NULL,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR (50) NOT NULL,
PhoneNumber BIGINT NOT NULL,
EmergencyName NVARCHAR(20),
EmergencyNumber INT,
Notes NVARCHAR(MAX)
)

CREATE TABLE RoomStatus (
RoomStatus NVARCHAR(20) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE RoomTypes (
RoomType NVARCHAR(20) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE BedTypes (
BedType NVARCHAR(20) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Rooms (
RoomNumber INT PRIMARY KEY NOT NULL,
RoomType NVARCHAR(20) NOT NULL,
BedType NVARCHAR(20) NOT NULL,
Rate INT,
RoomStatus NVARCHAR(20) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Payments (
Id INT PRIMARY KEY IDENTITY(1,1),
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
PaymentDate DATE,
AccountNumber INT NOT NULL,
FirstDateOccupied DATE,
LastDateOccupied DATE,
TotalDays INT,
AmountCharged DECIMAL(15,2),
TaxRate DECIMAL(15,2),
TaxAmount DECIMAL(15,2),
PaymentTotal DECIMAL(15,2),
Notes TEXT
)

CREATE TABLE Occupancies (
Id INT PRIMARY KEY IDENTITY (1,1),
EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
DateOccupied DATE,
AccountNumber INT,
RoomNumber VARCHAR(20),
RateApplied BIT,
PhoneCharge DECIMAL (15,2),
Notes TEXT
)

INSERT INTO Employees (FirstName,LastName,Title,Notes)
VALUES ('Ivan','Ivanov','Heyooo',NULL),
('Ivan','Ivanov','Heyooo',NULL),
('Ivan','Ivanov','Heyooo',NULL)

INSERT INTO Customers (FirstName,LastName,PhoneNumber,EmergencyName,EmergencyNumber,Notes)
VALUES ('Ivan','Ivanov',0884672758,NULL,NULL,NULL),
('Ivan','Ivanov',0884672758,NULL,NULL,NULL),
('Ivan','Ivanov',0884672758,NULL,NULL,NULL)

INSERT INTO RoomStatus (RoomStatus,Notes)
VALUES ('Free',NULL),
('Free',NULL),
('Free',NULL)

INSERT INTO RoomTypes (RoomType,Notes)
VALUES ('Big',NULL),
('Big',NULL),
('Big',NULL)

INSERT INTO BedTypes (BedType,Notes)
VALUES ('For Two',NULL),
('For Two',NULL),
('For Two',NULL)

INSERT INTO Rooms (RoomNumber,RoomType,BedType,Rate,RoomStatus,Notes)
VALUES (1,'Lux','Big',NULL,'Free',NULL),
(2,'Lux','Big',NULL,'Free',NULL),
(3,'Lux','Big',NULL,'Free',NULL)

INSERT INTO Payments (EmployeeId,PaymentDate,AccountNumber,FirstDateOccupied,LastDateOccupied,TotalDays,AmountCharged,TaxRate,TaxAmount,PaymentTotal,Notes)
VALUES (1,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
(2,NULL,2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
(3,NULL,3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)

INSERT INTO Occupancies (EmployeeId,DateOccupied,AccountNumber,RoomNumber,RateApplied,PhoneCharge,Notes)
VALUES (1,NULL,NULL,NULL,NULL,NULL,NULL),
(1,NULL,NULL,NULL,NULL,NULL,NULL),
(1,NULL,NULL,NULL,NULL,NULL,NULL)