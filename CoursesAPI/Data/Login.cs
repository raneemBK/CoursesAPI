using System;
using System.Collections.Generic;

namespace CoursesAPI.Data
{
    public partial class Login
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Roleid { get; set; }
        public int? Studentid { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Student? Student { get; set; }
    }
}
