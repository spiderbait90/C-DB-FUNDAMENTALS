SELECT Name,COUNT(mv.MinionId) AS Count FROM Villains as v
JOIN MinionsVillains AS mv ON mv.VillainId=v.Id
GROUP BY Name
HAVING COUNT(mv.MinionId)>3
ORDER BY Count DESC