using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class CinematicketsContext : DbContext
    {
        public CinematicketsContext()
        {
        }

        public CinematicketsContext(DbContextOptions<CinematicketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientsMoviesHall> ClientsMoviesHalls { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MoviesHall> MoviesHalls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.Login, "UQ__clients__7838F272B7B79CD7")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<ClientsMoviesHall>(entity =>
            {
                entity.HasKey(e => new { e.IdClient, e.IdMovie, e.IdHall })
                    .HasName("PK_CLIENTS_MOVIES_HALLS");

                entity.ToTable("clients_movies_halls");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.IdMovie).HasColumnName("id_movie");

                entity.Property(e => e.IdHall).HasColumnName("id_hall");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.ClientsMoviesHalls)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_movies_halls_fk0");

                entity.HasOne(d => d.IdHallNavigation)
                    .WithMany(p => p.ClientsMoviesHalls)
                    .HasForeignKey(d => d.IdHall)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_movies_halls_fk2");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.ClientsMoviesHalls)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_movies_halls_fk1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.Login, "UQ__employee__7838F272D2760A53")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("halls");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoomNumber).HasColumnName("room_number");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Year)
                    .HasColumnType("date")
                    .HasColumnName("year");
            });

            modelBuilder.Entity<MoviesHall>(entity =>
            {
                entity.HasKey(e => new { e.IdMovie, e.IdHall })
                    .HasName("PK_MOVIES_HALLS");

                entity.ToTable("movies_halls");

                entity.Property(e => e.IdMovie).HasColumnName("id_movie");

                entity.Property(e => e.IdHall).HasColumnName("id_hall");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.IdHallNavigation)
                    .WithMany(p => p.MoviesHalls)
                    .HasForeignKey(d => d.IdHall)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movies_halls_fk1");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.MoviesHalls)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movies_halls_fk0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
