import { getResultsByFilter, filterByQuery } from '../utils/searchHelpers';

/**
 * Search service for handling API-like operations
 */
class SearchService {
    /**
     * Simulates API search with loading delay
     * @param {string} query - Search query
     * @param {string} filter - Filter category
     * @param {Object} mockData - Mock data object
     * @returns {Promise<Array>} Promise resolving to search results
     */
    static async performSearch(query, filter, mockData) {
        // Simulate API delay
        await new Promise(resolve => setTimeout(resolve, 300));

        if (!query.trim()) {
            return [];
        }

        // Get results based on filter
        const results = getResultsByFilter(mockData, filter);

        // Filter by search query
        return filterByQuery(results, query);
    }

    /**
     * Gets filter counts (could be from API in real implementation)
     * @param {Object} mockData - Mock data object
     * @returns {Object} Filter counts
     */
    static getFilterCounts(mockData) {
        return {
            All: mockData.people.length + mockData.pages.length + mockData.photos.length,
            People: mockData.people.length,
            Pages: mockData.pages.length,
            Photos: mockData.photos.length,
            Videos: 78, // Mock data
            Places: 45, // Mock data
            Events: 23  // Mock data
        };
    }
}

export default SearchService;