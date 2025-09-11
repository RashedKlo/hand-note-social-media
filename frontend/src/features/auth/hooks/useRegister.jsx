import { useState } from 'react';
import { registerUser } from '../services/authService';
import Cookie from 'cookie-universal';

export const useRegister = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const cookie = Cookie();

    const register = async (formData, onSuccess) => {
        try {
            setLoading(true);
            setError(null);
             
            const response = await registerUser(formData);
            
            // Set cookies
            const expirationDate = new Date();
            expirationDate.setDate(expirationDate.getDate() + 30);
            
            cookie.set("token", response.token, { path: '/', expires: expirationDate });
            cookie.set("reftoken", response.refreshToken, { path: '/', expires: expirationDate });
            cookie.set("currentUser", response.user, { path: '/', expires: expirationDate });
            
            // Clear old searches
            localStorage.removeItem("recentSearches");
            
            if (onSuccess) onSuccess();
            
        } catch (err) {
            if (err.response?.status === 409) {
                setError("Email has been taken");
            } else {
                setError("Registration failed. Please try again.");
            }
        } finally {
            setLoading(false);
        }
    };

    return { register, loading, error };
};