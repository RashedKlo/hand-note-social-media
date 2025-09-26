import { sidebarItems } from "../SidebarItems";

const MobileNavTabs = ({ activeSection, setActiveSection }) => {
    return <div className="lg:hidden mb-4 sm:mb-6">
        <div
            className="rounded-xl border p-1 sm:p-2 overflow-hidden"
            style={{
                backgroundColor: 'var(--color-bg-primary)',
                borderColor: 'var(--color-border-primary)',
            }}
        >
            <div className="flex overflow-x-auto no-scrollbar">
                {sidebarItems.map((item) => (
                    <button
                        key={item.id}
                        onClick={() => setActiveSection(item.id)}
                        className="flex-shrink-0 flex flex-col items-center gap-0.5 sm:gap-1 p-2 sm:p-3 rounded-lg transition-all duration-200 min-w-[80px] sm:min-w-max"
                        style={{
                            backgroundColor: activeSection === item.id ? 'var(--color-bg-active)' : 'transparent',
                            color: activeSection === item.id ? 'var(--color-primary)' : 'var(--color-text-secondary)',
                        }}
                    >
                        <item.icon className="w-4 h-4 sm:w-5 sm:h-5" />
                        <span className="text-xs sm:text-sm font-medium whitespace-nowrap">{item.title}</span>
                    </button>
                ))}
            </div>
        </div>
    </div>
}
export default MobileNavTabs;