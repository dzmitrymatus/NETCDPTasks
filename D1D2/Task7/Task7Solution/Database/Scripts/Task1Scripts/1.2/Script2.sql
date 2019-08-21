SELECT Customers.ContactName, Customers.Country
FROM Customers
WHERE Customers.Country NOT IN('USA', 'Canada')
ORDER BY Customers.ContactName