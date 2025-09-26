import React from 'react';

/**
 * Photo search result card component
 */
const PhotoResult = ({ result }) => {
    return (
        <div className="card overflow-hidden hover:shadow-lg transition-all duration-300">
            {/* Image placeholder */}
            <div className="text-8xl p-12 bg-gray-50 text-center border-b border-gray-200">
                {result.image}
            </div>

            {/* Photo details */}
            <div className="p-4">
                <p className="text-sm text-gray-700 mb-2 font-medium">{result.description}</p>
                <p className="text-xs text-gray-500 mb-3">by {result.photographer} • {result.timestamp}</p>

                <div className="flex items-center justify-between">
                    <div className="flex items-center space-x-4">
                        <button className="flex items-center space-x-1 text-xs text-gray-600 hover:text-blue-600 transition-colors">
                            <span>👍</span>
                            <span>{result.likes}</span>
                        </button>
                        <button className="flex items-center space-x-1 text-xs text-gray-600 hover:text-blue-600 transition-colors">
                            <span>💬</span>
                            <span>{result.comments}</span>
                        </button>
                    </div>
                    <button className="text-xs text-blue-600 hover:text-blue-800 font-medium transition-colors">
                        View Photo
                    </button>
                </div>
            </div>
        </div>
    );
};

export default PhotoResult;
