﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RedPhase.Crm.Data.Migrations
{
    public partial class CrmPreInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "CustomerId_HiLo",
                startValue: 1000L,
                incrementBy: 100);

            migrationBuilder.CreateSequence(
                name: "OrganizationId_HiLo",
                startValue: 1000L,
                incrementBy: 100);

            migrationBuilder.CreateSequence(
                name: "PartyId_HiLo",
                startValue: 1000L,
                incrementBy: 100);

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidThru = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Host = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TokenId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    PartyType = table.Column<int>(type: "integer", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PartyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InitialContactDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastContactDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidThru = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateActivityId = table.Column<long>(type: "bigint", nullable: false),
                    UpdateActivityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parties_Activities_CreateActivityId",
                        column: x => x.CreateActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parties_Activities_UpdateActivityId",
                        column: x => x.UpdateActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MotherName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MotherMaidenName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MaidenName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MarriageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Language = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidThru = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateActivityId = table.Column<long>(type: "bigint", nullable: false),
                    UpdateActivityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Activities_CreateActivityId",
                        column: x => x.CreateActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Activities_UpdateActivityId",
                        column: x => x.UpdateActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Parties_Id",
                        column: x => x.Id,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    GroupNameShort = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    TaxOfficePartyId = table.Column<int>(type: "integer", nullable: true),
                    TaxOfficeName = table.Column<string>(type: "text", nullable: true),
                    FoundationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FoundationGeoId = table.Column<int>(type: "integer", nullable: true),
                    PublicOrganization = table.Column<string>(type: "text", nullable: true),
                    TradeChamberRegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidThru = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateActivityId = table.Column<long>(type: "bigint", nullable: false),
                    UpdateActivityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Activities_CreateActivityId",
                        column: x => x.CreateActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organizations_Activities_UpdateActivityId",
                        column: x => x.UpdateActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Organizations_Parties_Id",
                        column: x => x.Id,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TokenId",
                table: "Activities",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreateActivityId",
                table: "Customers",
                column: "CreateActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UpdateActivityId",
                table: "Customers",
                column: "UpdateActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CreateActivityId",
                table: "Organizations",
                column: "CreateActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UpdateActivityId",
                table: "Organizations",
                column: "UpdateActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_CreateActivityId",
                table: "Parties",
                column: "CreateActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_UpdateActivityId",
                table: "Parties",
                column: "UpdateActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropSequence(
                name: "CustomerId_HiLo");

            migrationBuilder.DropSequence(
                name: "OrganizationId_HiLo");

            migrationBuilder.DropSequence(
                name: "PartyId_HiLo");
        }
    }
}
