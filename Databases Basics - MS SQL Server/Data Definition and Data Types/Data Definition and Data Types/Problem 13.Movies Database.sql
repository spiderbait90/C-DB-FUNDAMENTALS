CREATE TABLE Directors (
Id INT PRIMARY KEY NOT NULL,
DirectorName VARCHAR(20) NOT NULL,
Notes VARCHAR(max)
)
CREATE TABLE Genres (
Id INT PRIMARY KEY NOT NULL,
GenreName VARCHAR(20) NOT NULL,
Notes VARCHAR(max)
)
CREATE TABLE Categories (
Id INT PRIMARY KEY NOT NULL,
CategoryName VARCHAR(20) NOT NULL,
Notes VARCHAR(max)
)
CREATE TABLE Movies(
Id INT PRIMARY KEY NOT NULL,
Title VARCHAR(50) NOT NULL,
CopyrightYear INT NOT NULL,
Lenght TIME NOT NULL,
GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
Rating INT DEFAULT 0,
Notes VARCHAR(MAX)
)

INSERT INTO Directors (Id,DirectorName,Notes) VALUES
(1,'Ivan','some other time'),
(2,'Kiro',NULL),
(3,'Asen','some other time'),
(4,'Blagoi',NULL),
(5,'Valio','some other time')

INSERT INTO Genres (Id,GenreName,Notes) VALUES
(1,'Ivan','some other time'),
(2,'Kiro',NULL),
(3,'Asen','some other time'),
(4,'Blagoi',NULL),
(5,'Valio','some other time')

INSERT INTO Categories (Id,CategoryName,Notes) VALUES
(1,'Ivan','some other time'),
(2,'Kiro',NULL),
(3,'Asen','some other time'),
(4,'Blagoi',NULL),
(5,'Valio','some other time')

INSERT INTO Movies(Id,Title, DirectorId, CopyrightYear, Lenght, GenreId, CategoryId, Rating, Notes) VALUES
(1,'AS',1,1999,'02:32:22',1,1,99,NULL),
(2,'AS',2,1999,'02:32:22',2,2,99,NULL),
(3,'AS',3,1999,'02:32:22',3,3,99,NULL),
(4,'AS',4,1999,'02:32:22',4,4,99,NULL),
(5,'AS',5,1999,'02:32:22',4,4,99,NULL)

SELECT * FROM Movies