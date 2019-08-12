using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.Entities;

namespace CoreSchool.App
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

        public void PrintDictionary(Dictionary<DictionaryKey, IEnumerable<SchoolObjectBase>> dictionary,
        bool printEvaluations = false
        )
        {
            foreach (var obj in dictionary)
            {
                foreach (var value in obj.Value)
                {
                    switch (obj.Key)
                    {
                        case DictionaryKey.Evaluation:
                            if(printEvaluations)
                                Console.WriteLine(value);
                        break;
                        case DictionaryKey.School:
                            Console.WriteLine("School: " + value);
                        break;
                        case DictionaryKey.Student:
                            Console.WriteLine("Nombre: " + value.Name);
                        break;
                        case DictionaryKey.Course:
                            var tmpcourse = value as Course;
                            if(tmpcourse != null)
                            {
                                int count = tmpcourse.Students.Count;
                                Console.WriteLine($"Course: {value.Name} Students Quantity: {count}");
                            }
                        break;
                        default:
                            Console.WriteLine(value);
                        break;
                    }   
                }
            }
        }

        public Dictionary<DictionaryKey, IEnumerable<SchoolObjectBase>> GetObjectDictionary()
        {
            var dictionary = new Dictionary<DictionaryKey, IEnumerable<SchoolObjectBase>>();

            dictionary.Add(DictionaryKey.School, new [] {School});
            dictionary.Add(DictionaryKey.Course, School.Courses.Cast<SchoolObjectBase>());

            var tmpSubjects = new List<Subject>();
            var tmpStudents = new List<Student>();
            var tmpEvaluations = new List<Evaluation>();
            foreach (var course in School.Courses)
            {
                tmpSubjects.AddRange(course.Subjects);
                tmpStudents.AddRange(course.Students);
                
                foreach (var student in course.Students)
                {
                    tmpEvaluations.AddRange(student.Evaluations);
                }

                
            }
            dictionary.Add(DictionaryKey.Subject, tmpSubjects.Cast<SchoolObjectBase>());
            dictionary.Add(DictionaryKey.Student, tmpStudents.Cast<SchoolObjectBase>());
            dictionary.Add(DictionaryKey.Evaluation, tmpEvaluations.Cast<SchoolObjectBase>());
            return dictionary;
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

        public IReadOnlyList<SchoolObjectBase> GetSchoolObjects (
            bool bringEvaluations = true,
            bool bringStudents = true,
            bool bringSubjects = true,
            bool bringCourses = true
            )
        {
            return GetSchoolObjects(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<SchoolObjectBase> GetSchoolObjects (out int evaluationsQuantity,
            bool bringEvaluations = true,
            bool bringStudents = true,
            bool bringSubjects = true,
            bool bringCourses = true
            )
        {
            return GetSchoolObjects(out evaluationsQuantity, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<SchoolObjectBase> GetSchoolObjects (out int evaluationsQuantity, out int coursesQuantity,
            bool bringEvaluations = true,
            bool bringStudents = true,
            bool bringSubjects = true,
            bool bringCourses = true
            )
        {
            return GetSchoolObjects(out evaluationsQuantity, out coursesQuantity, out int dummy, out dummy);
        }

        public IReadOnlyList<SchoolObjectBase> GetSchoolObjects (out int evaluationsQuantity, out int coursesQuantity,
            out int subjectsQuantity,
            bool bringEvaluations = true,
            bool bringStudents = true,
            bool bringSubjects = true,
            bool bringCourses = true
            )
        {
            return GetSchoolObjects(out evaluationsQuantity, out coursesQuantity, out subjectsQuantity, out int dummy);
        }

        public IReadOnlyList<SchoolObjectBase> GetSchoolObjects (
            out int evaluationsQuantity,
            out int coursesQuantity,
            out int subjectsQuantity,
            out int studentsQuantity,
            bool bringEvaluations = true,
            bool bringStudents = true,
            bool bringSubjects = true,
            bool bringCourses = true
            )
        {
            var objList = new List<SchoolObjectBase>();
            evaluationsQuantity = 0;
            subjectsQuantity = 0;
            studentsQuantity = 0;

            objList.Add(School);

            if(bringCourses)
                objList.AddRange(School.Courses);
                coursesQuantity = School.Courses.Count;

            foreach(var course in School.Courses)
            {
                subjectsQuantity += course.Subjects.Count;
                studentsQuantity += course.Students.Count;

                if(bringSubjects)
                    objList.AddRange(course.Subjects);

                if(bringStudents)
                    objList.AddRange(course.Students);

                if(bringEvaluations)
                {
                    foreach (var student in course.Students)
                    {
                        objList.AddRange(student.Evaluations);

                        evaluationsQuantity += student.Evaluations.Count;
                    }
                }
                
            }
            return objList.AsReadOnly();
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

        #region uploadMethods
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

        private void UploadEvaluations()
        {
            var rnd = new Random();
            foreach (var course in School.Courses)
            {
                foreach (var subject in course.Subjects)
                {
                    foreach (var student in course.Students)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluation
                            {
                                Subject = subject,
                                Name = $"{subject.Name} Ev# {i + 1}",
                                Score = MathF.Round(5 * (float)rnd.NextDouble(), 2),
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
        #endregion 
    }
}