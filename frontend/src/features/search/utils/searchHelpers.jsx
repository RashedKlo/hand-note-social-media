/**
 * Utility functions for search functionality
 */

/**
 * Filters results by search query (case-insensitive)
 * @param {Array} results - Array of search results
 * @param {string} query - Search query string
 * @returns {Array} Filtered results
 */
export const filterByQuery = (results, query) => {
    if (!query.trim()) return results;

    return results.filter(item =>
        item.name?.toLowerCase().includes(query.toLowerCase()) ||
        item.description?.toLowerCase().includes(query.toLowerCase()) ||
        item.category?.toLowerCase().includes(query.toLowerCase())
    );
};

/**
 * Combines all data types into a single array
 * @param {Object} mockData - Object containing arrays of different data types
 * @returns {Array} Combined array of all results
 */
export const combineAllResults = (mockData) => {
    return [
        ...mockData.people,
        ...mockData.pages,
        ...mockData.photos
    ];
};

/**
 * Gets results based on filter category
 * @param {Object} mockData - Object containing arrays of different data types
 * @param {string} filter - Filter category name
 * @returns {Array} Filtered results by category
 */
export const getResultsByFilter = (mockData, filter) => {
    switch (filter) {
        case 'All':
            return combineAllResults(mockData);
        case 'People':
            return mockData.people;
        case 'Pages':
            return mockData.pages;
        case 'Photos':
            return mockData.photos;
        default:
            return [];
    }
};

/**
 * Determines the result type based on object properties
 * @param {Object} result - Search result object
 * @returns {string} Type of result (person, page, photo)
 */
export const getResultType = (result) => {
    if (result.mutualFriends !== undefined) return 'person';
    if (result.category !== undefined) return 'page';
    return 'photo';
};