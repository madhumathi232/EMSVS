using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMSAdminService.Models;

namespace EMSAdminService.Data
{
    public class EMSAdminServiceContext : DbContext
    {
        public EMSAdminServiceContext (DbContextOptions<EMSAdminServiceContext> options)
            : base(options)
        {
        }

        public DbSet<EMSAdminService.Models.Employee> Employee { get; set; } = default!;
    }
}
