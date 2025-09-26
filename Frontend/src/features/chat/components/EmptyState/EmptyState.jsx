// features/chat/components/EmptyState/EmptyState.jsx
import React from 'react';

const EmptyState = () => {
  return (
    <div className="hidden md:flex flex-1 items-center justify-center bg-gray-50">
      <div className="text-center max-w-sm mx-auto px-4">
        <div className="w-20 h-20 md:w-24 md:h-24 bg-blue-100 rounded-full flex items-center justify-center mx-auto mb-4">
          <svg 
            className="w-10 h-10 md:w-12 md:h-12 text-blue-600" 
            fill="currentColor" 
            viewBox="0 0 20 20"
          >
            <path 
              fillRule="evenodd" 
              d="M18 10c0 3.866-3.582 7-8 7a8.841 8.841 0 01-4.083-.98L2 17l1.338-3.123C2.493 12.767 2 11.434 2 10c0-3.866 3.582-7 8-7s8 3.134 8 7zM7 9H5v2h2V9zm8 0h-2v2h2V9zM9 9h2v2H9V9z" 
              clipRule="evenodd" 
            />
          </svg>
        </div>
        <h3 className="text-lg font-medium text-gray-900 mb-2">Your Messages</h3>
        <p className="text-sm text-gray-500 leading-relaxed">
          Send private photos and messages to a friend or group. 
          Select a conversation to get started.
        </p>
      </div>
    </div>
  );
};

export default EmptyState;