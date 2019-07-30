using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.Entities;

namespace CoreSchool
{
    public sealed class SchoolEngine
    {
        public School School { get; set; }

        public SchoolEngine()
        {
            
        }

        public void Initialize()
        {
            School = new School("Platzi Academy", 2012, SchoolType.Primary,
                                city: "Bogota");
            
            UploadCourses();
            UploadSubjects();
            
            UploadEvaluations();
            
        }

        private void UploadEvaluations()
        {
            foreach (var course in School.Courses)
            {
                foreach (var subject in course.Subjects)
                {
                    foreach (var student in course.Students)
                    {
                        var rnd = new Random(System.Environment.TickCount);

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluations
                            {
                                Subject = subject,
                                Name = $"{subject.Name} Ev# {i + 1}",
                                Score = (float)(5 * rnd.NextDouble()),
                                Student = student
                            };
                            student.Evaluations.Add(ev);
                        }
                    }
                }
            }
        }

        private void UploadSubjects()
        {
            foreach (var course in School.Courses)
            {
                List<Subject> subjectList = new List<Subject>(){
                    new Subject { Name = "Mathematics"},
                    new Subject { Name = "Sports"},
                    new Subject { Name = "Spanish"},
                    new Subject { Name = "Science"}
                };
                course.Subjects = subjectList;
            }
        }

        private List<Student> GenerateStudentsRandom(int quantity)
        {
            string[] name1 = {"Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolas"};
            string[] name2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes"};
            string[] surname = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera"};

            var studentsList = from n1 in name1 
                                from n2 in name2
                                from s1 in surname
                                select new Student {Name = $"{n1} {n2} {s1}" };

            return studentsList.OrderBy( al => al.UniqueId).Take(quantity).ToList();
        }

        public void UploadCourses()
        {
            School.Courses = new List<Course>() 
            {
                new Course() { Name = "101", Journey = JourneyType.Morning },
                new Course() { Name = "201", Journey = JourneyType.Morning },
                new Course() { Name = "301", Journey = JourneyType.Morning },
                new Course() { Name = "401", Journey = JourneyType.Afternoon },
                new Course() { Name = "501", Journey = JourneyType.Afternoon }
            };

            Random rnd = new Random();
            
            foreach (var course in School.Courses)
            {
                int randomQuantity = rnd.Next(5,20);
                course.Students = GenerateStudentsRandom(randomQuantity);
            }
        }

        public List<SchoolObjectBase> GetSchoolObjects ()
        {
            var objList = new List<SchoolObjectBase>();

            objList.Add(School);
            objList.AddRange(School.Courses);

            foreach(var course in School.Courses)
            {
                objList.AddRange(course.Subjects);
                objList.AddRange(course.Students);

                foreach(var student in course.Students)
                {
                    objList.AddRange(student.Evaluations);
                }
            }
            

            return objList;
        }
    }
}