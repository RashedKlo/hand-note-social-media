import { useState, useEffect } from 'react';
import SearchService from '../services/SearchService';

/**
 * Custom hook for search functionality
 * @param {Object} mockData - Mock data object
 * @returns {Object} Search state and functions
 */
export const useSearch = (mockData) => {
    // State management
    const [searchQuery, setSearchQuery] = useState('');
    const [selectedFilter, setSelectedFilter] = useState('All');
    const [searchResults, setSearchResults] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    /**
     * Performs search operation
     * @param {string} query - Search query
     * @param {string} filter - Filter category
     */
    const performSearch = async (query, filter) => {
        setIsLoading(true);
        try {
            const results = await SearchService.performSearch(query, filter, mockData);
            setSearchResults(results);
        } catch (error) {
            console.error('Search error:', error);
            setSearchResults([]);
        } finally {
            setIsLoading(false);
        }
    };

    /**
     * Clears search query and results
     */
    const clearSearch = () => {
        setSearchQuery('');
        setSearchResults([]);
    };

    /**
     * Handles filter change
     * @param {string} filterName - New filter name
     */
    const handleFilterChange = (filterName) => {
        setSelectedFilter(filterName);
        if (searchQuery) {
            performSearch(searchQuery, filterName);
        }
    };

    /**
     * Handles search query change
     * @param {string} query - New search query
     */
    const handleSearchChange = (query) => {
        setSearchQuery(query);
    };

    // Auto-search effect
    useEffect(() => {
        if (searchQuery) {
            performSearch(searchQuery, selectedFilter);
        }
    }, [searchQuery, selectedFilter]);

    return {
        // State
        searchQuery,
        selectedFilter,
        searchResults,
        isLoading,

        // Actions
        handleSearchChange,
        handleFilterChange,
        clearSearch,
        performSearch
    };
};