import React from 'react';

/**
 * Loading spinner component
 */
const LoadingSpinner = () => {
    return (
        <div className="text-center py-20">
            <div className="animate-spin rounded-full h-12 w-12 border-4 border-blue-600 border-t-transparent mx-auto mb-4"></div>
            <p className="text-gray-600">Searching...</p>
        </div>
    );
};

export default LoadingSpinner;
