// navItems.js
import {
  HomeIcon,
  UsersIcon,
  UserIcon,
  BellIcon,
  MagnifyingGlassIcon,
  Cog6ToothIcon,
} from "@heroicons/react/24/outline";
import {
  HomeIcon as HomeIconSolid,
  UsersIcon as UsersIconSolid,
  UserIcon as UserIconSolid,
  BellIcon as BellIconSolid,
  MagnifyingGlassIcon as MagnifyingGlassIconSolid,
  Cog6ToothIcon as Cog6ToothIconSolid
} from "@heroicons/react/24/solid";

// Function to generate navigation items dynamically based on currentUser
export const getNavigationItems = (currentUser) => [
  { 
    id: "home",
    icon: <HomeIcon className="w-6 h-6" />, 
    iconSolid: <HomeIconSolid className="w-6 h-6" />,
    label: "Home", 
    path: "/" ,
    count:0,

  },
  { 
    id: "friends",
    icon: <UsersIcon className="w-6 h-6" />, 
    iconSolid: <UsersIconSolid className="w-6 h-6" />,
    label: "Friends", 
    path: "/friends" ,
    count:99,

  },
  {
    id: "profile",
    icon: <UserIcon className="w-6 h-6" />,
    iconSolid: <UserIconSolid className="w-6 h-6" />,
    label: "Profile",
    path: `/profile/${currentUser?.userID || "me"}`,
    count:0,


  },
  {
    id: "notifications",
    icon:<BellIcon className="w-6 h-6" />,
    iconSolid:<BellIconSolid className="w-6 h-6" />,
    label: "Notifications",
    path: "/notifications",
    count:6,

  },
  {
    id: "search",
    icon: <MagnifyingGlassIcon className="w-6 h-6" />,
    iconSolid: <MagnifyingGlassIconSolid className="w-6 h-6" />,
    label: "Search",
    path: "/search",
    count:6,
  },
  {
    id:"settings",
    icon:<Cog6ToothIcon className="w-7 h-7"/>,
    iconSolid:<Cog6ToothIconSolid className="w-7 h-7"/>,
    label:"settings",
    path:"/settings",
    count:0
  }
];
