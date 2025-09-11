import { MoonIcon, SunIcon, LanguageIcon, MapPinIcon } from '@heroicons/react/24/outline';
import SettingItem from '../settingItem';
import Toggle from '../../../../components/common/Toggle';
import SelectField from '../../../../components/common/SelectField';
import { useState, useCallback } from 'react';

// Configuration constants
const LANGUAGE_OPTIONS = [
  { value: 'en', label: 'English' },
  { value: 'es', label: 'Spanish' },
  { value: 'fr', label: 'French' },
  { value: 'de', label: 'German' },
  { value: 'it', label: 'Italian' },
  { value: 'pt', label: 'Portuguese' }
];

const COUNTRY_OPTIONS = [
  { value: 'us', label: 'United States' },
  { value: 'uk', label: 'United Kingdom' },
  { value: 'ca', label: 'Canada' },
  { value: 'au', label: 'Australia' },
  { value: 'de', label: 'Germany' },
  { value: 'fr', label: 'France' }
];

export default function GeneralSection() {
  const [darkMode, setDarkMode] = useState(false);
  const [selectedLanguage, setSelectedLanguage] = useState('en');
  const [selectedCountry, setSelectedCountry] = useState('us');
  const [locationTracking, setLocationTracking] = useState(true);

  const handleDarkModeToggle = useCallback((enabled) => {
    setDarkMode(enabled);
    document.documentElement.classList.toggle('dark', enabled);
  }, []);

  const handleLanguageChange = useCallback((value) => {
    setSelectedLanguage(value);
  }, []);

  const handleCountryChange = useCallback((value) => {
    setSelectedCountry(value);
  }, []);

  const handleLocationToggle = useCallback((enabled) => {
    setLocationTracking(enabled);
    if (enabled && navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => console.log('Location enabled:', position.coords),
        (error) => console.warn('Location access denied:', error)
      );
    }
  }, []);

  return (
    <div className="space-y-6">
      <header>
        <h2 className="text-2xl font-bold mb-2 text-gray-900">
          General Settings
        </h2>
        <p className="text-gray-600">
          Manage your basic account preferences
        </p>
      </header>

      <SettingItem
        icon={darkMode ? MoonIcon : SunIcon}
        title="Dark Mode"
        description="Switch between light and dark theme"
      >
        <Toggle
          enabled={darkMode}
          onChange={handleDarkModeToggle}
          label="Enable dark mode"
        />
      </SettingItem>

      <SettingItem
        icon={LanguageIcon}
        title="Language"
        description="Choose your preferred language"
      >
        <SelectField
          value={selectedLanguage}
          onChange={handleLanguageChange}
          label="Language"
          options={LANGUAGE_OPTIONS}
        />
      </SettingItem>

      <SettingItem
        icon={MapPinIcon}
        title="Location"
        description="Set your current location"
      >
        <div className="space-y-3">
          <SelectField
            value={selectedCountry}
            onChange={handleCountryChange}
            label="Country"
            options={COUNTRY_OPTIONS}
          />
          <div className="flex items-center justify-between">
            <Toggle
              enabled={locationTracking}
              onChange={handleLocationToggle}
              label="Allow location tracking"
            />
          </div>
        </div>
      </SettingItem>
    </div>
  );
}