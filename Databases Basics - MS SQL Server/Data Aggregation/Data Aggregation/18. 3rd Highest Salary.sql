SELECT DepartmentID,ThirdHighestSalary FROM
(SELECT DepartmentID,Salary AS ThirdHighestSalary,
DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS Line	    
FROM Employees
GROUP BY DepartmentID,Salary) AS NEW
WHERE Line = 3


