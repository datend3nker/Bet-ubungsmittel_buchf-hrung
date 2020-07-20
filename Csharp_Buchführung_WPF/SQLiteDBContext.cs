using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Csharp_Buchführung_WPF
{
    class SQLiteDBContext : DbContext
    {
        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<Stufe> Stufen { get; set; }
        public DbSet<Eintrag> Einträge { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=BtmDB.db");
    }
}
