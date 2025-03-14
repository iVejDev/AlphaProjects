// Common JavaScript for the entire site

document.addEventListener('DOMContentLoaded', function () {
    console.log('Site.js loaded');

    // User avatar dropdown toggle
    const avatarToggle = document.getElementById('avatar-dropdown-toggle');
    const avatarMenu = document.getElementById('avatar-dropdown-menu');

    if (avatarToggle && avatarMenu) {
        console.log('Avatar dropdown elements found');

        avatarToggle.addEventListener('click', function () {
            console.log('Avatar toggle clicked');
            avatarMenu.classList.toggle('show');
        });

        // Close dropdown when clicking outside
        document.addEventListener('click', function (event) {
            if (!avatarToggle.contains(event.target) && !avatarMenu.contains(event.target)) {
                avatarMenu.classList.remove('show');
            }
        });
    } else {
        console.log('Avatar dropdown elements not found', {
            toggleFound: !!avatarToggle,
            menuFound: !!avatarMenu
        });
    }

    // Initialize date fields with today's date
    const dateInputs = document.querySelectorAll('input[type="date"]');
    if (dateInputs.length > 0) {
        console.log(`Found ${dateInputs.length} date inputs`);
        const today = new Date().toISOString().split('T')[0];
        dateInputs.forEach(input => {
            if (!input.value) {
                input.value = today;
            }
        });
    }

    // Check if Bootstrap is available
    if (typeof bootstrap !== 'undefined') {
        console.log('Bootstrap is loaded correctly');
    } else {
        console.error('Bootstrap is not loaded!');
    }

    // Check if jQuery is available
    if (typeof jQuery !== 'undefined') {
        console.log('jQuery is loaded correctly', jQuery.fn.jquery);
    } else {
        console.error('jQuery is not loaded!');
    }
});