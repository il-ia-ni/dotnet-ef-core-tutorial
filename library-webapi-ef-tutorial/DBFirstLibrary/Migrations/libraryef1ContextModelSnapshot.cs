﻿// <auto-generated />
using System;
using DBFirstLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBFirstLibrary.Migrations
{
    [DbContext(typeof(libraryef1Context))]
    partial class libraryef1ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf16_german2_ci")
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf16");

            modelBuilder.Entity("DBFirstLibrary.Author", b =>
                {
                    b.Property<long>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("AuthorID");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DeathDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("AuthorId");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("DBFirstLibrary.Book", b =>
                {
                    b.Property<long>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("BookID");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint(20)")
                        .HasColumnName("AuthorID");

                    b.Property<DateOnly?>("PublishedDate")
                        .HasColumnType("date");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("BookId");

                    b.HasIndex(new[] { "AuthorId" }, "Books_FK");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("DBFirstLibrary.Book", b =>
                {
                    b.HasOne("DBFirstLibrary.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Books_FK");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("DBFirstLibrary.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
