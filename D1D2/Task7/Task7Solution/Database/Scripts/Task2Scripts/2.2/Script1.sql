SELECT YEAR(ord.OrderDate) AS [Year], COUNT(*) AS [Total]
FROM Orders AS ord
GROUP BY YEAR(ord.OrderDate)



SELECT COUNT(*) AS Total
FROM Orders