import React, { useState } from 'react';
import { Outlet } from 'react-router-dom';
import { authStyles, getInputClasses, getIconClasses } from '../../styles/authStyles';

export default function PersonalInfoFields({ formData, errors, countries, onChange }) {
    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);
    const [focusedField, setFocusedField] = useState(null);

    const getFieldStatus = (fieldName) => {
        if (errors[fieldName]) return 'error';
        if (formData[fieldName] && !errors[fieldName] && fieldName !== 'countryID') return 'success';
        if (fieldName === 'countryID' && formData[fieldName] && formData[fieldName] !== 0) return 'success';
        return 'default';
    };

    const handleFocus = (fieldName) => setFocusedField(fieldName);
    const handleBlur = () => setFocusedField(null);

    const FieldIcon = ({ type, status }) => {
        const iconClasses = getIconClasses(status);
        
        if (status === 'error') {
            return (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
            );
        }
        
        if (status === 'success') {
            return (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                </svg>
            );
        }

        // Default icons for different field types
        const icons = {
            email: (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 12a4 4 0 10-8 0 4 4 0 008 0zm0 0v1.5a2.5 2.5 0 005 0V12a9 9 0 10-9 9m4.5-1.206a8.959 8.959 0 01-4.5 1.207" />
                </svg>
            ),
            date: (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
            ),
            country: (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3.055 11H5a2 2 0 012 2v1a2 2 0 002 2 2 2 0 012 2v2.945M8 3.935V5.5A2.5 2.5 0 0010.5 8h.5a2 2 0 012 2 2 2 0 104 0 2 2 0 012-2h1.064M15 20.488V18a2 2 0 012-2h3.064M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
            ),
            user: (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                </svg>
            )
        };

        return icons[type] || null;
    };

    const PasswordToggle = ({ show, onClick }) => (
        <button
            type="button"
            onClick={onClick}
            className={authStyles.passwordToggle}
        >
            {show ? (
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.878 9.878L3 3m6.878 6.878L21 21" />
                </svg>
            ) : (
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                </svg>
            )}
        </button>
    );

    return (
        <div className="space-y-6">
            {/* Name Fields */}
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div>
                    <label className={authStyles.label}>
                        First Name <span className="text-red-500">*</span>
                    </label>
                    <div className="relative">
                        <input
                            type="text"
                            name="firstName"
                            value={formData.firstName}
                            onChange={onChange}
                            onFocus={() => handleFocus('firstName')}
                            onBlur={handleBlur}
                            className={getInputClasses(getFieldStatus('firstName'), focusedField === 'firstName')}
                            placeholder="Enter your first name"
                        />
                        <FieldIcon type="user" status={getFieldStatus("firstName")} />
                    </div>
                    {errors.firstName && (
                        <p className={authStyles.errorText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                            <span>{errors.firstName}</span>
                        </p>
                    )}
                    {getFieldStatus('firstName') === 'success' && (
                        <p className={authStyles.successText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                            </svg>
                            <span>Looks good!</span>
                        </p>
                    )}
                </div>
                
                <div>
                    <label className={authStyles.label}>
                        Last Name <span className="text-red-500">*</span>
                    </label>
                    <div className="relative">
                        <input
                            type="text"
                            name="lastName"
                            value={formData.lastName}
                            onChange={onChange}
                            onFocus={() => handleFocus('lastName')}
                            onBlur={handleBlur}
                            className={getInputClasses(getFieldStatus('lastName'), focusedField === 'lastName')}
                            placeholder="Enter your last name"
                        />
                        <FieldIcon type="user" status={getFieldStatus("lastName")} />
                    </div>
                    {errors.lastName && (
                        <p className={authStyles.errorText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                            <span>{errors.lastName}</span>
                        </p>
                    )}
                    {getFieldStatus('lastName') === 'success' && (
                        <p className={authStyles.successText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                            </svg>
                            <span>Looks good!</span>
                        </p>
                    )}
                </div>
            </div>

            {/* Email and Birth Date */}
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div>
                    <label className={authStyles.label}>
                        Email Address <span className="text-red-500">*</span>
                    </label>
                    <div className="relative">
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={onChange}
                            onFocus={() => handleFocus('email')}
                            onBlur={handleBlur}
                            className={getInputClasses(getFieldStatus('email'), focusedField === 'email')}
                            placeholder="Enter your email address"
                        />
                        <FieldIcon type="email" status={getFieldStatus("email")} />
                    </div>
                    {errors.email && (
                        <p className={authStyles.errorText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                            <span>{errors.email}</span>
                        </p>
                    )}
                    {getFieldStatus('email') === 'success' && (
                        <p className={authStyles.successText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                            </svg>
                            <span>Valid email!</span>
                        </p>
                    )}
                </div>
                
                <div>
                    <label className={authStyles.label}>
                        Birth Date <span className="text-red-500">*</span>
                    </label>
                    <div className="relative">
                        <input
                            type="date"
                            name="birthDate"
                            value={formData.birthDate}
                            onChange={onChange}
                            onFocus={() => handleFocus('birthDate')}
                            onBlur={handleBlur}
                            className={getInputClasses(getFieldStatus('birthDate'), focusedField === 'birthDate')}
                        />
                        <FieldIcon type="date" status={getFieldStatus("birthDate")} />
                    </div>
                    {errors.birthDate && (
                        <p className={authStyles.errorText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                            <span>{errors.birthDate}</span>
                        </p>
                    )}
                    {getFieldStatus('birthDate') === 'success' && (
                        <p className={authStyles.successText}>
                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                            </svg>
                            <span>Date confirmed!</span>
                        </p>
                    )}
                </div>
            </div>

            {/* Country */}
            <div>
                <label className={authStyles.label}>
                    Country <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                    <select
                        name="countryID"
                        value={formData.countryID}
                        onChange={onChange}
                        onFocus={() => handleFocus('countryID')}
                        onBlur={handleBlur}
                        className={getInputClasses(getFieldStatus('countryID'), focusedField === 'countryID')}
                    >
                        <option value={0} disabled>🌍 Select your country</option>
                        {countries.map((item, index) => (
                            <option key={index} value={item.countryID}>
                                {item.country}
                            </option>
                        ))}
                    </select>
                    <FieldIcon type="country" status={getFieldStatus("countryID")} />
                </div>
                {errors.countryID && (
                    <p className={authStyles.errorText}>
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                        </svg>
                        <span>{errors.countryID}</span>
                    </p>
                )}
                {getFieldStatus('countryID') === 'success' && (
                    <p className={authStyles.successText}>
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                        </svg>
                        <span>Country selected!</span>
                    </p>
                )}
            </div>

            {/* Password Fields */}
            <div>
                <label className={authStyles.label}>
                    Password <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                    <input
                        type={showPassword ? "text" : "password"}
                        name="password"
                        value={formData.password}
                        onChange={onChange}
                        onFocus={() => handleFocus('password')}
                        onBlur={handleBlur}
                        className={getInputClasses(getFieldStatus('password'), focusedField === 'password')}
                        placeholder="Create a strong password"
                    />
                    <PasswordToggle 
                        show={showPassword} 
                        onClick={() => setShowPassword(!showPassword)}
                    />
                </div>
                {errors.password && (
                    <p className={authStyles.errorText}>
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                        </svg>
                        <span>{errors.password}</span>
                    </p>
                )}
                {getFieldStatus('password') === 'success' && (
                    <p className={authStyles.successText}>
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                        </svg>
                        <span>Strong password!</span>
                    </p>
                )}
            </div>
            
            <div>
                <label className={authStyles.label}>
                    Confirm Password <span className="text-red-500">*</span>
                </label>
                <div className="relative">
                    <input
                        type={showConfirmPassword ? "text" : "password"}
                        name="confirmPassword"
                        value={formData.confirmPassword}
                        onChange={onChange}
                        onFocus={() => handleFocus('confirmPassword')}
                        onBlur={handleBlur}
                        className={getInputClasses(getFieldStatus('confirmPassword'), focusedField === 'confirmPassword')}
                        placeholder="Confirm your password"
                    />
                    <PasswordToggle 
                        show={showConfirmPassword} 
                        onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                    />
                </div>
                {errors.confirmPassword && (
                    <p className={authStyles.errorText}>
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                        </svg>
                        <span>{errors.confirmPassword}</span>
                    </p>
                )}
                {getFieldStatus('confirmPassword') === 'success' && (
                    <p className={authStyles.successText}>
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                        </svg>
                        <span>Passwords match!</span>
                    </p>
                )}
            </div>
        </div>
    );
}