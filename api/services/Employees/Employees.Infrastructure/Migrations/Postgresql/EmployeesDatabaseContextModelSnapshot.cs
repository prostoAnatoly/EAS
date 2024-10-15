﻿// <auto-generated />
using System;
using Employees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Employees.Infrastructure.Migrations.Postgresql
{
    [DbContext(typeof(EmployeesDatabaseContext))]
    partial class EmployeesDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Employees.Domain.Aggregates.Employees.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("Birthday")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday");

                    b.Property<DateTimeOffset>("CreateAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_at");

                    b.Property<DateTimeOffset?>("DateOfDismissal")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_dismissal");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<DateTimeOffset>("EmploymentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("employment_date");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("phone_number");

                    b.HasKey("Id")
                        .HasName("pk_employee");

                    b.ToTable("employee", (string)null);
                });

            modelBuilder.Entity("Employees.Domain.Aggregates.Employees.Employee", b =>
                {
                    b.OwnsOne("Employees.Domain.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("full_name_name");

                            b1.Property<string>("Patronymic")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("full_name_patronymic");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("full_name_surname");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId")
                                .HasConstraintName("fk_employee_employee_id");
                        });

                    b.Navigation("FullName")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
