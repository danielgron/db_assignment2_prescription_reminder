using System;
using System.Collections.Generic;

namespace PrescriptionService
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? LoginId { get; set; }
        public int? RoleId { get; set; }

        public virtual LoginInfo? Login { get; set; }
        public virtual UserRole? Role { get; set; }
    }
}
