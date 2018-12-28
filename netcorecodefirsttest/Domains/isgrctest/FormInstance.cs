using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    [Table("ms_FormInstance")]
    public class FormInstance
    {
        /// <summary>
        /// 实例Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 制度Id
        /// </summary>
        public int InstId { get; set; }
    }
}
