﻿// <auto-generated />
using System;
using ControlDocuments.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlDocuments.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ControlDocuments.Models.DocumentoModel", b =>
                {
                    b.Property<int>("ID_Documento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Documento"));

                    b.Property<DateTime>("Data_Lancamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_Vencimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Loja")
                        .HasColumnType("int");

                    b.Property<string>("Numero_Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Documento");

                    b.HasIndex("ID_Loja");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("ControlDocuments.Models.LojaModel", b =>
                {
                    b.Property<int>("ID_Loja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Loja"));

                    b.Property<string>("Nome_Loja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Loja");

                    b.ToTable("Lojas");
                });

            modelBuilder.Entity("ControlDocuments.Models.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmaSenha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ControlDocuments.Models.DocumentoModel", b =>
                {
                    b.HasOne("ControlDocuments.Models.LojaModel", "Lojas")
                        .WithMany("Documentos")
                        .HasForeignKey("ID_Loja")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lojas");
                });

            modelBuilder.Entity("ControlDocuments.Models.LojaModel", b =>
                {
                    b.Navigation("Documentos");
                });
#pragma warning restore 612, 618
        }
    }
}
