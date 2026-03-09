using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class CountriesDbContext : DbContext // db implementation
    {
        public DbSet<Country> Coutries { get; set; }

        public CountriesDbContext(DbContextOptions<CountriesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder) // for relationships in databases
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Country>().ToTable("Coutries"); // table creation

            // add dummy data
            modelbuilder.Entity<Country>().HasData(new Country("Brazil", Guid.Parse("89d1e09d-e685-4f88-acdb-7df862831e8c")));
            modelbuilder.Entity<Country>().HasData(new Country("Canada", Guid.Parse("a2914ee4-c8f6-4b91-9e2b-dc6da114d0f9")));
        }

        public List<Country> sp_GetCountries()
        {
            return Coutries.FromSqlRaw("EXECUTE [dbo].[GetCountries]").ToList();
        }
    }
}
