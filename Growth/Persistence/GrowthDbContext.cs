using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Growth.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Growth.Persistence
{
    public class GrowthDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Species> Species { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<User> Users { get; set; }

        public GrowthDbContext(DbContextOptions<GrowthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlantFeature>().HasKey(pf =>
                new {pf.PlantId, pf.FeatureId});
        }


    }
}


