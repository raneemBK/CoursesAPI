using System;
using System.Collections.Generic;

namespace CoursesAPI.Data
{
    public partial class Stdcourse
    {
        public int Id { get; set; }
        public int? Stdid { get; set; }
        public int? Courseid { get; set; }
        public double? Markofstd { get; set; }
        public DateTime? Dateofregistration { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Std { get; set; }
    }
}
