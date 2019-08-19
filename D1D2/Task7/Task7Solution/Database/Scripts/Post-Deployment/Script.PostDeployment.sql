/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DELETE FROM [dbo].[Order Details];
DELETE FROM [dbo].[Orders];
DELETE FROM [dbo].[EmployeeTerritories];
DELETE FROM [dbo].[Territories];
DELETE FROM [dbo].[Products];
DELETE FROM [dbo].[Suppliers];
DELETE FROM [dbo].[Shippers];
DELETE FROM [dbo].[Region];
DELETE FROM [dbo].[Employees];
DELETE FROM [dbo].[Customers];
DELETE FROM [dbo].[Categories];
:r .\Categories.sql
:r .\Customers.sql
:r .\Employees.sql
:r .\Region.sql
:r .\Shippers.sql
:r .\Suppliers.sql
:r .\Products.sql
:r .\Territories.sql
:r .\EmployeeTerritories.sql
:r .\Orders.sql
:r .\OrderDetails.sql
