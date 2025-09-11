import { UserCircleIcon, PencilIcon, CalendarIcon } from '@heroicons/react/24/outline';
import { useState } from 'react';
import SettingItem from '../settingItem';
import Button from '../../../../components/common/Button';
import Toggle from '../../../../components/common/Toggle';

const INITIAL_PROFILE_DATA = {
  displayName: 'John Doe',
  bio: 'Software developer passionate about creating amazing user experiences.',
  birthday: '',
  location: 'New York, NY',
  showBirthday: false
};

export default function ProfileSection() {
  const [profileData, setProfileData] = useState(INITIAL_PROFILE_DATA);
  const [isEditing, setIsEditing] = useState(false);

  const handleSaveChanges = () => {
    console.log('Saving profile data:', profileData);
    setIsEditing(false);
  };

  const handleInputChange = (field, value) => {
    setProfileData(prev => ({ ...prev, [field]: value }));
    setIsEditing(true);
  };

  const handleCancel = () => {
    setProfileData(INITIAL_PROFILE_DATA);
    setIsEditing(false);
  };

  const getInitials = () => {
    return profileData.displayName
      .split(' ')
      .map(name => name[0])
      .join('')
      .toUpperCase();
  };

  return (
    <div className="space-y-6">
      <header>
        <h2 className="text-2xl font-bold mb-2 text-gray-900">
          Profile Settings
        </h2>
        <p className="text-gray-600">
          Manage your profile information
        </p>
      </header>

      <SettingItem
        icon={UserCircleIcon}
        title="Profile Picture"
        description="Change your profile picture"
      >
        <div className="flex flex-col sm:flex-row items-center gap-4">
          <div className="w-16 h-16 bg-gradient-to-br from-blue-500 to-blue-600 rounded-full flex items-center justify-center text-white font-bold text-xl flex-shrink-0">
            {getInitials()}
          </div>
          <div className="flex flex-wrap gap-2">
            <Button variant="secondary">Change Photo</Button>
            <Button variant="secondary">Remove</Button>
          </div>
        </div>
      </SettingItem>

      <SettingItem
        icon={PencilIcon}
        title="Basic Information"
        description="Update your name, bio, and other details"
      >
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium mb-1 text-gray-900">
              Display Name
            </label>
            <input
              type="text"
              value={profileData.displayName}
              onChange={(e) => handleInputChange('displayName', e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-white text-gray-900 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
            />
          </div>
          <div>
            <label className="block text-sm font-medium mb-1 text-gray-900">
              Bio
            </label>
            <textarea
              rows={3}
              value={profileData.bio}
              onChange={(e) => handleInputChange('bio', e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-white text-gray-900 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors resize-none"
              placeholder="Tell us a bit about yourself..."
            />
          </div>
          <div className="flex gap-2">
            <Button
              onClick={handleSaveChanges}
              disabled={!isEditing}
              className={!isEditing ? 'opacity-50 cursor-not-allowed' : ''}
            >
              Save Changes
            </Button>
            {isEditing && (
              <Button variant="secondary" onClick={handleCancel}>
                Cancel
              </Button>
            )}
          </div>
        </div>
      </SettingItem>

      <SettingItem
        icon={CalendarIcon}
        title="Personal Details"
        description="Manage your personal information"
      >
        <div className="space-y-3">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium mb-1 text-gray-900">
                Birthday
              </label>
              <input
                type="date"
                value={profileData.birthday}
                onChange={(e) => handleInputChange('birthday', e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-white text-gray-900 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
              />
            </div>
            <div>
              <label className="block text-sm font-medium mb-1 text-gray-900">
                Location
              </label>
              <input
                type="text"
                value={profileData.location}
                onChange={(e) => handleInputChange('location', e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-white text-gray-900 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors"
                placeholder="Enter your location"
              />
            </div>
          </div>
          <Toggle
            enabled={profileData.showBirthday}
            onChange={(value) => handleInputChange('showBirthday', value)}
            label="Show birthday on profile"
          />
          {isEditing && (
            <div className="pt-2">
              <Button onClick={handleSaveChanges}>
                Save Personal Details
              </Button>
            </div>
          )}
        </div>
      </SettingItem>
    </div>
  );
}