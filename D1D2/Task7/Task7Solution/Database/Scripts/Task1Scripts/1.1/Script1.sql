SELECT Orders.OrderID, Orders.ShippedDate, Orders.ShipVia  
FROM Orders
WHERE Orders.ShippedDate >= '1998-05-06' 
AND Orders.ShipVia >= 2
