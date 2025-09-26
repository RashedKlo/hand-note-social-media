
import React from 'react';
import { CheckIcon } from '@heroicons/react/24/solid';

/**
 * Page search result card component
 */
const PageResult = ({ result }) => {
    return (
        <div className="card p-6 hover:shadow-lg transition-all duration-300">
            <div className="flex items-center justify-between">
                <div className="flex items-center space-x-4">
                    <div className="text-5xl">{result.avatar}</div>

                    <div className="flex-1">
                        <div className="flex items-center space-x-2 mb-1">
                            <h3 className="text-lg font-semibold text-gray-900 hover:text-blue-600 cursor-pointer transition-colors">
                                {result.name}
                            </h3>
                            {result.verified && (
                                <CheckIcon className="h-5 w-5 text-blue-500" title="Verified" />
                            )}
                        </div>
                        <p className="text-sm text-gray-600 mb-1">{result.category}</p>
                        <p className="text-sm text-gray-500 mb-2">{result.description}</p>
                        <p className="text-xs text-gray-400">{result.likes} likes</p>
                    </div>
                </div>

                <button className="btn-secondary px-6 py-2 text-sm font-medium rounded-lg transition-all duration-200 hover:scale-105">
                    Like
                </button>
            </div>
        </div>
    );
};

export default PageResult;
