﻿// <auto-generated />
using System;
using Generator.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Generator.Server.Migrations
{
    [DbContext(typeof(GeneratorContext))]
    [Migration("20230710090426_altTableFix")]
    partial class altTableFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.Abstracts.AUTH_BASE", b =>
                {
                    b.Property<int>("AUTH_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AUTH_ROWID"), 1L, 1);

                    b.Property<string>("AUTH_NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AUTH_TYPE")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AUTH_ROWID");

                    b.ToTable("AUTH_BASE");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.Abstracts.COMPONENTS_BASE", b =>
                {
                    b.Property<int>("CB_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CB_ROWID"), 1L, 1);

                    b.Property<int>("CB_COMMAND_TYPE")
                        .HasColumnType("int");

                    b.Property<string>("CB_DATABASE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CB_IDENTIFIER")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CB_QUERY_OR_METHOD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CB_TITLE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CB_TYPE")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CB_ROWID");

                    b.HasIndex("CB_IDENTIFIER")
                        .IsUnique()
                        .HasFilter("[CB_IDENTIFIER] IS NOT NULL");

                    b.ToTable("COMPONENTS_BASE");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.CRUD_VIEW", b =>
                {
                    b.Property<int>("VBM_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VBM_ROWID"), 1L, 1);

                    b.Property<int>("VBM_GRID_REFNO")
                        .HasColumnType("int");

                    b.Property<string>("VBM_TITLE")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VBM_TYPE")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("VBM_ROWID");

                    b.HasIndex("VBM_GRID_REFNO")
                        .IsUnique();

                    b.HasIndex("VBM_GRID_REFNO", "VBM_TYPE", "VBM_TITLE")
                        .IsUnique()
                        .HasFilter("[VBM_TITLE] IS NOT NULL");

                    b.ToTable("CRUD_VIEW");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_FIELDS", b =>
                {
                    b.Property<int>("GF_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GF_ROWID"), 1L, 1);

                    b.Property<string>("GF_BINDINGFIELD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_CONTROL_TYPE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_DATABASE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_DATASOURCE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_DATASOURCE_QUERY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_DISPLAYFIELD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GF_EDITOR_ENABLED")
                        .HasColumnType("bit");

                    b.Property<bool>("GF_EDITOR_VISIBLE")
                        .HasColumnType("bit");

                    b.Property<string>("GF_FALSE_TEXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_FORMAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GF_GRID_VISIBLE")
                        .HasColumnType("bit");

                    b.Property<int>("GF_INPUT_TYPE")
                        .HasColumnType("int");

                    b.Property<string>("GF_LABEL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GF_LG")
                        .HasColumnType("int");

                    b.Property<int?>("GF_MD")
                        .HasColumnType("int");

                    b.Property<bool>("GF_REQUIRED")
                        .HasColumnType("bit");

                    b.Property<int?>("GF_SM")
                        .HasColumnType("int");

                    b.Property<string>("GF_SOURCE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GF_TRUE_TEXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GF_USE_AS_SEARCHFIELD")
                        .HasColumnType("bit");

                    b.Property<string>("GF_VALUEFIELD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GF_VIEW_REFNO")
                        .HasColumnType("int");

                    b.Property<int?>("GF_XLG")
                        .HasColumnType("int");

                    b.Property<int?>("GF_XS")
                        .HasColumnType("int");

                    b.Property<int?>("GF_XXLG")
                        .HasColumnType("int");

                    b.HasKey("GF_ROWID");

                    b.HasIndex("GF_VIEW_REFNO");

                    b.ToTable("GRID_FIELDS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.ROLES_DETAILS", b =>
                {
                    b.Property<int>("RD_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RD_ROWID"), 1L, 1);

                    b.Property<int>("RD_M_REFNO")
                        .HasColumnType("int");

                    b.Property<int>("RD_PERMISSION_REFNO")
                        .HasColumnType("int");

                    b.HasKey("RD_ROWID");

                    b.HasIndex("RD_M_REFNO");

                    b.HasIndex("RD_PERMISSION_REFNO");

                    b.ToTable("ROLES_DETAILS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.USER_AUTHORIZATIONS", b =>
                {
                    b.Property<int>("UA_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UA_ROWID"), 1L, 1);

                    b.Property<int?>("UA_AUTH_CODE")
                        .HasColumnType("int");

                    b.Property<int>("UA_USER_REFNO")
                        .HasColumnType("int");

                    b.HasKey("UA_ROWID");

                    b.HasIndex("UA_AUTH_CODE");

                    b.HasIndex("UA_USER_REFNO");

                    b.ToTable("USER_AUTHORIZATIONS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.USERS", b =>
                {
                    b.Property<int>("U_ROWID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("U_ROWID"), 1L, 1);

                    b.Property<string>("U_USERNAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("U_ROWID");

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_M", b =>
                {
                    b.HasBaseType("Generator.Shared.Models.ComponentModels.Abstracts.COMPONENTS_BASE");

                    b.Property<bool>("GB_CREATE")
                        .HasColumnType("bit");

                    b.Property<string>("GB_CRUD_SOURCE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GB_DELETE")
                        .HasColumnType("bit");

                    b.Property<bool>("GB_DENSE")
                        .HasColumnType("bit");

                    b.Property<int>("GB_EDIT_MODE")
                        .HasColumnType("int");

                    b.Property<int>("GB_EDIT_TRIGGER")
                        .HasColumnType("int");

                    b.Property<bool>("GB_ENABLE_FILTERING")
                        .HasColumnType("bit");

                    b.Property<bool>("GB_ENABLE_SORTING")
                        .HasColumnType("bit");

                    b.Property<int>("GB_MAX_WIDTH")
                        .HasColumnType("int");

                    b.Property<int?>("GB_ROWS_PER_PAGE")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("GB_STRIPED")
                        .HasColumnType("bit");

                    b.Property<bool>("GB_UPDATE")
                        .HasColumnType("bit");

                    b.ToTable("GRID_M");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.PERMISSIONS", b =>
                {
                    b.HasBaseType("Generator.Shared.Models.ComponentModels.Abstracts.AUTH_BASE");

                    b.Property<int>("PER_COMPONENT_REFNO")
                        .HasColumnType("int");

                    b.Property<string>("PER_DESCRIPTION")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("PER_COMPONENT_REFNO");

                    b.ToTable("PERMISSIONS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.ROLES", b =>
                {
                    b.HasBaseType("Generator.Shared.Models.ComponentModels.Abstracts.AUTH_BASE");

                    b.ToTable("ROLES");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_D", b =>
                {
                    b.HasBaseType("Generator.Shared.Models.ComponentModels.GRID_M");

                    b.Property<int>("GD_M_REFNO")
                        .HasColumnType("int");

                    b.HasIndex("GD_M_REFNO");

                    b.ToTable("GRID_D");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.CRUD_VIEW", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.GRID_M", null)
                        .WithMany("CRUD_VIEW")
                        .HasForeignKey("VBM_GRID_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_FIELDS", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.CRUD_VIEW", null)
                        .WithMany("GRID_FIELDS")
                        .HasForeignKey("GF_VIEW_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.ROLES_DETAILS", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.ROLES", null)
                        .WithMany("ROLES_DETAILS")
                        .HasForeignKey("RD_M_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Generator.Shared.Models.ComponentModels.PERMISSIONS", "PERMISSIONS")
                        .WithMany()
                        .HasForeignKey("RD_PERMISSION_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PERMISSIONS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.USER_AUTHORIZATIONS", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.Abstracts.AUTH_BASE", "AUTH_BASE")
                        .WithMany()
                        .HasForeignKey("UA_AUTH_CODE");

                    b.HasOne("Generator.Shared.Models.ComponentModels.USERS", null)
                        .WithMany("USER_AUTHORIZATIONS")
                        .HasForeignKey("UA_USER_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AUTH_BASE");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_M", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.Abstracts.COMPONENTS_BASE", null)
                        .WithOne()
                        .HasForeignKey("Generator.Shared.Models.ComponentModels.GRID_M", "CB_ROWID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.PERMISSIONS", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.Abstracts.AUTH_BASE", null)
                        .WithOne()
                        .HasForeignKey("Generator.Shared.Models.ComponentModels.PERMISSIONS", "AUTH_ROWID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Generator.Shared.Models.ComponentModels.Abstracts.COMPONENTS_BASE", "COMPONENTS_BASE")
                        .WithMany("PERMISSIONS")
                        .HasForeignKey("PER_COMPONENT_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("COMPONENTS_BASE");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.ROLES", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.Abstracts.AUTH_BASE", null)
                        .WithOne()
                        .HasForeignKey("Generator.Shared.Models.ComponentModels.ROLES", "AUTH_ROWID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_D", b =>
                {
                    b.HasOne("Generator.Shared.Models.ComponentModels.GRID_M", null)
                        .WithOne()
                        .HasForeignKey("Generator.Shared.Models.ComponentModels.GRID_D", "CB_ROWID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Generator.Shared.Models.ComponentModels.GRID_M", null)
                        .WithMany("GRID_D")
                        .HasForeignKey("GD_M_REFNO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.Abstracts.COMPONENTS_BASE", b =>
                {
                    b.Navigation("PERMISSIONS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.CRUD_VIEW", b =>
                {
                    b.Navigation("GRID_FIELDS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.USERS", b =>
                {
                    b.Navigation("USER_AUTHORIZATIONS");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.GRID_M", b =>
                {
                    b.Navigation("CRUD_VIEW");

                    b.Navigation("GRID_D");
                });

            modelBuilder.Entity("Generator.Shared.Models.ComponentModels.ROLES", b =>
                {
                    b.Navigation("ROLES_DETAILS");
                });
#pragma warning restore 612, 618
        }
    }
}
