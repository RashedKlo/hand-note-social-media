// features/chat/components/ProfileSidebar/ProfileSidebar.jsx
import React from 'react';
import { XMarkIcon, PhoneIcon, VideoCameraIcon } from '@heroicons/react/24/outline';

const ProfileSidebar = ({ chatData, onClose, onCall, onVideoCall }) => {
  return (
    <div className="w-full sm:w-80 md:w-96 border-l border-gray-200 bg-white flex flex-col absolute sm:relative inset-0 sm:inset-auto z-20 sm:z-auto">
      {/* Header */}
      <div className="p-4 border-b border-gray-200 bg-white sticky top-0 z-10">
        <div className="flex items-center justify-between">
          <h3 className="text-lg font-semibold text-gray-900">Profile</h3>
          <button
            onClick={onClose}
            className="p-1.5 hover:bg-gray-100 rounded-full transition-colors"
          >
            <XMarkIcon className="h-5 w-5 text-gray-500" />
          </button>
        </div>
      </div>
      
      <div className="flex-1 overflow-y-auto">
        <div className="p-4 space-y-6">
          {/* Profile Picture and Name */}
          <div className="text-center">
            <div className="relative inline-block">
              <img
                src={chatData.avatar}
                alt={chatData.name}
                className="w-20 h-20 md:w-24 md:h-24 rounded-full object-cover mx-auto"
              />
              {chatData.online && (
                <div className="absolute bottom-1 right-1 w-5 h-5 md:w-6 md:h-6 bg-green-400 rounded-full border-3 border-white"></div>
              )}
            </div>
            <h2 className="text-xl font-bold text-gray-900 mt-3">
              {chatData.name}
            </h2>
            <p className="text-sm text-gray-500 mt-1">
              {chatData.online ? 'Active now' : 'Last seen recently'}
            </p>
          </div>

          {/* Action Buttons */}
          <div className="grid grid-cols-2 gap-3">
            <button
              onClick={onCall}
              className="flex flex-col items-center p-4 bg-gray-100 rounded-lg hover:bg-gray-200 transition-colors"
            >
              <PhoneIcon className="h-6 w-6 text-blue-600 mb-2" />
              <span className="text-sm font-medium text-gray-700">Call</span>
            </button>
            <button
              onClick={onVideoCall}
              className="flex flex-col items-center p-4 bg-gray-100 rounded-lg hover:bg-gray-200 transition-colors"
            >
              <VideoCameraIcon className="h-6 w-6 text-blue-600 mb-2" />
              <span className="text-sm font-medium text-gray-700">Video</span>
            </button>
          </div>

          {/* Profile Information */}
          <div className="space-y-4">
            {chatData.bio && (
              <div>
                <h4 className="text-sm font-medium text-gray-900 mb-2">About</h4>
                <p className="text-sm text-gray-600 leading-relaxed">{chatData.bio}</p>
              </div>
            )}
            
            {chatData.phone && (
              <div>
                <h4 className="text-sm font-medium text-gray-900 mb-2">Phone</h4>
                <p className="text-sm text-gray-600">{chatData.phone}</p>
              </div>
            )}
            
            {chatData.email && (
              <div>
                <h4 className="text-sm font-medium text-gray-900 mb-2">Email</h4>
                <p className="text-sm text-gray-600 break-all">{chatData.email}</p>
              </div>
            )}
            
            {chatData.mutualFriends !== undefined && (
              <div>
                <h4 className="text-sm font-medium text-gray-900 mb-2">Mutual Friends</h4>
                <p className="text-sm text-gray-600">
                  {chatData.mutualFriends} mutual friend{chatData.mutualFriends !== 1 ? 's' : ''}
                </p>
              </div>
            )}
          </div>

          {/* Media & Files */}
          <div>
            <h4 className="text-sm font-medium text-gray-900 mb-3">Media & Files</h4>
            <div className="grid grid-cols-3 gap-2">
              {/* Placeholder for media thumbnails */}
              <div className="aspect-square bg-gray-100 rounded-lg flex items-center justify-center">
                <span className="text-xs text-gray-400">Photo</span>
              </div>
              <div className="aspect-square bg-gray-100 rounded-lg flex items-center justify-center">
                <span className="text-xs text-gray-400">Photo</span>
              </div>
              <div className="aspect-square bg-gray-100 rounded-lg flex items-center justify-center">
                <span className="text-xs text-gray-400">Photo</span>
              </div>
            </div>
            <button className="w-full mt-3 text-sm text-blue-600 hover:text-blue-700 font-medium">
              See all media
            </button>
          </div>

          {/* Privacy & Support */}
          <div className="pt-4 border-t border-gray-200">
            <div className="space-y-2">
              <button className="w-full text-left p-3 hover:bg-gray-50 rounded-lg text-sm text-gray-600 transition-colors">
                🔕 Mute conversation
              </button>
              <button className="w-full text-left p-3 hover:bg-gray-50 rounded-lg text-sm text-gray-600 transition-colors">
                🚫 Block user
              </button>
              <button className="w-full text-left p-3 hover:bg-gray-50 rounded-lg text-sm text-red-600 transition-colors">
                🚨 Report user
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfileSidebar;