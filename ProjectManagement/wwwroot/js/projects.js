// JavaScript for managing projects
document.addEventListener('DOMContentLoaded', function () {
    console.log('Document loaded, initializing project functionality');

    // Show Add Project Modal
    const addProjectBtn = document.getElementById('addProjectBtn');

    if (addProjectBtn) {
        console.log('Add Project button found');

        addProjectBtn.addEventListener('click', function () {
            console.log('Add Project button clicked');

            const addProjectModal = document.getElementById('addProjectModal');
            if (addProjectModal) {
                // Set default dates (today for start, one month later for end)
                const today = new Date();
                const nextMonth = new Date(today);
                nextMonth.setMonth(nextMonth.getMonth() + 1);

                const startDateInput = document.getElementById('StartDate');
                const endDateInput = document.getElementById('EndDate');

                if (startDateInput) startDateInput.value = formatDate(today);
                if (endDateInput) endDateInput.value = formatDate(nextMonth);

                // Show the modal using Bootstrap
                const modal = new bootstrap.Modal(addProjectModal);
                modal.show();
            } else {
                console.error('Add Project modal not found');
            }
        });
    } else {
        console.error('Add Project button not found');
    }

    // Add Project Form Validation
    const addProjectForm = document.getElementById('addProjectForm');
    if (addProjectForm) {
        addProjectForm.addEventListener('submit', function (event) {
            if (!validateProjectForm('addProjectForm')) {
                event.preventDefault();
                return false;
            }

            // If validation passes, allow the form to submit normally
            return true;
        });
    }

    // Edit Project Handling
    const editProjectBtns = document.querySelectorAll('.edit-project-btn');
    const editProjectModal = document.getElementById('editProjectModal');

    if (editProjectBtns.length > 0 && editProjectModal) {
        const modal = new bootstrap.Modal(editProjectModal);

        editProjectBtns.forEach(btn => {
            btn.addEventListener('click', function () {
                const projectId = this.getAttribute('data-project-id');

                // Fetch project data
                fetch(`/Project/Edit/${projectId}`)
                    .then(response => response.json())
                    .then(project => {
                        // Populate the form with project data
                        document.getElementById('editId').value = project.id;
                        document.getElementById('editUserId').value = project.userId;
                        document.getElementById('editName').value = project.name;
                        document.getElementById('editClientName').value = project.clientName;
                        document.getElementById('editDescription').value = project.description;
                        document.getElementById('editStartDate').value = formatDate(new Date(project.startDate));
                        document.getElementById('editEndDate').value = formatDate(new Date(project.endDate));
                        document.getElementById('editBudget').value = project.budget;
                        document.getElementById('editStatus').value = project.status;

                        // Show the modal
                        modal.show();
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            });
        });
    }

    // Edit Project Form Validation
    const editProjectForm = document.getElementById('editProjectForm');
    if (editProjectForm) {
        editProjectForm.addEventListener('submit', function (event) {
            if (!validateProjectForm('editProjectForm')) {
                event.preventDefault();
                return false;
            }

            // If validation passes, allow the form to submit normally
            return true;
        });
    }

    // Delete Project Handling
    const deleteProjectBtns = document.querySelectorAll('.delete-project-btn');
    const deleteProjectModal = document.getElementById('deleteProjectModal');

    if (deleteProjectBtns.length > 0 && deleteProjectModal) {
        const modal = new bootstrap.Modal(deleteProjectModal);

        deleteProjectBtns.forEach(btn => {
            btn.addEventListener('click', function () {
                const projectId = this.getAttribute('data-project-id');
                const projectName = this.getAttribute('data-project-name');

                document.getElementById('deleteId').value = projectId;
                document.getElementById('deleteProjectName').textContent = projectName;

                modal.show();
            });
        });
    }

    // Helper Functions

    // Format date to YYYY-MM-DD
    function formatDate(date) {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    }

    // Validate the project form
    function validateProjectForm(formId) {
        const form = document.getElementById(formId);
        let isValid = true;

        // Project Name validation
        const nameInput = form.querySelector('[name="Name"]');
        const nameField = nameInput.closest('.form-group');
        const nameError = nameField.querySelector('.invalid-feedback');

        if (!nameInput.value.trim()) {
            nameInput.classList.add('is-invalid');
            nameError.textContent = 'Project name is required.';
            isValid = false;
        } else if (nameInput.value.length > 100) {
            nameInput.classList.add('is-invalid');
            nameError.textContent = 'Project name cannot be longer than 100 characters.';
            isValid = false;
        } else {
            nameInput.classList.remove('is-invalid');
            nameError.textContent = '';
        }

        // Client Name validation
        const clientInput = form.querySelector('[name="ClientName"]');
        const clientField = clientInput.closest('.form-group');
        const clientError = clientField.querySelector('.invalid-feedback');

        if (!clientInput.value.trim()) {
            clientInput.classList.add('is-invalid');
            clientError.textContent = 'Client name is required.';
            isValid = false;
        } else if (clientInput.value.length > 100) {
            clientInput.classList.add('is-invalid');
            clientError.textContent = 'Client name cannot be longer than 100 characters.';
            isValid = false;
        } else {
            clientInput.classList.remove('is-invalid');
            clientError.textContent = '';
        }

        // Description validation
        const descInput = form.querySelector('[name="Description"]');
        const descField = descInput.closest('.form-group');
        const descError = descField.querySelector('.invalid-feedback');

        if (!descInput.value.trim()) {
            descInput.classList.add('is-invalid');
            descError.textContent = 'Description is required.';
            isValid = false;
        } else {
            descInput.classList.remove('is-invalid');
            descError.textContent = '';
        }

        // Start Date validation
        const startInput = form.querySelector('[name="StartDate"]');
        const startField = startInput.closest('.form-group');
        const startError = startField.querySelector('.invalid-feedback');

        if (!startInput.value) {
            startInput.classList.add('is-invalid');
            startError.textContent = 'Start date is required.';
            isValid = false;
        } else {
            startInput.classList.remove('is-invalid');
            startError.textContent = '';
        }

        // End Date validation
        const endInput = form.querySelector('[name="EndDate"]');
        const endField = endInput.closest('.form-group');
        const endError = endField.querySelector('.invalid-feedback');

        if (!endInput.value) {
            endInput.classList.add('is-invalid');
            endError.textContent = 'End date is required.';
            isValid = false;
        } else if (new Date(endInput.value) < new Date(startInput.value)) {
            endInput.classList.add('is-invalid');
            endError.textContent = 'End date must be after the start date.';
            isValid = false;
        } else {
            endInput.classList.remove('is-invalid');
            endError.textContent = '';
        }

        // Budget validation
        const budgetInput = form.querySelector('[name="Budget"]');
        const budgetField = budgetInput.closest('.form-group');
        const budgetError = budgetField.querySelector('.invalid-feedback');

        if (!budgetInput.value) {
            budgetInput.classList.add('is-invalid');
            budgetError.textContent = 'Budget is required.';
            isValid = false;
        } else if (parseFloat(budgetInput.value) < 0) {
            budgetInput.classList.add('is-invalid');
            budgetError.textContent = 'Budget must be a positive value.';
            isValid = false;
        } else {
            budgetInput.classList.remove('is-invalid');
            budgetError.textContent = '';
        }

        return isValid;
    }

    // Display error messages from the server
    function displayErrors(errors) {
        if (errors && errors.length > 0) {
            errors.forEach(error => {
                // Display the error message
                alert(`Error: ${error}`);
            });
        }
    }
});