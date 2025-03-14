using ProjectManagement.Models;

namespace ProjectManagement.Services
{
    public interface IProjectService
    {
        // Get all projects for a specific user
        Task<List<Project>> GetAllProjectsAsync(string userId);

        // Get projects filtered by status for a specific user
        Task<List<Project>> GetProjectsByStatusAsync(string userId, ProjectStatus status);

        // Get a single project by ID
        Task<Project> GetProjectByIdAsync(int id);

        // Create a new project
        Task<Project> CreateProjectAsync(Project project);

        // Update an existing project
        Task<bool> UpdateProjectAsync(Project project);

        // Delete a project
        Task<bool> DeleteProjectAsync(int id);
    }
}