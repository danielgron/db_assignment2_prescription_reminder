using System;
using System.Collections.Generic;

namespace PresciptionService
{
    public partial class PersonalDatum
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public int? LoginId { get; set; }
        public int? RoleId { get; set; }
        public int? AddressId { get; set; }
    }
}
