using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using ProjectManagement.Services;

namespace ProjectManagement.Controllers
{
    [Authorize] // Only authenticated users can access this controller
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectController(IProjectService projectService, UserManager<IdentityUser> userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
        }

        // GET: /Project
        public async Task<IActionResult> Index(string filter = "all")
        {
            // Get the currently logged-in user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get projects based on the filter
            List<Project> projects;
            ViewBag.CurrentFilter = filter;

            switch (filter.ToLower())
            {
                case "started":
                    projects = await _projectService.GetProjectsByStatusAsync(userId, ProjectStatus.Started);
                    break;

                case "completed":
                    projects = await _projectService.GetProjectsByStatusAsync(userId, ProjectStatus.Completed);
                    break;

                default:
                    projects = await _projectService.GetAllProjectsAsync(userId);
                    break;
            }

            // Get user's full name for display
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserFullName = user.UserName; // You might want to store the full name in a custom property

            return View(projects);
        }

        // GET: /Project/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: /Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            try
            {
                // Get the current user ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    // If user is not authenticated, return unauthorized
                    return Unauthorized("User must be logged in to create a project.");
                }

                // Set the user ID
                project.UserId = userId;

                // Clear the User navigation property to avoid EF Core tracking issues
                project.User = null;

                // Check if we have the required fields before proceeding
                if (string.IsNullOrEmpty(project.Name) || string.IsNullOrEmpty(project.ClientName))
                {
                    ModelState.AddModelError(string.Empty, "Project name and client name are required.");
                    return Json(new { success = false, errors = new[] { "Project name and client name are required." } });
                }

                // Set default creation date if not provided
                if (project.CreatedAt == default)
                {
                    project.CreatedAt = DateTime.Now;
                }

                // Create the project
                var createdProject = await _projectService.CreateProjectAsync(project);

                // Return success response
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error details
                Console.WriteLine($"Error creating project: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                // Add error to model state
                ModelState.AddModelError(string.Empty, "Error creating project: " + ex.Message);

                // Return error response
                return Json(new
                {
                    success = false,
                    errors = new[] { "Error creating project: " + ex.Message }
                });
            }
        }

        // GET: /Project/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            // Check if the current user owns this project
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (project.UserId != userId)
            {
                return Forbid();
            }

            return Json(project);
        }

        // POST: /Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            // Set the user ID to the current user before validation
            project.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the existing project to preserve CreatedAt value
                    var existingProject = await _projectService.GetProjectByIdAsync(id);
                    if (existingProject == null)
                    {
                        return NotFound();
                    }

                    // Set the CreatedAt to the original value
                    project.CreatedAt = existingProject.CreatedAt;

                    // Update the project
                    var success = await _projectService.UpdateProjectAsync(project);
                    if (!success)
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the error
                    ModelState.AddModelError(string.Empty, "Error updating project: " + ex.Message);
                }
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        // POST: /Project/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // Get the project
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            // Check if the current user owns this project
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (project.UserId != userId)
            {
                return Forbid();
            }

            // Delete the project
            var success = await _projectService.DeleteProjectAsync(id);
            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}