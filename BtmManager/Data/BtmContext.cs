using BtmManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtmManager.Data
{
    public class BtmContext : DbContext
    {
        public DbSet<Eintrag> Einträge { get; set; }
        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<Stufe> Stufen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=BtmDB.db");
        }
    }
}
