CREATE TABLE People(
Id INT PRIMARY KEY IDENTITY NOT NULL,
[Name] NVARCHAR(200) NOT NULL,
Picture VARBINARY,
Height DECIMAL(15,2),
[Weight] DECIMAL(15,2),
Gender VARCHAR(1) NOT NULL CHECK (Gender IN('m','f')),
Birthdate DATE NOT NULL,
Biography NVARCHAR(MAX)
)

INSERT INTO People
([Name],Picture,Height,[Weight],Gender,Birthdate,Biography)
VALUES
('Ginka',NULL,1.75,56,'f','1990-06-01',NULL),
('Ivan',NULL,1.75,56,'m','1990-06-01',NULL),
('MAriq',NULL,1.75,56,'f','1990-06-01',NULL),
('Gosho',NULL,1.75,56,'m','1990-06-01',NULL),
('Lenka',NULL,1.75,56,'f','1990-06-01',NULL)


