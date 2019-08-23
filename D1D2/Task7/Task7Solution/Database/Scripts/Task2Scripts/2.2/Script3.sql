SELECT ord.EmployeeID, ord.CustomerID, COUNT(*) AS Amount, YEAR(ord.OrderDate) AS [Year]
FROM Orders AS ord
WHERE YEAR(ord.OrderDate) = 1998
GROUP BY ord.EmployeeID, ord.CustomerID, YEAR(ord.OrderDate)
ORDER BY COUNT(*) desc