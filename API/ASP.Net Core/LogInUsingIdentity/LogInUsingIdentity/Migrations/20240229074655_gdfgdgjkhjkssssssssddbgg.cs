using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogInUsingIdentity.Migrations
{
    /// <inheritdoc />
    public partial class gdfgdgjkhjkssssssssddbgg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AdressId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "AspNetUsers",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AdressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "AspNetUsers",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AdressId",
                table: "AspNetUsers",
                column: "AdressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
