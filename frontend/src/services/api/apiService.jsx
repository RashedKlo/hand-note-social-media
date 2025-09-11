import { apiClient } from './client';
import { COUNTRIES } from './endpoints';

export const getCountries = async () => {
    const response = await apiClient.get(COUNTRIES);
    return response.data;
};