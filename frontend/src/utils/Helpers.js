/**
 * Gets the current user ID from localStorage
 * @returns {number|null} User ID or null if not found
 */
export const getCurrentUserId = () => {
  try {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user.id || null;
  } catch (error) {
    console.error('Error parsing user data from localStorage:', error);
    return null;
  }
};