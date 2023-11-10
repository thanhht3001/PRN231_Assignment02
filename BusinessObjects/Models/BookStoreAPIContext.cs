using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class BookStoreAPIContext : DbContext
    {
        public BookStoreAPIContext()
        {
        }

        public BookStoreAPIContext(DbContextOptions<BookStoreAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .HasColumnName("email_address");

                entity.Property(e => e.FistName)
                    .HasMaxLength(255)
                    .HasColumnName("fist_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Zip)
                    .HasMaxLength(255)
                    .HasColumnName("zip");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Advance)
                    .HasMaxLength(255)
                    .HasColumnName("advance");

                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .HasColumnName("notes");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.PubId).HasColumnName("pub_id");

                entity.Property(e => e.PublishedDate).HasColumnName("published_date");

                entity.Property(e => e.Royalty)
                    .HasMaxLength(255)
                    .HasColumnName("royalty");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.YtdSales)
                    .HasMaxLength(255)
                    .HasColumnName("ytd_sales");

                entity.HasOne(d => d.Pub)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Publisher");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BookAuthor");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.AuthorOrder)
                    .HasMaxLength(255)
                    .HasColumnName("author_order");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.RoyalityPercentage)
                    .HasMaxLength(255)
                    .HasColumnName("royality_percentage");

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_BookAuthor_Author");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookAuthor_Book");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PubId);

                entity.ToTable("Publisher");

                entity.Property(e => e.PubId).HasColumnName("pub_id");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.PublisherName)
                    .HasMaxLength(255)
                    .HasColumnName("publisher_name");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleDesc)
                    .HasMaxLength(255)
                    .HasColumnName("role_desc");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .HasColumnName("email_address");

                entity.Property(e => e.FistName)
                    .HasMaxLength(255)
                    .HasColumnName("fist_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("date")
                    .HasColumnName("hire_date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.PubId).HasColumnName("pub_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Source)
                    .HasMaxLength(255)
                    .HasColumnName("source");

                entity.HasOne(d => d.Pub)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Publisher");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
