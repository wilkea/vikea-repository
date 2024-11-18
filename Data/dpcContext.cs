using dpcleague_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace dpcleague_2.Data
{
    public class dpcContext : DbContext
    {
        public dpcContext(DbContextOptions<dpcContext> options) : base(options) { }

        public DbSet<Organizatie> Organizaties { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<RosterSportiv> RosterSportivs { get; set; }
        public DbSet<Sportiv> Sportivs { get; set; }
        public DbSet<RosterEveniment> RosterEveniments { get; set; }
        public DbSet<Eveniment> Eveniments { get; set; }
        public DbSet<Bilet> Bilets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure tables with correct names for SQL Server
            modelBuilder.Entity<Organizatie>().ToTable("Organizaties");
            modelBuilder.Entity<Roster>().ToTable("Rosters");
            modelBuilder.Entity<RosterSportiv>().ToTable("RosterSportivs");
            modelBuilder.Entity<Sportiv>().ToTable("Sportivs");
            modelBuilder.Entity<RosterEveniment>().ToTable("RosterEveniments");
            modelBuilder.Entity<Eveniment>().ToTable("Eveniments");
            modelBuilder.Entity<Bilet>().ToTable("Bilets");

            // Configure relationships
            modelBuilder.Entity<Bilet>()
                .HasOne(b => b.Eveniment)
                .WithMany(e => e.Bilets)
                .HasForeignKey(b => b.EvenimentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Roster>()
                .HasOne(r => r.Organizatie)
                .WithMany(o => o.Rostere)
                .HasForeignKey(r => r.OrganizatieId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RosterEveniment>()
                .HasOne(re => re.Eveniment)
                .WithMany(e => e.RosterEvenimente)
                .HasForeignKey(re => re.EvenimentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RosterEveniment>()
                .HasOne(re => re.Roster)
                .WithMany(r => r.RosterEvenimente)
                .HasForeignKey(re => re.RosterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RosterSportiv>()
                .HasOne(rs => rs.Roster)
                .WithMany(r => r.RosterSportivi)
                .HasForeignKey(rs => rs.RosterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RosterSportiv>()
                .HasOne(rs => rs.Sportiv)
                .WithMany(s => s.RosterSportivi)
                .HasForeignKey(rs => rs.SportivId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sportiv>()
                .HasOne(s => s.Organizatie)
                .WithMany(o => o.Sportivi)
                .HasForeignKey(s => s.OrganizatieId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
