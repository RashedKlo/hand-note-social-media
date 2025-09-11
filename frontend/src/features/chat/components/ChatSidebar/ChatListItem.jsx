// features/chat/components/ChatSidebar/ChatListItem.jsx
import React from 'react';

const ChatListItem = ({ chat, isSelected, onClick }) => {
  return (
    <div
      onClick={onClick}
      className={`flex items-center p-3 md:p-4 hover:bg-gray-50 cursor-pointer transition-colors ${
        isSelected ? 'bg-blue-50 border-r-2 border-blue-500' : ''
      }`}
    >
      {/* Avatar */}
      <div className="relative flex-shrink-0">
        <img
          src={chat.avatar}
          alt={chat.name}
          className="w-10 h-10 md:w-12 md:h-12 rounded-full object-cover"
        />
        {chat.online && (
          <div className="absolute bottom-0 right-0 w-2.5 h-2.5 md:w-3 md:h-3 bg-green-400 rounded-full border-2 border-white"></div>
        )}
      </div>
      
      {/* Chat Info */}
      <div className="ml-3 flex-1 min-w-0">
        <div className="flex justify-between items-start mb-1">
          <p className="text-sm md:text-base font-medium text-gray-900 truncate pr-2">
            {chat.name}
          </p>
          <p className="text-xs text-gray-500 flex-shrink-0">
            {chat.timestamp}
          </p>
        </div>
        
        <div className="flex justify-between items-center">
          <p className="text-xs md:text-sm text-gray-500 truncate pr-2">
            {chat.lastMessage}
          </p>
          {chat.unread > 0 && (
            <span className="inline-flex items-center justify-center px-2 py-1 text-xs font-bold leading-none text-white bg-blue-600 rounded-full min-w-[20px]">
              {chat.unread > 99 ? '99+' : chat.unread}
            </span>
          )}
        </div>
      </div>
    </div>
  );
};

export default ChatListItem;