using System;
using System.Collections.Generic;

namespace CoreSchool.Entities
{  
    public class Student
    {
        public string UniqueId { get; private set; }
        public string Name { get; set; }

        public List<Evaluations> Evaluations {get; set;}

        public Student(){
            UniqueId = Guid.NewGuid().ToString();
            Evaluations = new List<Evaluations>();
        }
    }
}