using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleRpgEntities.Migrations
{
    public partial class AddEquipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentListId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponId = table.Column<int>(type: "int", nullable: true),
                    ArmorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentList_Items_ArmorId",
                        column: x => x.ArmorId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentList_Items_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquipmentListId",
                table: "Players",
                column: "EquipmentListId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentList_ArmorId",
                table: "EquipmentList",
                column: "ArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentList_WeaponId",
                table: "EquipmentList",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_EquipmentList_EquipmentListId",
                table: "Players",
                column: "EquipmentListId",
                principalTable: "EquipmentList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_EquipmentList_EquipmentListId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "EquipmentList");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquipmentListId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquipmentListId",
                table: "Players");
        }
    }
}
