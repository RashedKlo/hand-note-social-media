import { apiClient } from '../../../services/api/client';
import { REGISTER,LOGIN } from '../../../services/api/endpoints';


export const registerUser = async (userData) => {
    const response = await apiClient.post(REGISTER, userData);
    return response.data;
};

export const loginUser = async (userData) => {
    const response = await apiClient.post(LOGIN, userData);
    return response.data;
};