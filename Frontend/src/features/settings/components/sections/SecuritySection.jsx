import { useState } from 'react';
import { ShieldCheckIcon, DevicePhoneMobileIcon, ComputerDesktopIcon } from '@heroicons/react/24/outline';
import Button from '../../../../components/common/Button';
import SettingItem from '../settingItem';
import Toggle from '../../../../components/common/Toggle';

const INITIAL_SESSIONS = [
  {
    id: 1,
    device: 'Chrome on Windows',
    location: 'New York, NY',
    status: 'Active now',
    isCurrentSession: true
  },
  {
    id: 2,
    device: 'Safari on iPhone',
    location: 'Los Angeles, CA',
    status: '2 hours ago',
    isCurrentSession: false
  },
  {
    id: 3,
    device: 'Firefox on Mac',
    location: 'San Francisco, CA',
    status: '1 day ago',
    isCurrentSession: false
  }
];

export default function SecuritySection() {
  const [twoFactorEnabled, setTwoFactorEnabled] = useState(false);
  const [sessions, setSessions] = useState(INITIAL_SESSIONS);
  const [showAllSessions, setShowAllSessions] = useState(false);
  const [isLoading, setIsLoading] = useState({
    password: false,
    twoFactor: false,
    sessions: false
  });

  const handlePasswordChange = async () => {
    setIsLoading(prev => ({ ...prev, password: true }));
    await new Promise(resolve => setTimeout(resolve, 1000));
    setIsLoading(prev => ({ ...prev, password: false }));
    alert('Password change initiated! Check your email for instructions.');
  };

  const handleTwoFactorToggle = async (enabled) => {
    setIsLoading(prev => ({ ...prev, twoFactor: true }));
    await new Promise(resolve => setTimeout(resolve, 1500));
    setTwoFactorEnabled(enabled);
    setIsLoading(prev => ({ ...prev, twoFactor: false }));

    const message = enabled
      ? 'Two-factor authentication enabled successfully!'
      : 'Two-factor authentication disabled.';
    alert(message);
  };

  const handleSetup2FA = () => {
    alert('Redirecting to 2FA setup wizard...');
  };

  const handleSessionLogout = async (sessionId) => {
    setIsLoading(prev => ({ ...prev, sessions: true }));
    await new Promise(resolve => setTimeout(resolve, 800));
    setSessions(prev => prev.filter(session => session.id !== sessionId));
    setIsLoading(prev => ({ ...prev, sessions: false }));
    alert('Session logged out successfully!');
  };

  const toggleShowAllSessions = () => {
    setShowAllSessions(prev => !prev);
  };

  const visibleSessions = showAllSessions ? sessions : sessions.slice(0, 1);

  return (
    <div className="space-y-6">
      <header>
        <h2 className="text-2xl font-bold mb-2 text-gray-900">
          Security & Login
        </h2>
        <p className="text-gray-600">
          Keep your account secure
        </p>
      </header>

      <SettingItem
        icon={ShieldCheckIcon}
        title="Change Password"
        description="Update your password regularly for better security"
      >
        <Button
          onClick={handlePasswordChange}
          disabled={isLoading.password}
        >
          {isLoading.password ? 'Processing...' : 'Change Password'}
        </Button>
      </SettingItem>

      <SettingItem
        icon={DevicePhoneMobileIcon}
        title="Two-Factor Authentication"
        description="Add an extra layer of security to your account"
      >
        <div className="space-y-3">
          <div className="flex items-center justify-between">
            <span className="text-gray-900 font-medium">
              Enable 2FA
            </span>
            <Toggle
              enabled={twoFactorEnabled}
              onChange={handleTwoFactorToggle}
              disabled={isLoading.twoFactor}
            />
          </div>
          {!twoFactorEnabled && (
            <Button
              variant="secondary"
              onClick={handleSetup2FA}
              disabled={isLoading.twoFactor}
            >
              {isLoading.twoFactor ? 'Loading...' : 'Setup 2FA'}
            </Button>
          )}
        </div>
      </SettingItem>

      <SettingItem
        icon={ComputerDesktopIcon}
        title="Active Sessions"
        description={`See where you're logged in and log out of other sessions (${sessions.length} active)`}
      >
        <div className="space-y-3">
          {visibleSessions.map((session) => (
            <div
              key={session.id}
              className="p-3 bg-gray-100 border border-gray-300 rounded-lg hover:bg-gray-200 transition-colors duration-200"
            >
              <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
                <div className="flex-1">
                  <div className="flex items-center gap-2">
                    <p className="font-medium text-sm text-gray-900">
                      {session.device}
                    </p>
                    {session.isCurrentSession && (
                      <span className="px-2 py-1 bg-green-500 text-white rounded-full text-xs font-medium">
                        Current
                      </span>
                    )}
                  </div>
                  <p className="text-sm text-gray-600">
                    {session.location} • {session.status}
                  </p>
                </div>
                {!session.isCurrentSession && (
                  <Button
                    variant="secondary"
                    size="sm"
                    onClick={() => handleSessionLogout(session.id)}
                    disabled={isLoading.sessions}
                  >
                    Log Out
                  </Button>
                )}
              </div>
            </div>
          ))}

          {sessions.length > 1 && (
            <Button
              variant="secondary"
              onClick={toggleShowAllSessions}
            >
              {showAllSessions ? 'Show Less' : `See All Sessions (${sessions.length})`}
            </Button>
          )}

          {sessions.length === 0 && (
            <div className="text-center p-4 bg-gray-100 text-gray-600 rounded-lg">
              No active sessions found
            </div>
          )}
        </div>
      </SettingItem>
    </div>
  );
}