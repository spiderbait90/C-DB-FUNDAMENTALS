CREATE TABLE Users(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Username VARCHAR(30) NOT NULL,
[Password] VARCHAR(26) NOT NULL,
ProfilePicture VARBINARY(900),
LastLoginTime TIME,
IsDeleted BIT
)

INSERT INTO Users(Username,[Password],ProfilePicture,LastLoginTime,IsDeleted)
VALUES ('Gosho','asdasd',NULL,'12:34:54',1),
('Gossho','asdasd',NULL,'12:34:54',1),
('Gosssho','asdasd',NULL,'12:34:54',0),
('Gossssho','asdasd',NULL,'12:34:54',1),
('Gosssssho','asdasd',NULL,'12:34:54',1)

