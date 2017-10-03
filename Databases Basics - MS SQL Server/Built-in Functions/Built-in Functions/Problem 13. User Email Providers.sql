SELECT Username,SUBSTRING(Email,CHARINDEX('@',Email,1)+1,30)
	AS [Email Provider]
  FROM Users
 ORDER BY [Email Provider],Username