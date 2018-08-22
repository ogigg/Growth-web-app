﻿using System;
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
        public GrowthDbContext(DbContextOptions<GrowthDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Plant> Plants { get; set; }

    }
}


