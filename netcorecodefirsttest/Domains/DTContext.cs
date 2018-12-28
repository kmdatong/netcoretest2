using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Domains
{
    public class DTContext:DbContext
    {
       public DbSet<ClassInfo> ClassInfo { get; set; }

        public DbSet<Account> Account { get; set; }

        public DbSet<LunBo> LunBo { get; set; }

        public DbSet<FileMgmt> FileMgmt { get; set; }

        public DbSet<FormInstance> FormInstance { get; set; }

        public DbSet<FormTemplate> FormTemplate { get; set; }

        public DbSet<FormValue> FormValue { get; set; }

        public DTContext(DbContextOptions<DTContext> options)
            : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
            
        //    base.OnModelCreating(builder);
        //}
    }
}
