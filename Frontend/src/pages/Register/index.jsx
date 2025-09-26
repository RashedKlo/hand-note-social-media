import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useCountries } from '../../hooks/useCountries';
import { useRegister } from '../../features/auth/hooks/useRegister';
import Placeholder from '../../components/common/Placeholder/placeholder';
import RegisterForm from '../../features/auth/components/Register/RegisterForm';
import AuthBranding from '../../features/auth/components/AuthBranding';
import BackgroundElements from '../../features/auth/components/BackgroundElements';

export default function RegisterPage() {
    const navigate = useNavigate();
    const { countries, loading: countriesLoading } = useCountries();
    const { register, loading, error } = useRegister();

    const handleRegisterSuccess = () => {
        navigate('/');
    };

    const handleRegisterSubmit = async (formData) => {
        await register(formData, handleRegisterSuccess);
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-indigo-400 to-purple-600 relative overflow-hidden flex items-center justify-center p-2 sm:p-3 md:p-4">
            <Placeholder loading={loading || countriesLoading} />
            
            {/* Background Elements */}
            <BackgroundElements />
            
            <div className="relative z-10 w-full flex items-center justify-center">
                <div className="w-full max-w-sm sm:max-w-2xl md:max-w-7xl grid grid-cols-1 md:grid-cols-2 gap-3 md:gap-6 items-center justify-center">
                    <div className="w-full">
                        <AuthBranding
                            title="Join HandNote"
                            subtitle="Create your account and start connecting."
                            description="Join millions of people sharing moments and staying connected with friends and family."
                        />
                    </div>

                    <div className="w-full">
                        <RegisterForm
                            countries={countries}
                            onSubmit={handleRegisterSubmit}
                            loading={loading}
                            error={error}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
}

// Background decoration component
