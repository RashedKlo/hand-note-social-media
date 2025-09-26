import React, { useState } from 'react';
import SearchHeader from '../../features/search/components/SearchHeader.jsx';
import FilterSidebar from '../../features/search/components/FilterSidebar.jsx';
import SearchResults from '../../features/search/components/SearchResults/index.jsx';
import { useSearch } from '../../features/search/hooks/useSearch.jsx';
import { mockDataConfig } from '../../features/search/data/mockData.jsx';
import { filterConfig } from '../../features/search/config/filterConfig.jsx';

/**
 * Main Search page component - now clean and modular
 */
const Search = () => {
    // ========================================
    // STATE & HOOKS
    // ========================================
    const [showFilters, setShowFilters] = useState(false);

    // Custom search hook handles all search logic
    const {
        searchQuery,
        selectedFilter,
        searchResults,
        isLoading,
        handleSearchChange,
        handleFilterChange,
        clearSearch
    } = useSearch(mockDataConfig);

    // ========================================
    // HANDLERS
    // ========================================

    /**
     * Toggles mobile filter sidebar visibility
     */
    const handleToggleFilters = () => {
        setShowFilters(!showFilters);
    };

    // ========================================
    // RENDER
    // ========================================
    return (
        <div className="min-h-screen" style={{ backgroundColor: 'var(--color-bg-secondary)' }}>
            {/* Header with search input */}
            <SearchHeader
                searchQuery={searchQuery}
                onSearchChange={handleSearchChange}
                onClearSearch={clearSearch}
                onToggleFilters={handleToggleFilters}
            />

            {/* Main content area */}
            <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                <div className="flex flex-col lg:flex-row gap-8">
                    {/* Filter sidebar */}
                    <FilterSidebar
                        filterConfig={filterConfig}
                        selectedFilter={selectedFilter}
                        onFilterChange={handleFilterChange}
                        showFilters={showFilters}
                    />

                    {/* Search results */}
                    <div className="flex-1 min-w-0">
                        <SearchResults
                            searchQuery={searchQuery}
                            searchResults={searchResults}
                            isLoading={isLoading}
                            selectedFilter={selectedFilter}
                            onClearSearch={clearSearch}
                            onFilterChange={handleFilterChange}
                        />
                    </div>
                </div>
            </main>
        </div>
    );
};

export default Search;