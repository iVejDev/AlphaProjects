using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Models;

namespace ProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjectsAsync(string userId)
        {
            // Return all projects for the specific user
            return await _context.Projects
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsByStatusAsync(string userId, ProjectStatus status)
        {
            // Return projects filtered by status for the specific user
            return await _context.Projects
                .Where(p => p.UserId == userId && p.Status == status)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            // Get a single project by ID
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            try
            {
                // Detach any potential navigation properties to avoid EF tracking issues
                project.User = null;

                // Add the project to the context
                _context.Projects.Add(project);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the created project with its generated ID
                return project;
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Error creating project: {ex.Message}");
                throw; // Rethrow to handle in the controller
            }
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            try
            {
                // Get the existing project from the database
                var existingProject = await _context.Projects.FindAsync(project.Id);

                if (existingProject == null)
                    return false;

                // Update project properties
                existingProject.Name = project.Name;
                existingProject.ClientName = project.ClientName;
                existingProject.Description = project.Description;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                existingProject.Budget = project.Budget;
                existingProject.Status = project.Status;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            try
            {
                // Find the project to delete
                var project = await _context.Projects.FindAsync(id);

                if (project == null)
                    return false;

                // Remove the project from the context
                _context.Projects.Remove(project);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}