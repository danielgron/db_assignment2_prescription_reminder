using System;
using System.Collections.Generic;

namespace PresciptionService
{
    public partial class Pharmaceut
    {
        public Pharmaceut()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? LoginId { get; set; }
        public int? RoleId { get; set; }
        public int? PharmacyId { get; set; }

        public virtual Pharmacy? Pharmacy { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
