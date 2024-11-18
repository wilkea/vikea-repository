function initializeRosterValidation(organizatieSelect, rosterSelect) {
    const rostersByOrganization = JSON.parse(document.getElementById('rostersData').value);

    function updateRosterOptions() {
        const selectedOrganizationId = organizatieSelect.value;
        const availableRosters = rostersByOrganization[selectedOrganizationId] || [];
        
        // Clear current options
        rosterSelect.innerHTML = '<option value="">-- Select Roster --</option>';
        
        // Add new options
        availableRosters.forEach(roster => {
            const option = document.createElement('option');
            option.value = roster.rosterId;
            option.textContent = `${roster.disciplina}`;
            rosterSelect.appendChild(option);
        });
    }

    // Update roster options when organization changes
    organizatieSelect.addEventListener('change', updateRosterOptions);
    
    // Initial update
    updateRosterOptions();
} 