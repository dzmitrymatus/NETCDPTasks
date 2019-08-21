SELECT Customers.ContactName, Customers.Country
FROM Customers
WHERE Customers.Country IN('USA', 'Canada')
ORDER BY Customers.ContactName, Customers.Address