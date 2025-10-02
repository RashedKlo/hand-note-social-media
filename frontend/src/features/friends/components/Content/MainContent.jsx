import { useState } from "react";
import NavigationTabs from "../Header/NavigationTabs"
import AllFriendsTab from "../Tabs/AllFriendsTab";
import FriendRequestsTab from "../Tabs/FriendRequestsTab";

const MainContent = () => {
    const [activeTab, setActiveTab] = useState('friends');
    return (
        <div>
            <NavigationTabs setActiveTab={setActiveTab} activeTab={activeTab} />
            {activeTab == 'friends' && <AllFriendsTab />}
            {activeTab == 'received' && <FriendRequestsTab />}


        </div>
    );
};
export default MainContent;