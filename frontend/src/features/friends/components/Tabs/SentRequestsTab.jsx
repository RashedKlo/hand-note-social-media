import { useState } from "react";
import GetEmptyStateContent from "../EmptyState/GetEmptyStateContent";
import FriendRequestCard from "../Card/FriendRequestCard";
import SentRequestCard from "../Card/SentRequestCard";

const sentRequests = [
    {
        id: 7,
        name: 'Alex Rodriguez',
        avatar: 'https://images.unsplash.com/photo-1519244703995-f4e0f30006d5?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 3,
        sentDate: '2 days ago',
    },
    {
        id: 8,
        name: 'Lisa Park',
        avatar: 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&h=150&fit=crop&crop=face',
        mutualFriends: 7,
        sentDate: '1 week ago',
    },
]
const SentRequestsTab = () => {
    const [searchTerm, setSearchTerm] = useState('');
    return <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
        <div className="mb-6">
            <div className="mb-2 flex items-center justify-between">
                <h2 className="text-lg font-bold text-gray-900 sm:text-xl">Sent Requests</h2>
                <div className="flex items-center gap-2">
                    <span className="badge bg-gray-100 text-gray-600">
                        {sentRequests.length} {sentRequests.length === 1 ? 'person' : 'people'}
                    </span>
                </div>
            </div>
            <p className="text-sm text-gray-500 sm:text-base">Requests waiting for response</p>
        </div>

        {sentRequests.length === 0 || searchTerm ? (
            <EmptyState title={GetEmptyStateContent('requests')?.title} description={GetEmptyStateContent("requests")?.description}
                Icon={GetEmptyStateContent("requests")?.icon} />
        ) : (
            <div className="grid gap-4 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4">
                {sentRequests?.map((request) => (
                    <SentRequestCard
                        key={request.id}
                        request={request}
                    />
                ))}
            </div>
        )}
    </div>
}
export default SentRequestsTab;