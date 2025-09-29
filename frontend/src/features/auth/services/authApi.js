import { apiClient } from "../../../services/api/client";

export const authApi={
 registerUser :async (userData) => {
    const response = await apiClient.post("/Register", userData);
    return response.data;
},

  loginUser : async (userData) => {
    const response = await apiClient.post("/Login", userData);
    return response.data;
}
}