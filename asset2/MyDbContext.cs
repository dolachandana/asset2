using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asset2
{
    internal class MyDbContext:DbContext
    {
        public DbSet<item> items { get; set; }

        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Database = cars; Integrated Security = True"; 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
