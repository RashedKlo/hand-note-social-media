export const friendsHelpers = {
  // Format friend data consistently
  formatFriend: (friend) => ({
    id: friend.userId || friend.id,
    friendshipId: friend.friendshipId,
    name: friend.name || `${friend.firstName || ''} ${friend.lastName || ''}`.trim(),
    avatar: friend.avatar || friend.profilePicture || '/default-avatar.png',
    mutualFriends: friend.mutualFriends || friend.mutualFriendsCount || 0,
    isOnline: friend.isOnline || false,
    lastActive: friend.lastActive || friend.lastActiveAt,
    status: friend.status || 'accepted'
  }),

  // Sort friends by different criteria
  sortFriends: (friends, sortBy = 'name') => {
    const sortFunctions = {
      name: (a, b) => (a.name || '').localeCompare(b.name || ''),
      recent: (a, b) => {
        const dateA = new Date(a.lastActive || 0);
        const dateB = new Date(b.lastActive || 0);
        return dateB - dateA;
      },
      mutual: (a, b) => (b.mutualFriends || 0) - (a.mutualFriends || 0),
      online: (a, b) => {
        if (a.isOnline && !b.isOnline) return -1;
        if (!a.isOnline && b.isOnline) return 1;
        return (a.name || '').localeCompare(b.name || '');
      }
    };

    return [...friends].sort(sortFunctions[sortBy] || sortFunctions.name);
  },

  // Filter friends based on criteria
  filterFriends: (friends, filters = {}) => {
    return friends.filter(friend => {
      // Search filter
      if (filters.search) {
        const searchTerm = filters.search.toLowerCase();
        const name = (friend.name || '').toLowerCase();
        if (!name.includes(searchTerm)) return false;
      }

      // Online filter
      if (filters.onlineOnly && !friend.isOnline) return false;

      // Mutual friends filter
      if (filters.minMutual && (friend.mutualFriends || 0) < filters.minMutual) return false;

      return true;
    });
  },

  // Get friendship status display info
  getStatusInfo: (status) => {
    const statusMap = {
      'none': { text: 'Not Friends', color: 'gray', canAdd: true },
      'pending': { text: 'Request Sent', color: 'yellow', canAdd: false },
      'accepted': { text: 'Friends', color: 'green', canAdd: false },
      'declined': { text: 'Declined', color: 'red', canAdd: true },
      'blocked': { text: 'Blocked', color: 'red', canAdd: false }
    };

    return statusMap[status] || statusMap.none;
  },

  // Format time ago
  timeAgo: (dateString) => {
    if (!dateString) return 'Unknown';
    
    const now = new Date();
    const date = new Date(dateString);
    const diffInSeconds = Math.floor((now - date) / 1000);

    if (diffInSeconds < 60) return 'Just now';
    if (diffInSeconds < 3600) return `${Math.floor(diffInSeconds / 60)}m ago`;
    if (diffInSeconds < 86400) return `${Math.floor(diffInSeconds / 3600)}h ago`;
    if (diffInSeconds < 604800) return `${Math.floor(diffInSeconds / 86400)}d ago`;
    
    return date.toLocaleDateString();
  },

  // Validate search query
  validateSearchQuery: (query) => {
    if (!query || typeof query !== 'string') return false;
    const trimmed = query.trim();
    return trimmed.length >= 2 && trimmed.length <= 50;
  }
};


