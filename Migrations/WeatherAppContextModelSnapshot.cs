﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using weatherApp.Data;

#nullable disable

namespace weatherApp.Migrations
{
    [DbContext(typeof(WeatherAppContext))]
    partial class WeatherAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("weatherApp.Models.City.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("weatherApp.Models.City.FavouriteCity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("FavouriteCities");
                });

            modelBuilder.Entity("weatherApp.Models.FollowedCity.FollowedCity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId1")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.ToTable("FollowedCities");
                });

            modelBuilder.Entity("weatherApp.Models.User.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("weatherApp.Models.City.FavouriteCity", b =>
                {
                    b.HasOne("weatherApp.Models.User.User", null)
                        .WithOne("FavouriteCity")
                        .HasForeignKey("weatherApp.Models.City.FavouriteCity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("weatherApp.Models.FollowedCity.FollowedCity", b =>
                {
                    b.HasOne("weatherApp.Models.User.User", null)
                        .WithMany("FollowedCities")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("weatherApp.Models.User.User", b =>
                {
                    b.Navigation("FavouriteCity")
                        .IsRequired();

                    b.Navigation("FollowedCities");
                });
#pragma warning restore 612, 618
        }
    }
}
