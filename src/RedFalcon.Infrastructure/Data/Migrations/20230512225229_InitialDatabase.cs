using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RedFalcon.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
