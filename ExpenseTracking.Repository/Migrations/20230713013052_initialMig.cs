using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracking.Repository.Migrations
{
    /// <inheritdoc />
    public partial class initialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8496), true, "Beslenme/Gıda", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8509), true, "Temizlik", null },
                    { 3, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8510), true, "Teknolojik Alışveriş", null },
                    { 4, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8512), true, "Eğlence", null },
                    { 5, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8513), true, "Sağlık", null },
                    { 6, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8514), true, "Acil İhtiyaç", null },
                    { 7, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8516), true, "Giyim", null }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8936), true, "Nakit", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8938), true, "Kredi", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(9053), true, "Admin", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(9055), true, "Standart", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "IsActive", "LastActivity", "Name", "Password", "RefreshToken", "RefreshTokenExpireDate", "RoleId", "Surname", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(9171), "enes@gmail.com", true, null, "Enes", "$2a$11$Mnqi9RoGPlhOgFalgcahL.okqa2tnZgz3gtkTs7oeHFVmlSC507GS", null, null, 1, "Arat", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(9174), "eren@gmail.com", true, null, "Eren", "$2a$11$Owh2PufGb1wWYTUt4bNUK.iqTrAMi5E09oRPFSIKVnuRawxm.flIO", null, null, 2, "Arat", null }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Cost", "CreatedBy", "CreatedDate", "Description", "IsActive", "Name", "PaymentTypeId", "TransactionDate", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 560m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8773), "Hazırlanan listeye göre aylık market alışverişi yapıldı.", true, "Market Alışverişi", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8778), null, 1 },
                    { 2, 2, 300m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8780), "Bulaşık deterjanı, peçete ve tuvalet kağıdı alındı.", true, "Deterjan ve Peçete", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8782), null, 1 },
                    { 3, 3, 3400m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8783), "X markanın şarjlı elektrikli süpürgesinden alındı.", true, "Kablosuz Süpürge", 2, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8785), null, 2 },
                    { 4, 4, 850m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8786), "Duman Grubu'nun 22 Temmuz'daki Harbiye açık hava konserine bilet alındı.", true, "Duman Konseri", 2, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8788), null, 2 },
                    { 5, 5, 200m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8789), "KBB Doktor kontrolüne gidildi ve ilaç alındı.", true, "KBB Doktor Kontrolü", 2, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8790), null, 1 },
                    { 6, 6, 250m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8791), "Telefonun yırtılan kılıfının yerine yenisi alındı.", true, "Telefon Kılıfı", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8793), null, 2 },
                    { 7, 7, 1190m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8794), "Eskiyen ayakkabı yerine Converse ayakkabı alındı.", true, "Ayakkabı", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8795), null, 2 },
                    { 8, 1, 850m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8796), "Akşam iş dönüşü dışarıda arkadaşlarla pizza ziyafeti.", true, "Pizza ve İçecek", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8798), null, 1 },
                    { 9, 2, 300m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8799), "Kuaförde traş olunup, özel şampuan alındı.", true, "Kuaför ve Şampuan", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8800), null, 1 },
                    { 10, 3, 430m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8801), "Telefon ve tablet için powerbank alındı.", true, "Powerbank", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8803), null, 2 },
                    { 11, 4, 85m, "SYSTEM", new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8804), "Eskiyen ayakkabı yerine Converse ayakkabı alındı.", true, "Sinema", 1, new DateTime(2023, 7, 13, 4, 30, 51, 934, DateTimeKind.Local).AddTicks(8805), null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentTypeId",
                table: "Expenses",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
