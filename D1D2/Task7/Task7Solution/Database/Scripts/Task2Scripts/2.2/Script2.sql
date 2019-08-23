SELECT ord.EmployeeID AS SellerId, 
		(SELECT CONCAT(em.FirstName,' ', em.LastName) FROM Employees AS em WHERE em.EmployeeID = ord.EmployeeID ) AS SellerName,
		COUNT(*) AS Amount
FROM Orders as ord
GROUP BY ord.EmployeeID
ORDER BY COUNT(*) desc