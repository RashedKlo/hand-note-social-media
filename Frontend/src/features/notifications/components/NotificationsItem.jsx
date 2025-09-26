import { formatTimestamp } from "../utils/timeAgo";
import {
    BellIcon,
    HeartIcon,
    ChatBubbleLeftRightIcon,
    UserPlusIcon,
    EllipsisHorizontalIcon,
    HomeIcon,
    MagnifyingGlassIcon,
    UserGroupIcon,
    PlayIcon,
    ShoppingBagIcon,
    Bars3Icon,
    ChevronDownIcon,
    XMarkIcon,
    UsersIcon
} from '@heroicons/react/24/outline';
import {
    HeartIcon as HeartSolid,
    ChatBubbleLeftRightIcon as ChatSolid,
    UserPlusIcon as UserPlusSolid,
    BellIcon as BellSolid
} from '@heroicons/react/24/solid';
// Component for rendering a single notification item
const NotificationItem = ({ notification, setNotifications }) => {

    const deleteNotification = (notificationId) => {
        setNotifications(prev => prev.filter(n => n.id !== notificationId));
    };
    // Handle friend request actions
    const handleFriendRequest = (notificationId, action) => {
        console.log(`Friend request ${action} for notification ${notificationId}`);
        deleteNotification(notificationId);
    };
    const getNotificationIcon = (type) => {
        const iconClass = 'w-5 h-5';
        switch (type) {
            case 'like_post':
            case 'like_comment':
                return <HeartSolid className={`${iconClass} text-red-500`} />;
            case 'comment_reply':
                return <ChatSolid className={`${iconClass} text-blue-500`} />;
            case 'friend_request':
                return <UserPlusSolid className={`${iconClass} text-green-500`} />;
            default:
                return <BellSolid className={`${iconClass} text-gray-500`} />;
        }
    };

    const markAsRead = (notificationId) => {
        setNotifications(prev =>
            prev.map(notification =>
                notification.id === notificationId
                    ? { ...notification, isRead: true }
                    : notification
            )
        );
    };
    return (
        <div
            key={notification.id}
            className={`relative p-4 hover:bg-gray-50 transition-all duration-200 cursor-pointer group ${!notification.isRead ? 'bg-blue-50/30' : ''
                }`}
            onClick={() => markAsRead(notification.id)}
        >
            <div className="flex items-start space-x-3">

                {/* User Avatar with Icon Badge */}
                <div className="flex-shrink-0 relative">
                    <img
                        src={notification.user.avatar}
                        alt={notification.user.name}
                        className="w-14 h-14 rounded-full object-cover ring-2 ring-white"
                    />
                    <div className="absolute -bottom-1 -right-1 bg-white rounded-full p-1 shadow-md">
                        {getNotificationIcon(notification.type)}
                    </div>
                </div>

                {/* Notification Content */}
                <div className="flex-1 min-w-0">
                    <div className="flex items-start justify-between">
                        <div className="flex-1 min-w-0 pr-4">

                            {/* Main Message */}
                            <p className="text-sm leading-5">
                                <span className="font-semibold text-gray-900">
                                    {notification.user.name}
                                </span>
                                <span className="text-gray-600 ml-1">
                                    {notification.message}
                                </span>
                            </p>

                            {/* Post Preview */}
                            {notification.postPreview && (
                                <p className="text-sm text-gray-500 mt-1.5 italic line-clamp-2 bg-gray-50 px-3 py-2 rounded-lg">
                                    "{notification.postPreview}"
                                </p>
                            )}

                            {/* Timestamp and Read Indicator */}
                            <div className="flex items-center space-x-2 mt-2">
                                <span className="text-sm text-blue-600 font-medium">
                                    {formatTimestamp(notification.timestamp)}
                                </span>
                                {!notification.isRead && (
                                    <div className="w-2 h-2 bg-blue-600 rounded-full"></div>
                                )}
                            </div>

                            {/* Friend Request Actions */}
                            {notification.type === 'friend_request' && !notification.isRead && (
                                <div className="flex space-x-2 mt-3">
                                    <button
                                        onClick={(e) => {
                                            e.stopPropagation();
                                            handleFriendRequest(notification.id, 'accept');
                                        }}
                                        className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-xl text-sm font-semibold transition-colors duration-200"
                                    >
                                        Confirm
                                    </button>
                                    <button
                                        onClick={(e) => {
                                            e.stopPropagation();
                                            handleFriendRequest(notification.id, 'decline');
                                        }}
                                        className="bg-gray-200 hover:bg-gray-300 text-gray-700 px-6 py-2 rounded-xl text-sm font-semibold transition-colors duration-200"
                                    >
                                        Delete
                                    </button>
                                </div>
                            )}
                        </div>

                        {/* Actions Menu */}
                        <div className="flex-shrink-0">
                            <button
                                onClick={(e) => {
                                    e.stopPropagation();
                                    deleteNotification(notification.id);
                                }}
                                className="text-gray-400 hover:text-gray-600 p-2 rounded-full hover:bg-gray-100 transition-all duration-200 opacity-0 group-hover:opacity-100"
                                title="Remove notification"
                            >
                                <XMarkIcon className="w-5 h-5" />
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};
export default NotificationItem;