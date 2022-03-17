﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RedPhase.Identity.Api.Data;

#nullable disable

namespace RedPhase.Identity.Api.Migrations.PersistedGrantDb
{
    [DbContext(typeof(PersistedGrantDbContext))]
    [Migration("20220224141931_PersistantInit")]
    partial class PersistantInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DeviceCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.Property<string>("SubjectId")
                        .HasColumnType("text");

                    b.Property<string>("UserCode")
                        .HasColumnType("text");

                    b.ToTable("DeviceFlowCodes", "Identity");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Algorithm")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("boolean");

                    b.Property<string>("Use")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Keys", "Identity");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.Property<string>("SubjectId")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.ToTable("PersistedGrants", "Identity");
                });
#pragma warning restore 612, 618
        }
    }
}
