CREATE TABLE Persons
(PersonID  INT NOT NULL,
 FirstName VARCHAR(50) ,
 Salary    DECIMAL(15, 2) ,
 PassportID INT UNIQUE 
);
CREATE TABLE Passports
(PassportID     INT PRIMARY KEY ,
 PassportNumber VARCHAR(20)
);

INSERT INTO Persons
VALUES
(1,'Roberto', 43300.00, 102),
(2,'Tom', 56100.00, 103),
(3,'Yana', 60200.00, 101
);
INSERT INTO Passports
VALUES
(101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2');

ALTER TABLE Persons
ADD CONSTRAINT PK_PersonID
PRIMARY KEY (PersonID)

ALTER TABLE Persons
ADD CONSTRAINT FK_Persons_Passports
FOREIGN KEY (PassportID) REFERENCES Passports(PassportID)

