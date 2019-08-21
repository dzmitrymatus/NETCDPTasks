SELECT Orders.OrderID,
CASE 
WHEN Orders.ShippedDate IS NULL THEN 'Not Shipped'
END AS ShippedDate
FROM Orders
WHERE Orders.ShippedDate IS NULL