using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    [Table("ClassInfo")]
    public class ClassInfo
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public string Remark { get; set; }
    }
}
