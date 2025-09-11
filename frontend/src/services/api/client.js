import axios from 'axios';

export const apiClient = axios.create({
  baseURL: 'https://example.com/api', // change to your API base URL
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});
