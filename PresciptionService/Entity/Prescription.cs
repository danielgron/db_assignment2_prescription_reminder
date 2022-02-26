using System;
using System.Collections.Generic;

namespace PresciptionService
{
    public partial class Prescription
    {
        public long Id { get; set; }
        public DateOnly? Expiration { get; set; }
        public DateTime Creation { get; set; }
        public int MedicineId { get; set; }
        public int PrescribedBy { get; set; }
        public int LastAdministeredBy { get; set; }

        public virtual Pharmaceut LastAdministeredByNavigation { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
        public virtual Doctor PrescribedByNavigation { get; set; } = null!;
    }
}
