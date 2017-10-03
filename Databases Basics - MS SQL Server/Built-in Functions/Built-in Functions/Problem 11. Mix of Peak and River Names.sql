SELECT PeakName,RiverName,
 LOWER(PeakName+SUBSTRING(RiverName,2,200)) AS [MIX]  
  FROM Peaks,Rivers
 WHERE UPPER(RIGHT(PeakName,1)) = LEFT(RiverName,1)
 ORDER BY MIX
