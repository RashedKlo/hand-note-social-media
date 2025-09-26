import { UserCircleIcon, PhotoIcon, UserGroupIcon, ChatBubbleLeftRightIcon } from '@heroicons/react/24/outline';
import { useState } from 'react';
import SettingItem from '../settingItem';
import SelectField from '../../../../components/common/SelectField';
import Toggle from '../../../../components/common/Toggle';

const PRIVACY_OPTIONS = {
  basic: [
    { value: 'public', label: 'Public' },
    { value: 'friends', label: 'Friends' },
    { value: 'only-me', label: 'Only Me' }
  ],
  posts: [
    { value: 'public', label: 'Public' },
    { value: 'friends', label: 'Friends' },
    { value: 'friends-except', label: 'Friends except...' },
    { value: 'only-me', label: 'Only Me' }
  ],
  photos: [
    { value: 'friends', label: 'Friends' },
    { value: 'friends-of-friends', label: 'Friends of Friends' },
    { value: 'only-me', label: 'Only Me' }
  ]
};

export default function PrivacySection() {
  const [privacy, setPrivacy] = useState({
    profile: 'friends',
    posts: 'friends',
    friendsList: 'only-me',
    photos: 'friends'
  });

  const handlePrivacyChange = (field, value) => {
    setPrivacy(prev => ({ ...prev, [field]: value }));
  };

  return (
    <div className="space-y-6">
      <header>
        <h2 className="text-2xl font-bold mb-2 text-gray-900">
          Privacy Settings
        </h2>
        <p className="text-gray-600">
          Control who can see your content
        </p>
      </header>

      <SettingItem
        icon={UserCircleIcon}
        title="Profile Visibility"
        description="Who can see your profile information"
      >
        <SelectField
          value={privacy.profile}
          onChange={(value) => handlePrivacyChange('profile', value)}
          label="Profile visibility"
          options={PRIVACY_OPTIONS.basic}
        />
      </SettingItem>

      <SettingItem
        icon={ChatBubbleLeftRightIcon}
        title="Posts and Stories"
        description="Control who can see your posts and stories"
      >
        <div className="space-y-3">
          <SelectField
            value={privacy.posts}
            onChange={(value) => handlePrivacyChange('posts', value)}
            label="Default post privacy"
            options={PRIVACY_OPTIONS.posts}
          />
          <Toggle
            enabled={true}
            onChange={() => { }}
            label="Allow friends to tag you in posts"
          />
        </div>
      </SettingItem>

      <SettingItem
        icon={PhotoIcon}
        title="Photos and Videos"
        description="Control who can see your media content"
      >
        <div className="space-y-3">
          <SelectField
            value={privacy.photos}
            onChange={(value) => handlePrivacyChange('photos', value)}
            label="Photo/Video privacy"
            options={PRIVACY_OPTIONS.photos}
          />
          <Toggle
            enabled={false}
            onChange={() => { }}
            label="Allow photo tagging by others"
          />
        </div>
      </SettingItem>

      <SettingItem
        icon={UserGroupIcon}
        title="Friend List"
        description="Control who can see your friends list"
      >
        <SelectField
          value={privacy.friendsList}
          onChange={(value) => handlePrivacyChange('friendsList', value)}
          label="Friends list visibility"
          options={PRIVACY_OPTIONS.basic}
        />
      </SettingItem>
    </div>
  );
}