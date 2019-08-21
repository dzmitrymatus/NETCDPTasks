SELECT Orders.OrderID AS [Order Number],
	CASE
		WHEN Orders.ShippedDate IS NULL THEN 'Not Shipped'
		ELSE CAST(Orders.ShippedDate AS nvarchar)
	END AS [Shipped Date]
FROM Orders
WHERE (Orders.ShippedDate > '1998-05-06' OR Orders.ShippedDate IS NULL)