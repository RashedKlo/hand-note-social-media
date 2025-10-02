import { BellIcon, PaperAirplaneIcon, UsersIcon } from "@heroicons/react/24/outline";

const TABS_CONFIG = [
    { id: 'friends', label: 'All Friends', icon: UsersIcon },
    { id: 'received', label: 'Requests', icon: BellIcon },
];
// Calculates total counts for the navigation tabs.
const totalCounts = {
    friends: 12,
    received: 32,
    total: 12 + 32 + 50,
};
// Adds dynamic counts to the tab configuration.
const tabs = TABS_CONFIG.map((tab) => ({
    ...tab,
    count: totalCounts[tab.id] || 0,
}));
{/* Navigation Tabs */ }
const NavigationTabs = ({ activeTab, setActiveTab }) => {
    return <div>
        <div className="-mb-px pb-0">
            <div className="scrollbar-hide flex gap-0 overflow-x-auto border-b border-gray-100">
                {tabs.map((tab) => {
                    const Icon = tab.icon;
                    const isActive = activeTab === tab.id;
                    return (
                        <button
                            key={tab.id}
                            onClick={() => setActiveTab(tab.id)}
                            className={`nav-item relative flex min-w-0 flex-shrink-0 items-center gap-2 border-b-2 px-3 py-3 text-sm font-medium transition-all duration-200 sm:px-6 sm:py-4 whitespace-nowrap ${isActive ? 'active border-blue-500' : 'border-transparent'
                                }`}
                        >
                            <Icon className="h-4 w-4 flex-shrink-0" />
                            <span className="truncate">{tab.label}</span>
                            {tab.count > 0 && (
                                <span className={`badge ml-1 ${isActive ? 'badge-primary' : 'bg-gray-100 text-gray-600'}`}>
                                    {tab.count}
                                </span>
                            )}
                        </button>
                    );
                })}
            </div>
        </div>
    </div>
}
export default NavigationTabs;