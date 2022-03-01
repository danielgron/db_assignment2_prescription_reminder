using System;
using System.Collections.Generic;

namespace PresciptionService
{
    public partial class Patient
    {
        public int Id { get; set; }
        public int? LoginId { get; set; }
        public int? RoleId { get; set; }
        public PersonalDatum PersonalDatum { get; set; }
    }
}
