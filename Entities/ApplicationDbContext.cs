using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> // db implementation
    {
        public virtual DbSet<Country> Coutries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder) // for relationships in databases
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Country>().ToTable("Coutries"); // table creation
            modelbuilder.Entity<Person>().ToTable("Persons");

            // add dummy data
            modelbuilder.Entity<Country>().HasData(new Country("Brazil", Guid.Parse("89d1e09d-e685-4f88-acdb-7df862831e8c")));
            modelbuilder.Entity<Country>().HasData(new Country("Canada", Guid.Parse("a2914ee4-c8f6-4b91-9e2b-dc6da114d0f9")));

            //Fluent API
            // change column properties
            modelbuilder.Entity<Country>().Property(temp => temp.Name)
                .HasColumnName("name")
                .HasDefaultValue("");
            //.HasColumnType("");

            // create index
            modelbuilder.Entity<Country>()
                .HasIndex(temp => temp.CountryID) // add index
                .IsUnique(); // add unique constraint

            // add constraint
            //modelbuilder.Entity<Country>()
            //    .HasCheckConstraint("CONSTRAINT_NAME", "len([name]) = 20");

            // add foreign key
            modelbuilder.Entity<Person>(entity =>
            {
                entity.HasOne<Country>(c => c.Country)
                .WithMany(p => p.Persons)
                .HasForeignKey(p => p.CountryID);
            });
        }

        public List<Country> sp_GetCountries()
        {
            return Coutries.FromSqlRaw("EXECUTE [dbo].[GetCountriesFix]").ToList();
        }
    }
}
