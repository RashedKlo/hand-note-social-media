import React, { useState } from 'react';
import { authStyles, getInputClasses, getIconClasses } from '../../styles/authStyles';

export default function LoginFields({ formData, errors, onChange }) {
    const [showPassword, setShowPassword] = useState(false);
    const [focusedField, setFocusedField] = useState(null);

    const getFieldStatus = (fieldName) => {
        if (errors[fieldName]) return 'error';
        if (formData[fieldName] && !errors[fieldName]) return 'success';
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

        // Default icons
        const icons = {
            email: (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 12a4 4 0 10-8 0 4 4 0 008 0zm0 0v1.5a2.5 2.5 0 005 0V12a9 9 0 10-9 9m4.5-1.206a8.959 8.959 0 01-4.5 1.207" />
                </svg>
            ),
            password: (
                <svg className={iconClasses} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 11c1.104 0 2 .896 2 2v1H10v-1c0-1.104.896-2 2-2z" />
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 12a7 7 0 0114 0v3a3 3 0 01-3 3H8a3 3 0 01-3-3v-3z" />
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
            {/* Email */}
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
                    <FieldIcon type="email" status={getFieldStatus('email')} />
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

            {/* Password */}
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
                        placeholder="Enter your password"
                    />
                    <PasswordToggle 
                        show={showPassword} 
                        onClick={() => setShowPassword(!showPassword)}
                    />
                    <FieldIcon type="password" status={getFieldStatus('password')} />
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
                        <span>Looks good!</span>
                    </p>
                )}
            </div> 
        </div>
    );
}
