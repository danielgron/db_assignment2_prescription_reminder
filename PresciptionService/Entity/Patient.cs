using System;
using System.Collections.Generic;

namespace PresciptionService
{
    public partial class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? LoginId { get; set; }
        public int? RoleId { get; set; }
    }
}
