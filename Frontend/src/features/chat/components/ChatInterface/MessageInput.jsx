// features/chat/components/ChatInterface/MessageInput.jsx
import React from 'react';
import {
  PaperAirplaneIcon,
  FaceSmileIcon,
  PhotoIcon,
  PaperClipIcon
} from '@heroicons/react/24/outline';
import { emojis } from '../../utils/constants';

const MessageInput = ({
  message,
  showEmojiPicker,
  onMessageChange,
  onSendMessage,
  onFileUpload,
  onImageUpload,
  onEmojiClick,
  onToggleEmojiPicker,
  onStartTyping,
  fileInputRef,
  imageInputRef
}) => {
  const handleKeyPress = (e) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault();
      onSendMessage();
    } else {
      onStartTyping();
    }
  };

  return (
    <div className="p-3 md:p-4 border-t border-gray-200 bg-white sticky bottom-0">
      <div className="flex items-end space-x-2 md:space-x-3">
        {/* Attachment Buttons */}
        <div className="flex space-x-1 flex-shrink-0">
          <button
            onClick={() => fileInputRef.current?.click()}
            className="p-2 hover:bg-gray-100 rounded-full transition-colors"
            title="Attach file"
          >
            <PaperClipIcon className="h-4 w-4 md:h-5 md:w-5 text-blue-600" />
          </button>
          <button
            onClick={() => imageInputRef.current?.click()}
            className="p-2 hover:bg-gray-100 rounded-full transition-colors"
            title="Attach photo"
          >
            <PhotoIcon className="h-4 w-4 md:h-5 md:w-5 text-blue-600" />
          </button>
        </div>
        
        {/* Message Input Field */}
        <div className="flex-1 relative">
          <div className="relative">
            <textarea
              placeholder="Aa"
              className="w-full px-4 py-2 md:py-2.5 pr-12 bg-gray-100 rounded-full border-0 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:bg-white transition-all resize-none text-sm md:text-base min-h-[40px] max-h-[120px] overflow-y-auto"
              value={message}
              onChange={(e) => onMessageChange(e.target.value)}
              onKeyPress={handleKeyPress}
              rows={1}
              style={{
                height: 'auto',
                minHeight: '40px'
              }}
              onInput={(e) => {
                e.target.style.height = 'auto';
                e.target.style.height = Math.min(e.target.scrollHeight, 120) + 'px';
              }}
            />
            
            {/* Emoji Button */}
            <button
              onClick={onToggleEmojiPicker}
              className="absolute right-3 top-1/2 transform -translate-y-1/2 p-1 hover:bg-gray-200 rounded-full transition-colors"
              title="Add emoji"
            >
              <FaceSmileIcon className="h-4 w-4 md:h-5 md:w-5 text-blue-600" />
            </button>
            
            {/* Emoji Picker */}
            {showEmojiPicker && (
              <div className="absolute bottom-full right-0 mb-2 bg-white rounded-lg shadow-xl border border-gray-200 p-3 z-30">
                <div className="grid grid-cols-5 sm:grid-cols-6 md:grid-cols-8 gap-2 max-w-xs md:max-w-sm max-h-48 overflow-y-auto">
                  {emojis.map((emoji, index) => (
                    <button
                      key={index}
                      onClick={() => onEmojiClick(emoji)}
                      className="hover:bg-gray-100 rounded p-2 text-lg transition-colors"
                    >
                      {emoji}
                    </button>
                  ))}
                </div>
              </div>
            )}
          </div>
        </div>
        
        {/* Send Button */}
        <button
          onClick={onSendMessage}
          className="p-2 bg-blue-600 text-white rounded-full hover:bg-blue-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex-shrink-0"
          disabled={!message.trim()}
          title="Send message"
        >
          <PaperAirplaneIcon className="h-4 w-4 md:h-5 md:w-5" />
        </button>
      </div>

      {/* Hidden File Inputs */}
      <input
        ref={fileInputRef}
        type="file"
        onChange={onFileUpload}
        className="hidden"
        accept=".pdf,.doc,.docx,.txt,.zip,.rar"
      />
      <input
        ref={imageInputRef}
        type="file"
        onChange={onImageUpload}
        className="hidden"
        accept="image/*"
      />
    </div>
  );
};

export default MessageInput;