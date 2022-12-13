using Microsoft.EntityFrameworkCore.Migrations;

namespace finalAPI.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51d8232d-d4c2-4518-9367-6d2c8941457b", "AQAAAAEAACcQAAAAEP4tWOXgYo9vp3X/vQtoxkLj+y4xOdGLf/d2J39DUGOGMst9WSCM9TddJNh16cdRwg==", "ceae98e7-61e6-456f-9152-3aeecc983737" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e925a6bb-4439-4547-a61f-5055b7343316", "AQAAAAEAACcQAAAAEF5oEVtIvhwprWYHkZDe01gpO5UE/GAz7cyqHIKZwPzo2yR0Zrx1TKCitb+NZpkzlg==", "b15b8e69-59a8-4b78-a09f-3a6437c6a5f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1d2bae2-57a3-42e0-b422-3383ae0e621d", "AQAAAAEAACcQAAAAEL79pWhA/eGySNzVsNHL72M1RigxlqoQXIDyMSb89TaHXWGEpKAOPrE2NIfkoZhmAg==", "eb3548c7-c95c-417d-b52b-f91932b81853" });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "Text", "ThreadId", "UserId" },
                values: new object[] { 6, "j'Espere jene coule pas", 1, "11111111-1111-1111-111111111113" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "971a72dd-0e9f-4e85-a45f-4dcbf20ee085", "AQAAAAEAACcQAAAAEBKvBWhf6rI+Gy0mNoO4viZAYm1/OKU/fL1FxcX4jn0N1Lvbm7wYxhB7KU01tu6hxw==", "f8fccc3c-f70c-44ed-91ee-de4162ef2dbd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c44e09cc-0871-4be2-914c-c2a88d970e1c", "AQAAAAEAACcQAAAAEBx0WeMhW5OqKm8As79FuSkoNLyzs1fepeV8fYbtf7QLi5NcHrxIG9o0Xvv5QG3rBA==", "2e7c71fa-f929-478d-95e7-052dc97265a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "962846de-0b3c-4ef4-ac76-c6a2ee575ee2", "AQAAAAEAACcQAAAAECR6rSXzxEMMmwaFHM0EAHkHeKfLVVAAwfm97TyNAO0hhD4HtipipD6jD+CORt6pAQ==", "bbaecfad-fe2f-46b6-a71c-713134f4d5f1" });
        }
    }
}
