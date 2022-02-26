using System;
using System.Collections.Generic;

namespace PresciptionService
{
    public partial class LoginInfo
    {
        public LoginInfo()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; }
    }
}
