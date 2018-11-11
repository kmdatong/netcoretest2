using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    [Table("Account")]
    public class Account
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int Age { get; set; }

        public String LoginName { get; set; }

        public String Tel { get; set; }

        public int UserType { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        public int ClassId { get; set; }
    }
}
