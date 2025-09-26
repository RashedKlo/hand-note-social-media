// features/chat/components/ChatInterface/ChatHeader.jsx
import React from 'react';
import {
  ChevronDownIcon,
  PhoneIcon,
  VideoCameraIcon,
  InformationCircleIcon
} from '@heroicons/react/24/outline';

const ChatHeader = ({
  chatData,
  isTyping,
  onBackToList,
  onCall,
  onVideoCall,
  onToggleProfile,
  showProfile
}) => {
  return (
    <div className="flex items-center justify-between p-3 md:p-4 border-b border-gray-200 bg-white sticky top-0 z-10">
      <div className="flex items-center flex-1 min-w-0">
        {/* Back Button (Mobile) */}
        <button 
          onClick={onBackToList}
          className="md:hidden mr-2 p-1.5 hover:bg-gray-100 rounded-full transition-colors"
        >
          <ChevronDownIcon className="h-5 w-5 text-gray-600 transform rotate-90" />
        </button>

        {/* Profile Section */}
        <div 
          className="flex items-center cursor-pointer hover:bg-gray-50 rounded-lg p-1 -m-1 transition-colors flex-1 min-w-0"
          onClick={onToggleProfile}
        >
          <div className="relative flex-shrink-0">
            <img
              src={chatData?.avatar}
              alt={chatData?.name}
              className="w-8 h-8 md:w-10 md:h-10 rounded-full object-cover"
            />
            {chatData?.online && (
              <div className="absolute bottom-0 right-0 w-2.5 h-2.5 md:w-3 md:h-3 bg-green-400 rounded-full border-2 border-white"></div>
            )}
          </div>
          
          <div className="ml-2 md:ml-3 flex-1 min-w-0">
            <h2 className="text-sm md:text-base font-semibold text-gray-900 truncate">
              {chatData?.name}
            </h2>
            <p className="text-xs md:text-sm text-gray-500 truncate">
              {isTyping 
                ? <span className="text-blue-600 font-medium">Typing...</span>
                : chatData?.online 
                  ? 'Active now' 
                  : 'Last seen recently'
              }
            </p>
          </div>
        </div>
      </div>
      
      {/* Action Buttons */}
      <div className="flex items-center space-x-1 md:space-x-2 flex-shrink-0 ml-2">
        <button 
          onClick={onCall}
          className="p-2 hover:bg-gray-100 rounded-full transition-colors"
          title="Voice call"
        >
          <PhoneIcon className="h-4 w-4 md:h-5 md:w-5 text-blue-600" />
        </button>
        
        <button 
          onClick={onVideoCall}
          className="p-2 hover:bg-gray-100 rounded-full transition-colors"
          title="Video call"
        >
          <VideoCameraIcon className="h-4 w-4 md:h-5 md:w-5 text-blue-600" />
        </button>
        
        <button 
          onClick={onToggleProfile}
          className={`p-2 hover:bg-gray-100 rounded-full transition-colors ${
            showProfile ? 'bg-blue-50 text-blue-600' : ''
          }`}
          title="Chat info"
        >
          <InformationCircleIcon className="h-4 w-4 md:h-5 md:w-5 text-gray-600" />
        </button>
      </div>
    </div>
  );
};

export default ChatHeader;