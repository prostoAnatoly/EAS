using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations.Postgresql
{
    /// <inheritdoc />
    public partial class RENAME_IDENTITY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_infos",
                table: "identity_infos");

            migrationBuilder.DropPrimaryKey(
                name: "pk_access_token",
                table: "access_token");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "access_token");

            migrationBuilder.RenameTable(
                name: "identity_infos",
                newName: "identities");

            migrationBuilder.RenameTable(
                name: "access_token",
                newName: "access_tokens");

            migrationBuilder.RenameIndex(
                name: "ix_identity_infos_user_name",
                table: "identities",
                newName: "ix_identities_user_name");

            migrationBuilder.AddColumn<Guid>(
                name: "identity_id",
                table: "access_tokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_identities",
                table: "identities",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_access_tokens",
                table: "access_tokens",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_access_tokens_identity_id",
                table: "access_tokens",
                column: "identity_id");

            migrationBuilder.AddForeignKey(
                name: "fk_access_tokens_identities_identity_info_temp_id",
                table: "access_tokens",
                column: "identity_id",
                principalTable: "identities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_access_tokens_identities_identity_info_temp_id",
                table: "access_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identities",
                table: "identities");

            migrationBuilder.DropPrimaryKey(
                name: "pk_access_tokens",
                table: "access_tokens");

            migrationBuilder.DropIndex(
                name: "ix_access_tokens_identity_id",
                table: "access_tokens");

            migrationBuilder.DropColumn(
                name: "identity_id",
                table: "access_tokens");

            migrationBuilder.RenameTable(
                name: "identities",
                newName: "identity_infos");

            migrationBuilder.RenameTable(
                name: "access_tokens",
                newName: "access_token");

            migrationBuilder.RenameIndex(
                name: "ix_identities_user_name",
                table: "identity_infos",
                newName: "ix_identity_infos_user_name");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "access_token",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_infos",
                table: "identity_infos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_access_token",
                table: "access_token",
                column: "id");
        }
    }
}
