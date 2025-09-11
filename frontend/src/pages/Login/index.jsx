import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useLogin } from '../../features/auth/hooks/useLogin';
import Placeholder from '../../components/common/Placeholder/placeholder';
import LoginForm from '../../features/auth/components/Login/LoginForm';
import AuthBranding from '../../features/auth/components/AuthBranding';
import BackgroundElements from '../../features/auth/components/BackgroundElements';

export default function LoginPage() {
  const navigate = useNavigate();
  const { login, loading, error } = useLogin();

  const handleLoginSuccess = () => {
    navigate('/');
  };

  const handleLoginSubmit = async (formData) => {
    await login(formData, handleLoginSuccess);
  };

  return (
    <div
      className="min-h-screen flex items-center justify-center relative overflow-hidden p-4 sm:p-6 md:p-8"
      style={{
        background:
          'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
        zIndex: 0,
      }}
    >
      <Placeholder loading={loading} />

      <BackgroundElements/>

      <div className="relative z-10 w-full max-w-[1400px] flex flex-col md:flex-row items-center justify-center gap-6 md:gap-12 mx-auto">
        {/* Branding */}
        <div className="w-full md:w-1/2 max-w-md">
          <AuthBranding
            title="Welcome Back"
            subtitle="Log in to your HandNote account"
            description="Stay connected with friends, family, and your favorite communities."
          />
        </div>

        {/* Login form */}
        <div className="w-full md:w-1/2 max-w-md">
          <LoginForm onSubmit={handleLoginSubmit} loading={loading} error={error} />
        </div>
      </div>
    </div>
  );
}
