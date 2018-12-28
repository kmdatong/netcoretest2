using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    /// <summary>
    /// 表单数据
    /// </summary>
    [Table("ms_FormValue")]
    public class FormValue
    {  
        public int Id { get; set; }

        /// <summary>
        /// 实例id
        /// </summary>
        public int InstId { get; set; }

        /// <summary>
        /// 指标项
        /// </summary>
        public int NormId { get; set; }

        /// <summary>
        /// 数值型
        /// </summary>
        public int? ValueInt { get; set; }

        /// <summary>
        /// 数值型
        /// </summary>
        public decimal? ValueDecimal { get; set; }

        /// <summary>
        /// 数值型
        /// </summary>
        public string ValueString { get; set; }
    }
}
