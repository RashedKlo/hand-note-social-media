import React from 'react';

/**
 * Filter sidebar component
 */
const FilterSidebar = ({
    filterConfig,
    selectedFilter,
    onFilterChange,
    showFilters
}) => {
    return (
        <div className={`lg:w-64 ${showFilters ? 'block' : 'hidden lg:block'}`}>
            <div className="card p-6">
                <h2 className="help-section-title mb-6">Search Results</h2>
                <div className="space-y-2">
                    {filterConfig.map((filter) => {
                        const IconComponent = filter.icon;
                        const isActive = selectedFilter === filter.name;

                        return (
                            <button
                                key={filter.name}
                                onClick={() => onFilterChange(filter.name)}
                                className={`w-full flex items-center justify-between p-4 rounded-lg text-left transition-all duration-200 group ${isActive
                                    ? 'bg-blue-50 text-blue-600 border-l-4 border-blue-600 shadow-sm'
                                    : 'hover:bg-gray-50 text-gray-700 hover:shadow-sm'
                                    }`}
                            >
                                <div className="flex items-center space-x-3">
                                    <IconComponent
                                        className={`h-5 w-5 transition-colors ${isActive ? 'text-blue-600' : filter.color + ' group-hover:text-gray-900'
                                            }`}
                                    />
                                    <span className="font-medium">{filter.name}</span>
                                </div>
                                <span className={`text-sm font-semibold ${isActive ? 'text-blue-500' : 'text-gray-400'
                                    }`}>
                                    {filter.count.toLocaleString()}
                                </span>
                            </button>
                        );
                    })}
                </div>
            </div>
        </div>
    );
};

export default FilterSidebar;