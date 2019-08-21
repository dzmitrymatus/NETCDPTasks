SELECT cu.CustomerID, cu.Country
FROM Customers as cu
WHERE LOWER(SUBSTRING(cu.Country, 1, 1)) BETWEEN 'b' AND 'g'
ORDER BY cu.Country