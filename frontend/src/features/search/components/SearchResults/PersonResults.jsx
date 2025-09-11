
import React from 'react';

/**
 * Person search result card component
 */
const PersonResult = ({ result }) => {
    return (
        <div className="card p-6 hover:shadow-lg transition-all duration-300">
            <div className="flex items-center justify-between">
                <div className="flex items-center space-x-4">
                    {/* Avatar with online indicator */}
                    <div className="relative">
                        <div className="text-5xl">{result.avatar}</div>
                        {result.isOnline && (
                            <div className="absolute -bottom-1 -right-1 w-4 h-4 bg-green-500 rounded-full border-2 border-white"></div>
                        )}
                    </div>

                    {/* User info */}
                    <div className="flex-1">
                        <h3 className="text-lg font-semibold text-gray-900 hover:text-blue-600 cursor-pointer transition-colors">
                            {result.name}
                        </h3>
                        <p className="text-sm text-gray-600">{result.profession}</p>
                        <p className="text-sm text-gray-500">{result.location}</p>
                        <p className="text-xs text-gray-400">{result.mutualFriends} mutual friends</p>
                    </div>
                </div>

                {/* Action button */}
                <button className="btn-primary px-6 py-2 text-sm font-medium rounded-lg transition-all duration-200 hover:scale-105">
                    Add Friend
                </button>
            </div>
        </div>
    );
};

export default PersonResult;