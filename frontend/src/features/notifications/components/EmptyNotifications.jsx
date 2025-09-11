import { BellIcon } from "@heroicons/react/24/outline";

const EmptyNotifications = ({ activeTab }) => {
    return (
        <div className="text-center py-16">
            <div className="w-20 h-20 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <BellIcon className="w-10 h-10 text-gray-400" />
            </div>
            <h3 className="text-lg font-semibold text-gray-900 mb-2">
                {activeTab === 'unread' ? 'No unread notifications' : 'No notifications yet'}
            </h3>
            <p className="text-gray-500 text-sm max-w-xs mx-auto">
                {activeTab === 'unread'
                    ? 'All your notifications have been read.'
                    : 'When you have notifications, they\'ll appear here.'
                }
            </p>
        </div>
    );
};
export default EmptyNotifications;