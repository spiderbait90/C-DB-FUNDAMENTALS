SELECT MountainRange,PeakName,Elevation
FROM Peaks AS p
JOIN Mountains AS M ON M.Id=p.MountainId
WHERE MountainRange='Rila'
ORDER BY Elevation DESC
