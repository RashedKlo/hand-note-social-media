import { useState } from "react";
import GetEmptyStateContent from "../EmptyState/GetEmptyStateContent";
import FriendRequestCard from "../Card/FriendRequestCard";

const friendRequests = [
    {
        id: 9,
        name: 'David Brown',
        avatar: 'https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 5,
        receivedDate: '3 hours ago',
    },
    {
        id: 10,
        name: 'Anna Taylor',
        avatar: 'https://images.unsplash.com/photo-1517841905240-472988babdf9?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 9,
        receivedDate: '1 day ago',
    },
    {
        id: 11,
        name: 'Chris Lee',
        avatar: 'https://images.unsplash.com/photo-1507591064344-4c6ce005b128?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 2,
        receivedDate: '4 days ago',
    },

];
const FriendRequestsTab = () => {
    const [searchTerm, setSearchTerm] = useState('');
    return <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
        <div className="mb-6">
            <div className="mb-2 flex items-center justify-between">
                <h2 className="text-lg font-bold text-gray-900 sm:text-xl">Friend Requests</h2>
                <div className="flex items-center gap-2">
                    <span className="badge bg-gray-100 text-gray-600">
                        {friendRequests.length} {friendRequests.length === 1 ? 'person' : 'people'}
                    </span>
                </div>
            </div>
            <p className="text-sm text-gray-500 sm:text-base">People who want to connect with you</p>
        </div>

        {friendRequests.length === 0 || searchTerm ? (
            <EmptyState title={GetEmptyStateContent('requests')?.title} description={GetEmptyStateContent("requests")?.description}
                Icon={GetEmptyStateContent("requests")?.icon} />
        ) : (
            <div className="grid gap-4 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4">
                {friendRequests?.map((request) => (
                    <FriendRequestCard
                        key={request.id}
                        friendRequest={request}
                    />
                ))}
            </div>
        )}
    </div>
}
export default FriendRequestsTab;