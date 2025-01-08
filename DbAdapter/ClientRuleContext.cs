
using AlertManagerFMR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using InfoProperty;


namespace DbAdapter
{
    public class ClientRuleContext : DbContext
    {
        public DbSet<RuleInfo> Ruls { get; set; }

        public ClientRuleContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {     
            optionsBuilder.UseSqlServer("Server=localhost;Database=RuleInfoDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<RuleInfo>(entity =>
            {
                entity.HasKey(f => f.ID); 
            });
        }
         
    }

}
