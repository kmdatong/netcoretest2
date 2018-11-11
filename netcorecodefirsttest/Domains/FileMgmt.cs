using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    [Table("FileMgmt")]
    public class FileMgmt
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Length { get; set; }

        public string Guid { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
