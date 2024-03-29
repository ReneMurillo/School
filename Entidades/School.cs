using System;
using System.Collections.Generic;
using CoreSchool.Util;

namespace CoreSchool.Entities
{
    public class School: SchoolObjectBase, IPlace
    {

        public int FundationYear { get; set;}
        public string Country { get; set; }
        public string City { get; set; }

        public string Address { get; set; }
        public SchoolType SchoolType { get; set; }
        public List<Course> Courses { get; set; }
        public School(string nombre, int anio) => (Name,FundationYear) = (nombre,anio);

        public School(string name, int year, SchoolType type, string country="", string city="")
        {
            (Name,FundationYear) = (name,year);
            this.SchoolType = type;
            this.Country = country;
            this.City = city;
        }

        public override string ToString()
        {
            return $"Name {Name}, Type {SchoolType} \n Country {Country}, City {City}";
        }

        public void CleanPlace()
        {
            Printer.DrawLine();
            Console.WriteLine("Cleaning school");
            foreach(var course in Courses)
            {
                course.CleanPlace();
            }

            Console.WriteLine($"School {Name} now is clean");
        }
    }
}