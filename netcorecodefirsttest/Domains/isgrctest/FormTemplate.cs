using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    [Table("ms_FormTemplate")]
    public class FormTemplate
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 制度Id
        /// </summary>
        public int InstId { get; set; }

        /// <summary>
        /// 控制Id
        /// </summary>
        public int CtrlId { get; set; }

        /// <summary>
        /// 检查点ID
        /// </summary>
        public int CheckId { get; set; }

        /// <summary>
        /// 指标项
        /// </summary>
        public int NormId { get; set; }
    }
}
