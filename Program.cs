﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.Entities;
using CoreSchool.Util;
using static System.Console;

namespace CoreSchool
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new SchoolEngine();
            engine.Initialize(); 

            PrintSchoolCourses(engine.School);

            //Printer.Beeper(2000,500,3);

            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.DrawLine(20);

            var objectList = engine.GetSchoolObjects(
            out int evaluationsQuantity,
            out int coursesQuantity,
            out int subjectsQuantity,
            out int studentsQuantity
            );

        }

        private static void PrintSchoolCourses(School school)
        {
            Printer.WriteTitle("School's Courses");

            if(school?.Courses != null)
            {
                foreach (var course in school.Courses)
                {
                    WriteLine($"Name {course.Name}, Id {course.UniqueId}");
                }
            }
        }
    }
}
