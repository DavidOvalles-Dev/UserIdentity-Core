﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserIdentity_Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class otroCambioaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_AspNetUsers_UserId",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Productos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_AspNetUsers_UserId",
                table: "Productos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_AspNetUsers_UserId",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Productos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_AspNetUsers_UserId",
                table: "Productos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
