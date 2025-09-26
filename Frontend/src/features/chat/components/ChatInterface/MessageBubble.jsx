// features/chat/components/ChatInterface/MessageBubble.jsx
import React from 'react';
import { CheckIcon, FaceSmileIcon, PaperClipIcon } from '@heroicons/react/24/outline';
import { reactions } from '../../utils/constants';

const MessageBubble = ({
  message,
  showReactionPicker,
  onReaction,
  onToggleReactionPicker
}) => {
  const isMe = message.sender === 'me';

  return (
    <div className={`flex ${isMe ? 'justify-end' : 'justify-start'}`}>
      <div className="group relative max-w-[75%] sm:max-w-xs md:max-w-sm lg:max-w-md">
        {/* Message Bubble */}
        <div className={`relative ${
          isMe
            ? 'bg-blue-600 text-white'
            : 'bg-white text-gray-900 border border-gray-200'
        } rounded-2xl overflow-hidden shadow-sm`}>
          
          {/* Text Message */}
          {message.text && (
            <div className="px-3 py-2 md:px-4 md:py-2.5">
              <p className="text-sm md:text-base break-words">{message.text}</p>
            </div>
          )}
          
          {/* Image Message */}
          {message.type === 'image' && (
            <div>
              <img 
                src={message.src} 
                alt="Shared image" 
                className="w-full max-w-xs md:max-w-sm rounded-t-2xl object-cover"
                style={{ maxHeight: '300px' }}
              />
              {message.caption && (
                <div className="px-3 py-2 md:px-4 md:py-2.5">
                  <p className="text-sm md:text-base">{message.caption}</p>
                </div>
              )}
            </div>
          )}
          
          {/* File Message */}
          {message.type === 'file' && (
            <div className="px-3 py-3 md:px-4 md:py-3 flex items-center space-x-3">
              <div className={`p-2 rounded-lg ${
                isMe ? 'bg-blue-500' : 'bg-gray-100'
              }`}>
                <PaperClipIcon className={`h-4 w-4 md:h-5 md:w-5 ${
                  isMe ? 'text-white' : 'text-gray-600'
                }`} />
              </div>
              <div className="flex-1 min-w-0">
                <p className="text-sm font-medium truncate">{message.fileName}</p>
                <p className={`text-xs ${
                  isMe ? 'text-blue-200' : 'text-gray-500'
                }`}>{message.fileSize}</p>
              </div>
            </div>
          )}
          
          {/* Timestamp and Read Receipt */}
          <div className={`flex items-center justify-end px-3 pb-2 md:px-4 md:pb-2 space-x-1 ${
            isMe ? 'text-blue-200' : 'text-gray-500'
          }`}>
            <span className="text-xs">{message.timestamp}</span>
            {isMe && (
              <CheckIcon className={`h-3 w-3 ${
                message.read ? 'text-blue-200' : 'text-blue-300'
              }`} />
            )}
          </div>
        </div>
        
        {/* Reactions */}
        {message.reactions && Object.keys(message.reactions).length > 0 && (
          <div className="flex flex-wrap gap-1 mt-1 justify-center">
            {Object.entries(message.reactions).map(([reaction, count]) => (
              <span 
                key={reaction}
                className="inline-flex items-center px-2 py-1 bg-white border border-gray-200 rounded-full text-xs shadow-sm hover:bg-gray-50 cursor-pointer transition-colors"
                onClick={() => onReaction(message.id, reaction)}
              >
                <span className="mr-1">{reaction}</span>
                <span className="text-gray-600">{count}</span>
              </span>
            ))}
          </div>
        )}
        
        {/* Reaction Button */}
        <button
          onClick={() => onToggleReactionPicker(
            showReactionPicker === message.id ? null : message.id
          )}
          className={`absolute ${
            isMe ? 'left-0 -translate-x-8' : 'right-0 translate-x-8'
          } top-1/2 transform -translate-y-1/2 opacity-0 group-hover:opacity-100 transition-opacity p-1.5 bg-white rounded-full shadow-md border border-gray-200 hover:bg-gray-50`}
        >
          <FaceSmileIcon className="h-4 w-4 text-gray-500" />
        </button>
        
        {/* Reaction Picker */}
        {showReactionPicker === message.id && (
          <div className={`absolute ${
            isMe ? 'left-0 -translate-x-full' : 'right-0 translate-x-full'
          } top-0 -translate-y-2 flex space-x-1 bg-white rounded-full shadow-lg border border-gray-200 px-2 py-1.5 z-20`}>
            {reactions.map((reaction) => (
              <button
                key={reaction}
                onClick={() => onReaction(message.id, reaction)}
                className="hover:bg-gray-100 rounded-full p-1 text-base md:text-lg transition-colors"
              >
                {reaction}
              </button>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default MessageBubble;