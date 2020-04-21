using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qrAPI.DAL.Data.EFData.Migrations
{
    public partial class UserPetsTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPet_Pets_PetId",
                table: "UserPet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPet_Users_UserId",
                table: "UserPet");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPet",
                table: "UserPet");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPet");

            migrationBuilder.RenameTable(
                name: "UserPet",
                newName: "UserPets");

            migrationBuilder.RenameIndex(
                name: "IX_UserPet_PetId",
                table: "UserPets",
                newName: "IX_UserPets_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPets",
                table: "UserPets",
                columns: new[] { "UserId", "PetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPets_Pets_PetId",
                table: "UserPets",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPets_Users_UserId",
                table: "UserPets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPets_Pets_PetId",
                table: "UserPets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPets_Users_UserId",
                table: "UserPets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPets",
                table: "UserPets");

            migrationBuilder.RenameTable(
                name: "UserPets",
                newName: "UserPet");

            migrationBuilder.RenameIndex(
                name: "IX_UserPets_PetId",
                table: "UserPet",
                newName: "IX_UserPet_PetId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserPet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPet",
                table: "UserPet",
                columns: new[] { "UserId", "PetId" });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PetId",
                table: "MedicalRecords",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPet_Pets_PetId",
                table: "UserPet",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPet_Users_UserId",
                table: "UserPet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
