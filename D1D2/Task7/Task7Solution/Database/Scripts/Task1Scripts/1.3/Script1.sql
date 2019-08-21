SELECT DISTINCT od.OrderID
FROM [Order Details] AS od
WHERE od.Quantity BETWEEN 3 AND 10