// features/chat/utils/constants.js

export const reactions = ['👍', '❤️', '😂', '😮', '😢', '😡'];

export const emojis = [
  '😊', '😂', '😍', '🥰', '😘', '😉', '😎', '🤔', 
  '😴', '🙄', '😭', '😤', '🥺', '😇', '🤗', '🎉', 
  '💯', '🔥', '❤️', '💕', '👍', '👎', '🎊', '🌟',
  '💫', '⭐', '✨', '🎈', '🎁', '🌈', '🦄', '🎯'
];

export const CHAT_CONFIG = {
  MAX_MESSAGE_LENGTH: 1000,
  MAX_FILE_SIZE: 50 * 1024 * 1024, // 50MB
  SUPPORTED_IMAGE_TYPES: ['image/jpeg', 'image/png', 'image/gif', 'image/webp'],
  SUPPORTED_FILE_TYPES: ['.pdf', '.doc', '.docx', '.txt', '.zip', '.rar'],
  TYPING_TIMEOUT: 3000,
};

export const BREAKPOINTS = {
  mobile: '0px',
  tablet: '768px',
  desktop: '1024px',
  wide: '1280px',
};