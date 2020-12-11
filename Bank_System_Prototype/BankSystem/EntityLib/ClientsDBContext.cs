using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EntityLib
{
    public partial class ClientsDBContext : DbContext
    {
        public ClientsDBContext()
        {
        }

        public ClientsDBContext(DbContextOptions<ClientsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllLegalClient> AllLegalClients { get; set; }
        public virtual DbSet<AllNaturalClient> AllNaturalClients { get; set; }
        public virtual DbSet<AllVipLegalClient> AllVipLegalClients { get; set; }
        public virtual DbSet<AllVipNaturalClient> AllVipNaturalClients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localDB)\\MSSQLLocalDB;Database=ClientsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllLegalClient>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AllLegal__3214EC06590C0F40")
                    .IsClustered(false);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AmountOfMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CheckContribution).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CheckDebt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Reputation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("reputation");
            });

            modelBuilder.Entity<AllNaturalClient>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AllNatur__3214EC065D92CA55")
                    .IsClustered(false);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AmountOfMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CheckContribution).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CheckDebt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Reputation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("reputation");
            });

            modelBuilder.Entity<AllVipLegalClient>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AllVipLe__3214EC063B660D0E")
                    .IsClustered(false);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AmountOfMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CheckContribution).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CheckDebt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AllVipNaturalClient>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AllVipNa__3214EC069C16CF7C")
                    .IsClustered(false);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AmountOfMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CheckContribution).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CheckDebt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
