// features/chat/components/ChatSidebar/ChatSidebar.jsx
import React from 'react';
import { MagnifyingGlassIcon, EllipsisHorizontalIcon } from '@heroicons/react/24/outline';
import ChatListItem from './ChatListItem';

const ChatSidebar = ({
  isVisible,
  chats,
  selectedChat,
  searchTerm,
  onSearchChange,
  onChatSelect
}) => {
  return (
    <div className={`${
      isVisible ? 'flex' : 'hidden'
    } md:flex flex-col w-full md:w-80 lg:w-96 border-r border-gray-200 bg-white flex-shrink-0`}>
      {/* Header */}
      <div className="p-3 md:p-4 border-b border-gray-200 bg-white sticky top-0 z-10">
        <div className="flex items-center justify-between mb-3 md:mb-4">
          <h1 className="text-lg md:text-xl font-bold text-gray-900">Chats</h1>
          <div className="flex space-x-1 md:space-x-2">
            <button className="p-2 hover:bg-gray-100 rounded-full transition-colors">
              <EllipsisHorizontalIcon className="h-4 w-4 md:h-5 md:w-5 text-gray-600" />
            </button>
          </div>
        </div>
        
        {/* Search */}
        <div className="relative">
          <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
          <input
            type="text"
            placeholder="Search Messenger"
            className="w-full pl-10 pr-4 py-2 md:py-2.5 bg-gray-100 border-0 rounded-full text-sm focus:outline-none focus:bg-white focus:ring-2 focus:ring-blue-500 transition-all"
            value={searchTerm}
            onChange={(e) => onSearchChange(e.target.value)}
          />
        </div>
      </div>

      {/* Chat List */}
      <div className="flex-1 overflow-y-auto">
        {chats.length > 0 ? (
          <div className="divide-y divide-gray-100">
            {chats.map((chat) => (
              <ChatListItem
                key={chat.id}
                chat={chat}
                isSelected={selectedChat === chat.id}
                onClick={() => onChatSelect(chat.id)}
              />
            ))}
          </div>
        ) : (
          <div className="flex items-center justify-center h-32">
            <p className="text-gray-500 text-sm">No chats found</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default ChatSidebar;