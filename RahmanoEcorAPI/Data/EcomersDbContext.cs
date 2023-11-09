using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RahmanoEcorAPI.Models;

namespace RahmanoEcorAPI.Data
{
    public class EcomersDbContext : DbContext
    {
        public EcomersDbContext(DbContextOptions<EcomersDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}