using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fist_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    zip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    pub_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publisher_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    state = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.pub_id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_desc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    pub_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    advance = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    royalty = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ytd_sales = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    published_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_Book_Publisher",
                        column: x => x.pub_id,
                        principalTable: "Publisher",
                        principalColumn: "pub_id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fist_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    pub_id = table.Column<int>(type: "int", nullable: false),
                    hire_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_User_Publisher",
                        column: x => x.pub_id,
                        principalTable: "Publisher",
                        principalColumn: "pub_id");
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: true),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    author_order = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    royality_percentage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_BookAuthor_Author",
                        column: x => x.author_id,
                        principalTable: "Author",
                        principalColumn: "author_id");
                    table.ForeignKey(
                        name: "FK_BookAuthor_Book",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_pub_id",
                table: "Book",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_author_id",
                table: "BookAuthor",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_book_id",
                table: "BookAuthor",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_pub_id",
                table: "User",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
