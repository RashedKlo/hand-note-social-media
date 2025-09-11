
import { 
  UserCircleIcon,
  ShieldCheckIcon,
  BellIcon,
  EyeIcon,
  CogIcon,
  QuestionMarkCircleIcon,
  ChevronRightIcon,
} from '@heroicons/react/24/outline';
export const sidebarItems = [
    {
      id: 'general',
      title: 'General',
      icon: CogIcon,
      description: 'Basic account settings'
    },
    {
      id: 'security',
      title: 'Security and Login',
      icon: ShieldCheckIcon,
      description: 'Password and login settings'
    },
    {
      id: 'privacy',
      title: 'Privacy',
      icon: EyeIcon,
      description: 'Control who sees your content'
    },
    {
      id: 'notifications',
      title: 'Notifications',
      icon: BellIcon,
      description: 'Email and push notifications'
    },
    {
      id: 'profile',
      title: 'Profile',
      icon: UserCircleIcon,
      description: 'Manage your profile information'
    },
    {
      id: 'help',
      title: 'Help & Support',
      icon: QuestionMarkCircleIcon,
      description: 'Get help and contact support'
    }
  ];