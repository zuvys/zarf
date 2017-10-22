﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;
using Zarf.Mapping.Bindings;
using Zarf.Query.Expressions;

namespace Zarf
{

    public class User
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public DateTime BDay { get; set; }

        public IEnumerable<Address> Address { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public override string ToString()
        {
            return "Id:" + Id + "\tAge=" + Age + "\tName=" + Name + "\t AddressId=" + AddressId + "\t BDay=" + BDay.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public int UserId { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }

    public class Order
    {
        public int? AddressID { get; set; }

        public string OrderName { get; set; }
    }

    public class Abc
    {
        public int Id { get; set; }

        public int Count;
    }


    public class C
    {
        public User User { get; set; }

        public int Id { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DataContext();

            //var y = db.DataQuery<User>()
            //    .Include(item => item.Address, (user, address) => user.Id == address.UserId && user.Id != 1)
            //    .ToList();
            //.ThenInclude(item => item.Orders, (address, order) => order.AddressID == address.Id)
            //BasicTest(db);

            var x = db.DataQuery<User>().Select(item => new C { User = item, Id = 2 })
                .ToList();

            //Console.WriteLine(typeof(User[]).GetTypeInfo().IsGenericType);
            var t = new FromTableExpression(typeof(User));
            var id = typeof(User).GetProperty("Id");

            var c = new ColumnExpression(t, id);
            var d = new ColumnExpression(t, id);

            Console.WriteLine(c.GetHashCode() == d.GetHashCode());
            Console.WriteLine(c == d);
            Console.ReadKey();
        }

        static void BasicTest(DataContext db)
        {
            Console.WriteLine("All..........................");
            db.DataQuery<User>().ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("First..........................");
            Console.WriteLine(db.DataQuery<User>().First());

            Console.WriteLine();
            Console.WriteLine("First id=2.......................");
            Console.WriteLine(db.DataQuery<User>().First(item => item.Id == 2));

            Console.WriteLine();
            Console.WriteLine("Skip 2..........................");
            db.DataQuery<User>().Skip(2).ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("Take 2..........................");
            db.DataQuery<User>().Take(2).ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("Count..........................");
            Console.WriteLine(db.DataQuery<User>().Count());

            Console.WriteLine();
            Console.WriteLine("Sum..........................");
            Console.WriteLine(db.DataQuery<User>().Sum(item => item.Id));

            Console.WriteLine();
            Console.WriteLine("Sum..........................");
            Console.WriteLine(db.DataQuery<User>().Sum(item => item.Id));

            Console.WriteLine();
            Console.WriteLine("Where Id>1..........................");
            db.DataQuery<User>().Where(item => item.Id > 1).ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("Inner Join..........................");
            db.DataQuery<User>().Join(
                db.DataQuery<Address>(),
                item => item.AddressId,
                item => item.Id,
                (user, address) => new { user.Name, address.Street })
                .ToList().ForEach(item => Console.WriteLine($"Name:{item.Name} Street:{item.Street}"));

            Console.WriteLine();
            Console.WriteLine("LEFT Join..........................");
            db.DataQuery<User>().Join(
                db.DataQuery<Address>().DefaultIfEmpty(),
                item => item.AddressId,
                item => item.Id,
                (user, address) => new { user.Name, address.Street })
                .ToList().ForEach(item => Console.WriteLine($"Name:{item.Name} Street:{item.Street}"));

            Console.WriteLine();
            Console.WriteLine("Order By DESC..........................");
            db.DataQuery<User>().OrderByDescending(item => item.Id)
                .ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("RIGHT Join..........................");
            db.DataQuery<User>().DefaultIfEmpty().Join(
                db.DataQuery<Address>(),
                item => item.AddressId,
                item => item.Id,
                (user, address) => new { user.Name, address.Street })
                .ToList().ForEach(item => Console.WriteLine($"Name:{item.Name} Street:{item.Street}"));

            Console.WriteLine();
            Console.WriteLine("Full Join..........................");
            db.DataQuery<User>().DefaultIfEmpty().Join(
                db.DataQuery<Address>().DefaultIfEmpty(),
                item => item.AddressId,
                item => item.Id,
                (user, address) => new { user.Name, address.Street })
                .ToList().ForEach(item => Console.WriteLine($"Name:{item.Name} Street:{item.Street}"));

            Console.WriteLine();
            Console.WriteLine("CONCAT..........................");
            db.DataQuery<User>().Concat(db.DataQuery<User>()).ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("UNION..........................");
            db.DataQuery<User>().Union(db.DataQuery<User>()).ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine();
            Console.WriteLine("All Id>0..........................");
            Console.WriteLine(db.DataQuery<User>().All(item => item.Id > 0));

            Console.WriteLine();
            Console.WriteLine("All Id>10000..........................");
            Console.WriteLine(db.DataQuery<User>().All(item => item.Id > 10000));

            Console.WriteLine();
            Console.WriteLine("All Id MAX..........................");
            Console.WriteLine(db.DataQuery<User>().Max(item => item.Id));

            Console.WriteLine();
            Console.WriteLine("Any Id>0..........................");
            Console.WriteLine(db.DataQuery<User>().Any(item => item.Id > 0));
        }
    }

}