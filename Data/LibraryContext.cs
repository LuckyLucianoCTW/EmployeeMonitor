using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DawProject.Models;

namespace DawProject.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<RegisterE> Register { get; set; }
        public DbSet<Projects> Project { get; set; }
        public DbSet<ProjectsAssigned> ProjectAssigned { get; set; }
        public DbSet<Logs> Data_Logs { get; set; }
        public DbSet<Roles> Role { get; set; } 
    }
}