import { PhotoIcon, PlayIcon, ShoppingBagIcon, UserGroupIcon, UsersIcon } from "@heroicons/react/24/outline";
import { memo } from "react";

// LeftSidebar Component
 const LeftSidebar = memo(({ currentUser }) => (
  <aside className="hidden lg:block w-64 xl:w-80 p-2 lg:p-4 fixed left-0 top-14 h-screen overflow-y-auto">
    <div className="space-y-4">
      {/* User Profile */}
      <button onClick={() => alert('View Profile')} className="flex items-center space-x-3 p-2 hover:bg-gray-100 rounded-lg cursor-pointer w-full text-left">
        <img src={currentUser.avatar} alt={currentUser.name} className="w-9 h-9 rounded-full object-cover" />
        <span className="font-medium text-gray-900">{currentUser.name}</span>
      </button>

      {/* Quick Links */}
      <div className="space-y-1">
        {[
          { icon: UsersIcon, label: 'Friends', color: 'text-blue-500' },
          { icon: UserGroupIcon, label: 'Groups', color: 'text-blue-500' },
          { icon: ShoppingBagIcon, label: 'Marketplace', color: 'text-blue-500' },
          { icon: PlayIcon, label: 'Watch', color: 'text-blue-500' },
          { icon: PhotoIcon, label: 'Photos', color: 'text-green-500' },
        ].map((item, index) => (
          <button key={index} onClick={() => alert(`${item.label} coming soon`)} className="flex items-center space-x-3 p-2 hover:bg-gray-100 rounded-lg cursor-pointer w-full text-left">
            <item.icon className={`w-6 h-6 ${item.color}`} />
            <span className="text-gray-900">{item.label}</span>
          </button>
        ))}
      </div>

      {/* Shortcuts */}
      <div className="border-t border-gray-200 pt-4">
        <h3 className="font-semibold text-gray-700 mb-2">Your shortcuts</h3>
        <div className="space-y-2">
          <button onClick={() => alert('Group coming soon')} className="flex items-center space-x-3 p-2 hover:bg-gray-100 rounded-lg cursor-pointer w-full text-left">
            <div className="w-6 h-6 bg-blue-100 rounded-lg flex items-center justify-center">
              <UserGroupIcon className="w-4 h-4 text-blue-600" />
            </div>
            <span className="text-gray-900 text-sm">React Developers</span>
          </button>
        </div>
      </div>
    </div>
  </aside>
));
export default LeftSidebar;