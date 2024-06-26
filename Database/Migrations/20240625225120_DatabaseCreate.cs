using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Line1 = table.Column<string>(type: "varchar(200)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: false),
                    Postcode = table.Column<string>(type: "varchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    GMC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Line1", "PersonId", "Postcode" },
                values: new object[,]
                {
                    { 1, "Brynteg", "17 Cae Rhos", 1, "LL78 8JF" },
                    { 2, "Exeter", "8 Lonsdale Road", 2, "EX1 3DW" },
                    { 3, "Brighouse", "36 Close Lea", 3, "HD6 3AR" },
                    { 4, "Bournemouth", "14 The Avenue", 4, "BH2 6LT" },
                    { 5, "Plymouth", "22 Oakdale Road", 5, "PL3 6QN" },
                    { 6, "Newcastle", "11 The Crescent", 6, "NE2 4AU" },
                    { 7, "Leeds", "19 The Green", 7, "LS12 1AQ" },
                    { 8, "Sheffield", "25 The Drive", 8, "S10 2DF" },
                    { 9, "Bristol", "30 The Avenue", 9, "BS6 5BX" },
                    { 10, "Nottingham", "42 The Close", 10, "NG1 4BH" },
                    { 11, "Oxford", "15 The Square", 11, "OX1 1AQ" },
                    { 12, "Cambridge", "28 The Street", 12, "CB2 3TN" },
                    { 13, "London", "31 The Lane", 13, "SW1A 1AA" },
                    { 14, "Manchester", "18 The Grove", 14, "M1 5FN" },
                    { 15, "Birmingham", "24 The Rise", 15, "B1 2EA" },
                    { 16, "Leicester", "39 The Hill", 16, "LE1 5DE" },
                    { 17, "Cardiff", "45 The View", 17, "CF10 1BH" },
                    { 18, "Edinburgh", "50 The Way", 18, "EH1 3YL" },
                    { 19, "Glasgow", "55 The Place", 19, "G1 1XE" },
                    { 20, "Belfast", "60 The Street", 20, "BT1 5GS" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "FirstName", "GMC", "LastName" },
                values: new object[,]
                {
                    { 1, "Karilynn", 1169492, "Rock" },
                    { 2, "Arlyn", 8836597, "Coetzee" },
                    { 3, "Carly", 9548968, "Heeran" },
                    { 4, "Rae", 1807233, "Tuft" },
                    { 5, "Eugenius", 3229745, "Uden" },
                    { 6, "Constancia", 7208570, "Salerno" },
                    { 7, "Harmon", 1215810, "Stent" },
                    { 8, "Adriano", 6428880, "Jull" },
                    { 9, "Garrek", 9279644, "Frenzl" },
                    { 10, "Letitia", 3699271, "Grabert" },
                    { 11, "Dulcia", 4793921, "Chadbourn" },
                    { 12, "Hilliard", 8854603, "Glendza" },
                    { 13, "Rena", 9442324, "McCarl" },
                    { 14, "Tobiah", 9488055, "Wheldon" },
                    { 15, "Morgun", 2520979, "Downe" },
                    { 16, "Cullan", 7708262, "Weiser" },
                    { 17, "Niles", 7046029, "Kief" },
                    { 18, "Randi", 3086851, "McLorinan" },
                    { 19, "Virgil", 3165201, "Sperry" },
                    { 20, "Hoyt", 1795805, "Turbern" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
