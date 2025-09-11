import { useState } from 'react';


import NotificationHeader from '../../features/notifications/components/NotificationHeader';
import NotificationsList from '../../features/notifications/components/NotificationsList';

const Notifications = () => {
  const [notifications, setNotifications] = useState([
    {
      id: 1,
      type: 'like_post',
      user: { name: 'Sarah Johnson', avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b786?w=50&h=50&fit=crop&crop=face' },
      message: 'liked your post',
      postPreview: 'Just finished my morning workout! Feeling energized 💪',
      timestamp: new Date(Date.now() - 5 * 60 * 1000),
      isRead: false
    },
    {
      id: 2,
      type: 'comment_reply',
      user: { name: 'Mike Chen', avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=50&h=50&fit=crop&crop=face' },
      message: 'replied to your comment',
      postPreview: 'Great photo! Where was this taken?',
      timestamp: new Date(Date.now() - 15 * 60 * 1000),
      isRead: false
    },
    {
      id: 3,
      type: 'friend_request',
      user: { name: 'Emily Rodriguez', avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=50&h=50&fit=crop&crop=face' },
      message: 'sent you a friend request',
      timestamp: new Date(Date.now() - 30 * 60 * 1000),
      isRead: false
    },
    {
      id: 4,
      type: 'like_comment',
      user: { name: 'David Park', avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=50&h=50&fit=crop&crop=face' },
      message: 'liked your comment',
      postPreview: 'Thanks for sharing this! Very helpful.',
      timestamp: new Date(Date.now() - 2 * 60 * 60 * 1000),
      isRead: true
    },
    {
      id: 5,
      type: 'like_post',
      user: { name: 'Lisa Wang', avatar: 'https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=50&h=50&fit=crop&crop=face' },
      message: 'and 3 others liked your post',
      postPreview: 'Sunday brunch with friends! 🥞☕',
      timestamp: new Date(Date.now() - 4 * 60 * 60 * 1000),
      isRead: true
    },
    {
      id: 6,
      type: 'comment_reply',
      user: { name: 'Alex Thompson', avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=50&h=50&fit=crop&crop=face' },
      message: 'mentioned you in a comment',
      postPreview: '@you should check this out!',
      timestamp: new Date(Date.now() - 6 * 60 * 60 * 1000),
      isRead: true
    }
  ]);
  const [activeTab, setActiveTab] = useState('all');
  // Filter notifications based on active tab
  const filteredNotifications = notifications.filter(notification => {
    if (activeTab === 'unread') return !notification.isRead;
    return true;
  });


  return (
    <div className="min-h-screen bg-gray-100">
      {/* Main Content */}
      <div className="max-w-2xl mx-auto px-4 py-16 sm:px-6">
        <NotificationHeader
          notifications={notifications}
          setNotifications={setNotifications}
          activeTab={activeTab}
          setActiveTab={setActiveTab}
        />

        <NotificationsList
          activeTab={activeTab}
          setNotifications={setNotifications}
          filteredNotifications={filteredNotifications}
        />

        {filteredNotifications.length > 0 && (
          <div className="text-center mt-6">
            <button className="text-blue-600 hover:text-blue-700 font-semibold hover:bg-blue-50 px-6 py-2 rounded-xl transition-all duration-200">
              See older notifications
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default Notifications;