SELECT SUM((od.UnitPrice - (od.UnitPrice*od.Discount))*od.Quantity) AS Totals
FROM [Order Details] AS od