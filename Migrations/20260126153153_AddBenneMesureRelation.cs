using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionBornesCollecte.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBenneMesureRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mesures_BenneId",
                table: "Mesures",
                column: "BenneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesures_Bennes_BenneId",
                table: "Mesures",
                column: "BenneId",
                principalTable: "Bennes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesures_Bennes_BenneId",
                table: "Mesures");

            migrationBuilder.DropIndex(
                name: "IX_Mesures_BenneId",
                table: "Mesures");
        }
    }
}
