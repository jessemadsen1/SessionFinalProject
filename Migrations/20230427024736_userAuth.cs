using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class userAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_userId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "Authorzation");

            migrationBuilder.RenameColumn(
                name: "ExpiersOn",
                table: "SignupCodes",
                newName: "ExpiresOn");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Sessions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "sessionCode",
                table: "Sessions",
                newName: "SessionCode");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_userId",
                table: "Sessions",
                newName: "IX_Sessions_UserId");

            migrationBuilder.CreateTable(
                name: "Authorization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthorizations",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorizationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthorizations", x => new { x.UserId, x.AuthorizationId });
                    table.ForeignKey(
                        name: "FK_UserAuthorizations_Authorization_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "Authorization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAuthorizations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthorizations_AuthorizationId",
                table: "UserAuthorizations",
                column: "AuthorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "UserAuthorizations");

            migrationBuilder.DropTable(
                name: "Authorization");

            migrationBuilder.RenameColumn(
                name: "ExpiresOn",
                table: "SignupCodes",
                newName: "ExpiersOn");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sessions",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "SessionCode",
                table: "Sessions",
                newName: "sessionCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sessions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                newName: "IX_Sessions_userId");

            migrationBuilder.CreateTable(
                name: "Authorzation",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorzation", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_userId",
                table: "Sessions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
