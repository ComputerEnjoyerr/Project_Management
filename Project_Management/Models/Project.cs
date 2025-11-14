using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models;

public partial class Project
{
    public int ProjectId { get; set; }
    [Required(ErrorMessage = "Tên dự án không được để trống")]
    public string Name { get; set; } = null!;
    [StringLength(300, ErrorMessage = "Mô tả tối đa 300 ký tự")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Phương pháp không được để trống")]
    public string? Methodology { get; set; }

    public string? Status { get; set; }
    [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
    [DataType(DataType.Date)]
    public DateOnly? StartDate { get; set; }
    [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
    [DataType(DataType.Date)]
    public DateOnly? EndDate { get; set; }

    public DateTime? CompletedAt { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn người quản lý dự án")]
    public string? CreatedByEmail { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();

    //public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    public virtual ICollection<Objective> Objectives { get; set; } = new List<Objective>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();

    public bool IsDateValid()
    {
        return StartDate <= EndDate;
    }
}
