using System;
using System.Collections.Generic;

namespace CoursesAPI.Data
{
    public partial class Course
    {
        public Course()
        {
            Stdcourses = new HashSet<Stdcourse>();
        }

        public int Id { get; set; }
        public string? Coursename { get; set; }
        public int? Categoryid { get; set; }
        public string? Image { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Stdcourse> Stdcourses { get; set; }
    }
}
