using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using mvc.Models;

namespace mvc.Data
{
    public class mvcContext : DbContext
    {
        public mvcContext (DbContextOptions<mvcContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;

        public DbSet<mvc.Models.Panier> Panier { get; set; }

        public DbSet<mvc.Models.lignepanier> lignepanier { get; set; }

        public DbSet<mvc.Models.Client> Client { get; set; }
    }
}
