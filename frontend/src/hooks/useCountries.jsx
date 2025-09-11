import { useState, useEffect } from 'react';
import { getCountries } from '../services/api/apiService';

export const useCountries = () => {
    const [countries, setCountries] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchCountries = async () => {
            try {
                const response = await getCountries();
                setCountries(response);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchCountries();
    }, []);

    return { countries, loading, error };
};