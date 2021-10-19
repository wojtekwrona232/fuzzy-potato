using Employees.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Database
{
    internal static class ReadCsvDataFiles
    {
        public static ICollection<Employee> CreateEmployees()
        {
            var employees = new List<Employee>();
            var addresses = new List<Address>();

            using (var parser = new TextFieldParser(@"Employees.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                while (!parser.EndOfData)
                {
                    var emp = parser.ReadFields();
                    employees.Add(new Employee
                    {
                        FirstName = emp[0],
                        LastName = emp[1],
                        Email = emp[2],
                        PhoneNumber = emp[3],
                        Gender = emp[4],
                        DateOfBirth = DateTime.Parse(emp[5]),
                        DateOfHire = DateTime.Parse(emp[6]),
                        DateOfDismission = emp[7] == "" ? default : DateTime.Parse(emp[7]),
                        Salary = Double.Parse(emp[8]),
                        Position = emp[9]
                    });
                }
            }
            using (var parser = new TextFieldParser(@"address.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    addresses.Add(new Address
                    {
                        Street = fields[0],
                        ZipCode = fields[1],
                        City = fields[2],
                        Region = fields[3],
                        Country = fields[4]
                    });
                }
            }

            for (var i = 0; i < employees.Count; i++)
            {
                employees[i].Address = addresses[i];
            }

            return employees;
        }
    }
}
