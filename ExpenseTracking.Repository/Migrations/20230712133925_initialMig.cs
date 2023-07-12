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
                    { 1, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2776), true, "Beslenme/Gıda", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2788), true, "Temizlik", null },
                    { 3, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2792), true, "Teknolojik Alışveriş", null },
                    { 4, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2793), true, "Eğlence", null },
                    { 5, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2794), true, "Sağlık", null },
                    { 6, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2796), true, "Acil İhtiyaç", null },
                    { 7, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(2797), true, "Giyim", null }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3333), true, "Nakit", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3336), true, "Kredi", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3512), true, "Admin", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3514), true, "Standart", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "IsActive", "LastActivity", "Name", "Password", "RefreshToken", "RefreshTokenExpireDate", "RoleId", "Surname", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3618), "enes@gmail.com", true, null, "Enes", "ens123", null, null, 1, "Arat", null },
                    { 2, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3620), "eren@gmail.com", true, null, "Eren", "ern123", null, null, 2, "Arat", null }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Cost", "CreatedBy", "CreatedDate", "Description", "IsActive", "Name", "PaymentTypeId", "TransactionDate", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 560m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3095), "Hazırlanan listeye göre aylık market alışverişi yapıldı.", true, "Market Alışverişi", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3105), null, 1 },
                    { 2, 2, 300m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3106), "Bulaşık deterjanı, peçete ve tuvalet kağıdı alındı.", true, "Deterjan ve Peçete", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3108), null, 1 },
                    { 3, 3, 3400m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3109), "X markanın şarjlı elektrikli süpürgesinden alındı.", true, "Kablosuz Süpürge", 2, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3110), null, 2 },
                    { 4, 4, 850m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3111), "Duman Grubu'nun 22 Temmuz'daki Harbiye açık hava konserine bilet alındı.", true, "Duman Konseri", 2, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3112), null, 2 },
                    { 5, 5, 200m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3113), "KBB Doktor kontrolüne gidildi ve ilaç alındı.", true, "KBB Doktor Kontrolü", 2, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3114), null, 1 },
                    { 6, 6, 250m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3115), "Telefonun yırtılan kılıfının yerine yenisi alındı.", true, "Telefon Kılıfı", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3116), null, 2 },
                    { 7, 7, 1190m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3117), "Eskiyen ayakkabı yerine Converse ayakkabı alındı.", true, "Ayakkabı", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3119), null, 2 },
                    { 8, 1, 850m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3119), "Akşam iş dönüşü dışarıda arkadaşlarla pizza ziyafeti.", true, "Pizza ve İçecek", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3121), null, 1 },
                    { 9, 2, 300m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3122), "Kuaförde traş olunup, özel şampuan alındı.", true, "Kuaför ve Şampuan", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3123), null, 1 },
                    { 10, 3, 430m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3124), "Telefon ve tablet için powerbank alındı.", true, "Powerbank", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3125), null, 2 },
                    { 11, 4, 85m, "SYSTEM", new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3126), "Eskiyen ayakkabı yerine Converse ayakkabı alındı.", true, "Sinema", 1, new DateTime(2023, 7, 12, 16, 39, 25, 58, DateTimeKind.Local).AddTicks(3127), null, 2 }
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
