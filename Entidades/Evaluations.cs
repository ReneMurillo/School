using System;

namespace CoreSchool.Entities
{  
    public class Evaluations: SchoolObjectBase
    {
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public float Score { get; set; }

        public override string ToString() 
        {
            return $"{Score}, {Student.Name}, {Subject.Name}";
        }
    }
}