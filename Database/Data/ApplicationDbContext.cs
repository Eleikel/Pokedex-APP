using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Tipo> Types { get; set; }
        public DbSet<Region> Region { get; set; }   
        public DbSet<Pokemon> Pokemons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // API FLUENTE

            #region  ===> Naming Tables
            modelBuilder.Entity<Tipo>().ToTable("Types");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<Pokemon>().ToTable("Pokemons");
            #endregion


            #region ===> Primary Keys
            modelBuilder.Entity<Tipo>().HasKey(tipo => tipo.Id);
            modelBuilder.Entity<Region>().HasKey(region => region.Id);
            modelBuilder.Entity<Pokemon>().HasKey(pokemon => pokemon.Id);
            #endregion


            #region ==> Relationships or Foreign Keys
            modelBuilder.Entity<Tipo>()
                .HasMany<Pokemon>(tipos => tipos.PokemonsPrimaryType)
                .WithOne(pokemon => pokemon.TipoPrimary)
                .HasForeignKey(pokemon => pokemon.PrimaryTypeId)
                .OnDelete(DeleteBehavior.Cascade); //Cascade

            modelBuilder.Entity<Tipo>()
                .HasMany<Pokemon>(tipos => tipos.PokemonsSecondaryType)
                .WithOne(pokemon => pokemon.TipoSecondary)
                .HasForeignKey(pokemon => pokemon.SecondaryTypeId)
                .OnDelete(DeleteBehavior.Restrict);

           


            modelBuilder.Entity<Region>()
                .HasMany<Pokemon>(region => region.Pokemons)
                .WithOne(pokemon => pokemon.Region)
                .HasForeignKey(pokemon => pokemon.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion



            #region ==> Property Configurations

            #region Pokemon Required
            modelBuilder.Entity<Pokemon>().Property(pokemon => pokemon.Name).IsRequired();
            modelBuilder.Entity<Pokemon>().Property(Pokemon => Pokemon.Foto).IsRequired();
            modelBuilder.Entity<Pokemon>().Property(Pokemon => Pokemon.PrimaryTypeId).IsRequired();
            #endregion

            #region Region Required
            modelBuilder.Entity<Region>().Property(region => region.Name).IsRequired();
            #endregion

            #region Tipo Required
            modelBuilder.Entity<Tipo>().Property(tipo => tipo.TypeName).IsRequired();
            #endregion

            #endregion


        }

    }
}
