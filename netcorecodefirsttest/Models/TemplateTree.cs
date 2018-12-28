using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Models
{
    public class TemplateTree
    {
        public int Id { get; set; }
        public string name { get; set; }

        public List<Ctrl> CtrlList { get; set; } = new List<Ctrl>();

    }
    

    public class Ctrl
    {
        public int Id { get; set; }
        public List<Check> CheckList { get; set; } = new List<Check>();
    }

    public class Check
    {
        public int Id { get; set; }
        public List<Norm> NormList { get; set; } = new List<Norm>();
    }

    public class Norm
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
