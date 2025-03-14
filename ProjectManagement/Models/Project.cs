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
        public string Name { get; set; }

        [Required(ErrorMessage = "Client name is required")]
        [StringLength(100, ErrorMessage = "Client name cannot be longer than 100 characters")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 1000000000, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        [Required]
        public ProjectStatus Status { get; set; } = ProjectStatus.Started;

        // Foreign key for the user who owns this project
        [Required]
        public string UserId { get; set; }

        // Navigation property to the user (made optional)
        public IdentityUser? User { get; set; }

        // Timestamp for tracking when the project was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum ProjectStatus
    {
        Started,
        Completed
    }
}