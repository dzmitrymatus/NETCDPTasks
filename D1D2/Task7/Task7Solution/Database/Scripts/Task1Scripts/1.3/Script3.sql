SELECT cu.CustomerID, cu.Country
FROM Customers as cu
WHERE LOWER(SUBSTRING(cu.Country, 1, 1)) IN ('b','c', 'd', 'e', 'f', 'g')
ORDER BY cu.Country

SELECT cu.CustomerID, cu.Country
FROM Customers as cu
WHERE cu.Country LIKE '[b-g]%'
ORDER BY cu.Country