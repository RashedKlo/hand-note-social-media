import { useState } from "react";
import LoadingCard from "../Loading/LoadingCard";
import NavigationTabs from "../Header/NavigationTabs"
import AllFriendsTab from "../Tabs/AllFriendsTab";
import SentRequestsTab from "../Tabs/SentRequestsTab";
import FriendRequestsTab from "../Tabs/FriendRequestsTab";

const MainContent = () => {
    // Determines the header text based on the active tab.

    const [isLoading, setIsLoading] = useState(false);
    const [activeTab, setActiveTab] = useState('friends');


    // Renders loading cards while the data is being fetched.
    if (isLoading) {
        return (
            <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
                <div className="grid gap-4 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4">
                    {[...Array(6)].map((_, index) => (
                        <LoadingCard key={index} />
                    ))}
                </div>
            </div>
        );
    }

    return (
        <div>
            <NavigationTabs setActiveTab={setActiveTab} activeTab={activeTab} />
            {activeTab == 'friends' && <AllFriendsTab />}
            {activeTab == 'sent' && <SentRequestsTab />}
            {activeTab == 'received' && <FriendRequestsTab />}


        </div>
    );
};
export default MainContent;