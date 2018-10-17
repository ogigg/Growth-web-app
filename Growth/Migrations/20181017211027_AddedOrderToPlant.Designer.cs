﻿// <auto-generated />
using System;
using Growth.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Growth.Migrations
{
    [DbContext(typeof(GrowthDbContext))]
    [Migration("20181017211027_AddedOrderToPlant")]
    partial class AddedOrderToPlant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Growth.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("SpeciesId");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Growth.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Growth.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Growth.Models.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ImageId");

                    b.Property<int?>("OderId");

                    b.Property<int>("SpeciesId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("OderId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Growth.Models.PlantFeature", b =>
                {
                    b.Property<int>("PlantId");

                    b.Property<int>("FeatureId");

                    b.HasKey("PlantId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("PlantFeatures");
                });

            modelBuilder.Entity("Growth.Models.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("Growth.Models.Feature", b =>
                {
                    b.HasOne("Growth.Models.Species")
                        .WithMany("Features")
                        .HasForeignKey("SpeciesId");
                });

            modelBuilder.Entity("Growth.Models.Plant", b =>
                {
                    b.HasOne("Growth.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("Growth.Models.Order", "Oder")
                        .WithMany()
                        .HasForeignKey("OderId");

                    b.HasOne("Growth.Models.Species", "Species")
                        .WithMany()
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Growth.Models.PlantFeature", b =>
                {
                    b.HasOne("Growth.Models.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Growth.Models.Plant", "Plant")
                        .WithMany("Features")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Growth.Models.Species", b =>
                {
                    b.HasOne("Growth.Models.Order")
                        .WithMany("Species")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
