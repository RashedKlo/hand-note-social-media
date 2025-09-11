// features/chat/utils/helpers.js
import { CHAT_CONFIG } from './constants';

/**
 * Format file size to human readable format
 * @param {number} bytes - File size in bytes
 * @returns {string} Formatted file size
 */
export const formatFileSize = (bytes) => {
  if (bytes === 0) return '0 Bytes';
  
  const k = 1024;
  const sizes = ['Bytes', 'KB', 'MB', 'GB'];
  const i = Math.floor(Math.log(bytes) / Math.log(k));
  
  return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
};

/**
 * Format timestamp to readable format
 * @param {string|Date} timestamp - Timestamp to format
 * @returns {string} Formatted timestamp
 */
export const formatTimestamp = (timestamp) => {
  if (!timestamp) return '';
  
  const date = new Date(timestamp);
  const now = new Date();
  const diffMs = now - date;
  const diffMins = Math.floor(diffMs / 60000);
  const diffHours = Math.floor(diffMs / 3600000);
  const diffDays = Math.floor(diffMs / 86400000);
  
  if (diffMins < 1) return 'now';
  if (diffMins < 60) return `${diffMins}m`;
  if (diffHours < 24) return `${diffHours}h`;
  if (diffDays < 7) return `${diffDays}d`;
  
  return date.toLocaleDateString();
};

/**
 * Validate file upload
 * @param {File} file - File to validate
 * @param {string} type - 'image' or 'file'
 * @returns {Object} Validation result
 */
export const validateFileUpload = (file, type = 'file') => {
  const errors = [];
  
  if (!file) {
    errors.push('No file selected');
    return { isValid: false, errors };
  }
  
  // Check file size
  if (file.size > CHAT_CONFIG.MAX_FILE_SIZE) {
    errors.push(`File size must be less than ${formatFileSize(CHAT_CONFIG.MAX_FILE_SIZE)}`);
  }
  
  // Check file type
  if (type === 'image') {
    if (!CHAT_CONFIG.SUPPORTED_IMAGE_TYPES.includes(file.type)) {
      errors.push('Unsupported image format. Please use JPEG, PNG, GIF, or WebP');
    }
  }
  
  return {
    isValid: errors.length === 0,
    errors
  };
};

/**
 * Truncate text to specified length
 * @param {string} text - Text to truncate
 * @param {number} maxLength - Maximum length
 * @returns {string} Truncated text
 */
export const truncateText = (text, maxLength = 50) => {
  if (!text || text.length <= maxLength) return text;
  return text.substring(0, maxLength) + '...';
};

/**
 * Check if user is online
 * @param {string} lastSeen - Last seen timestamp
 * @returns {boolean} Whether user is online
 */
export const isUserOnline = (lastSeen) => {
  if (!lastSeen) return false;
  const now = new Date();
  const lastSeenDate = new Date(lastSeen);
  const diffMinutes = (now - lastSeenDate) / (1000 * 60);
  return diffMinutes < 5; // Consider online if last seen within 5 minutes
};

/**
 * Generate unique message ID
 * @returns {string} Unique ID
 */
export const generateMessageId = () => {
  return Date.now().toString() + Math.random().toString(36).substr(2, 9);
};

/**
 * Scroll to bottom of messages container
 * @param {HTMLElement} container - Messages container element
 */
export const scrollToBottom = (container) => {
  if (container) {
    container.scrollTop = container.scrollHeight;
  }
};

/**
 * Check if element is in viewport
 * @param {HTMLElement} element - Element to check
 * @returns {boolean} Whether element is in viewport
 */
export const isElementInViewport = (element) => {
  if (!element) return false;
  
  const rect = element.getBoundingClientRect();
  return (
    rect.top >= 0 &&
    rect.left >= 0 &&
    rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
    rect.right <= (window.innerWidth || document.documentElement.clientWidth)
  );
};