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
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.Login, "UQ__clients__7838F272340C5F09")
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
                entity.HasNoKey();

                entity.ToTable("clients_movies_halls");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.IdHall).HasColumnName("id_hall");

                entity.Property(e => e.IdMovie).HasColumnName("id_movie");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_movies_halls_fk0");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.Login, "UQ__employee__7838F2727FB14FCF")
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
                entity.HasNoKey();

                entity.ToTable("movies_halls");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.IdHall).HasColumnName("id_hall");

                entity.Property(e => e.IdMovie).HasColumnName("id_movie");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.IdHallNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdHall)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movies_halls_fk1");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movies_halls_fk0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
