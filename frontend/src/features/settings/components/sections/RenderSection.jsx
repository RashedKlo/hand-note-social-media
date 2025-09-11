import GeneralSection from "./GeneralSection";
import HelpSection from "./HelpSection";
import NotificationSection from "./NotificationSection";
import PrivacySection from "./PrivacySection";
import ProfileSection from "./ProfileSection";
import SecuritySection from "./SecuritySection";

export default function RenderSection({ activeSection }) {
  switch (activeSection) {
    case 'general':
      return <GeneralSection />;

    case 'security':
      return <SecuritySection />;

    case 'privacy':
      return <PrivacySection />;

    case 'notifications':
      return <NotificationSection />;

    case 'profile':
      return <ProfileSection />;

    case 'help':
      return <HelpSection />;

    default:
      return null;
  }
}