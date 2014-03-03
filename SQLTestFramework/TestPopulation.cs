﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Class used to populate database with sample data.
    /// </summary>
    public static class TestPopulation
    {
        public static void Clear()
        {
            Db.Transaction(delegate
            {
                var persons = Db.SQL<Person>("SELECT p FROM PERSON p");
                foreach (Person p in persons)
                {
                    p.Delete();
                }

                foreach (Location l in Db.SQL<Location>("SELECT l FROM Location l"))
                {
                    l.Delete();
                }

                foreach (Company c in Db.SQL<Company>("SELECT c FROM Company c"))
                {
                    c.Delete();
                }
            });
        }

        public static void Populate()
        {
            Db.Transaction(delegate
            {
                new Person() { Name = "Albert" };
                new Person() { Name = "Einstein" };
                new Location() { Country = "Sweden" };
                new Location() { Country = "Norway" };
                new Company() { CompanyName = "Starcounter" };
                new Company() { CompanyName = "Siba" };
            });
        }
    }

    [Database]
    public class Person
    {
        public String Name;
    }

    [Database]
    public class Location
    {
        public String Country;
    }

    [Database]
    public class Company
    {
        public String CompanyName;
    }
}
