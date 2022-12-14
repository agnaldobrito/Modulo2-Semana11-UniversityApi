// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityApi.Context;

#nullable disable

namespace UniversityApi.Migrations
{
    [DbContext(typeof(UniversityContext))]
    partial class UniversityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UniversityApi.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Nome");

                    b.Property<string>("Requirement")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Requisito");

                    b.Property<decimal?>("Value")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Valor");

                    b.Property<int?>("WorkLoad")
                        .HasColumnType("int")
                        .HasColumnName("CargaHoraria");

                    b.HasKey("Id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("UniversityApi.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Certificates")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Certificados");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("HourlyValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("ValorHora");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Nome");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Instrutor");
                });

            modelBuilder.Entity("UniversityApi.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataNascimento");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Nome");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Aluno");
                });
#pragma warning restore 612, 618
        }
    }
}
