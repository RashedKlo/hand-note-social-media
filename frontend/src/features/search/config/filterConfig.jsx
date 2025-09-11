import {
    MagnifyingGlassIcon,
    UserIcon,
    UserGroupIcon,
    PhotoIcon,
    VideoCameraIcon,
    MapPinIcon,
    CalendarDaysIcon
} from '@heroicons/react/24/outline';

/**
 * Filter configuration with icons and styling
 */
export const filterConfig = [
    { name: 'All', icon: MagnifyingGlassIcon, count: 847, color: 'text-gray-600' },
    { name: 'People', icon: UserIcon, count: 156, color: 'text-blue-600' },
    { name: 'Pages', icon: UserGroupIcon, count: 89, color: 'text-green-600' },
    { name: 'Photos', icon: PhotoIcon, count: 342, color: 'text-purple-600' },
    { name: 'Videos', icon: VideoCameraIcon, count: 78, color: 'text-red-600' },
    { name: 'Places', icon: MapPinIcon, count: 45, color: 'text-yellow-600' },
    { name: 'Events', icon: CalendarDaysIcon, count: 23, color: 'text-indigo-600' }
];