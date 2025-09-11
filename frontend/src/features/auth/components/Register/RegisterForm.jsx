import React, { useState, useRef } from 'react';
import { Link } from 'react-router-dom';
import { validateForm } from '../../utils/registerValidations';
import ProfilePictureUpload from './ProfilePictureUpload';
import PersonalInfoFields from './PersonalInfoFileds';
import SocialLogin from '../SocialLogin';
// Main RegisterForm Component
export default function RegisterForm({ countries, onSubmit, loading, error }) {
    const [formData, setFormData] = useState({
        userName: '',
        lastName: '',
        firstName: '',
        email: '',
        password: '',
        confirmPassword: '',
        birthDate: '',
        countryID: 0,
        profilePicture: null,
    });
    
    const [errors, setErrors] = useState({});
    const uploadImage = useRef(null);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({ ...prevData, [name]: value }));
        setErrors((prevErrors) => ({ ...prevErrors, [name]: '' }));
    };

    const handleImageChange = (e) => {
        const file = e.target.files[0];
        if (!file) return;
        
        setFormData((prevData) => ({
            ...prevData,
            profilePicture: URL.createObjectURL(file),
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        
        const validationErrors = validateForm(formData);
        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        const submitData = {
            ...formData,
            userName: `${formData.firstName} ${formData.lastName}`
        };

        await onSubmit(submitData);
    };

    return (
        <div className="w-full max-w-lg sm:max-w-xl rounded-2xl sm:rounded-3xl bg-white/95 backdrop-blur-xl border border-white/20 shadow-2xl transition-all duration-300 hover:-translate-y-1 hover:shadow-3xl overflow-hidden">
            {/* Header */}
            <div className="text-center p-6 sm:p-8 pb-4">
                <h2 className="text-slate-800 font-bold text-2xl sm:text-3xl md:text-4xl mb-2 bg-gradient-to-r from-indigo-500 to-purple-600 bg-clip-text text-transparent">
                    Create Account
                </h2>
                <p className="text-slate-600 text-base sm:text-lg font-medium">
                    Fill in your information to get started
                </p>
            </div>

            {/* Form */}
            <form onSubmit={handleSubmit} className="p-6 sm:p-8 pt-0 space-y-6">
                <ProfilePictureUpload
                    profilePicture={formData.profilePicture}
                    onImageChange={handleImageChange}
                    uploadRef={uploadImage}
                />

                <PersonalInfoFields
                    formData={formData}
                    errors={errors}
                    countries={countries}
                    onChange={handleChange}
                />

                <input 
                    type="file" 
                    ref={uploadImage} 
                    className="hidden" 
                    onChange={handleImageChange} 
                />

                {error && (
                    <div className="text-red-500 text-center mb-4 font-medium">
                        {error}
                    </div>
                )}

                <button 
                    type="submit" 
                    disabled={loading}
                    className="w-full py-3 sm:py-4 px-6 bg-gradient-to-r from-indigo-500 to-purple-600 text-white font-semibold text-base sm:text-lg rounded-xl sm:rounded-2xl shadow-lg hover:shadow-xl transform hover:-translate-y-0.5 transition-all duration-300 disabled:opacity-50 disabled:cursor-not-allowed disabled:transform-none focus:outline-none focus:ring-4 focus:ring-indigo-300"
                >
                    {loading ? 'Creating Account...' : 'Create Account'}
                </button>

                <SocialLogin />

                <div className="text-center mt-8">
                    <Link 
                        to="/login" 
                        className="text-slate-600 hover:text-indigo-600 transition-colors duration-200 font-medium"
                    >
                        Already have an account? <span className="font-semibold">Sign In</span>
                    </Link>
                </div>
            </form>
        </div>
    );
}
