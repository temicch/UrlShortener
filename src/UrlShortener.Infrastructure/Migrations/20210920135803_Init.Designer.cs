﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlShortener.Infrastructure;

namespace UrlShortener.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210920135803_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("UrlShortener.Domain.Entities.ShortLink", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkFull")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkShort")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ShortLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
