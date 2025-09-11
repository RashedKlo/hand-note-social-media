import React from 'react';
import { MagnifyingGlassIcon, AdjustmentsHorizontalIcon, XMarkIcon } from '@heroicons/react/24/outline';

/**
 * Search header component with logo and search input
 */
const SearchHeader = ({
    searchQuery,
    onSearchChange,
    onClearSearch,
    onToggleFilters
}) => {
    return (
        <header className="bg-white shadow-sm border-b" style={{ borderColor: 'var(--color-border-primary)' }}>
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-16">
                    {/* Logo and Search */}
                    <div className="flex items-center space-x-6">
                        <h1 className="text-2xl font-bold" style={{ color: 'var(--color-primary)' }}>
                            Hand Note
                        </h1>

                        {/* Search Input */}
                        <div className="relative">
                            <div className="flex items-center bg-gray-100 rounded-full px-4 py-2.5 w-72 sm:w-96 transition-all duration-200 focus-within:ring-2 focus-within:ring-blue-500 focus-within:ring-opacity-20">
                                <MagnifyingGlassIcon className="h-5 w-5 text-gray-400 mr-3" />
                                <input
                                    type="text"
                                    placeholder="Search Hand Note"
                                    value={searchQuery}
                                    onChange={(e) => onSearchChange(e.target.value)}
                                    className="bg-transparent outline-none flex-1 text-gray-700 placeholder-gray-500"
                                />
                                {searchQuery && (
                                    <button
                                        type="button"
                                        onClick={onClearSearch}
                                        className="ml-2 p-1.5 hover:bg-gray-200 rounded-full transition-colors duration-200"
                                    >
                                        <XMarkIcon className="h-4 w-4 text-gray-400" />
                                    </button>
                                )}
                            </div>
                        </div>
                    </div>

                    {/* Mobile Filter Toggle */}
                    <button
                        onClick={onToggleFilters}
                        className="lg:hidden p-2 text-gray-600 hover:bg-gray-100 rounded-lg transition-colors duration-200"
                    >
                        <AdjustmentsHorizontalIcon className="h-6 w-6" />
                    </button>
                </div>
            </div>
        </header>
    );
};

export default SearchHeader;