import RenderSection from '../../features/settings/components/sections/RenderSection';
import DesktopSidebar from '../../features/settings/components/Sidebar/DesktopSidebar';
import Header from '../../features/settings/components/Header/Header';
import MobileNavTabs from '../../features/settings/components/Header/MobileNavTabs';
import { useState } from 'react';
export default function SettingsPage() {
  const [activeSection, setActiveSection] = useState('general');
  return (
    <div className="min-h-screen" style={{ backgroundColor: 'var(--color-bg-primary)' }}>
      {/* Container with responsive spacing for navbars */}
      <div className="pt-[40px] pb-[80px]  md:pb-[60px]  lg:pb-8">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          {/* Header */}
          <Header />
          {/* Mobile Navigation Tabs */}
          <MobileNavTabs setActiveSection={setActiveSection} activeSection={activeSection} />
          <div className="flex flex-col lg:flex-row gap-4 sm:gap-6 lg:gap-8">
            <DesktopSidebar setActiveSection={setActiveSection} activeSection={activeSection} />
            {/* Main Content */}
            <div className="flex-1 min-w-0">
              <div
                className="rounded-xl border p-4 sm:p-6"
                style={{
                  backgroundColor: 'var(--color-bg-primary)',
                  borderColor: 'var(--color-border-primary)',
                }}
              >
                <RenderSection activeSection={activeSection} />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}