import { useState } from "react";
import { BellIcon, EnvelopeIcon } from '@heroicons/react/24/outline';
import SettingItem from "../settingItem";
import Toggle from "../../../../components/common/Toggle";

export default function NotificationSection() {
  const [notifications, setNotifications] = useState({
    comments: true,
    likes: true,
    messages: false,
    friendRequests: true,
    posts: true
  });

  const handleNotificationChange = (key, value) => {
    setNotifications(prev => ({ ...prev, [key]: value }));
  };

  return (
    <div className="space-y-6">
      <header>
        <h2 className="text-2xl font-bold mb-2 text-gray-900">
          Notifications
        </h2>
        <p className="text-gray-600">
          Choose what notifications you want to receive
        </p>
      </header>

      <SettingItem
        icon={BellIcon}
        title="Push Notifications"
        description="Get notified about activity on your posts and profile"
      >
        <div className="space-y-4">
          <Toggle
            enabled={notifications.comments}
            onChange={(value) => handleNotificationChange('comments', value)}
            label="Comments on your posts"
          />
          <Toggle
            enabled={notifications.likes}
            onChange={(value) => handleNotificationChange('likes', value)}
            label="Likes and reactions"
          />
          <Toggle
            enabled={notifications.posts}
            onChange={(value) => handleNotificationChange('posts', value)}
            label="New posts from friends"
          />
          <Toggle
            enabled={notifications.friendRequests}
            onChange={(value) => handleNotificationChange('friendRequests', value)}
            label="Friend requests"
          />
        </div>
      </SettingItem>

      <SettingItem
        icon={EnvelopeIcon}
        title="Email Notifications"
        description="Get important updates via email"
      >
        <div className="space-y-4">
          <Toggle
            enabled={notifications.messages}
            onChange={(value) => handleNotificationChange('messages', value)}
            label="New messages"
          />
          <Toggle
            enabled={true}
            onChange={() => { }}
            label="Security alerts"
            disabled
          />
          <Toggle
            enabled={false}
            onChange={() => { }}
            label="Weekly summary"
            disabled
          />
        </div>
      </SettingItem>
    </div>
  );
}