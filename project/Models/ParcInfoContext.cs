using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project.Models;

namespace project.Models
{
    public class ParcInfoContext : DbContext
    {
        public ParcInfoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Parc> Parcs { get; set; }
        public DbSet<Pc> Pcs { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<AppPc> AppPcs { get; set; }
        public DbSet<InfoNetwork> InfoNetworks { get; set; }
        public DbSet<InfoSystem> InfoSystems { get; set; }
        public DbSet<Performance> Performances { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppPc>()
        //    .HasKey(e => new { e.AppId, e.PcId });
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppPc>()
                .HasKey(bc => new { bc.AppId, bc.PcId });
            modelBuilder.Entity<AppPc>()
                .HasOne(bc => bc.Pc)
                .WithMany(b => b.AppsPCs)
                .HasForeignKey(bc => bc.PcId);
            modelBuilder.Entity<AppPc>()
                .HasOne(bc => bc.App)
                .WithMany(c => c.AppPcs)
                .HasForeignKey(bc => bc.AppId);
        }
    }
}
