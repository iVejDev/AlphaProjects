// Theme Toggle Functionality
document.addEventListener('DOMContentLoaded', function () {
    // Check for saved theme preference or respect OS preference
    const savedTheme = localStorage.getItem('theme');
    const prefersDark = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;

    // Set initial theme
    if (savedTheme) {
        document.documentElement.setAttribute('data-theme', savedTheme);
        updateThemeIcon(savedTheme);
    } else if (prefersDark) {
        document.documentElement.setAttribute('data-theme', 'dark');
        updateThemeIcon('dark');
    }

    // Theme toggle button click handler
    const themeToggle = document.getElementById('theme-toggle');
    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            // Get current theme
            const currentTheme = document.documentElement.getAttribute('data-theme') || 'light';
            // Toggle theme
            const newTheme = currentTheme === 'light' ? 'dark' : 'light';

            // Save preference
            localStorage.setItem('theme', newTheme);
            // Apply theme
            document.documentElement.setAttribute('data-theme', newTheme);
            // Update icon
            updateThemeIcon(newTheme);
        });
    }

    // Update theme toggle icon based on current theme
    function updateThemeIcon(theme) {
        const themeToggle = document.getElementById('theme-toggle');
        if (!themeToggle) return;

        // Clear existing icon
        themeToggle.innerHTML = '';

        // Add appropriate icon
        if (theme === 'dark') {
            // Sun icon for switching to light mode
            themeToggle.innerHTML = '<i class="fas fa-sun"></i>';
            themeToggle.setAttribute('title', 'Switch to light mode');
        } else {
            // Moon icon for switching to dark mode
            themeToggle.innerHTML = '<i class="fas fa-moon"></i>';
            themeToggle.setAttribute('title', 'Switch to dark mode');
        }
    }
});