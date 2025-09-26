import EmptyNotifications from "./EmptyNotifications";
import NotificationItem from "./NotificationsItem";

const NotificationsList = ({ activeTab, filteredNotifications, setNotifications }) => {
    // Delete notification

    return (
        <div className="bg-white rounded-2xl shadow-sm overflow-hidden">
            {filteredNotifications.length === 0 ? (
                <EmptyNotifications activeTab={activeTab} />
            ) : (
                <div className="divide-y divide-gray-100">
                    {filteredNotifications.map((notification, index) => (
                        <NotificationItem
                            key={index}
                            notification={notification}
                            setNotifications={setNotifications}
                        />
                    ))}
                </div>
            )}
        </div>
    );
};
export default NotificationsList;