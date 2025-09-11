// features/chat/components/ChatInterface/MessageList.jsx
import React from 'react';
import MessageBubble from './MessageBubble';

const MessageList = ({
  messages,
  showReactionPicker,
  onReaction,
  onToggleReactionPicker
}) => {
  return (
    <div className="flex-1 overflow-y-auto p-3 md:p-4 space-y-3 md:space-y-4 bg-gray-50">
      {messages.length > 0 ? (
        messages.map((message) => (
          <MessageBubble
            key={message.id}
            message={message}
            showReactionPicker={showReactionPicker}
            onReaction={onReaction}
            onToggleReactionPicker={onToggleReactionPicker}
          />
        ))
      ) : (
        <div className="flex items-center justify-center h-full">
          <div className="text-center">
            <div className="w-16 h-16 md:w-20 md:h-20 bg-gray-200 rounded-full flex items-center justify-center mx-auto mb-3">
              <svg className="w-8 h-8 md:w-10 md:h-10 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
                <path fillRule="evenodd" d="M18 10c0 3.866-3.582 7-8 7a8.841 8.841 0 01-4.083-.98L2 17l1.338-3.123C2.493 12.767 2 11.434 2 10c0-3.866 3.582-7 8-7s8 3.134 8 7zM7 9H5v2h2V9zm8 0h-2v2h2V9zM9 9h2v2H9V9z" clipRule="evenodd" />
              </svg>
            </div>
            <p className="text-sm text-gray-500">Start your conversation</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default MessageList;