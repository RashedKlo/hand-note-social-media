import { useState } from 'react';
import { Link } from 'react-router-dom';
import LoginFields from './LoginFileds';
import SocialLogin from '../SocialLogin';


export default function LoginForm({ onSubmit, loading, error }) {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });
  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
    setErrors((prev) => ({ ...prev, [name]: '' }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    await onSubmit(formData);
  };

  return (
    <div className="w-full max-w-md rounded-2xl bg-white/95 backdrop-blur-xl border border-white/20 shadow-2xl p-8">
      <h2 className="text-center text-3xl font-bold mb-4 bg-gradient-to-r from-indigo-500 to-purple-600 bg-clip-text text-transparent">
        Sign In
      </h2>

      <form onSubmit={handleSubmit} className="space-y-6">
        <LoginFields
          formData={formData}
          errors={errors}
          onChange={handleChange} />

        {error && (
          <div className="text-red-600 text-center font-medium">{error}</div>
        )}

        {/* Submit Button */}
        <button
          type="submit"
          disabled={loading}
          className="w-full py-3 bg-gradient-to-r from-indigo-500 to-purple-600 text-white font-semibold rounded-xl shadow-lg hover:shadow-xl transition-transform transform hover:-translate-y-0.5 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          {loading ? 'Signing In...' : 'Sign In'}
        </button>
      </form>
      <SocialLogin />

      <p className="mt-6 text-center text-sm text-slate-600">
        Don't have an account?{' '}
        <Link
          to="/register"
          className="font-semibold text-indigo-600 hover:text-indigo-800"
        >
          Create Account
        </Link>
      </p>
    </div>
  );
}
