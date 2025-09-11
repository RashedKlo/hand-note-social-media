import { useState } from "react";
import FriendCard from "../Card/FriendCard";
import GetEmptyStateContent from "../EmptyState/GetEmptyStateContent";

const friends = [
    {
        id: 1,
        name: 'John Doe',
        avatar: 'https://images.unsplash.com/photo-1519085360753-af0119f7cbe7?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 12,
        status: 'online',
        lastActive: '2 hours ago',
        isOnline: true,
    },
    {
        id: 2,
        name: 'David Wilson',
        avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 8,
        status: 'away',
        lastActive: '1 day ago',
        isOnline: false,
    },
    {
        id: 3,
        name: 'Mike Johnson',
        avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 15,
        status: 'online',
        lastActive: 'Just now',
        isOnline: true,
    },
    {
        id: 4,
        name: 'Alex Chen',
        avatar: 'https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?w=150&h=150&fit=crop&crop=face',
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
        name: 'James Liu',
        avatar: 'https://images.unsplash.com/photo-1463453091185-61582044d556?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 4,
        status: 'offline',
        lastActive: '3 days ago',
        isOnline: false,
    },
];
const AllFriendsTab = () => {
    const [searchTerm, setSearchTerm] = useState('');
    return <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
        <div className="mb-6">
            <div className="mb-2 flex items-center justify-between">
                <h2 className="text-lg font-bold text-gray-900 sm:text-xl">All Friends</h2>
                <div className="flex items-center gap-2">
                    <span className="badge bg-gray-100 text-gray-600">
                        {friends.length} {friends.length === 1 ? 'person' : 'people'}
                    </span>
                </div>
            </div>
            <p className="text-sm text-gray-500 sm:text-base">Stay connected with your friends</p>
        </div>

        {friends.length === 0 || searchTerm ? (
            <EmptyState title={GetEmptyStateContent('friends')?.title} description={GetEmptyStateContent("friends")?.description}
                Icon={GetEmptyStateContent("friends")?.icon} />
        ) : (
            <div className="grid gap-4 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4">
                {friends?.map((person) => (
                    <FriendCard
                        key={person.id}
                        friend={person}
                    />
                ))}
            </div>
        )}
    </div>
}
export default AllFriendsTab;