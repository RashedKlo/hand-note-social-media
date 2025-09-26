export const debounce = (func, wait) => {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      clearTimeout(timeout);
      func(...args);
    };
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
  };
};

export const handleApiError = (error, defaultMessage) => {
  console.error('API Error:', error);
  
  if (error.response?.data?.message) {
    return { success: false, message: error.response.data.message };
  }
  
  if (error.message) {
    return { success: false, message: error.message };
  }
  
  return { success: false, message: defaultMessage };
};

export const formatApiResponse = (response) => {
  return {
    success: response.success || false,
    data: response.data || null,
    message: response.message || '',
    pagination: response.pagination || null
  };
};

export const getCurrentUserId = () => {
  // Get from your auth context/store
  // This is just a placeholder
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  return user.id || null;
};