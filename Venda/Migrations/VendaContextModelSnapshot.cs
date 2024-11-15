﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Venda.Data;

#nullable disable

namespace Venda.Migrations
{
    [DbContext(typeof(VendaContext))]
    partial class VendaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Venda.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Venda.Models.ItemPedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPedidos")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PedidoId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItemPedidos");
                });

            modelBuilder.Entity("Venda.Models.Movimentacao", b =>
                {
                    b.Property<int>("MovimentacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovimentacaoId"));

                    b.Property<DateTime>("DataMovimentacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("MovimentacaoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Movimentacoes");
                });

            modelBuilder.Entity("Venda.Models.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedidoId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PedidoId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Venda.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdutoId"));

                    b.Property<int>("Estoque")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProdutoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Venda.Models.ItemPedido", b =>
                {
                    b.HasOne("Venda.Models.Pedido", "Pedido")
                        .WithMany("ItensPedidos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Venda.Models.Produto", "Produto")
                        .WithMany("ItensPedidos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Venda.Models.Movimentacao", b =>
                {
                    b.HasOne("Venda.Models.Produto", "Produto")
                        .WithMany("Movimentacoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Venda.Models.Pedido", b =>
                {
                    b.HasOne("Venda.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Venda.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Venda.Models.Pedido", b =>
                {
                    b.Navigation("ItensPedidos");
                });

            modelBuilder.Entity("Venda.Models.Produto", b =>
                {
                    b.Navigation("ItensPedidos");

                    b.Navigation("Movimentacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
