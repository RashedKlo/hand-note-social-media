export const validateEmail = (email) => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(String(email).toLowerCase());
};

export const validateName = (name) => {
    return name.length >= 3;
};

export const validateBirthDate = (birthDate) => {
    const date = new Date(birthDate);
    const year = new Date().getFullYear() - date.getFullYear();
    return year > 8 && year <= 100;
};

export const validatePassword = (password) => {
    const hasFourLetters = (password.match(/[a-zA-Z]/g) || []).length >= 3;
    const hasFourNumbers = (password.match(/\d/g) || []).length >= 3;
    return hasFourLetters && hasFourNumbers;
};

export const validateConfirmPassword = (password, confirmPassword) => {
    return password === confirmPassword;
};

export const validateForm = (formData) => {
    const errors = {};
    
    if (!formData.firstName) {
        errors.firstName = 'First name is required';
    } else if (!validateName(formData.firstName)) {
        errors.firstName = 'First Name must be at least 3 characters long';
    }
    
    if (!formData.lastName) {
        errors.lastName = 'Last name is required';
    } else if (!validateName(formData.lastName)) {
        errors.lastName = 'Last Name must be at least 3 characters long';
    }
    
    if (!formData.email) {
        errors.email = 'Email is required';
    } else if (!validateEmail(formData.email)) {
        errors.email = 'Invalid email address';
    }
    
    if (!formData.password) {
        errors.password = 'Password is required';
    } else if (!validatePassword(formData.password)) {
        errors.password = 'Password must be strong';
    }
    
    if (!validateConfirmPassword(formData.password, formData.confirmPassword)) {
        errors.confirmPassword = 'Passwords do not match';
    }
    
    if (!formData.birthDate) {
        errors.birthDate = 'Birth date is required';
    } else if (!validateBirthDate(formData.birthDate)) {
        errors.birthDate = 'Age must be between 8 and 100 years';
    }
    
    if (formData.countryID === 0) {
        errors.countryID = 'Country is required';
    }
    
    return errors;
};