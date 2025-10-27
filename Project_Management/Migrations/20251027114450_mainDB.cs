using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management.Migrations
{
    /// <inheritdoc />
    public partial class mainDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Member"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC66835DB4", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E32EA5C8CF1", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__71D1E811",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Methodology = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Agile"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Active"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects__761ABED09D3EDD14", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK__Projects__Create__3F466844",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    ChatRoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChatRoom__69733F17C13D433A", x => x.ChatRoomID);
                    table.ForeignKey(
                        name: "FK__ChatRooms__Proje__5FB337D6",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    MilestoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mileston__09C48058399B51BD", x => x.MilestoneID);
                    table.ForeignKey(
                        name: "FK__Milestone__Proje__47DBAE45",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "ProjectMembers",
                columns: table => new
                {
                    ProjectMemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Member"),
                    JoinedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectM__E4E9983CDE7A9092", x => x.ProjectMemberID);
                    table.ForeignKey(
                        name: "FK__ProjectMe__Proje__440B1D61",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK__ProjectMe__UserI__44FF419A",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Goal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Planned")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sprints__29F16AE0E62AC214", x => x.SprintID);
                    table.ForeignKey(
                        name: "FK__Sprints__Project__4BAC3F29",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    StageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stages__03EB7AF8EDB7C611", x => x.StageID);
                    table.ForeignKey(
                        name: "FK__Stages__ProjectI__4E88ABD4",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatRoomID = table.Column<int>(type: "int", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__C87C037CCDC71F5C", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK__Messages__ChatRo__6383C8BA",
                        column: x => x.ChatRoomID,
                        principalTable: "ChatRooms",
                        principalColumn: "ChatRoomID");
                    table.ForeignKey(
                        name: "FK__Messages__Sender__6477ECF3",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    ObjectiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    SprintID = table.Column<int>(type: "int", nullable: true),
                    MilestoneID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Normal"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Todo"),
                    AssignedTo = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Objectiv__8C56338D492B22C6", x => x.ObjectiveID);
                    table.ForeignKey(
                        name: "FK__Objective__Assig__5629CD9C",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Objective__Creat__571DF1D5",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Objective__Miles__5535A963",
                        column: x => x.MilestoneID,
                        principalTable: "Milestones",
                        principalColumn: "MilestoneID");
                    table.ForeignKey(
                        name: "FK__Objective__Proje__534D60F1",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK__Objective__Sprin__5441852A",
                        column: x => x.SprintID,
                        principalTable: "Sprints",
                        principalColumn: "SprintID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__C3B4DFAA254CD6AC", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__Comments__TaskID__5AEE82B9",
                        column: x => x.TaskID,
                        principalTable: "Objectives",
                        principalColumn: "ObjectiveID");
                    table.ForeignKey(
                        name: "FK__Comments__UserID__5BE2A6F2",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "TaskHistory",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserID = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    OldStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskHist__4D7B4ADD46EB060A", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK__TaskHisto__Chang__6D0D32F4",
                        column: x => x.ChangedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__TaskHisto__TaskI__6C190EBB",
                        column: x => x.TaskID,
                        principalTable: "Objectives",
                        principalColumn: "ObjectiveID");
                });

            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    TimeEntryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true, computedColumnSql: "(datediff(minute,[StartTime],[EndTime]))", stored: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TimeEntr__36FCE7E963276B75", x => x.TimeEntryID);
                    table.ForeignKey(
                        name: "FK__TimeEntri__TaskI__6754599E",
                        column: x => x.TaskID,
                        principalTable: "Objectives",
                        principalColumn: "ObjectiveID");
                    table.ForeignKey(
                        name: "FK__TimeEntri__UserI__68487DD7",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_ProjectID",
                table: "ChatRooms",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TaskID",
                table: "Comments",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomID",
                table: "Messages",
                column: "ChatRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProjectID",
                table: "Milestones",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_AssignedTo",
                table: "Objectives",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_CreatedBy",
                table: "Objectives",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_MilestoneID",
                table: "Objectives",
                column: "MilestoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ProjectID",
                table: "Objectives",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_SprintID",
                table: "Objectives",
                column: "SprintID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_ProjectID",
                table: "ProjectMembers",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_UserID",
                table: "ProjectMembers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectID",
                table: "Sprints",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ProjectID",
                table: "Stages",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_ChangedByUserID",
                table: "TaskHistory",
                column: "ChangedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_TaskID",
                table: "TaskHistory",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_TaskID",
                table: "TimeEntries",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_UserID",
                table: "TimeEntries",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534EC1CA12B",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ProjectMembers");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "TaskHistory");

            migrationBuilder.DropTable(
                name: "TimeEntries");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
