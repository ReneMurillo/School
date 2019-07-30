using System;

namespace CoreSchool.Entities
{
    public class SchoolObjectBase
    {
        public string UniqueId { get; set; }
        public string Name { get; set; }

        public SchoolObjectBase() 
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString() 
        {
            return $"{Name}, {UniqueId}";
        }
    }
}