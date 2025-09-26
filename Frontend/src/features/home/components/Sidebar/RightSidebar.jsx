import { EllipsisHorizontalIcon, MagnifyingGlassCircleIcon } from "@heroicons/react/24/outline";
import { memo } from "react";
const mockFriends = [
  { id: 2, name: 'Sarah Johnson', avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b1c3?w=40&h=40&fit=crop&crop=face' },
  { id: 3, name: 'John Doe', avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=32&h=32&fit=crop&crop=face' },
  { id: 4, name: 'Emma Wilson', avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=32&h=32&fit=crop&crop=face' },
  { id: 5, name: 'Mike Chen', avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=32&h=32&fit=crop&crop=face' },
];
// RightSidebar Component
const RightSidebar = memo(() => (
  <aside className="hidden xl:block w-64 2xl:w-80 p-2 xl:p-4 fixed right-0 top-14 h-screen overflow-y-auto">
    <div className="space-y-4">
      {/* Sponsored */}
      <div>
        <h3 className="font-semibold text-gray-700 mb-3">Sponsored</h3>
        <div className="space-y-3">
          <button onClick={() => window.open('https://example.com', '_blank')} className="flex space-x-3 cursor-pointer hover:bg-gray-50 p-2 rounded-lg w-full text-left">
            <img src="https://images.unsplash.com/photo-1556742049-0cfed4f6a45d?w=100&h=100&fit=crop" alt="Ad" className="w-16 h-16 rounded-lg object-cover" />
            <div>
              <p className="font-medium text-sm text-gray-900">Amazing Product</p>
              <p className="text-xs text-gray-500">example.com</p>
            </div>
          </button>
        </div>
      </div>

      {/* Contacts */}
      <div className="border-t border-gray-200 pt-4">
        <div className="flex items-center justify-between mb-3">
          <h3 className="font-semibold text-gray-700">Contacts</h3>
          <div className="flex space-x-2">
            <button className="p-1 hover:bg-gray-100 rounded">
              <MagnifyingGlassCircleIcon className="w-4 h-4 text-gray-500" />
            </button>
            <button className="p-1 hover:bg-gray-100 rounded">
              <EllipsisHorizontalIcon className="w-4 h-4 text-gray-500" />
            </button>
          </div>
        </div>
        <div className="space-y-2">
          {mockFriends.map((contact) => (
            <button key={contact.id} onClick={() => alert(`Chat with ${contact.name} coming soon`)} className="flex items-center space-x-3 p-2 hover:bg-gray-100 rounded-lg cursor-pointer w-full text-left">
              <div className="relative">
                <img src={contact.avatar} alt={contact.name} className="w-8 h-8 rounded-full object-cover" />
                <div className="absolute -bottom-1 -right-1 w-3 h-3 bg-green-500 rounded-full border-2 border-white"></div>
              </div>
              <span className="text-sm text-gray-900">{contact.name}</span>
            </button>
          ))}
        </div>
      </div>
    </div> 
  </aside>
));
export default RightSidebar;