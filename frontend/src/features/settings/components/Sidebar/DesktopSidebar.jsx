
import { ChevronRightIcon } from "@heroicons/react/24/outline";
import { sidebarItems } from "../SidebarItems";

const DesktopSidebar = ({ activeSection, setActiveSection }) => {

    return <div className="hidden lg:block w-64 xl:w-80 flex-shrink-0">
        <div className="sticky top-[80px] lg:top-24 max-h-[calc(100vh-100px)] overflow-y-auto">
            <div
                className="rounded-xl border p-2 xl:p-4"
                style={{
                    backgroundColor: 'var(--color-bg-primary)',
                    borderColor: 'var(--color-border-primary)',
                }}
            >
                <nav className="space-y-1 xl:space-y-2">
                    {sidebarItems.map((item) => (
                        <button
                            key={item.id}
                            onClick={() => setActiveSection(item.id)}
                            className="w-full text-left p-2 xl:p-3 rounded-lg transition-all duration-200 group"
                            style={{
                                backgroundColor: activeSection === item.id ? 'var(--color-bg-active)' : 'transparent',
                                borderColor: activeSection === item.id ? 'var(--color-border-focus)' : 'transparent',
                                borderWidth: activeSection === item.id ? '1px' : '0',
                            }}
                        >
                            <div className="flex items-center gap-2 xl:gap-3">
                                <div
                                    className="p-1.5 xl:p-2 rounded-lg transition-colors duration-200"
                                    style={{
                                        backgroundColor: activeSection === item.id ? 'var(--color-info-light)' : 'var(--color-bg-tertiary)',
                                    }}
                                >
                                    <item.icon
                                        className="w-4 h-4 xl:w-5 xl:h-5"
                                        style={{
                                            color: activeSection === item.id ? 'var(--color-primary)' : 'var(--color-text-secondary)',
                                        }}
                                    />
                                </div>
                                <div className="flex-1 min-w-0">
                                    <p
                                        className="font-medium text-sm xl:text-base truncate"
                                        style={{
                                            color: activeSection === item.id ? 'var(--color-primary)' : 'var(--color-text-primary)',
                                        }}
                                    >
                                        {item.title}
                                    </p>
                                    <p className="text-xs xl:text-sm truncate" style={{ color: 'var(--color-text-secondary)' }}>
                                        {item.description}
                                    </p>
                                </div>
                                <ChevronRightIcon
                                    className="w-4 h-4 xl:w-5 xl:h-5 flex-shrink-0"
                                    style={{
                                        color: activeSection === item.id ? 'var(--color-primary)' : 'var(--color-text-tertiary)',
                                    }}
                                />
                            </div>
                        </button>
                    ))}
                </nav>
            </div>
        </div>
    </div>
}
export default DesktopSidebar;
