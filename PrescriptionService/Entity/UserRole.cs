using System;
using System.Collections.Generic;

namespace PrescriptionService
{
    public partial class UserRole
    {
        public UserRole()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
