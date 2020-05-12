using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ticketverkoop.Domain.Entities
{
    public partial class TicketverkoopContext : DbContext
    {
        public TicketverkoopContext()
        {
        }

        public TicketverkoopContext(DbContextOptions<TicketverkoopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abonnement> Abonnement { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Bestelling> Bestelling { get; set; }
        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<Stadion> Stadion { get; set; }
        public virtual DbSet<StadionVak> StadionVak { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Vak> Vak { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<Wedstrijd> Wedstrijd { get; set; }
        public virtual DbSet<Zitplaats> Zitplaats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQL_VIVES;Database=TicketVerkoop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Abonnement>(entity =>
            {
                entity.Property(e => e.GebruikerId)
                    .IsRequired()
                    .HasColumnName("Gebruiker_id")
                    .HasMaxLength(450);

                entity.Property(e => e.Prijs).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.VakId).HasColumnName("Vak_id");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.Abonnement)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vak_id_Abonnement");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Bestelling>(entity =>
            {
                entity.Property(e => e.AbonnementId).HasColumnName("Abonnement_id");

                entity.Property(e => e.GebruikerId)
                    .IsRequired()
                    .HasColumnName("Gebruiker_id")
                    .HasMaxLength(450);

                entity.Property(e => e.TicketId).HasColumnName("Ticket_id");

                entity.HasOne(d => d.Abonnement)
                    .WithMany(p => p.Bestelling)
                    .HasForeignKey(d => d.AbonnementId)
                    .HasConstraintName("FK_Abonnement_id_Bestelling");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.Bestelling)
                    .HasForeignKey(d => d.GebruikerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gebruiker_id_Bestelling");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Bestelling)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK_Ticket_id_Bestelling");
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.StadionId).HasColumnName("Stadion_id");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.Club)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadion_id_Club");
            });

            modelBuilder.Entity<Stadion>(entity =>
            {
                entity.Property(e => e.Adres).HasMaxLength(50);

                entity.Property(e => e.Naam).HasMaxLength(50);
            });

            modelBuilder.Entity<StadionVak>(entity =>
            {
                entity.Property(e => e.AbonnementPrijs).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Prijs).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.StadionId).HasColumnName("Stadion_id");

                entity.Property(e => e.VakId).HasColumnName("Vak_id");

                entity.Property(e => e.WedstrijdId).HasColumnName("Wedstrijd_id");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.StadionVak)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadion_id_StadionVak");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.StadionVak)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vak_id_StadionVak");

                entity.HasOne(d => d.Wedstrijd)
                    .WithMany(p => p.StadionVak)
                    .HasForeignKey(d => d.WedstrijdId)
                    .HasConstraintName("FK_Wedstrijd_id_StadionVak");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.GebruikerId)
                    .IsRequired()
                    .HasColumnName("Gebruiker_id")
                    .HasMaxLength(450);

                entity.Property(e => e.Prijs).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.VakId).HasColumnName("Vak_id");

                entity.Property(e => e.WedstrijdId).HasColumnName("Wedstrijd_id");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.GebruikerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gebruiker_id_Ticket");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vak_id_Ticket");

                entity.HasOne(d => d.Wedstrijd)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.WedstrijdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_id_Ticket");
            });

            modelBuilder.Entity<Vak>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.GebruikerId)
                    .IsRequired()
                    .HasColumnName("Gebruiker_id")
                    .HasMaxLength(450);

                entity.Property(e => e.TicketId)
                    .HasColumnName("Ticket_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ZitplaatsId).HasColumnName("Zitplaats_id");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.GebruikerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gebruiker_id_Voucher");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_id_Voucher");

                entity.HasOne(d => d.Zitplaats)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.ZitplaatsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zitplaats_id_Voucher");
            });

            modelBuilder.Entity<Wedstrijd>(entity =>
            {
                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.ThuisClubId).HasColumnName("ThuisClub_id");

                entity.Property(e => e.UitClubId).HasColumnName("UitClub_id");

                entity.HasOne(d => d.ThuisClub)
                    .WithMany(p => p.WedstrijdThuisClub)
                    .HasForeignKey(d => d.ThuisClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThuisClub_id_Wedstrijd");

                entity.HasOne(d => d.UitClub)
                    .WithMany(p => p.WedstrijdUitClub)
                    .HasForeignKey(d => d.UitClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UitClub_id_Wedstrijd");
            });
        }
    }
}
