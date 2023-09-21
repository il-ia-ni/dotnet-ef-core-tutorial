using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBFirstLibrary
{
    public partial class libraryef1Context : DbContext
    {
        public libraryef1Context()
        {
        }

        public libraryef1Context(DbContextOptions<libraryef1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=testadmin-library-ef;password=test;database=library-ef1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.5-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf16_german2_ci")
                .HasCharSet("utf16");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors");

                entity.Property(e => e.AuthorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("AuthorID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.AuthorId, "Books_FK");

                entity.Property(e => e.BookId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("BookID");

                entity.Property(e => e.AuthorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("AuthorID");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("Books_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
