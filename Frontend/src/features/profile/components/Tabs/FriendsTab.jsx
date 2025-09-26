import FriendCard from "../../../friends/components/Card/FriendCard";
const friends =
    [
        {
            id: 1,
            name: 'John Doe',
            avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&h=150&fit=crop&crop=face',
            mutualFriends: 12,
            status: 'online',
            lastActive: '2 hours ago',
            isOnline: true,
        },
        {
            id: 2,
            name: 'Sarah Wilson',
            avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b786?w=150&h=150&fit=crop&crop=face',
            mutualFriends: 8,
            status: 'away',
            lastActive: '1 day ago',
            isOnline: false,
        },
        {
            id: 3,
            name: 'Mike Johnson',
            avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&h=150&fit=crop&crop=face',
            mutualFriends: 15,
            status: 'online',
            lastActive: 'Just now',
            isOnline: true,
        },
        {
            id: 4,
            name: 'Emily Chen',
            avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=150&h=150&fit=crop&crop=face',
            mutualFriends: 6,
            status: 'busy',
            lastActive: '5 minutes ago',
            isOnline: true,
        },
        {
            id: 5,
            name: 'Robert Kim',
            avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&h=150&fit=crop&crop=face',
            mutualFriends: 11,
            status: 'online',
            lastActive: 'Active now',
            isOnline: true,
        },
        {
            id: 6,
            name: 'Jessica Liu',
            avatar: 'https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=150&h=150&fit=crop&crop=face',
            mutualFriends: 4,
            status: 'offline',
            lastActive: '3 days ago',
            isOnline: false,
        },
    ];
const FriendsTab = () => {
    return <div className="bg-white rounded-lg shadow-sm p-6">
        <div className="flex items-center justify-between mb-6">
            <h3 className="text-xl font-semibold text-gray-900">Friends</h3>
            <div className="flex items-center space-x-4">
                <input
                    type="text"
                    placeholder="Search friends"
                    className="bg-gray-100 rounded-full px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                />
            </div>
        </div>
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {friends.map(friend => (
                <FriendCard friend={friend} />
            ))}
        </div>
    </div>
}
export default FriendsTab;