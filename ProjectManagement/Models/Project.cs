using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters")]
        public string Name { get; set; } = string.Empty; // Initialize to prevent CS8618

        [Required(ErrorMessage = "Client name is required")]
        [StringLength(100, ErrorMessage = "Client name cannot be longer than 100 characters")]
        public string ClientName { get; set; } = string.Empty; // Initialize to prevent CS8618

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty; // Initialize to prevent CS8618

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);

        [Required(ErrorMessage = "Budget is required")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 1000000000, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        [Required]
        public ProjectStatus Status { get; set; } = ProjectStatus.Started;

        // Foreign key for the user who owns this project
        [Required]
        public string UserId { get; set; } = string.Empty; // Initialize to prevent CS8618

        // Navigation property to the user
        public IdentityUser? User { get; set; }

        // Timestamp for tracking when the project was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Collection of project members
        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();

        // Path to project image
        public string? ImagePath { get; set; }

        // Calculate time remaining
        [NotMapped]
        public TimeSpan TimeRemaining => EndDate - DateTime.Now;

        // Get time remaining display text
        [NotMapped]
        public string TimeRemainingText
        {
            get
            {
                var daysLeft = (int)TimeRemaining.TotalDays;

                if (daysLeft < 0)
                    return "Overdue";

                if (daysLeft < 7)
                    return $"{daysLeft} day{(daysLeft == 1 ? "" : "s")} left";

                var weeksLeft = daysLeft / 7;
                return $"{weeksLeft} week{(weeksLeft == 1 ? "" : "s")} left";
            }
        }

        // Check if project is close to deadline (5 days or less)
        [NotMapped]
        public bool IsCloseToDeadline => TimeRemaining.TotalDays <= 5 && TimeRemaining.TotalDays > 0;
    }

    // Project Member class to track members assigned to projects
    public class ProjectMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // Initialize to prevent CS8618

        // Navigation properties
        public Project Project { get; set; } = null!; // Use null! to suppress CS8618

        public IdentityUser User { get; set; } = null!; // Use null! to suppress CS8618
    }

    // Define the ProjectStatus enum within the same file
    public enum ProjectStatus
    {
        Started,
        Completed
    }
}