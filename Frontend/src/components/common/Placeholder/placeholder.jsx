import React from 'react';

const Placeholder = ({ loading }) => {
    if (!loading) return null;

    return (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm z-50 flex items-center justify-center">
            {/* Spinning loader */}
            <div className="relative">
                {/* Outer ring */}
                <div className="w-12 h-12 rounded-full border-4 border-white/20 border-t-white animate-spin"></div>
                
                {/* Inner pulsing dot (optional enhancement) */}
                <div className="absolute inset-0 flex items-center justify-center">
                    <div className="w-2 h-2 bg-white rounded-full animate-pulse"></div>
                </div>
            </div>
        </div>
    );
};

export default Placeholder;