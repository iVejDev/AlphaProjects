using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.ViewModels
{
    public class ProjectFormViewModel
    {
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
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);

        [Required(ErrorMessage = "Budget is required")]
        [Range(0, 1000000000, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Started;
    }
}