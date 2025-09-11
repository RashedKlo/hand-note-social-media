import React from 'react';
import { MagnifyingGlassIcon } from '@heroicons/react/24/outline';

/**
 * Empty state component for no search results
 */
const EmptyState = ({ searchQuery, selectedFilter, onClearSearch, onSearchAll }) => {
    return (
        <div className="text-center py-16">
            <MagnifyingGlassIcon className="h-16 w-16 text-gray-300 mx-auto mb-6" />
            <h3 className="text-xl font-semibold text-gray-900 mb-3">No results found</h3>
            <p className="text-gray-600 mb-6 max-w-md mx-auto">
                We couldn't find anything matching "{searchQuery}" in {selectedFilter.toLowerCase()}.
            </p>
            <div className="flex flex-col sm:flex-row gap-3 justify-center">
                <button
                    onClick={onClearSearch}
                    className="btn-secondary-custom px-6 py-2 rounded-lg"
                >
                    Clear Search
                </button>
                <button
                    onClick={onSearchAll}
                    className="btn-primary px-6 py-2 rounded-lg"
                >
                    Search All Categories
                </button>
            </div>
        </div>
    );
};

export default EmptyState;
