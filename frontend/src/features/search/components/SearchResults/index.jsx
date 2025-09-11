
import React from 'react';
import { MagnifyingGlassIcon } from '@heroicons/react/24/outline';
import PersonResults from './PersonResults.jsx';
import PageResults from './PageResults.jsx';
import PhotoResults from './PhotoResults.jsx';
import LoadingSpinner from './LoadingSpinner.jsx';
import EmptyState from './EmptyState.jsx';
import { getResultType } from '../../utils/searchHelpers';

/**
 * Main search results component
 */
const SearchResults = ({
    searchQuery,
    searchResults,
    isLoading,
    selectedFilter,
    onClearSearch,
    onFilterChange
}) => {
    // Empty search state
    if (!searchQuery) {
        return (
            <div className="text-center py-20">
                <MagnifyingGlassIcon className="h-20 w-20 text-gray-300 mx-auto mb-8" />
                <h2 className="help-section-title mb-4">Search Facebook</h2>
                <p className="help-section-description max-w-md mx-auto leading-relaxed">
                    Find people, pages, photos, videos, and more. Start typing to see results instantly.
                </p>
            </div>
        );
    }

    // Loading state
    if (isLoading) {
        return <LoadingSpinner />;
    }

    // Results found
    if (searchResults.length > 0) {
        return (
            <div>
                {/* Results header */}
                <div className="mb-8">
                    <h2 className="help-section-title mb-2">
                        Search results for "{searchQuery}"
                    </h2>
                    <p className="help-section-description">
                        {searchResults.length.toLocaleString()} results in {selectedFilter}
                        {selectedFilter !== 'All' && ` • Showing ${selectedFilter.toLowerCase()} only`}
                    </p>
                </div>

                {/* Results grid */}
                <div className="space-y-4">
                    {searchResults.map(result => {
                        const resultType = getResultType(result);

                        switch (resultType) {
                            case 'person':
                                return <PersonResults key={result.id} result={result} />;
                            case 'page':
                                return <PageResults key={result.id} result={result} />;
                            case 'photo':
                                return <PhotoResults key={result.id} result={result} />;
                            default:
                                return null;
                        }
                    })}
                </div>
            </div>
        );
    }

    // No results found
    return (
        <EmptyState
            searchQuery={searchQuery}
            selectedFilter={selectedFilter}
            onClearSearch={onClearSearch}
            onSearchAll={() => onFilterChange('All')}
        />
    );
};

export default SearchResults;