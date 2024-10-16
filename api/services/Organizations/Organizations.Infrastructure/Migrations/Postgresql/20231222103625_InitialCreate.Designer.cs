﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Organizations.Infrastructure.Persistence;

#nullable disable

namespace Organizations.Infrastructure.Migrations.Postgresql
{
    [DbContext(typeof(OrganizationsDatabaseContext))]
    [Migration("20231222103625_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Organizations.Domain.Aggregates.Organizations.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_at");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("creator_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_organizations");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_organizations_name");

                    b.ToTable("organizations", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
