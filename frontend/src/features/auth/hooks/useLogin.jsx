import { useState } from 'react';
// import { authService } from '../services/authService'; // You'll need to implement loginUser API call
import Cookie from 'cookie-universal';

export const useLogin = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const cookie = Cookie();
    const { loginUser } = {};
    const login = async (formData, onSuccess) => {
        try {
            setLoading(true);
            setError(null);

            const response = await loginUser(formData);

            // Set cookies
            const expirationDate = new Date();
            expirationDate.setDate(expirationDate.getDate() + 30);

            cookie.set("token", response.token, { path: '/', expires: expirationDate });
            cookie.set("reftoken", response.refreshToken, { path: '/', expires: expirationDate });
            cookie.set("currentUser", response.user, { path: '/', expires: expirationDate });

            // Optionally clear old searches
            localStorage.removeItem("recentSearches");

            if (onSuccess) onSuccess();

        } catch (err) {
            if (err.response?.status === 401) {
                setError("Invalid email or password");
            } else {
                setError("Login failed. Please try again.");
            }
        } finally {
            setLoading(false);
        }
    };

    return { login, loading, error };
};
