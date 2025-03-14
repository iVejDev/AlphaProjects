// Client-side validation for authentication forms
document.addEventListener('DOMContentLoaded', function () {
    // Login form validation
    const loginForm = document.getElementById('account');
    if (loginForm) {
        loginForm.addEventListener('submit', function (event) {
            let isValid = true;

            // Email validation
            const email = document.getElementById('Input_Email');
            const emailError = document.querySelector('[data-valmsg-for="Input.Email"]');

            if (!email.value.trim()) {
                email.classList.add('is-invalid');
                emailError.textContent = 'Email is required.';
                isValid = false;
            } else if (!isValidEmail(email.value)) {
                email.classList.add('is-invalid');
                emailError.textContent = 'Please enter a valid email address.';
                isValid = false;
            } else {
                email.classList.remove('is-invalid');
                emailError.textContent = '';
            }

            // Password validation
            const password = document.getElementById('Input_Password');
            const passwordError = document.querySelector('[data-valmsg-for="Input.Password"]');

            if (!password.value.trim()) {
                password.classList.add('is-invalid');
                passwordError.textContent = 'Password is required.';
                isValid = false;
            } else {
                password.classList.remove('is-invalid');
                passwordError.textContent = '';
            }

            if (!isValid) {
                event.preventDefault();
            }
        });
    }

    // Registration form validation
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.addEventListener('submit', function (event) {
            let isValid = true;

            // Full Name validation
            const fullName = document.getElementById('Input_FullName');
            const fullNameError = document.querySelector('[data-valmsg-for="Input.FullName"]');

            if (!fullName.value.trim()) {
                fullName.classList.add('is-invalid');
                fullNameError.textContent = 'Full Name is required.';
                isValid = false;
            } else {
                fullName.classList.remove('is-invalid');
                fullNameError.textContent = '';
            }

            // Email validation
            const email = document.getElementById('Input_Email');
            const emailError = document.querySelector('[data-valmsg-for="Input.Email"]');

            if (!email.value.trim()) {
                email.classList.add('is-invalid');
                emailError.textContent = 'Email is required.';
                isValid = false;
            } else if (!isValidEmail(email.value)) {
                email.classList.add('is-invalid');
                emailError.textContent = 'Please enter a valid email address.';
                isValid = false;
            } else {
                email.classList.remove('is-invalid');
                emailError.textContent = '';
            }

            // Password validation
            const password = document.getElementById('Input_Password');
            const passwordError = document.querySelector('[data-valmsg-for="Input.Password"]');

            if (!password.value.trim()) {
                password.classList.add('is-invalid');
                passwordError.textContent = 'Password is required.';
                isValid = false;
            } else if (password.value.length < 6) {
                password.classList.add('is-invalid');
                passwordError.textContent = 'Password must be at least 6 characters long.';
                isValid = false;
            } else {
                password.classList.remove('is-invalid');
                passwordError.textContent = '';
            }

            // Confirm Password validation
            const confirmPassword = document.getElementById('Input_ConfirmPassword');
            const confirmPasswordError = document.querySelector('[data-valmsg-for="Input.ConfirmPassword"]');

            if (!confirmPassword.value.trim()) {
                confirmPassword.classList.add('is-invalid');
                confirmPasswordError.textContent = 'Confirm Password is required.';
                isValid = false;
            } else if (confirmPassword.value !== password.value) {
                confirmPassword.classList.add('is-invalid');
                confirmPasswordError.textContent = 'Passwords do not match.';
                isValid = false;
            } else {
                confirmPassword.classList.remove('is-invalid');
                confirmPasswordError.textContent = '';
            }

            // Terms acceptance
            const terms = document.getElementById('Input_AcceptTerms');
            const termsError = document.querySelector('[data-valmsg-for="Input.AcceptTerms"]');

            if (!terms.checked) {
                terms.classList.add('is-invalid');
                termsError.textContent = 'You must accept the terms and conditions.';
                isValid = false;
            } else {
                terms.classList.remove('is-invalid');
                termsError.textContent = '';
            }

            if (!isValid) {
                event.preventDefault();
            }
        });
    }

    // Helper function to validate email format
    function isValidEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }
});