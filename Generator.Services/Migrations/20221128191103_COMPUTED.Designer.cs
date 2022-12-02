﻿// <auto-generated />
using Generator.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Generator.Service.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20221128191103_COMPUTED")]
    partial class COMPUTED
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Generator.Service.TEST_WILL_DELETE_LATER.COMPUTED_TABLE", b =>
                {
                    b.Property<int>("CT_ROWID")
                        .HasColumnType("int");

                    b.Property<string>("CT_DESC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CT_ROWID");

                    b.ToTable("COMPUTED_TABLE");
                });

            modelBuilder.Entity("Generator.Service.TEST_WILL_DELETE_LATER.TEST_TABLE", b =>
                {
                    b.Property<int>("TT_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TT_ROWID"));

                    b.Property<string>("TT_DESC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TT_ROWID");

                    b.ToTable("TEST_TABLE");
                });
#pragma warning restore 612, 618
        }
    }
}
