﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SD.IdentitySystem.Repository.Migrations
{
    public partial class v440 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoSystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminLoginId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: true),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SavedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSystem", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_InfoSystem_Number", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "LoginRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartitionIndex = table.Column<int>(type: "int", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SavedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRecord", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PrivateKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SavedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_User_Number", x => x.Number);
                    table.UniqueConstraint("AK_User_PrivateKey", x => x.PrivateKey);
                });

            migrationBuilder.CreateTable(
                name: "Authority",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoSystemNo = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    AuthorityPath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Namespace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SavedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Authority_InfoSystem_InfoSystemNo",
                        column: x => x.InfoSystemNo,
                        principalTable: "InfoSystem",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoSystemNo = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsRoot = table.Column<bool>(type: "bit", nullable: false),
                    ParentNode_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SavedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Menu_InfoSystem_InfoSystemNo",
                        column: x => x.InfoSystemNo,
                        principalTable: "InfoSystem",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentNode_Id",
                        column: x => x.ParentNode_Id,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoSystemNo = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SavedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Role_InfoSystem_InfoSystemNo",
                        column: x => x.InfoSystemNo,
                        principalTable: "InfoSystem",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menu_Authority",
                columns: table => new
                {
                    Authority_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Menu_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu_Authority", x => new { x.Authority_Id, x.Menu_Id });
                    table.ForeignKey(
                        name: "FK_Menu_Authority_Authority_Authority_Id",
                        column: x => x.Authority_Id,
                        principalTable: "Authority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menu_Authority_Menu_Menu_Id",
                        column: x => x.Menu_Id,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Authority",
                columns: table => new
                {
                    Authority_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Authority", x => new { x.Authority_Id, x.Role_Id });
                    table.ForeignKey(
                        name: "FK_Role_Authority_Authority_Authority_Id",
                        column: x => x.Authority_Id,
                        principalTable: "Authority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Authority_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                columns: table => new
                {
                    Role_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role", x => new { x.Role_Id, x.User_Id });
                    table.ForeignKey(
                        name: "FK_User_Role_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authority_AddedTime",
                table: "Authority",
                column: "AddedTime")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Authority_InfoSystemNo",
                table: "Authority",
                column: "InfoSystemNo");

            migrationBuilder.CreateIndex(
                name: "IX_InfoSystem_AddedTime",
                table: "InfoSystem",
                column: "AddedTime")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginRecord_AddedTime",
                table: "LoginRecord",
                column: "AddedTime")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_InfoSystemNo",
                table: "Menu",
                column: "InfoSystemNo");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentNode_Id",
                table: "Menu",
                column: "ParentNode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Authority_Menu_Id",
                table: "Menu_Authority",
                column: "Menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_AddedTime",
                table: "Role",
                column: "AddedTime")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_InfoSystemNo",
                table: "Role",
                column: "InfoSystemNo");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Authority_Role_Id",
                table: "Role_Authority",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_AddedTime",
                table: "User",
                column: "AddedTime")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_User_Id",
                table: "User_Role",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginRecord");

            migrationBuilder.DropTable(
                name: "Menu_Authority");

            migrationBuilder.DropTable(
                name: "Role_Authority");

            migrationBuilder.DropTable(
                name: "User_Role");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Authority");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "InfoSystem");
        }
    }
}
