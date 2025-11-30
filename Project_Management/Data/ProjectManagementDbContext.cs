using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project_Management.Models;

namespace Project_Management.Data;

public partial class ProjectManagementDbContext : DbContext
{
    public ProjectManagementDbContext()
    {
    }

    public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Milestone> Milestones { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Objective> Objectives { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    public virtual DbSet<TaskHistory> TaskHistories { get; set; }

    public virtual DbSet<TimeEntry> TimeEntries { get; set; }

    // public virtual DbSet<ApplicationUser> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Project_ManagementDb;Trusted_Connection=True;TrustServerCertificate=True;");

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<ChatRoom>(entity =>
    //    {
    //        entity.HasKey(e => e.ChatRoomId).HasName("PK__ChatRoom__69733F17C13D433A");

    //        entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");
    //        entity.Property(e => e.CreatedAt)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");
    //        entity.Property(e => e.Name).HasMaxLength(255);
    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

    //        entity.HasOne(d => d.Project).WithMany(p => p.ChatRooms)
    //            .HasForeignKey(d => d.ProjectId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__ChatRooms__Proje__5FB337D6");
    //    });

    //    modelBuilder.Entity<Comment>(entity =>
    //    {
    //        entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAA254CD6AC");

    //        entity.Property(e => e.CommentId).HasColumnName("CommentID");
    //        entity.Property(e => e.CreatedAt)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");
    //        entity.Property(e => e.TaskId).HasColumnName("TaskID");
    //        entity.Property(e => e.UserId).HasColumnName("UserID");

    //        entity.HasOne(d => d.Task).WithMany(p => p.Comments)
    //            .HasForeignKey(d => d.TaskId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Comments__TaskID__5AEE82B9");

    //        //entity.HasOne(d => d.User).WithMany(p => p.Comments)
    //        //    .HasForeignKey(d => d.UserId)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__Comments__UserID__5BE2A6F2");
    //    });

    //    modelBuilder.Entity<Message>(entity =>
    //    {
    //        entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037CCDC71F5C");

    //        entity.Property(e => e.MessageId).HasColumnName("MessageID");
    //        entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");
    //        entity.Property(e => e.SenderId).HasColumnName("SenderID");
    //        entity.Property(e => e.SentAt)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");

    //        entity.HasOne(d => d.ChatRoom).WithMany(p => p.Messages)
    //            .HasForeignKey(d => d.ChatRoomId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Messages__ChatRo__6383C8BA");

    //        //entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
    //        //    .HasForeignKey(d => d.SenderId)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__Messages__Sender__6477ECF3");
    //    });

    //    modelBuilder.Entity<Milestone>(entity =>
    //    {
    //        entity.HasKey(e => e.MilestoneId).HasName("PK__Mileston__09C48058399B51BD");

    //        entity.Property(e => e.MilestoneId).HasColumnName("MilestoneID");
    //        entity.Property(e => e.CompletedAt).HasColumnType("datetime");
    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
    //        entity.Property(e => e.Title).HasMaxLength(100);

    //        entity.HasOne(d => d.Project).WithMany(p => p.Milestones)
    //            .HasForeignKey(d => d.ProjectId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Milestone__Proje__47DBAE45");
    //    });

    //    modelBuilder.Entity<Notification>(entity =>
    //    {
    //        entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32EA5C8CF1");

    //        entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
    //        entity.Property(e => e.CreatedAt)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");
    //        entity.Property(e => e.IsRead).HasDefaultValue(false);
    //        entity.Property(e => e.Type).HasMaxLength(50);
    //        entity.Property(e => e.UserId).HasColumnName("UserID");

    //        //entity.HasOne(d => d.User).WithMany(p => p.Notifications)
    //        //    .HasForeignKey(d => d.UserId)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__Notificat__UserI__71D1E811");
    //    });

    //    modelBuilder.Entity<Objective>(entity =>
    //    {
    //        entity.HasKey(e => e.ObjectiveId).HasName("PK__Objectiv__8C56338D492B22C6");

    //        entity.Property(e => e.ObjectiveId).HasColumnName("ObjectiveID");
    //        entity.Property(e => e.CompletedAt).HasColumnType("datetime");
    //        entity.Property(e => e.MilestoneId).HasColumnName("MilestoneID");
    //        entity.Property(e => e.Priority)
    //            .HasMaxLength(50)
    //            .HasDefaultValue("Normal");
    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
    //        entity.Property(e => e.SprintId).HasColumnName("SprintID");
    //        entity.Property(e => e.Status)
    //            .HasMaxLength(50)
    //            .HasDefaultValue("Todo");
    //        entity.Property(e => e.Title).HasMaxLength(100);

    //        //entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.ObjectiveAssignedToNavigations)
    //        //    .HasForeignKey(d => d.AssignedTo)
    //        //    .HasConstraintName("FK__Objective__Assig__5629CD9C");

    //        //entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ObjectiveCreatedByNavigations)
    //        //    .HasForeignKey(d => d.CreatedBy)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__Objective__Creat__571DF1D5");

    //        entity.HasOne(d => d.Milestone).WithMany(p => p.Objectives)
    //            .HasForeignKey(d => d.MilestoneId)
    //            .HasConstraintName("FK__Objective__Miles__5535A963");

    //        entity.HasOne(d => d.Project).WithMany(p => p.Objectives)
    //            .HasForeignKey(d => d.ProjectId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Objective__Proje__534D60F1");

    //        entity.HasOne(d => d.Sprint).WithMany(p => p.Objectives)
    //            .HasForeignKey(d => d.SprintId)
    //            .HasConstraintName("FK__Objective__Sprin__5441852A");
    //    });

    //    modelBuilder.Entity<Project>(entity =>
    //    {
    //        entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED09D3EDD14");

    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
    //        entity.Property(e => e.CompletedAt).HasColumnType("datetime");
    //        entity.Property(e => e.CreatedAt)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");
    //        entity.Property(e => e.Methodology)
    //            .HasMaxLength(50)
    //            .HasDefaultValue("Agile");
    //        entity.Property(e => e.Name).HasMaxLength(255);
    //        entity.Property(e => e.Status)
    //            .HasMaxLength(50)
    //            .HasDefaultValue("Active");

    //        //entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Projects)
    //        //    .HasForeignKey(d => d.CreatedBy)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__Projects__Create__3F466844");
    //    });

    //    modelBuilder.Entity<ProjectMember>(entity =>
    //    {
    //        entity.HasKey(e => e.ProjectMemberId).HasName("PK__ProjectM__E4E9983CDE7A9092");

    //        entity.Property(e => e.ProjectMemberId).HasColumnName("ProjectMemberID");
    //        entity.Property(e => e.JoinedAt)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");
    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
    //        entity.Property(e => e.Role)
    //            .HasMaxLength(50)
    //            .HasDefaultValue("Member");
    //        entity.Property(e => e.UserId).HasColumnName("UserID");

    //        entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
    //            .HasForeignKey(d => d.ProjectId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__ProjectMe__Proje__440B1D61");

    //        //entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
    //        //    .HasForeignKey(d => d.UserId)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__ProjectMe__UserI__44FF419A");
    //    });

    //    modelBuilder.Entity<Sprint>(entity =>
    //    {
    //        entity.HasKey(e => e.SprintId).HasName("PK__Sprints__29F16AE0E62AC214");

    //        entity.Property(e => e.SprintId).HasColumnName("SprintID");
    //        entity.Property(e => e.Name).HasMaxLength(100);
    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
    //        entity.Property(e => e.Status)
    //            .HasMaxLength(50)
    //            .HasDefaultValue("Planned");

    //        entity.HasOne(d => d.Project).WithMany(p => p.Sprints)
    //            .HasForeignKey(d => d.ProjectId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Sprints__Project__4BAC3F29");
    //    });

    //    modelBuilder.Entity<Stage>(entity =>
    //    {
    //        entity.HasKey(e => e.StageId).HasName("PK__Stages__03EB7AF8EDB7C611");

    //        entity.Property(e => e.StageId).HasColumnName("StageID");
    //        entity.Property(e => e.Name).HasMaxLength(100);
    //        entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
    //        entity.Property(e => e.Status).HasMaxLength(50);

    //        entity.HasOne(d => d.Project).WithMany(p => p.Stages)
    //            .HasForeignKey(d => d.ProjectId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__Stages__ProjectI__4E88ABD4");
    //    });

    //    modelBuilder.Entity<TaskHistory>(entity =>
    //    {
    //        entity.HasKey(e => e.HistoryId).HasName("PK__TaskHist__4D7B4ADD46EB060A");

    //        entity.ToTable("TaskHistory");

    //        entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
    //        entity.Property(e => e.ChangeDate)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasColumnType("datetime");
    //        entity.Property(e => e.ChangedByUserId).HasColumnName("ChangedByUserID");
    //        entity.Property(e => e.NewStatus).HasMaxLength(50);
    //        entity.Property(e => e.OldStatus).HasMaxLength(50);
    //        entity.Property(e => e.TaskId).HasColumnName("TaskID");

    //        //entity.HasOne(d => d.ChangedByUser).WithMany(p => p.TaskHistories)
    //        //    .HasForeignKey(d => d.ChangedByUserId)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__TaskHisto__Chang__6D0D32F4");

    //        entity.HasOne(d => d.Task).WithMany(p => p.TaskHistories)
    //            .HasForeignKey(d => d.TaskId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__TaskHisto__TaskI__6C190EBB");
    //    });

    //    modelBuilder.Entity<TimeEntry>(entity =>
    //    {
    //        entity.HasKey(e => e.TimeEntryId).HasName("PK__TimeEntr__36FCE7E963276B75");

    //        entity.Property(e => e.TimeEntryId).HasColumnName("TimeEntryID");
    //        entity.Property(e => e.Duration).HasComputedColumnSql("(datediff(minute,[StartTime],[EndTime]))", false);
    //        entity.Property(e => e.EndTime).HasColumnType("datetime");
    //        entity.Property(e => e.Note).HasMaxLength(255);
    //        entity.Property(e => e.StartTime).HasColumnType("datetime");
    //        entity.Property(e => e.TaskId).HasColumnName("TaskID");
    //        entity.Property(e => e.UserId).HasColumnName("UserID");

    //        entity.HasOne(d => d.Task).WithMany(p => p.TimeEntries)
    //            .HasForeignKey(d => d.TaskId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK__TimeEntri__TaskI__6754599E");

    //        //entity.HasOne(d => d.User).WithMany(p => p.TimeEntries)
    //        //    .HasForeignKey(d => d.UserId)
    //        //    .HasPrincipalKey(u => u.Id) // 👈 thêm dòng này
    //        //    .OnDelete(DeleteBehavior.ClientSetNull)
    //        //    .HasConstraintName("FK__TimeEntri__UserI__68487DD7");
    //    });

    //    //modelBuilder.Entity<User>(entity =>
    //    //{
    //    //    entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC66835DB4");

    //    //    entity.HasIndex(e => e.Email, "UQ__Users__A9D10534EC1CA12B").IsUnique();

    //    //    entity.Property(e => e.UserId).HasColumnName("UserID");
    //    //    entity.Property(e => e.CreatedAt)
    //    //        .HasDefaultValueSql("(getdate())")
    //    //        .HasColumnType("datetime");
    //    //    entity.Property(e => e.Email).HasMaxLength(255);
    //    //    entity.Property(e => e.FullName).HasMaxLength(255);
    //    //    entity.Property(e => e.PasswordHash).HasMaxLength(255);
    //    //    entity.Property(e => e.Role)
    //    //        .HasMaxLength(50)
    //    //        .HasDefaultValue("Member");
    //    //    entity.Property(e => e.Username).HasMaxLength(100);
    //    //});

    //    OnModelCreatingPartial(modelBuilder);
    //}

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
