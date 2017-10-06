CREATE TABLE Students(
StudentID INT PRIMARY KEY,
StudentNumber INT,
StudentName VARCHAR(50),
MajorID INT
)

CREATE TABLE Payments(
PaymentID INT PRIMARY KEY,
PaymentDate DATE,
PaymentAmount DECIMAL(15,2),
StudentID INT
)

CREATE TABLE Majors(
MajorID INT PRIMARY KEY,
[Name] VARCHAR(50)
)

CREATE TABLE Agenda(
StudentID INT,
SubjectID INT,
CONSTRAINT PK_Agenda
PRIMARY KEY (StudentID,SubjectID)
)

CREATE TABLE Subjects(
SubjectID INT PRIMARY KEY,
SubjectName VARCHAR(50)
)

ALTER TABLE Agenda
ADD CONSTRAINT FK_Agenda_Subjects
FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
CONSTRAINT FK_Agenda_Students
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

ALTER TABLE Students
ADD CONSTRAINT FK_Students_Majors
FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)

ALTER TABLE Payments
ADD CONSTRAINT FK_Payments_Students
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
