import React, { useState, useEffect } from 'react';
import { 
  XMarkIcon,
  MagnifyingGlassIcon,
  ClockIcon,
  TrashIcon
} from '@heroicons/react/24/outline';
import { useNavigate } from 'react-router-dom';

function SearchModal({ open, handleClose }) {
  const limitOfUsers = 10;
  const [showLoadMore, setShowLoadMore] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const [recentSearches, setRecentSearches] = useState([]);
  const [results, setResults] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    setRecentSearches(getRecentSearches());
  }, []);

  useEffect(() => {
    if (searchTerm) {
      const timeoutId = setTimeout(() => {
        fetchSearchResults(searchTerm, pageNumber);
      }, 300);
      return () => clearTimeout(timeoutId);
    } else {
      setResults([]);
    }
  }, [searchTerm, pageNumber]);

  const fetchSearchResults = async (query, pageNumber) => {
    setLoading(true);
    try {
      // let res = await MyAxios.get(
      //   `${BASEURL}/${FILTERUSERS}/${query}?pageNumber=${pageNumber}&limitOfUsers=${limitOfUsers}`
      // );
      let res={};
      
      if (pageNumber === 1) {
        setResults(res.data);
      } else {
        setResults(prev => [...prev, ...res.data]);
      }
      setShowLoadMore(res.data.length === limitOfUsers);
    } catch (error) {
      if (error.response?.status === 404) {
        setShowLoadMore(false);
      }
      console.error("Search error:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleSearchChange = (e) => {
    setSearchTerm(e.target.value);
    setPageNumber(1);
  };

  const loadMoreResults = () => {
    setPageNumber(prev => prev + 1);
  };

  const handleResultClick = (result) => {
    saveSearch(result);
    setRecentSearches(prev => [result, ...prev.filter(item => item !== result)]);
    navigate(`/search?query=${result}`);
    handleClose();
  };

  const handleClearRecentSearches = () => {
    localStorage.removeItem("recentSearches");
    setRecentSearches([]);
  };

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-150 flex items-center justify-center p-4 backdrop-blur-sm bg-black/30">
      {/* Mobile: Full Screen, Desktop: Modal */}
      <div className="relative w-full h-full sm:w-full sm:max-w-2xl sm:max-h-[85vh] sm:h-auto">
        {/* Background Container */}
        <div className="absolute inset-0 bg-gradient-to-br from-indigo-500 via-purple-600 to-purple-700 sm:rounded-3xl overflow-hidden">
          {/* Background Overlay */}
          <div 
            className="absolute inset-0 opacity-30"
            style={{
              background: `
                radial-gradient(circle at 20% 50%, rgba(120, 119, 198, 0.3) 0%, transparent 50%),
                radial-gradient(circle at 80% 20%, rgba(255, 255, 255, 0.1) 0%, transparent 50%)
              `
            }}
          />
          
          {/* Floating Elements */}
          <div className="absolute top-[10%] left-[5%] w-12 h-12 sm:w-16 sm:h-16 rounded-full bg-white/10 backdrop-blur-md animate-bounce" />
          <div className="absolute bottom-[20%] right-[5%] w-10 h-10 sm:w-12 sm:h-12 rounded-full bg-white/10 backdrop-blur-md animate-pulse" />
        </div>

        {/* Modal Content */}
        <div className="relative z-10 h-full bg-white/95 backdrop-blur-xl border border-white/20 shadow-2xl sm:rounded-3xl flex flex-col overflow-hidden">
          {/* Header */}
          <div className="flex items-center gap-4 p-6 pb-4 border-b border-indigo-500/10">
            <button
              onClick={handleClose}
              className="p-2 rounded-full bg-indigo-500/10 backdrop-blur-md border border-indigo-500/20 text-indigo-600 hover:bg-indigo-500/20 hover:scale-110 transition-all duration-300 hover:shadow-lg hover:shadow-indigo-500/25"
            >
              <XMarkIcon className="w-6 h-6" />
            </button>
            <div className="relative flex-1">
              <MagnifyingGlassIcon className="absolute left-4 top-1/2 transform -translate-y-1/2 w-5 h-5 text-indigo-600" />
              <input
                type="text"
                placeholder="Search people on HandNote..."
                value={searchTerm}
                onChange={handleSearchChange}
                autoFocus
                className="w-full pl-12 pr-4 py-3 bg-slate-50/80 backdrop-blur-md border border-slate-300/30 rounded-3xl text-base font-medium placeholder:text-slate-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 focus:bg-white focus:shadow-lg focus:shadow-indigo-500/20 focus:-translate-y-0.5 transition-all duration-300 hover:bg-slate-50 hover:-translate-y-0.5 hover:shadow-md"
              />
            </div>
          </div>

          {/* Content */}
          <div className="flex-1 overflow-hidden">
            {searchTerm === '' ? (
              <div className="h-full flex flex-col">
                {/* Recent Searches Header */}
                <div className="flex items-center justify-between p-6 bg-indigo-500/5 backdrop-blur-md border-b border-indigo-500/10">
                  <div className="flex items-center">
                    <ClockIcon className="w-5 h-5 text-indigo-600 mr-3" />
                    <h3 className="text-lg font-bold bg-gradient-to-r from-indigo-600 to-purple-600 bg-clip-text text-transparent">
                      Recent Searches
                    </h3>
                  </div>
                  <button
                    onClick={handleClearRecentSearches}
                    className="flex items-center gap-2 px-4 py-2 bg-red-500/10 backdrop-blur-md border border-red-500/20 text-red-600 rounded-2xl hover:bg-red-500/20 hover:-translate-y-0.5 hover:shadow-lg hover:shadow-red-500/25 transition-all duration-300 text-sm font-semibold"
                  >
                    <TrashIcon className="w-4 h-4" />
                    Clear All
                  </button>
                </div>

                {/* Recent Searches List */}
                <div className="flex-1 overflow-y-auto p-6">
                  {recentSearches.length > 0 ? (
                    <div className="space-y-3">
                      {recentSearches.map((search, index) => (
                        <button
                          key={index}
                          onClick={() => handleResultClick(search)}
                          className="w-full flex items-center p-4 bg-slate-50/50 backdrop-blur-md border border-slate-300/20 rounded-2xl hover:bg-indigo-500/10 hover:-translate-y-1 hover:shadow-lg hover:shadow-indigo-500/20 hover:border-indigo-500/30 transition-all duration-300"
                        >
                          <div className="w-11 h-11 rounded-full bg-indigo-500/10 backdrop-blur-md border border-indigo-500/20 flex items-center justify-center text-indigo-600 mr-4">
                            <MagnifyingGlassIcon className="w-5 h-5" />
                          </div>
                          <span className="text-slate-700 font-semibold text-left">
                            {search}
                          </span>
                        </button>
                      ))}
                    </div>
                  ) : (
                    <div className="flex flex-col items-center justify-center py-20">
                      <MagnifyingGlassIcon className="w-16 h-16 text-indigo-600 mb-4 opacity-50" />
                      <h3 className="text-xl font-semibold text-slate-600 mb-2">
                        No recent searches
                      </h3>
                      <p className="text-slate-500 text-center">
                        Start searching to see your recent searches here
                      </p>
                    </div>
                  )}
                </div>
              </div>
            ) : (
              <div className="h-full flex flex-col">
                {/* Search Results Header */}
                <div className="p-6 bg-indigo-500/5 backdrop-blur-md border-b border-indigo-500/10">
                  <h3 className="text-lg font-bold bg-gradient-to-r from-indigo-600 to-purple-600 bg-clip-text text-transparent">
                    {results.length > 0 ? `Search Results (${results.length})` : 'No results found'}
                  </h3>
                </div>

                {/* Search Results List */}
                <div className="flex-1 overflow-y-auto p-6">
                  {results.length > 0 ? (
                    <div className="space-y-3">
                      {results.map((result, index) => (
                        <button
                          key={index}
                          onClick={() => handleResultClick(result)}
                          className="w-full flex items-center p-4 bg-slate-50/50 backdrop-blur-md border border-slate-300/20 rounded-2xl hover:bg-indigo-500/10 hover:-translate-y-1 hover:shadow-lg hover:shadow-indigo-500/20 hover:border-indigo-500/30 transition-all duration-300"
                        >
                          <div className="w-11 h-11 rounded-full bg-gradient-to-br from-indigo-600 to-purple-600 text-white flex items-center justify-center font-semibold text-lg mr-4 shadow-lg shadow-indigo-500/30 border-2 border-transparent">
                            {result.charAt(0).toUpperCase()}
                          </div>
                          <span className="text-slate-700 font-semibold text-left">
                            {result}
                          </span>
                        </button>
                      ))}
                    </div>
                  ) : !loading && (
                    <div className="flex flex-col items-center justify-center py-20">
                      <MagnifyingGlassIcon className="w-16 h-16 text-indigo-600 mb-4 opacity-50" />
                      <h3 className="text-xl font-semibold text-slate-600 mb-2">
                        No results found
                      </h3>
                      <p className="text-slate-500 text-center">
                        Try searching with different keywords
                      </p>
                    </div>
                  )}

                  {loading && (
                    <div className="flex justify-center items-center py-8">
                      <div className="w-8 h-8 border-4 border-indigo-200 border-t-indigo-600 rounded-full animate-spin"></div>
                    </div>
                  )}
                </div>

                {/* Load More Button */}
                {showLoadMore && !loading && (
                  <div className="p-6 pt-0">
                    <button
                      onClick={loadMoreResults}
                      className="w-full py-4 px-6 bg-gradient-to-r from-indigo-600 to-purple-600 text-white font-semibold text-base rounded-3xl shadow-lg shadow-indigo-500/30 hover:from-indigo-700 hover:to-purple-700 hover:-translate-y-1 hover:shadow-xl hover:shadow-indigo-500/40 transition-all duration-300"
                    >
                      Load More Results
                    </button>
                  </div>
                )}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

// Helper functions
function saveSearch(query) {
  let searches = JSON.parse(localStorage.getItem('recentSearches')) || [];
  searches = searches.filter((item) => item !== query);
  searches.unshift(query);
  if (searches.length > 5) searches.pop();
  localStorage.setItem('recentSearches', JSON.stringify(searches));
}

function getRecentSearches() {
  return JSON.parse(localStorage.getItem('recentSearches')) || [];
}

export default SearchModal;