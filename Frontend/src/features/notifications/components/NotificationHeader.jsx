// Component for the header section including title, unread count, mark all read button, and tabs
const NotificationHeader = ({ activeTab, setActiveTab, notifications, setNotifications }) => {

    const unreadCount = notifications.filter(n => !n.isRead).length;
    const markAllAsRead = () => {
        setNotifications(prev =>
            prev.map(notification => ({ ...notification, isRead: true }))
        );
    };


    return (
        <div className="bg-white rounded-2xl shadow-sm mb-6 overflow-hidden">
            <div className="p-6 pb-4">
                <div className="flex items-center justify-between mb-6">
                    <div>
                        <h1 className="text-2xl font-bold text-gray-900 mb-1">Notifications</h1>
                        <p className="text-gray-500 text-sm">
                            {unreadCount > 0
                                ? `${unreadCount} new notification${unreadCount > 1 ? 's' : ''}`
                                : 'You\'re all caught up!'
                            }
                        </p>
                    </div>
                    {unreadCount > 0 && (
                        <button
                            onClick={markAllAsRead}
                            className="bg-blue-600 hover:bg-blue-700 text-white font-semibold px-4 py-2 rounded-xl transition-all duration-200 text-sm shadow-sm hover:shadow-md"
                        >
                            Mark all read
                        </button>
                    )}
                </div>

                {/* Modern Tab System */}
                <div className="flex bg-gray-100 rounded-xl p-1">
                    {[
                        { key: 'all', label: 'All', count: notifications?.length },
                        { key: 'unread', label: 'Unread', count: unreadCount }
                    ].map((tab) => (
                        <button
                            key={tab.key}
                            onClick={() => setActiveTab(tab.key)}
                            className={`flex-1 py-2.5 px-4 rounded-lg text-sm font-semibold transition-all duration-200 ${activeTab === tab.key
                                ? 'bg-white text-blue-600 shadow-sm'
                                : 'text-gray-600 hover:text-gray-800'
                                }`}
                        >
                            {tab.label}
                            {tab.count > 0 && (
                                <span className={`ml-2 px-2 py-0.5 rounded-full text-xs ${activeTab === tab.key ? 'bg-blue-100 text-blue-600' : 'bg-gray-200 text-gray-600'
                                    }`}>
                                    {tab.count}
                                </span>
                            )}
                        </button>
                    ))}
                </div>
            </div>
        </div>
    );
};
export default NotificationHeader;