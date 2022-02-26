using System;
using System.Collections.Generic;

namespace TestDataApi
{
    public partial class Patient
    {
        public Patient()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? LoginId { get; set; }
        public int? RoleId { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
