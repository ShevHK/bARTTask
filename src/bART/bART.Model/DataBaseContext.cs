using bART.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Model
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(IServiceProvider serviceProvider, DbContextOptions options) : base(options) { }

        public DbSet<incidents> incidents { get; set; }
        public DbSet<accounts> accounts { get; set; }
        public DbSet<contacts> contacts { get; set; }
    }
}
