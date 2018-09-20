// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using SampleSupport;
using Task.Data;

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Task 001")]
        [Description("This sample returns all customers with sum of orders greater than given value (20000, 50000)")]
        public void Linq001()
        {
            decimal filterValue = 20000;

            //var customersOrderSum = dataSource.Customers.Select(customer => new { Name = customer.CompanyName, Orders = customer.Orders.Sum(order => order.Total) });
            var customersOrderSum =
                from customer in dataSource.Customers
                select new { Name = customer.CompanyName, Orders = customer.Orders.Sum(order => order.Total) };

            Console.WriteLine($"All customers with orders:");
            foreach (var customerOrderSum in customersOrderSum)
            {
                Console.WriteLine($"{customerOrderSum.Name} - {customerOrderSum.Orders}");
            }

            // var customers = dataSource.Customers.Where(customer => customer.Orders.Sum(order => order.Total) > filterValue);
            var customers =
                from customer in dataSource.Customers
                where customer.Orders.Sum(order => order.Total) > filterValue
                select customer;

            Console.WriteLine($"{Environment.NewLine}Filtered customers with orders (where sum of orders is greater then {filterValue}):");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CompanyName} - {customer.Orders.Sum(order => order.Total)}");
            }

            filterValue = 50000;

            Console.WriteLine($"{Environment.NewLine}Filtered customers with orders (where sum of orders is greater then {filterValue}):");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CompanyName} - {customer.Orders.Sum(order => order.Total)}");
            }
        }

        [Category("Restriction and Join Operators")]
        [Title("Task 002")]
        [Description("This sample returns customers with suppliers in same country and city")]
        public void Linq002()
        {
            #region Simple
            var resultSimple = from customer in dataSource.Customers
                               select new
                               {
                                   Customer = customer,
                                   Suppliers =
                                         from supplier in dataSource.Suppliers
                                         where supplier.Country == customer.Country && supplier.City == customer.City
                                         select supplier

                               };

            Console.WriteLine("Result for simple query:");
            foreach (var item in resultSimple)
            {
                Console.WriteLine($"Customer: {item.Customer.CompanyName}; Country: {item.Customer.Country}; City: {item.Customer.City}.");
                foreach (var supplier in item.Suppliers)
                {
                    Console.WriteLine($"Supplier: {supplier.SupplierName}; Country: {supplier.Country}; City: {supplier.City}.");
                }
            }
            #endregion

            #region Join
            var resultWithJoin = from customer in dataSource.Customers
                                 join supplier in dataSource.Suppliers on new { customer.Country, customer.City } equals new { supplier.Country, supplier.City } into suppliers
                                 select new
                                 {
                                     Customer = customer,
                                     Suppliers = suppliers
                                 };


            Console.WriteLine("Result for query with join:");
            foreach (var item in resultWithJoin)
            {
                Console.WriteLine($"Customer: {item.Customer.CompanyName}; Country: {item.Customer.Country}; City: {item.Customer.City}.");
                foreach (var supplier in item.Suppliers)
                {
                    Console.WriteLine($"Supplier: {supplier.SupplierName}; Country: {supplier.Country}; City: {supplier.City}.");
                }
            }
            #endregion
        }

        [Category("Quantifiers")]
        [Title("Task 003")]
        [Description("This sample returns customers with order that greater than given value")]
        public void Linq003()
        {
            decimal filterValue = 10000;

            var result = from customer in dataSource.Customers
                         where customer.Orders.Any(x => x.Total > filterValue)
                         select customer;

            foreach (var item in result)
            {
                Console.WriteLine($"Customer: {item.CompanyName}.");
            }
        }

        [Category("Aggregate Operators")]
        [Title("Task 004")]
        [Description("This sample returns customers with first order date")]
        public void Linq004()
        {
            var result = from customer in dataSource.Customers
                         select new
                         {
                             Customer = customer,
                             StartDate = customer.Orders.Count() > 0 ? customer.Orders.Min(x => x.OrderDate) : DateTime.MinValue
                         };

            foreach (var item in result)
            {
                Console.WriteLine($"Customer: {item.Customer.CompanyName}; Start date: {item.StartDate}.");
            }
        }

        [Category("Ordering Operators")]
        [Title("Task 005")]
        [Description("This sample returns ordered customers")]
        public void Linq005()
        {
            var custList = from customer in dataSource.Customers
                           select new
                           {
                               Customer = customer,
                               StartDate = customer.Orders.Count() > 0 ? customer.Orders.Min(x => x.OrderDate) : DateTime.MinValue
                           };

            var result = from temp in custList
                         orderby temp.StartDate.Year, temp.StartDate.Month, temp.Customer.Orders.Sum(x => x.Total) descending, temp.Customer.CompanyName
                         select temp;

            foreach (var item in result)
            {
                Console.WriteLine($"Customer: {item.Customer.CompanyName}; Start date: {item.StartDate}.");
            }
        }

        [Category("Quantifiers and Restriction Operators")]
        [Title("Task 006")]
        [Description("This sample returns customers with non digit postal code or empty region or phone number without code")]
        public void Linq006()
        {
            var result = from customer in dataSource.Customers
                         where customer.PostalCode == null || customer.PostalCode.All(x => char.IsDigit(x)) == false
                         || string.IsNullOrEmpty(customer.Region)
                         || customer.Phone.StartsWith("(") == false
                         select customer;

            ObjectDumper.Write(result);
        }

        [Category("Grouping Operators")]
        [Title("Task 007")]
        [Description("This sample returns products grouped by category and grouped by units in stock")]
        public void Linq007()
        {
            var result = from product in dataSource.Products
                         group product by product.Category into groupedProducts
                         select new
                         {
                             Group = groupedProducts.Key,
                             Products =
                                from prod in groupedProducts
                                group prod by prod.UnitsInStock into groupedProd
                                select new
                                {
                                    Group = groupedProd.Key,
                                    Products =
                                        from product in groupedProd
                                        orderby product.UnitPrice
                                        select product
                                }
                         };

            foreach (var category in result)
            {
                Console.WriteLine($"Category: {category.Group}");
                foreach (var subCategory in category.Products)
                {
                    Console.WriteLine($"Products count: {subCategory.Group}");

                    foreach (var product in subCategory.Products)
                    {
                        ObjectDumper.Write(product);
                    }
                }
            }
        }

        [Category("Grouping Operators")]
        [Title("Task 008")]
        [Description("This sample returns products grouped by UnitPrice")]
        public void Linq008()
        {
            decimal lowBorder = 10;
            decimal mediumBorder = 100;


            var result = from product in dataSource.Products
                         group product by product.UnitPrice < lowBorder ? "low"
                            : product.UnitPrice >= lowBorder && product.UnitPrice <= mediumBorder ? "medium"
                            : "high";

            foreach (var category in result)
            {
                Console.WriteLine($"Category: {category.Key}");
                ObjectDumper.Write(category);
            }
        }

        [Category("Aggregate Operators")]
        [Title("Task 009")]
        [Description("This sample returns average profitability of each city")]
        public void Linq009()
        {
            var result = from customer in dataSource.Customers
                         group customer by customer.City into groupedCustomers
                         select new
                         {
                             City = groupedCustomers.Key,
                             AverageByPrice = groupedCustomers.Average(x => x.Orders.Sum(y => y.Total)),
                             AverageByCount = groupedCustomers.Average(x => x.Orders.Count())
                         };

            ObjectDumper.Write(result);
        }

        [Category("Grouping and Aggregate Operators")]
        [Title("Task 010")]
        [Description("This sample returns customers activity statistics by monts, years, months and years")]
        public void Linq010()
        {
            var resultByMonth = from customer in dataSource.Customers
                                from order in customer.Orders
                                group order by order.OrderDate.Month into groupedOrder
                                select new
                                {
                                    Month = groupedOrder.Key,
                                    Count = groupedOrder.Count()
                                };

            var resultByYear = from customer in dataSource.Customers
                               from order in customer.Orders
                               group order by order.OrderDate.Year into groupedOrder
                               select new
                               {
                                   Year = groupedOrder.Key,
                                   Count = groupedOrder.Count()
                               };

            var resultByMonthAndYear = from customer in dataSource.Customers
                                       from order in customer.Orders
                                       group order by $"{order.OrderDate.Month}/{order.OrderDate.Year}" into groupedOrder
                                       select new
                                       {
                                           Year = groupedOrder.Key,
                                           Count = groupedOrder.Count()
                                       };

            Console.WriteLine($"By Month:");
            ObjectDumper.Write(resultByMonth);
            Console.WriteLine($"By Year:");
            ObjectDumper.Write(resultByYear);
            Console.WriteLine($"By Month and Year:");
            ObjectDumper.Write(resultByMonthAndYear);
        }
    }
}
