import { BellIcon, MagnifyingGlassIcon, PaperAirplaneIcon, UsersIcon } from "@heroicons/react/24/outline";

export const GetEmptyStateContent = ({ activeTab, searchTerm = '' }) => {

    switch (activeTab) {
        case 'friends': return {
            title: 'No friends yet',
            description: 'Start connecting with people you know!',
            icon: <UsersIcon className="h-10 w-10 text-blue-600" />,
        }
        case 'requests': return {
            title: 'No pending requests',
            description: 'When people want to connect, their requests will appear here.',
            icon: <BellIcon className="h-10 w-10 text-blue-600" />,
        }
        case 'sent-requests': return {
            title: 'No sent requests',
            description: 'Friend requests you send will be tracked here.',
            icon: <PaperAirplaneIcon className="h-10 w-10 text-blue-600" />,
        }
        case 'search-term': return {
            title: 'No results found',
            description: `No friends found matching "${searchTerm}"`,
            icon: <MagnifyingGlassIcon className="h-10 w-10 text-blue-600" />,
        };
        default: return {
            title: 'No friends yet',
            description: 'Start connecting with people you know!',
            icon: <UsersIcon className="h-10 w-10 text-blue-600" />,
        }
    }
};
export default GetEmptyStateContent;